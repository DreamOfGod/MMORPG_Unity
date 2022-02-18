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

    // Start is called before the first frame update
    void Start()
    {
        m_CharacterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_CharacterController == null) return;

        //点击屏幕
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
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
                    m_TargetQuaternion = Quaternion.LookRotation(m_MoveDirection);

                    m_RotationRatio = 0;
                }
            }
        }

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
    }
}
