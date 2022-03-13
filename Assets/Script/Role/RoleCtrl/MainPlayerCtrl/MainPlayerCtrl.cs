//===============================================
//作    者：
//创建时间：2022-03-10 10:50:50
//备    注：
//===============================================
using System.Collections;
using UnityEngine;

/// <summary>
/// 主角控制器
/// </summary>
public partial class MainPlayerCtrl : MonoBehaviour
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
    #endregion

    #region 属性
    /// <summary>
    /// 头顶UI条位置
    /// </summary>
    [HideInInspector]
    public Transform HeadBarPos { get { return m_HeadBarPos; } }

    /// <summary>
    /// 移动的目标位置
    /// </summary>
    private Vector3 m_MoveTargetPos;

    /// <summary>
    /// 移动的目标方向
    /// </summary>
    private Vector3 m_MoveDirection;

    /// <summary>
    /// 转身的旋转插值比例
    /// </summary>
    private float m_RotationRatio;

    /// <summary>
    /// 转身的初始旋转
    /// </summary>
    private Quaternion m_BeginQuaternion;

    /// <summary>
    /// 转身的目标旋转
    /// </summary>
    private Quaternion m_TargetQuaternion;

    /// <summary>
    /// 休闲状态
    /// </summary>
    private MainPlayerStateIdle m_StateIdle;

    /// <summary>
    /// 跑步状态
    /// </summary>
    private MainPlayerStateRun m_StateRun;

    /// <summary>
    /// 受伤状态
    /// </summary>
    private MainPlayerStateHurt m_StateHurt;

    /// <summary>
    /// 死亡状态
    /// </summary>
    private MainPlayerStateDie m_StateDie;

    /// <summary>
    /// 当前状态
    /// </summary>
    private StateBase m_CurrState;

    private int HP = 100;

    private UISceneCityCtrl m_CityUICtrl;

    /// <summary>
    /// 头顶UI条控制器
    /// </summary>
    private RoleHeadBarCtrl m_RoleHeadBarCtrl;
    #endregion

    #region Start
    void Start()
    {
        InitStates();
    }
    #endregion

    #region InitStates 初始化状态：预先创建所有状态对象，并进入休闲状态
    /// <summary>
    /// 初始化状态：预先创建所有状态对象，并进入休闲状态
    /// </summary>
    private void InitStates()
    {
        m_StateIdle = new MainPlayerStateIdle(this);
        m_StateRun = new MainPlayerStateRun(this);
        m_StateHurt = new MainPlayerStateHurt(this);
        m_StateDie = new MainPlayerStateDie(this);

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
        m_RoleHeadBarCtrl = headBarCtrl;
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
    public void ChangeToRunState()
    {
        m_CurrState.OnLeave();
        m_CurrState = m_StateRun;
        m_CurrState.OnEnter();
    }

    /// <summary>
    /// 进入受伤状态
    /// </summary>
    /// <param name="hurtVal">受伤伤害值</param>
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

    /// <summary>
    /// 立即受伤
    /// </summary>
    /// <param name="hurtVal"></param>
    private void ToBeHurt(int hurtVal)
    {
        m_CurrState.OnLeave();

        HP -= hurtVal;
        HP = Mathf.Max(0, HP);
        m_RoleHeadBarCtrl.SetHUDText(hurtVal);
        m_CityUICtrl.SetMainPlayerHPBar(HP / 100f);
        m_CurrState = m_StateHurt;
        m_CurrState.OnEnter();
    }

    /// <summary>
    /// 延迟受伤
    /// </summary>
    /// <param name="hurtVal"></param>
    /// <param name="delayTime"></param>
    /// <returns></returns>
    private IEnumerator ToBeHurt(int hurtVal, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        ToBeHurt(hurtVal);
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
    public bool IsDieState()
    {
        return m_CurrState is MainPlayerStateDie;
    }
    #endregion

    #region OnPlayerClickGround 玩家点击地面回调
    /// <summary>
    /// 玩家点击地面回调
    /// </summary>
    /// <param name="screenPos"></param>
    private void OnPlayerClickGround(Vector2 screenPos)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPos);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            //点击地面
            if (hitInfo.collider.gameObject.layer == LayerMask.NameToLayer(LayerName.Ground))
            {
                if (Vector3.Distance(hitInfo.point, transform.position) > 0.1f)
                {
                    m_MoveTargetPos = hitInfo.point;
                    ChangeToRunState();
                }
            }
        }
    }
    #endregion
}