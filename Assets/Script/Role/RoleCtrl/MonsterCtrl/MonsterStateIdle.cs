//===============================================
//作    者：
//创建时间：2022-03-10 14:56:48
//备    注：
//===============================================
using UnityEngine;

/// <summary>
/// 怪物控制器
/// </summary>
public partial class MonsterCtrl
{
    /// <summary>
    /// 怪物休闲状态
    /// </summary>
    private class MonsterStateIdle : StateBase
    {
        private MonsterCtrl m_MonsterCtrl;

        public MonsterStateIdle(MonsterCtrl monsterCtrl)
        {
            m_MonsterCtrl = monsterCtrl;
        }

        #region OnEnter OnLeave
        public override void OnEnter()
        {
            m_MonsterCtrl.m_Animator.SetBool(AnimStateConditionName.ToIdleFight, true);

            m_MonsterCtrl.m_NextPatrolTime = Time.time + Random.Range(3, 6);
        }

        public override void OnLeave()
        {
            m_MonsterCtrl.m_Animator.SetBool(AnimStateConditionName.ToIdleFight, false);
        }
        #endregion

        #region OnUpdate
        public override void OnUpdate()
        {
            if(m_MonsterCtrl.m_MainPlayerCtrl.IsDieState())
            {
                //主角死亡
                if(m_MonsterCtrl.m_LockedEnemy)
                {
                    //已锁定主角，返回出生点
                    m_MonsterCtrl.m_LockedEnemy = false;
                    m_MonsterCtrl.ChangeToRunState(m_MonsterCtrl.m_BornPos);
                }
                else
                {
                    //没锁定主角，继续巡逻
                    Patrol();
                }
                return;
            }

            float distance = Vector3.Distance(m_MonsterCtrl.m_MainPlayerCtrl.transform.position, m_MonsterCtrl.transform.position);
            if (distance <= m_MonsterCtrl.m_AttackDistance)
            {
                //主角在攻击距离内
                m_MonsterCtrl.m_LockedEnemy = true;
                if(Time.time >= m_MonsterCtrl.m_NextAttackTime)
                {
                    //达到攻击时间，攻击主角
                    m_MonsterCtrl.ChangeToAttackState();
                }
                return;
            }
            
            if(distance <= m_MonsterCtrl.m_ViewRadius)
            {
                //主角在视野范围，跑向主角
                m_MonsterCtrl.m_LockedEnemy = true;
                m_MonsterCtrl.ChangeToRunState(m_MonsterCtrl.m_MainPlayerCtrl.transform.position);
                return;
            }

            if(m_MonsterCtrl.m_LockedEnemy)
            {
                //主角不在视野范围内，且之前已锁定主角，则返回出生点
                m_MonsterCtrl.m_LockedEnemy = false;
                m_MonsterCtrl.ChangeToRunState(m_MonsterCtrl.m_BornPos);
                return;
            }

            Patrol();
        }
        #endregion

        #region Patrol 巡逻
        /// <summary>
        /// 巡逻
        /// </summary>
        private void Patrol()
        {
            if (Time.time >= m_MonsterCtrl.m_NextPatrolTime)
            {
                //巡逻点在以出生点为中心点的边长为2*m_PatrolRange的矩形内，且在以当前位置为中心点的边长为2*m_CannotPatrolRange的矩形外
                float x;
                if (m_MonsterCtrl.transform.position.x + m_MonsterCtrl.m_CannotPatrolRange >= m_MonsterCtrl.m_BornPos.x + m_MonsterCtrl.m_PatrolRange)
                {
                    //x只能向负方向取随机值
                    x = Random.Range(m_MonsterCtrl.m_BornPos.x - m_MonsterCtrl.m_PatrolRange, m_MonsterCtrl.transform.position.x - m_MonsterCtrl.m_CannotPatrolRange);
                }
                else if (m_MonsterCtrl.transform.position.x - m_MonsterCtrl.m_CannotPatrolRange <= m_MonsterCtrl.m_BornPos.x - m_MonsterCtrl.m_PatrolRange)
                {
                    //x只能向正方向取随机值
                    x = Random.Range(m_MonsterCtrl.transform.position.x + m_MonsterCtrl.m_CannotPatrolRange, m_MonsterCtrl.m_BornPos.x + m_MonsterCtrl.m_PatrolRange);
                }
                else if (Random.Range(-1, 1) >= 0)
                {
                    //x向正方向取值
                    x = Random.Range(m_MonsterCtrl.transform.position.x + m_MonsterCtrl.m_CannotPatrolRange, m_MonsterCtrl.m_BornPos.x + m_MonsterCtrl.m_PatrolRange);
                }
                else
                {
                    //x向负方向取值
                    x = Random.Range(m_MonsterCtrl.m_BornPos.x - m_MonsterCtrl.m_PatrolRange, m_MonsterCtrl.transform.position.x - m_MonsterCtrl.m_CannotPatrolRange);
                }

                float z;
                if (m_MonsterCtrl.transform.position.z + m_MonsterCtrl.m_CannotPatrolRange >= m_MonsterCtrl.m_BornPos.z + m_MonsterCtrl.m_PatrolRange)
                {
                    //z只能向负方向取随机值
                    z = Random.Range(m_MonsterCtrl.m_BornPos.z - m_MonsterCtrl.m_PatrolRange, m_MonsterCtrl.transform.position.z - m_MonsterCtrl.m_CannotPatrolRange);
                }
                else if (m_MonsterCtrl.transform.position.z - m_MonsterCtrl.m_CannotPatrolRange <= m_MonsterCtrl.m_BornPos.z - m_MonsterCtrl.m_PatrolRange)
                {
                    //z只能向正方向取随机值
                    z = Random.Range(m_MonsterCtrl.transform.position.z + m_MonsterCtrl.m_CannotPatrolRange, m_MonsterCtrl.m_BornPos.z + m_MonsterCtrl.m_PatrolRange);
                }
                else if (Random.Range(-1, 1) >= 0)
                {
                    //z向正方向取值
                    z = Random.Range(m_MonsterCtrl.transform.position.z + m_MonsterCtrl.m_CannotPatrolRange, m_MonsterCtrl.m_BornPos.z + m_MonsterCtrl.m_PatrolRange);
                }
                else
                {
                    //z向负方向取值
                    z = Random.Range(m_MonsterCtrl.m_BornPos.z - m_MonsterCtrl.m_PatrolRange, m_MonsterCtrl.transform.position.z - m_MonsterCtrl.m_CannotPatrolRange);
                }

                Vector3 targetPos = new Vector3(x, m_MonsterCtrl.transform.position.y, z);
                m_MonsterCtrl.ChangeToRunState(targetPos);
            }
        }
        #endregion
    }
}