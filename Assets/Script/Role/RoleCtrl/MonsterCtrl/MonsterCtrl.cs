//===============================================
//作    者：
//创建时间：2022-03-10 11:11:32
//备    注：
//===============================================
using UnityEngine;

/// <summary>
/// 怪物控制器
/// </summary>
public partial class MonsterCtrl : MonoBehaviour
{
    #region unity属性
    /// <summary>
    /// 动画
    /// </summary>
    [SerializeField]
    private Animator m_Animator;

    /// <summary>
    /// 移动速度
    /// </summary>
    [SerializeField]
    private float m_MoveSpeed = 10f;

    /// <summary>
    /// 转身速度
    /// </summary>
    [SerializeField]
    private float m_RotationSpeed = 5f;

    /// <summary>
    /// 头顶UI条位置
    /// </summary>
    [SerializeField]
    private Transform m_HeadBarPos;

    /// <summary>
    /// 控制器
    /// </summary>
    [SerializeField]
    private CharacterController m_CharacterController;

    /// <summary>
    /// 以怪物出生点为中心的矩形巡逻范围的边长的一半
    /// </summary>
    [SerializeField]
    private float m_PatrolRange = 5;

    /// <summary>
    /// 以怪物所在位置为中心的矩形非巡逻范围的边长的一半，必须小于m_PatrolRange
    /// </summary>
    [SerializeField]
    private float m_CannotPatrolRange = 1;

    /// <summary>
    /// 视野半径
    /// </summary>
    [SerializeField]
    private float m_ViewRadius = 10;

    /// <summary>
    /// 攻击距离
    /// </summary>
    [SerializeField]
    private float m_AttackDistance = 2;
    #endregion

    #region 属性
    /// <summary>
    /// 移动的目标位置
    /// </summary>
    private Vector3 m_MoveTargetPos;

    /// <summary>
    /// 移动的目标方向
    /// </summary>
    private Vector3 m_MoveDirection;

    /// <summary>
    /// 转身的初始旋转
    /// </summary>
    private Quaternion m_BeginQuaternion;

    /// <summary>
    /// 转身的目标旋转
    /// </summary>
    private Quaternion m_TargetQuaternion;

    /// <summary>
    /// 转身的旋转插值比例
    /// </summary>
    private float m_RotationRatio;

    /// <summary>
    /// 头顶UI条位置
    /// </summary>
    [HideInInspector]
    public Transform HeadBarPos { get { return m_HeadBarPos; } }

    /// <summary>
    /// 出生点
    /// </summary>
    private Vector3 m_BornPos;

    /// <summary>
    /// 下次巡逻时间
    /// </summary>
    private float m_NextPatrolTime = 0;

    /// <summary>
    /// 下次攻击时间
    /// </summary>
    private float m_NextAttackTime = 0;

    /// <summary>
    /// 主角控制器
    /// </summary>
    [HideInInspector]
    public MainPlayerCtrl mainPlayerCtrl;

    /// <summary>
    /// 休闲状态
    /// </summary>
    private MonsterStateIdle m_StateIdle;

    /// <summary>
    /// 跑动状态
    /// </summary>
    private MonsterStateRun m_StateRun;

    /// <summary>
    /// 攻击状态
    /// </summary>
    private MonsterStateAttack m_StateAttack;

    /// <summary>
    /// 当前状态
    /// </summary>
    [HideInInspector]
    private StateBase m_CurrState;
    #endregion

    #region Start
    void Start()
    {
        m_BornPos = transform.position;
        InitStates();
    }
    #endregion

    #region InitStates 初始化状态：预先创建所有状态，并进入休闲状态
    /// <summary>
    /// 初始化状态：预先创建所有状态，并进入休闲状态
    /// </summary>
    private void InitStates()
    {
        m_StateIdle = new MonsterStateIdle(this);
        m_StateRun = new MonsterStateRun(this);
        m_StateAttack = new MonsterStateAttack(this);

        m_CurrState = m_StateIdle;
        m_CurrState.OnEnter();
    }
    #endregion

    #region Update
    void Update()
    {
        m_CurrState.OnUpdate();
    }
    #endregion

    #region 状态切换函数
    /// <summary>
    /// 进入休闲状态
    /// </summary>
    public void ChangeToIdleState()
    {
        m_CurrState.OnLeave();
        m_CurrState = m_StateIdle;
        m_CurrState.OnEnter();
    }

    /// <summary>
    /// 进入跑动状态
    /// </summary>
    /// <param name="targetPos">移动目标点</param>
    public void ChangeToRunState(Vector3 targetPos)
    {
        m_CurrState.OnLeave();
        m_MoveTargetPos = targetPos;
        m_CurrState = m_StateRun;
        m_CurrState.OnEnter();
    }

    /// <summary>
    /// 进入攻击状态
    /// </summary>
    public void ChangeToAttackState()
    {
        m_CurrState.OnLeave();
        m_CurrState = m_StateAttack;
        m_CurrState.OnEnter();
    }
    #endregion
}