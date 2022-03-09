//===============================================
//作    者：
//创建时间：2022-03-08 13:57:33
//备    注：
//===============================================
using UnityEngine;

/// <summary>
/// 跑状态
/// </summary>
public class RoleStateRun : RoleStateAbstract
{
    /// <summary>
    /// 移动方向
    /// </summary>
    private Vector3 m_MoveDirection;

    /// <summary>
    /// 转身插值比例
    /// </summary>
    private float m_RotationRatio;

    /// <summary>
    /// 转身的初始方向
    /// </summary>
    private Quaternion m_BeginQuaternion;

    /// <summary>
    /// 转身的目标方向
    /// </summary>
    private Quaternion m_TargetQuaternion;

    public RoleStateRun(RoleFSM roleFSM) : base(roleFSM) { }

    public override void OnEnter()
    {
        m_RoleFSM.RoleCtrl.Animator.SetBool(AnimStateConditionName.ToRun, true);

        calcuMoveDir();

        FingerEvent.Instance.OnFingerUpWithoutDrag += OnPlayerClickGround;

        Debug.Log("enter run state");
    }

    public override void OnLeave()
    {
        m_RoleFSM.RoleCtrl.Animator.SetBool(AnimStateConditionName.ToRun, false);

        FingerEvent.Instance.OnFingerUpWithoutDrag -= OnPlayerClickGround;

        Debug.Log("leave run state");
    }

    /// <summary>
    /// 计算移动方向
    /// </summary>
    private void calcuMoveDir() 
    {
        //计算移动方向
        m_MoveDirection = m_RoleFSM.RoleCtrl.TargetPos - m_RoleFSM.RoleCtrl.transform.position;
        m_MoveDirection = m_MoveDirection.normalized;//归一化
        m_MoveDirection.y = 0;

        m_BeginQuaternion = m_RoleFSM.RoleCtrl.transform.rotation;

        //计算目标四元数
        //欧拉角旋转产生的万向锁现象：https://www.cnblogs.com/trio/p/13696560.html
        //比如按xyz顺序旋转，如果绕y轴旋转90度，导致z轴转到x轴位置，那么接下来绕z轴旋转实际上和第一次绕x轴旋转，都是同一个轴向
        m_TargetQuaternion = Quaternion.LookRotation(m_MoveDirection);

        m_RotationRatio = 0;
    }

    private void OnPlayerClickGround(Vector2 screenPos)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPos);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            //点击地面
            if (hitInfo.collider.gameObject.name.Equals("Ground", System.StringComparison.CurrentCultureIgnoreCase))
            {
                if (Vector3.Distance(hitInfo.point, m_RoleFSM.RoleCtrl.transform.position) > 0.1f)
                {
                    m_RoleFSM.RoleCtrl.TargetPos = hitInfo.point;

                    calcuMoveDir();
                }
            }
        }
    }

    public override void OnUpdate()
    {
        if (Vector3.Distance(m_RoleFSM.RoleCtrl.TargetPos, m_RoleFSM.RoleCtrl.transform.position) > 0.1f)
        {
            //让角色转身
            if (m_RotationRatio < 1f)
            {
                //更新插值比例
                m_RotationRatio += m_RoleFSM.RoleCtrl.RotationSpeed * Time.deltaTime;
                m_RotationRatio = Mathf.Min(m_RotationRatio, 1f);
                //对旋转插值
                m_RoleFSM.RoleCtrl.transform.rotation = Quaternion.Lerp(m_BeginQuaternion, m_TargetQuaternion, m_RotationRatio);
            }

            Vector3 offset = m_MoveDirection * Time.deltaTime * m_RoleFSM.RoleCtrl.Speed;
            m_RoleFSM.RoleCtrl.CharacterController.Move(offset);
            if (Vector3.Distance(m_RoleFSM.RoleCtrl.TargetPos, m_RoleFSM.RoleCtrl.transform.position) <= 0.1f)
            {
                m_RoleFSM.ChangeToIdleState();
            }
        }
    }
}
