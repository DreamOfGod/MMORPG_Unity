//===============================================
//作    者：
//创建时间：2022-02-18 14:36:26
//备    注：
//===============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 角色控制器
/// </summary>
public class RoleCtrl : MonoBehaviour
{
    #region 属性
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

    /// <summary>
    /// 动画
    /// </summary>
    [SerializeField]
    public Animator Animator;

    /// <summary>
    /// 角色信息
    /// </summary>
    private RoleInfoBase RoleInfo;

    /// <summary>
    /// 角色AI
    /// </summary>
    public IRoleAI RoleAI;

    /// <summary>
    /// 角色有限状态机
    /// </summary>
    public RoleFSM RoleFSM;
    #endregion

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="roleInfo">角色信息</param>
    /// <param name="roleAI">角色AI</param>
    /// <param name="roleFSM">角色有限状态机</param>
    public void Init(RoleInfoBase roleInfo, IRoleAI roleAI)
    {
        RoleInfo = roleInfo;
        RoleAI = roleAI;
    }

    void Start()
    {
        m_CharacterController = GetComponent<CharacterController>();

        RoleFSM = new RoleFSM(this);
    }

    #region OnEnable
    void OnEnable()
    {
        FingerEvent.Instance.OnFingerUpWithoutDrag += OnPlayerClickGround;
        FingerEvent.Instance.OnFingerDrag += OnFingerDrag;
        FingerEvent.Instance.OnZoom += OnZoom;
    }
    #endregion

    #region OnDisable
    void OnDisable()
    {
        FingerEvent.Instance.OnFingerUpWithoutDrag -= OnPlayerClickGround;
        FingerEvent.Instance.OnFingerDrag -= OnFingerDrag;
        FingerEvent.Instance.OnZoom -= OnZoom;
    }
    #endregion

    #region OnPlayerClickGround 玩家点击地面
    /// <summary>
    /// 玩家点击地面
    /// </summary>
    /// <param name="screenPos"></param>
    void OnPlayerClickGround(Vector2 screenPos)
    {
        if(RoleFSM.isAttackState() || RoleFSM.isDieState())
        {
            return;
        }
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

                RoleFSM.ChangeToRunState();
            }
        }
    }
    #endregion

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

    #region OnZoom 摄像机缩放
    /// <summary>
    /// 摄像机缩放
    /// </summary>
    /// <param name="type"></param>
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
    #endregion

    void Update()
    {
        //if (RoleAI == null)
        //{
        //    return;
        //}

        //RoleAI.DoAI();

        if(RoleFSM != null)
        {
            RoleFSM.OnUpdate();
        }

        if (m_CharacterController == null) 
        { 
            return;
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            RoleFSM.ChangeToRunState();
        } 
        else if (Input.GetKeyUp(KeyCode.N)) 
        {
            RoleFSM.ChangeToIdleState();
        }
        else if (Input.GetKeyUp(KeyCode.F))
        {
            RoleFSM.ChangeToIdleState();
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            RoleFSM.ChangeToDieState();
        }
        else if (Input.GetKeyUp(KeyCode.H))
        {
            RoleFSM.ChangeToHurtState();
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            RoleFSM.ChangeToAttackState();
        }
        else if (Input.GetKeyUp(KeyCode.B))
        {
            RoleFSM.ChangeToAttackState();
        }
        else if (Input.GetKeyUp(KeyCode.C))
        {
            RoleFSM.ChangeToAttackState();
        }

        #region 移动
        //让角色贴着地面
        if (!m_CharacterController.isGrounded)
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
                if(Vector3.Distance(m_TargetPos, transform.position) <= 0.1f)
                {
                    RoleFSM.ChangeToIdleState();
                }
            }
        }
        #endregion

        CameraAutoFollow();
    }

    #region CameraAutoFollow 摄像机自动跟随
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
    #endregion
}