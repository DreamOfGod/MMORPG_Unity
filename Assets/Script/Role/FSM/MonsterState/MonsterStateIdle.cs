//===============================================
//作    者：
//创建时间：2022-03-10 14:56:48
//备    注：
//===============================================
using UnityEngine;

public class MonsterStateIdle : StateBase
{
    private MonsterCtrl m_MonsterCtrl;

    public MonsterStateIdle(MonsterCtrl monsterCtrl)
    {
        m_MonsterCtrl = monsterCtrl;
    }

    public override void OnEnter()
    {
        m_MonsterCtrl.Animator.SetBool(AnimStateConditionName.ToIdleFight, true);
    }

    public override void OnUpdate()
    {
        if(Vector3.Distance(m_MonsterCtrl.mainPlayerCtrl.transform.position, m_MonsterCtrl.transform.position) <= m_MonsterCtrl.ViewRadius)
        {
            Debug.Log("distance:" + Vector3.Distance(m_MonsterCtrl.mainPlayerCtrl.transform.position, m_MonsterCtrl.transform.position) + ", view radius:" + m_MonsterCtrl.ViewRadius);
            m_MonsterCtrl.ChangeToRunState(m_MonsterCtrl.mainPlayerCtrl.transform.position);
            return;
        }

        if(Time.time > m_MonsterCtrl.NextPatrolTime)
        {
            float x = Random.Range(m_MonsterCtrl.BornPos.x - m_MonsterCtrl.PatrolRadius, m_MonsterCtrl.BornPos.x + m_MonsterCtrl.PatrolRadius);
            float a = m_MonsterCtrl.PatrolRadius * m_MonsterCtrl.PatrolRadius;
            float b = x - m_MonsterCtrl.BornPos.x;
            float c = b * b;
            float d = Mathf.Sqrt(a - c);
            float z1 = m_MonsterCtrl.BornPos.z - d;
            float z2 = m_MonsterCtrl.BornPos.z + d;
            Vector3 targetPos = new Vector3(x, m_MonsterCtrl.BornPos.y, Random.Range(z1, z2));
            m_MonsterCtrl.ChangeToRunState(targetPos);
            return;
        }
    }

    public override void OnLeave()
    {
        m_MonsterCtrl.Animator.SetBool(AnimStateConditionName.ToIdleFight, false);
    }
}
