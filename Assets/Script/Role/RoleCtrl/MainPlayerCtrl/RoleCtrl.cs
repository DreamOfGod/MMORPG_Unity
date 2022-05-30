//===============================================
//作    者：
//创建时间：2022-03-10 10:50:50
//备    注：
//===============================================
using Pathfinding;
using System.Collections;
using UnityEngine;

/// <summary>
/// 角色控制器
/// </summary>
[RequireComponent(typeof(CapsuleCollider), typeof(Seeker), typeof(FunnelModifier))]
public partial class RoleCtrl : MonoBehaviour
{
    //动画
    [SerializeField]
    private Animator m_Animator;
    //移动速度
    [SerializeField]
    private float m_MoveSpeed = 10f;
    //转身速度
    [SerializeField]
    private float m_RotationSpeed = 5f;
    //头顶UI条位置
    [SerializeField]
    private Transform m_HeadBarPos;
    //攻击距离
    [SerializeField]
    private float m_AttackDistance = 2;

    /// <summary>
    /// 头顶UI条位置
    /// </summary>
    [HideInInspector]
    public Transform HeadBarPos { get=> m_HeadBarPos; }
    //寻路组件
    private Seeker m_Seeker;
    //A星路径
    private ABPath m_AStarPath;
    //A星路径索引
    private int m_AStarPathIdx = 1;

    // 移动的目标方向
    private Vector3 m_MoveDirection;
    // 转身的旋转插值比例
    private float m_RotationRatio;
    // 转身的初始旋转
    private Quaternion m_BeginQuaternion;
    // 转身的目标旋转
    private Quaternion m_TargetQuaternion;
    // 休闲状态
    private RoleStateIdle m_StateIdle;
    // 跑步状态
    private RoleStateRun m_StateRun;
    // 攻击状态
    private RoleStateAttack m_StateAttack;
    // 受伤状态
    private RoleStateHurt m_StateHurt;
    // 死亡状态
    private RoleStateDie m_StateDie;
    // 当前状态
    private StateBase m_CurrState;
    // 血量
    private int HP = 100;
    // 主城UI控制器
    private UISceneCityCtrl m_CityUICtrl;
    // 头顶UI条控制器
    private RoleHeadBarCtrl m_HeadBarCtrl;
    // 目标怪物
    private MonsterCtrl m_TargetMonster = null;
    // 下次攻击时间
    private float m_NextAttackTime = 0;

    private Vector3 m_TargetPos;

    void Start()
    {
        m_Seeker = GetComponent<Seeker>();
        InitStates();
    }

    #region InitStates 初始化状态
    // 初始化状态
    private void InitStates()
    {
        m_StateIdle = new RoleStateIdle(this);
        m_StateRun = new RoleStateRun(this, GetComponent<CapsuleCollider>());
        m_StateHurt = new RoleStateHurt(this);
        m_StateDie = new RoleStateDie(this);
        m_StateAttack = new RoleStateAttack(this);
        m_CurrState = m_StateIdle;
        m_CurrState.OnEnter();
    }
    #endregion

    #region SetHeadBarCtrl 设置头顶UI条控制器
    /// <summary>
    /// 设置头顶UI条控制器
    /// </summary>
    /// <param name="headBarCtrl"></param>
    public void SetHeadBarCtrl(RoleHeadBarCtrl headBarCtrl)
    {
        m_HeadBarCtrl = headBarCtrl;
    }
    #endregion

    #region SetCityUICtrl 设置主城UI控制器
    /// <summary>
    /// 设置主城UI控制器
    /// </summary>
    /// <param name="cityUICtrl">主城UI控制器</param>
    public void SetCityUICtrl(UISceneCityCtrl cityUICtrl)
    {
        m_CityUICtrl = cityUICtrl;
    }
    #endregion

