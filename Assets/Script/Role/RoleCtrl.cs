//===============================================
//作    者：
//创建时间：2022-02-18 14:36:26
//备    注：
//===============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleCtrl : MonoBehaviour
{
    /// <summary>
    /// 移动的目标点
    /// </summary>
    private Vector3 m_TargetPos = Vector3.zero;

    /// <summary>
    /// 移动方向
    /// </summary>
    private Vector3 m_MoveDirection = Vector3.zero;

    /// <summary>
    /// 移动速度
    /// </summary>
    [SerializeField]
    private float m_Speed = 10f;

    /// <summary>
    /// 控制器
    /// </summary>
    private CharacterController m_CharacterController;

    /// <summary>
    /// 转身插值比例
    /// </summary>
    private float m_RotationRatio = 0f;

    /// <summary>
    /// 转身速度
    /// </summary>
    [SerializeField]
    private float m_RotationSpeed = 5f;

    /// <summary>
    /// 转身的初始方向
    /// </summary>
    private Quaternion m_BeginQuaternion;

    /// <summary>
    /// 转身的目标方向
    /// </summary>
    private Quaternion m_TargetQuaternion;

    void Start()
    {
        m_CharacterController = GetComponent<CharacterController>();
    }

    void OnEnable()
    {
        FingerEvent.Instance.OnFingerUpWithoutDrag += OnFingerUpWithoutDrag;
        FingerEvent.Instance.OnFingerDrag += OnFingerDrag;
        FingerEvent.Instance.OnZoom += OnZoom;
    }

    void OnDisable()
    {
        FingerEvent.Instance.OnFingerUpWithoutDrag -= OnFingerUpWithoutDrag;
        FingerEvent.Instance.OnFingerDrag -= OnFingerDrag;
        FingerEvent.Instance.OnZoom -= OnZoom;
    }

    void OnFingerUpWithoutDrag(Vector2 screenPos)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPos);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            //点击地面
            if (hitInfo.collider.gameObject.name.Equals("Ground", System.StringComparison.CurrentCultureIgnoreCase))
            {
                m_TargetPos = hitInfo.point;

                //计算移动方向
                m_MoveDirection = m_TargetPos - transform.position;
                m_MoveDirection = m_MoveDirection.normalized;//归一化
                m_MoveDirection.y = 0;

                m_BeginQuaternion = transform.rotation;

                //计算目标四元数
                //欧拉角旋转产生的万向锁现象：https://www.cnblogs.com/trio/p/13696560.html
                //比如按xyz顺序旋转，如果绕y轴旋转90度，导致z轴转到x轴位置，那么接下来绕z轴旋转实际上和第一次绕x轴旋转，都是同一个轴向
                m_TargetQuaternion = Quaternion.LookRotation(m_MoveDirection);

                m_RotationRatio = 0;
            }
        }
    }

    void OnFingerDrag(FingerEvent.FingerDir dir)
    {
        switch(dir)
        {
            case FingerEvent.FingerDir.Up:
                CameraCtrl.Instance.setCameraUpAndDown(1);
                break;
            case FingerEvent.FingerDir.Down:
                CameraCtrl.Instance.setCameraUpAndDown(0);
                break;
            case FingerEvent.FingerDir.Left:
                CameraCtrl.Instance.SetCameraRotate(0);
                break;
            case FingerEvent.FingerDir.Right:
                CameraCtrl.Instance.SetCameraRotate(1);
                break;
        }
    }

    void OnZoom(FingerEvent.ZoomType type)
    {
        switch(type)
        {
            case FingerEvent.ZoomType.In:
                CameraCtrl.Instance.setCameraZoom(0);
                break;
            case FingerEvent.ZoomType.Out:
                CameraCtrl.Instance.setCameraZoom(1);
                break;
        }
    }

    void Update()
    {
        if (!m_CharacterController) return;

        #region 移动
        //让角色贴着地面
        if(!m_CharacterController.isGrounded)
        {
            m_CharacterController.Move(new Vector3(0, -1000, 0));
        }

        //如果目标点不是原点，进行移动
        if (m_TargetPos != Vector3.zero)
        {
            if (Vector3.Distance(m_TargetPos, transform.position) > 0.1f)
            {
                //让角色转身
                if(m_RotationRatio < 1f)
                {
                    //更新插值比例
                    m_RotationRatio += m_RotationSpeed * Time.deltaTime;
                    m_RotationRatio = Mathf.Min(m_RotationRatio, 1f);
                    //对旋转插值
                    transform.rotation = Quaternion.Lerp(m_BeginQuaternion, m_TargetQuaternion, m_RotationRatio);
                }

                Vector3 offset = m_MoveDirection * Time.deltaTime * m_Speed;
                m_CharacterController.Move(offset);
            }
        }
        #endregion

        CameraAutoFollow();
    }

    /// <summary>
    /// 摄像机自动跟随
    /// </summary>
    private void CameraAutoFollow()
    {
        if(CameraCtrl.Instance)
        {
            CameraCtrl.Instance.transform.position = transform.position;
            CameraCtrl.Instance.AutoLookAt(transform.position);
        }
    }
}