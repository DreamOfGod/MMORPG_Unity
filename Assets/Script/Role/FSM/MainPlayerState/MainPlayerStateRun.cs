//===============================================
//作    者：
//创建时间：2022-03-10 11:17:24
//备    注：
//===============================================
using UnityEngine;

public class MainPlayerStateRun: StateBase
{
    private MainPlayerCtrl m_MainPlayerCtrl;

    public MainPlayerStateRun(MainPlayerCtrl mainPlayerCtrl) 
    {
        m_MainPlayerCtrl = mainPlayerCtrl;
    }

    public override void OnEnter()
    {
        m_MainPlayerCtrl.Animator.SetBool(AnimStateConditionName.ToRun, true);

        calcuMoveDir();

        FingerEvent.Instance.OnFingerUpWithoutDrag += OnPlayerClickGround;
    }

    public override void OnLeave()
    {
        m_MainPlayerCtrl.Animator.SetBool(AnimStateConditionName.ToRun, false);

        FingerEvent.Instance.OnFingerUpWithoutDrag -= OnPlayerClickGround;
    }

    /// <summary>
    /// 计算移动方向
    /// </summary>
    private void calcuMoveDir()
    {
        //计算移动方向
        m_MainPlayerCtrl.MoveDirection = m_MainPlayerCtrl.MoveTargetPos - m_MainPlayerCtrl.transform.position;
        m_MainPlayerCtrl.MoveDirection = m_MainPlayerCtrl.MoveDirection.normalized;//归一化
        m_MainPlayerCtrl.MoveDirection.y = 0;

        m_MainPlayerCtrl.BeginQuaternion = m_MainPlayerCtrl.transform.rotation;

        //计算目标四元数
        //欧拉角旋转产生的万向锁现象：https://www.cnblogs.com/trio/p/13696560.html
        //比如按xyz顺序旋转，如果绕y轴旋转90度，导致z轴转到x轴位置，那么接下来绕z轴旋转实际上和第一次绕x轴旋转，都是同一个轴向
        m_MainPlayerCtrl.TargetQuaternion = Quaternion.LookRotation(m_MainPlayerCtrl.MoveDirection);

        m_MainPlayerCtrl.RotationRatio = 0;
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
                if (Vector3.Distance(hitInfo.point, m_MainPlayerCtrl.transform.position) > 0.1f)
                {
                    m_MainPlayerCtrl.MoveTargetPos = hitInfo.point;

                    calcuMoveDir();
                }
            }
        }
    }

    public override void OnUpdate()
    {
        if (Vector3.Distance(m_MainPlayerCtrl.MoveTargetPos, m_MainPlayerCtrl.transform.position) > 0.1f)
        {
            //让角色转身
            if (m_MainPlayerCtrl.RotationRatio < 1f)
            {
                //更新插值比例
                m_MainPlayerCtrl.RotationRatio += m_MainPlayerCtrl.RotationSpeed * Time.deltaTime;
                m_MainPlayerCtrl.RotationRatio = Mathf.Min(m_MainPlayerCtrl.RotationRatio, 1f);
                //对旋转插值
                m_MainPlayerCtrl.transform.rotation = Quaternion.Lerp(m_MainPlayerCtrl.BeginQuaternion, m_MainPlayerCtrl.TargetQuaternion, m_MainPlayerCtrl.RotationRatio);
            }

            Vector3 offset = m_MainPlayerCtrl.MoveDirection * Time.deltaTime * m_MainPlayerCtrl.MoveSpeed;
            m_MainPlayerCtrl.CharacterController.Move(offset);
            if (Vector3.Distance(m_MainPlayerCtrl.MoveTargetPos, m_MainPlayerCtrl.transform.position) <= 0.1f)
            {
                m_MainPlayerCtrl.ChangeToIdleState();
            }
        }
    }
}