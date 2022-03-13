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

            FingerEvent.Instance.OnFingerUpWithoutDrag += m_MainPlayerCtrl.OnPlayerClickGround;
        }

        public override void OnLeave()
        {
            m_MainPlayerCtrl.m_Animator.SetBool(AnimStateConditionName.ToIdleFight, false);

            FingerEvent.Instance.OnFingerUpWithoutDrag -= m_MainPlayerCtrl.OnPlayerClickGround;
        }
    }
}