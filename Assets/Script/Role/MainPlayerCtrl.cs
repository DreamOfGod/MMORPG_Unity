//===============================================
//作    者：
//创建时间：2022-03-10 10:50:50
//备    注：
//===============================================
using UnityEngine;

public class MainPlayerCtrl : MonoBehaviour
{
    [SerializeField]
    private Animator m_Animator;

    [SerializeField]
    private float m_MoveSpeed = 10f;

    [SerializeField]
    private float m_RotationSpeed = 5f;

    [SerializeField]
    private Transform m_HeadBarPos;

    [SerializeField]
    private CharacterController m_CharacterController;

    [HideInInspector]
    public Transform HeadBarPos { get { return m_HeadBarPos; } }

    [HideInInspector]
    public float RotationSpeed { get { return m_RotationSpeed; } }

    [HideInInspector]
    public float MoveSpeed { get { return m_MoveSpeed; } }

    [HideInInspector]
    public Animator Animator { get { return m_Animator; } }

    [HideInInspector]
    public CharacterController CharacterController { get { return m_CharacterController; } }

    [HideInInspector]
    public Vector3 MoveTargetPos;

    [HideInInspector]
    public Vector3 MoveDirection;

    [HideInInspector]
    public float RotationRatio;

    [HideInInspector]
    public Quaternion BeginQuaternion;

    [HideInInspector]
    public Quaternion TargetQuaternion;

    private StateBase m_CurrState;

    void Start()
    {
        InitState();
    }

    private void InitState()
    {
        m_CurrState = new MainPlayerStateIdle(this);
        m_CurrState.OnEnter();
    }
    
    void Update()
    {
        m_CurrState.OnUpdate();

        //if (Input.GetKeyUp(KeyCode.R))
        //{
        //    ChangeToRunState();
        //}
        //else if (Input.GetKeyUp(KeyCode.N))
        //{
        //    ChangeToIdleState();
        //}
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
