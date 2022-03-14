//===============================================
//作    者：
//创建时间：2022-03-10 11:17:24
//备    注：
//===============================================

using UnityEngine;
/// <summary>
/// 主角控制器
/// </summary>
public partial class MainPlayerCtrl
{
    /// <summary>
    /// 主角休闲状态
    /// </summary>
    private class MainPlayerStateIdle : StateBase
    {
        private MainPlayerCtrl m_MainPlayerCtrl;

        public MainPlayerStateIdle(MainPlayerCtrl mainPlayerCtrl)
        {
            m_MainPlayerCtrl = mainPlayerCtrl;
        }

        public override void OnEnter()
        {
            m_MainPlayerCtrl.m_Animator.SetBool(AnimStateConditionName.ToIdleFight, true);

            FingerEvent.Instance.OnFingerUpWithoutDrag += m_MainPlayerCtrl.OnPlayerClick;
        }

        public override void OnLeave()
        {
            m_MainPlayerCtrl.m_Animator.SetBool(AnimStateConditionName.ToIdleFight, false);

            FingerEvent.Instance.OnFingerUpWithoutDrag -= m_MainPlayerCtrl.OnPlayerClick;
        }

        public override void OnUpdate()
        {
            if (m_MainPlayerCtrl.m_TargetMonster != null && !m_MainPlayerCtrl.m_TargetMonster.isDisState())
            {
                //有目标怪物且怪物未死亡
                float distance = Vector3.Distance(m_MainPlayerCtrl.transform.position, m_MainPlayerCtrl.m_TargetMonster.transform.position);
                if(distance <= m_MainPlayerCtrl.m_AttackDistance)
                {
                    //目标怪物在攻击范围内
                    if(Time.time >= m_MainPlayerCtrl.m_NextAttackTime)
                    {
                        //达到攻击时间，转为攻击状态
                        m_MainPlayerCtrl.ChangeToAttackState();
                    }
                    return;
                }
                else
                {
                    //不在攻击范围内，跑向目标怪物
                    m_MainPlayerCtrl.ChangeToRunState(m_MainPlayerCtrl.m_TargetMonster.transform.position);
                }
                return;
            }
        }
    }
}