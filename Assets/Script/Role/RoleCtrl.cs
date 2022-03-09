//===============================================
//作    者：
//创建时间：2022-02-18 14:36:26
//备    注：
//===============================================
using UnityEngine;

/// <summary>
/// 角色控制器
/// </summary>
public class RoleCtrl : MonoBehaviour
{
    #region unity属性
    /// <summary>
    /// 移动速度
    /// </summary>
    [SerializeField]
    public float Speed = 10f;

    /// <summary>
    /// 转身速度
    /// </summary>
    [SerializeField]
    public float RotationSpeed = 5f;

    /// <summary>
    /// 动画
    /// </summary>
    [SerializeField]
    public Animator Animator;

    /// <summary>
    /// 头顶UI条位置
    /// </summary>
    [SerializeField]
    public Transform HeadBarPos;
    #endregion

    #region 属性
    /// <summary>
    /// 移动的目标点
    /// </summary>
    public Vector3 TargetPos = Vector3.zero;

    /// <summary>
    /// 控制器
    /// </summary>
    [HideInInspector]
    public CharacterController CharacterController;

    /// <summary>
    /// 角色信息
    /// </summary>
    private RoleInfoBase RoleInfo;

    /// <summary>
    /// 角色AI
    /// </summary>
    public IRoleAI RoleAI;

    /// <summary>
    /// 角色有限状态机
    /// </summary>
    public RoleFSM RoleFSM;
    #endregion

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="roleInfo">角色信息</param>
    /// <param name="roleAI">角色AI</param>
    /// <param name="roleFSM">角色有限状态机</param>
    public void Init(RoleInfoBase roleInfo, IRoleAI roleAI)
    {
        RoleInfo = roleInfo;
        RoleAI = roleAI;
    }

    void Start()
    {
        CharacterController = GetComponent<CharacterController>();

        //让角色贴着地面
        if (!CharacterController.isGrounded)
        {
            CharacterController.Move(new Vector3(0, -1000, 0));
        }

        RoleFSM = new RoleFSM(this);
    }

    #region OnEnable
    void OnEnable()
    {
        FingerEvent.Instance.OnFingerDrag += OnFingerDrag;
        FingerEvent.Instance.OnZoom += OnZoom;
    }
    #endregion

    #region OnDisable
    void OnDisable()
    {
        FingerEvent.Instance.OnFingerDrag -= OnFingerDrag;
        FingerEvent.Instance.OnZoom -= OnZoom;
    }
    #endregion

    void OnFingerDrag(FingerEvent.FingerDir dir)
    {
        switch(dir)
        {
            case FingerEvent.FingerDir.Up:
                CameraCtrl.Instance.setCameraUpAndDown(1);
                break;
            case FingerEvent.FingerDir.Down:
                CameraCtrl.Instance.setCameraUpAndDown(0);
                break;
            case FingerEvent.FingerDir.Left:
                CameraCtrl.Instance.SetCameraRotate(0);
                break;
            case FingerEvent.FingerDir.Right:
                CameraCtrl.Instance.SetCameraRotate(1);
                break;
        }
    }

    #region OnZoom 摄像机缩放
    /// <summary>
    /// 摄像机缩放
    /// </summary>
    /// <param name="type"></param>
    void OnZoom(FingerEvent.ZoomType type)
    {
        switch(type)
        {
            case FingerEvent.ZoomType.In:
                CameraCtrl.Instance.setCameraZoom(0);
                break;
            case FingerEvent.ZoomType.Out:
                CameraCtrl.Instance.setCameraZoom(1);
                break;
        }
    }
    #endregion

    void Update()
    {
        if (RoleAI == null)
        {
            return;
        }

        RoleAI.DoAI();

        if (CharacterController == null) 
        { 
            return;
        }

        RoleFSM.OnUpdate();

        CameraAutoFollow();

        if (Input.GetKeyUp(KeyCode.R))
        {
            RoleFSM.ChangeToRunState();
        } 
        else if (Input.GetKeyUp(KeyCode.N)) 
        {
            RoleFSM.ChangeToIdleState();
        }
        else if (Input.GetKeyUp(KeyCode.F))
        {
            RoleFSM.ChangeToIdleState();
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            RoleFSM.ChangeToDieState();
        }
        else if (Input.GetKeyUp(KeyCode.H))
        {
            RoleFSM.ChangeToHurtState();
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            RoleFSM.ChangeToAttackState();
        }
        else if (Input.GetKeyUp(KeyCode.B))
        {
            RoleFSM.ChangeToAttackState();
        }
        else if (Input.GetKeyUp(KeyCode.C))
        {
            RoleFSM.ChangeToAttackState();
        }
    }

    #region CameraAutoFollow 摄像机自动跟随
    /// <summary>
    /// 摄像机自动跟随
    /// </summary>
    private void CameraAutoFollow()
    {
        if(CameraCtrl.Instance)
        {
            CameraCtrl.Instance.transform.position = transform.position;
            CameraCtrl.Instance.AutoLookAt(transform.position);
        }
    }
    #endregion
}