    #region Update
    void Update()
    {
        m_CurrState.OnUpdate();
        

#if UNITY_EDITOR
        if (m_CurrState is RoleStateRun)
        {
            m_MoveDirectionRay.gameObject.SetActive(true);
            m_MoveDirectionRay.SetPosition(0, transform.position);
            m_MoveDirectionRay.SetPosition(1, transform.position + m_MoveDirection);
        }
#endif
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + Vector3.up * 10, transform.position + Vector3.down * 10);
        Gizmos.DrawWireSphere(transform.position, 2);
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
    /// 进入跑步状态
    /// </summary>
    public void ChangeToRunState(Vector3 targetPos)
    {
        DebugLogger.LogError("ChangeToRunState");
        m_TargetPos = targetPos;
        m_CurrState.OnLeave();
        m_CurrState = m_StateRun;
        m_CurrState.OnEnter();
        //m_Seeker.StartPath(transform.position, targetPos, (Path path) => {
        //    if(path.error)
        //    {
        //        DebugLogger.LogError($"寻路出错，error：{ path.errorLog }");
        //    }
        //    else
        //    {
        //        var abPath = path as ABPath;
        //        var originalEndPoint = abPath.originalEndPoint;
        //        var endPoint = abPath.endPoint;
        //        if (Vector3.Distance(endPoint, new Vector3(originalEndPoint.x, endPoint.y, originalEndPoint.z)) > 0.5f)
        //        {
        //            DebugLogger.LogWarning("A星计算出来的目标点与原始目标点距离太远，忽略本次点击");
        //            //return;
        //        }
        //        m_AStarPath = abPath;
        //        m_AStarPathIdx = 1;
        //        m_CurrState.OnLeave();
        //        m_CurrState = m_StateRun;
        //        m_CurrState.OnEnter();
        //    }
        //});
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

    /// <summary>
    /// 进入受伤状态
    /// </summary>
    /// <param name="hurtVal">伤害值</param>
    /// <param name="delayTime">延迟时间</param>
    public void ChangeToHurtState(int hurtVal, float delayTime = 0)
    {
        if(delayTime > 0)
        {
            StartCoroutine(ToBeHurt(hurtVal, delayTime));
        }
        else
        {
            ToBeHurt(hurtVal);
        }
    }

    // 受伤
    private void ToBeHurt(int hurtVal)
    {
        HP -= hurtVal;
        HP = Mathf.Max(0, HP);
        m_HeadBarCtrl.Hurt(hurtVal, HP / 100f);
        m_CityUICtrl.SetMainPlayerHPBar(HP / 100f);

        if (!(m_CurrState is RoleStateHurt))
        {
            m_CurrState.OnLeave();
            m_CurrState = m_StateHurt;
            m_CurrState.OnEnter();
        }
    }

    // 延迟受伤
    private IEnumerator ToBeHurt(int hurtVal, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        if(HP > 0)
        {
            ToBeHurt(hurtVal);
        }
    }

    /// <summary>
    /// 进入死亡状态
    /// </summary>
    public void ChangeToDieState()
    {
        m_CurrState.OnLeave();
        m_CurrState = m_StateDie;
        m_CurrState.OnEnter();
    }
    #endregion

    #region 状态判断函数
    /// <summary>
    /// 是否已死亡
    /// </summary>
    /// <returns></returns>
    public bool IsDieState()
    {
        return HP <= 0;
    }
    #endregion

    /// <summary>
    /// 用户点击屏幕回调
    /// </summary>
    /// <param name="pos"></param>
    public void OnPlayerClick(Vector2 pos)
    {
        if (m_CurrState is RoleStateIdle)
        {
            var ray = Camera.main.ScreenPointToRay(pos);
            RaycastHit hitInfo;
            var groundLayer = LayerMask.NameToLayer(LayerName.Ground);
            var monsterLayer = LayerMask.NameToLayer(LayerName.Monster);
            if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, (1 << groundLayer) | (1 << monsterLayer)))
            {
                //点击了怪物或地面
                var colliderLayer = hitInfo.collider.gameObject.layer;
                if (colliderLayer == monsterLayer)
                {
                    //点击了怪物，设置目标怪物，等OnUpdate执行时再处理
                    //m_MainPlayerCtrl.m_TargetMonster = hitInfo.collider.GetComponent<MonsterCtrl>();

                    var monsterCtrl = hitInfo.collider.GetComponent<MonsterCtrl>();
                    if (!monsterCtrl.isDieState())
                    {
                        m_TargetMonster = monsterCtrl;
                        var distance = Vector3.Distance(transform.position, monsterCtrl.transform.position);
                        if (distance <= m_AttackDistance)
                        {
                            //目标怪物在攻击范围内
                            if (Time.time >= m_NextAttackTime)
                            {
                                //达到攻击时间，转为攻击状态
                                ChangeToAttackState();
                            }
                        }
                        else
                        {
                            //不在攻击范围内，跑向目标怪物
                            ChangeToRunState(m_TargetMonster.transform.position);
                        }
                    }
                }
                else if (colliderLayer == groundLayer)
                {
                    //点击了地面
                    m_TargetMonster = null;
                    if (Vector3.Distance(hitInfo.point, transform.position) > 0.1f)
                    {
                        //目标点离当前位置足够远，转入移动状态
                        ChangeToRunState(hitInfo.point);
                    }
                }
            }
        }
        else if(m_CurrState is RoleStateRun)
        {
            Ray ray = Camera.main.ScreenPointToRay(pos);
            RaycastHit hitInfo;
            int groundLayer = LayerMask.NameToLayer(LayerName.Ground);
            int monsterLayer = LayerMask.NameToLayer(LayerName.Monster);
            int targetLayer = 1 << groundLayer | 1 << monsterLayer;
            if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, targetLayer))
            {
                //检测到点击地面或怪物
                int colliderLayer = hitInfo.collider.gameObject.layer;
                if (colliderLayer == monsterLayer)
                {
                    //点击了怪物
                    m_TargetMonster = hitInfo.collider.GetComponent<MonsterCtrl>();
                }
                else if (colliderLayer == groundLayer)
                {
                    //点击了地面

                    m_TargetMonster = null;
                    if (Vector3.Distance(hitInfo.point, transform.position) > 0.1f)
                    {
                        //目标位置离当前位置足够远
                        ChangeToRunState(hitInfo.point);
                    }
                    else
                    {
                        //目标位置离当前位置太近，转为休闲状态
                        ChangeToIdleState();
                    }
                }
            }
        }
        else if(m_CurrState is RoleStateHurt)
        {
            if (HP <= 0)
            {
                return;
            }
            var ray = Camera.main.ScreenPointToRay(pos);
            RaycastHit hitInfo;
            var groundLayer = LayerMask.NameToLayer(LayerName.Ground);
            var monsterLayer = LayerMask.NameToLayer(LayerName.Monster);
            if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, (1 << groundLayer) | (1 << monsterLayer)))
            {
                //点击了地面或怪物
                var colliderLayer = hitInfo.collider.gameObject.layer;
                if (colliderLayer == monsterLayer)
                {
                    //点击怪物
                    m_TargetMonster = hitInfo.collider.GetComponent<MonsterCtrl>();
                    var distance = Vector3.Distance(m_TargetMonster.transform.position, transform.position);
                    if (distance <= m_AttackDistance)
                    {
                        //怪物在攻击范围，转为攻击
                        ChangeToAttackState();
                    }
                    else
                    {
                        //怪物不在攻击范围，跑向怪物
                        ChangeToRunState(m_TargetMonster.transform.position);
                    }
                }
                else if (colliderLayer == groundLayer)
                {
                    //点击地面
                    m_TargetMonster = null;
                    if (Vector3.Distance(hitInfo.point, transform.position) > 0.1f)
                    {
                        ChangeToRunState(hitInfo.point);
                    }
                }
            }
        }
    }

    #region Debug
    //移动方向射线
    [SerializeField]
    private LineRenderer m_MoveDirectionRay;
    #endregion
}