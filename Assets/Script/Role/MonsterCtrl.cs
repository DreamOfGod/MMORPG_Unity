//===============================================
//作    者：
//创建时间：2022-03-10 11:11:32
//备    注：
//===============================================
using UnityEngine;

public class MonsterCtrl : RoleCtrlBase
{
    void Start()
    {
        InitCharacterController();
        InitState();
    }

    private void InitState()
    {
        m_CurrState = new MonsterStateIdle(this);
        m_CurrState.OnEnter();
    }

    void Update()
    {
        if (m_RoleAI == null)
        {
            return;
        }

        m_RoleAI.DoAI();
        m_CurrState.OnUpdate();
    }

    public void ChangeToIdleState()
    {
        m_CurrState.OnLeave();
        m_CurrState = new MonsterStateIdle(this);
        m_CurrState.OnEnter();
    }

    public void ChangeToRunState()
    {
        m_CurrState.OnLeave();
        m_CurrState = new MonsterStateRun(this);
        m_CurrState.OnEnter();
    }
}
