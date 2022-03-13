//===============================================
//作    者：
//创建时间：2022-03-13 01:19:03
//备    注：
//===============================================

using UnityEngine;

/// <summary>
/// 主角控制器
/// </summary>
public partial class MainPlayerCtrl
{
    /// <summary>
    /// 主角受伤状态
    /// </summary>
    private class MainPlayerStateHurt : StateBase
    {
        private MainPlayerCtrl m_MainPlayerCtrl;

        public MainPlayerStateHurt(MainPlayerCtrl mainPlayerCtrl)
        {
            m_MainPlayerCtrl = mainPlayerCtrl;
        }

        public override void OnEnter()
        {
            m_MainPlayerCtrl.m_Animator.SetBool(AnimStateConditionName.ToHurt, true);

            FingerEvent.Instance.OnFingerUpWithoutDrag += m_MainPlayerCtrl.OnPlayerClickGround;
        }

        public override void OnLeave()
        {
            m_MainPlayerCtrl.m_Animator.SetBool(AnimStateConditionName.ToHurt, false);

            FingerEvent.Instance.OnFingerUpWithoutDrag -= m_MainPlayerCtrl.OnPlayerClickGround;
        }

        public override void OnUpdate()
        {
            if(m_MainPlayerCtrl.m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            {
                if(m_MainPlayerCtrl.HP > 0)
                {
                    m_MainPlayerCtrl.ChangeToIdleState();
                }
                else
                {
                    m_MainPlayerCtrl.ChangeToDieState();
                }
            }
        }
    }
}
