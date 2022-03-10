//===============================================
//作    者：
//创建时间：2022-03-10 10:50:50
//备    注：
//===============================================
using UnityEngine;

public class MainPlayerCtrl : RoleCtrlBase
{
    void Start()
    {
        InitCharacterController();
        InitState();
    }

    private void InitState()
    {
        m_CurrState = new MainPlayerStateIdle(this);
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

        if (Input.GetKeyUp(KeyCode.R))
        {
            ChangeToRunState();
        }
        else if (Input.GetKeyUp(KeyCode.N))
        {
            ChangeToIdleState();
        }
    }

    public void ChangeToIdleState()
    {
        m_CurrState.OnLeave();
        m_CurrState = new MainPlayerStateIdle(this);
        m_CurrState.OnEnter();
    }

    public void ChangeToRunState()
    {
        m_CurrState.OnLeave();
        m_CurrState = new MainPlayerStateRun(this);
        m_CurrState.OnEnter();
    }
}
