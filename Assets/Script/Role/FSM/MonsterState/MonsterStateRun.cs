//===============================================
//作    者：
//创建时间：2022-03-10 14:57:02
//备    注：
//===============================================
using UnityEngine;

public class MonsterStateRun : StateBase
{
    private MonsterCtrl m_MonsterCtrl;

    public MonsterStateRun(MonsterCtrl monsterCtrl)
    {
        m_MonsterCtrl = monsterCtrl;
    }

    public override void OnEnter()
    {
        m_MonsterCtrl.Animator.SetBool(AnimStateConditionName.ToRun, true);

        calcuMoveDir();

        FingerEvent.Instance.OnFingerUpWithoutDrag += OnPlayerClickGround;
    }

    public override void OnLeave()
    {
        m_MonsterCtrl.Animator.SetBool(AnimStateConditionName.ToRun, false);

        FingerEvent.Instance.OnFingerUpWithoutDrag -= OnPlayerClickGround;
    }

    /// <summary>
    /// 计算移动方向
    /// </summary>
    private void calcuMoveDir()
    {
        //计算移动方向
        m_MonsterCtrl.MoveDirection = m_MonsterCtrl.MoveTargetPos - m_MonsterCtrl.transform.position;
        m_MonsterCtrl.MoveDirection = m_MonsterCtrl.MoveDirection.normalized;//归一化
        m_MonsterCtrl.MoveDirection.y = 0;

        m_MonsterCtrl.BeginQuaternion = m_MonsterCtrl.transform.rotation;

        //计算目标四元数
        //欧拉角旋转产生的万向锁现象：https://www.cnblogs.com/trio/p/13696560.html
        //比如按xyz顺序旋转，如果绕y轴旋转90度，导致z轴转到x轴位置，那么接下来绕z轴旋转实际上和第一次绕x轴旋转，都是同一个轴向
        m_MonsterCtrl.TargetQuaternion = Quaternion.LookRotation(m_MonsterCtrl.MoveDirection);

        m_MonsterCtrl.RotationRatio = 0;
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
                if (Vector3.Distance(hitInfo.point, m_MonsterCtrl.transform.position) > 0.1f)
                {
                    m_MonsterCtrl.MoveTargetPos = hitInfo.point;

                    calcuMoveDir();
                }
            }
        }
    }

    public override void OnUpdate()
    {
        if (Vector3.Distance(m_MonsterCtrl.mainPlayerCtrl.transform.position, m_MonsterCtrl.transform.position) <= m_MonsterCtrl.ViewRadius)
        {
            m_MonsterCtrl.MoveTargetPos = m_MonsterCtrl.mainPlayerCtrl.transform.position;
            calcuMoveDir();
        }

        if (Vector3.Distance(m_MonsterCtrl.MoveTargetPos, m_MonsterCtrl.transform.position) > 0.1f)
        {
            //让角色转身
            if (m_MonsterCtrl.RotationRatio < 1f)
            {
                //更新插值比例
                m_MonsterCtrl.RotationRatio += m_MonsterCtrl.RotationSpeed * Time.deltaTime;
                m_MonsterCtrl.RotationRatio = Mathf.Min(m_MonsterCtrl.RotationRatio, 1f);
                //对旋转插值
                m_MonsterCtrl.transform.rotation = Quaternion.Lerp(m_MonsterCtrl.BeginQuaternion, m_MonsterCtrl.TargetQuaternion, m_MonsterCtrl.RotationRatio);
            }

            Vector3 offset = m_MonsterCtrl.MoveDirection * Time.deltaTime * m_MonsterCtrl.MoveSpeed;
            m_MonsterCtrl.CharacterController.Move(offset);
            if (Vector3.Distance(m_MonsterCtrl.MoveTargetPos, m_MonsterCtrl.transform.position) <= 0.1f)
            {
                m_MonsterCtrl.ChangeToIdleState(Time.time + Random.Range(3, 6));
            }
        }
    }
}