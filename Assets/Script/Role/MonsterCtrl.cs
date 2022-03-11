//===============================================
//作    者：
//创建时间：2022-03-10 11:11:32
//备    注：
//===============================================
using UnityEngine;

public class MonsterCtrl : MonoBehaviour
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

    [SerializeField]
    private float m_PatrolRadius = 2;

    [SerializeField]
    private float m_ViewRadius = 10;

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

    [HideInInspector]
    public Vector3 BornPos { get; private set; }

    [HideInInspector]
    public float PatrolRadius { get { return m_PatrolRadius; } }

    [HideInInspector]
    public float NextPatrolTime = 0;

    [HideInInspector]
    public float ViewRadius { get { return m_ViewRadius; } }

    [HideInInspector]
    public MainPlayerCtrl mainPlayerCtrl;

    [HideInInspector]
    private StateBase m_CurrState;

    void Start()
    {
        BornPos = transform.position;
        InitState();
    }

    private void InitState()
    {
        m_CurrState = new MonsterStateIdle(this);
        m_CurrState.OnEnter();
    }

    void Update()
    {
        m_CurrState.OnUpdate();
    }

    public void ChangeToIdleState(float nextPatrolTime)
    {
        m_CurrState.OnLeave();
        NextPatrolTime = nextPatrolTime;
        m_CurrState = new MonsterStateIdle(this);
        m_CurrState.OnEnter();
    }

    public void ChangeToRunState(Vector3 targetPos)
    {
        m_CurrState.OnLeave();
        MoveTargetPos = targetPos;
        m_CurrState = new MonsterStateRun(this);
        m_CurrState.OnEnter();
    }
}
