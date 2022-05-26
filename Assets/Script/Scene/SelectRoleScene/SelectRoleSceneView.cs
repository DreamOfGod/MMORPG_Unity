//===============================================
//作    者：
//创建时间：2022-04-21 16:40:50
//备    注：
//===============================================
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SelectRoleSceneView : MonoBehaviour
{
    //主摄像机父物体
    [SerializeField]
    private Transform m_MainCameraParent;
    //摄像机旋转速度，单位：度/秒
    [SerializeField]
    private float m_CameraRotateSpeed = 100;
    //职业名
    [SerializeField]
    private Text m_JobName;
    //职业描述
    [SerializeField]
    private Text m_JobDesc;
    //昵称
    [SerializeField]
    private InputField m_NicknameInput;
    //角色项父物体
    [SerializeField]
    private RectTransform m_RoleItemParent;
    //角色项
    [SerializeField]
    private GameObject m_RoleItem;
    //创建角色相关的物体
    [SerializeField]
    private GameObject[] m_CreateRoleGameObjects;
    //选择角色相关的物体
    [SerializeField]
    private GameObject[] m_SelectRoleGameObjects;
    //角色容器
    [SerializeField]
    private Transform[] m_RoleContainers;
    //职业项列表
    [SerializeField]
    private JobItem[] m_JobItemList;
    //创建角色按钮
    [SerializeField]
    private RectTransform m_CreateRoleBtn;
    //切换为创建角色界面的按钮
    [SerializeField]
    private RectTransform m_ToCreateRoleBtn;
    //开始游戏按钮
    [SerializeField]
    private RectTransform m_StartBtn;

    //起始欧拉角的y值
    private float m_OriginalEulerAngleY;
    //目标欧拉角的y值
    private float m_TargetEulerAngleY;
    //旋转所需的总时间
    private float m_RotateTotalTime;
    //旋转经过的时间
    private float m_RotateElapsedTime;
    //是否需要旋转
    private bool m_IsNeedRotate = false;
    /// <summary>
    /// 是否正在旋转
    /// </summary>
    public bool IsRotating { get => m_IsNeedRotate; }
    //视图类型枚举
    public enum ViewType
    {
        CreateRole, SelectRole,
    }
    //当前视图类型
    private ViewType m_CurViewType;
    public ViewType CurViewType { get => m_CurViewType; }
    //克隆出来的角色
    private Dictionary<int, GameObject> m_RoleDic = new Dictionary<int, GameObject>();
    //角色项列表
    private List<RoleItem> m_RoleItemList = new List<RoleItem>();

    #region 旋转
    /// <summary>
    /// 向左旋转摄像机
    /// </summary>
    /// <param name="prevSelectJobIdx">前一个选择的职业项序号</param>
    /// <param name="curSelectJobIdx">当前选择的职业项序号</param>
    public void RotateCameraToLeft(int prevSelectJobIdx, int curSelectJobIdx, JobEntity jobInfo)
    {
        //欧拉角的y值增大到最近的90度的倍数
        RotateCamera((Mathf.FloorToInt(m_MainCameraParent.localEulerAngles.y / 90) + 1) * 90);
        ToggleSelectJob(prevSelectJobIdx, curSelectJobIdx, jobInfo);
    }

    /// <summary>
    /// 向右旋转摄像机
    /// </summary>
    /// <param name="prevSelectJobIdx">前一个选择的职业项序号</param>
    /// <param name="curSelectJobIdx">当前选择的职业项序号</param>
    public void RotateCameraToRight(int prevSelectJobIdx, int curSelectJobIdx, JobEntity jobInfo)
    {
        //eulerAngleY减小到最近的90度的倍数
        RotateCamera((Mathf.CeilToInt(m_MainCameraParent.localEulerAngles.y / 90) - 1) * 90);
        ToggleSelectJob(prevSelectJobIdx, curSelectJobIdx, jobInfo);
    }

    //切换选择的职业
    private void ToggleSelectJob(int prevSelectJobIdx, int curSelectJobIdx, JobEntity jobInfo)
    {
        m_JobItemList[prevSelectJobIdx].MoveToOrigin();
        m_JobItemList[curSelectJobIdx].MoveToLeft();
        SetSelectJobDesc(jobInfo.Name, jobInfo.Desc);
    }

    /// <summary>
    /// 旋转摄像机
    /// </summary>
    /// <param name="targetEulerAngleY">目标欧拉角的y值，[0, 360)</param>
    public void RotateCamera(float targetEulerAngleY)
    {
        m_TargetEulerAngleY = targetEulerAngleY;
        m_TargetEulerAngleY %= 360;
        if (m_TargetEulerAngleY < 0)
        {
            m_TargetEulerAngleY += 360;
        }
        m_OriginalEulerAngleY = m_MainCameraParent.localEulerAngles.y;
        var delta = m_TargetEulerAngleY - m_OriginalEulerAngleY;
        if (delta < -180)
        {
            m_TargetEulerAngleY += 360;
        }
        else if (delta > 180)
        {
            m_TargetEulerAngleY -= 360;
        }
        m_RotateTotalTime = Mathf.Abs(m_TargetEulerAngleY - m_OriginalEulerAngleY) / m_CameraRotateSpeed;
        m_RotateElapsedTime = 0;
        m_IsNeedRotate = true;
    }

    private void Update()
    {
        if (m_IsNeedRotate)
        {
            m_RotateElapsedTime += Time.deltaTime;
            float t = m_RotateElapsedTime / m_RotateTotalTime;
            if (t >= 1)
            {
                t = 1;
                m_IsNeedRotate = false;
            }
            float y = Mathf.Lerp(m_OriginalEulerAngleY, m_TargetEulerAngleY, t);
            m_MainCameraParent.localEulerAngles = new Vector3(0, y, 0);//欧拉角每个维度的范围都是[0, 360)，左闭右开。超出此范围会自动转换到这个范围内
        }
    }
    #endregion

    #region 创建角色
    /// <summary>
    /// 设置选择的职业项
    /// </summary>
    /// <param name="curSelectJobIdx"></param>
    public void SetSelectJobItem(int prevSelectJobIdx, int curSelectJobIdx, JobEntity jobInfo)
    {
        var targetEulerAngleY = 360 - curSelectJobIdx * 90;
        if (targetEulerAngleY == 360)
        {
            targetEulerAngleY = 0;
        }
        RotateCamera(targetEulerAngleY);
        ToggleSelectJob(prevSelectJobIdx, curSelectJobIdx, jobInfo);
    }

    /// <summary>
    /// 设置选择的职业描述
    /// </summary>
    /// <param name="jobName"></param>
    /// <param name="jobDesc"></param>
    public void SetSelectJobDesc(string jobName, string jobDesc)
    {
        m_JobName.text = jobName;
        m_JobDesc.text = jobDesc;
    }

    /// <summary>
    /// 设置昵称
    /// </summary>
    /// <param name="nickname"></param>
    public void SetNickname(string nickname)
    {
        m_NicknameInput.text = nickname;
    }

    /// <summary>
    /// 获取昵称
    /// </summary>
    /// <returns></returns>
    public string GetNickname()
    {
        return m_NicknameInput.text;
    }

    /// <summary>
    /// 切换成创建角色界面
    /// </summary>
    /// <param name="curSelectJobIdx"></param>
    public void ChangeToCreateRoleView(List<JobEntity> jobList, int curSelectJobIdx)
    {
        m_CurViewType = ViewType.CreateRole;
        SetCreateRoleGameObjectsActive(true);
        for(int i = 0; i < jobList.Count; ++i)
        {
            m_RoleDic[jobList[i].Id].SetActive(true);
            m_RoleDic[jobList[i].Id].transform.SetParent(m_RoleContainers[i], false);
        }
        var createRoleItemParent = m_JobItemList[0].transform.parent as RectTransform;
        var tmp = createRoleItemParent.localPosition.x;
        createRoleItemParent.anchoredPosition = new Vector2(150, createRoleItemParent.anchoredPosition.y);
        createRoleItemParent.DOLocalMoveX(tmp, 0.2f);
        var createRoleJobNameParent = m_JobName.transform.parent as RectTransform;
        tmp = createRoleJobNameParent.localPosition.y;
        createRoleJobNameParent.anchoredPosition = new Vector2(createRoleJobNameParent.anchoredPosition.x, 125);
        createRoleJobNameParent.DOLocalMoveY(tmp, 0.2f);
        var nicknameInputParent = m_NicknameInput.transform.parent as RectTransform;
        tmp = nicknameInputParent.localPosition.y;
        nicknameInputParent.anchoredPosition = new Vector2(0, -50);
        nicknameInputParent.DOLocalMoveY(tmp, 0.2f);
        tmp = m_CreateRoleBtn.localPosition.x;
        m_CreateRoleBtn.anchoredPosition = new Vector2(-90, m_CreateRoleBtn.anchoredPosition.y);
        m_CreateRoleBtn.DOLocalMoveX(tmp, 0.2f);

        var targetEulerAngleY = 360 - curSelectJobIdx * 90;
        if (targetEulerAngleY == 360)
        {
            targetEulerAngleY = 0;
        }
        m_MainCameraParent.localEulerAngles = new Vector3(0, targetEulerAngleY, 0);
        m_IsNeedRotate = false;
    }

    //加载角色预制
    public async Task LoadRolePrefab(List<JobEntity> jobList)
    {
        //加载角色资源包
        var abcrList = new List<AssetBundleCreateRequest>();
        for (int i = 0; i < jobList.Count; ++i)
        {
            abcrList.Add(AssetBundle.LoadFromFileAsync(AssetBundlePath.RolePath(jobList[i].PrefabName)));
        }
        await AsyncOperationUtility.WaitAll(abcrList);
        //加载角色预制体
        var abrList = new List<AssetBundleRequest>();
        for (int i = 0; i < abcrList.Count; ++i)
        {
            abrList.Add(abcrList[i].assetBundle.LoadAssetAsync(jobList[i].PrefabName));
        }
        await AsyncOperationUtility.WaitAll(abrList);
        //释放角色资源包
        for (int i = 0; i < abcrList.Count; ++i)
        {
            abcrList[i].assetBundle.Unload(false);
        }
        //克隆角色
        for (int i = 0; i < abrList.Count; ++i)
        {
            GameObject prefab = abrList[i].asset as GameObject;
            GameObject obj = Instantiate(prefab, null, false);
            obj.transform.localPosition = Vector3.zero;
            m_RoleDic[jobList[i].Id] = obj;
        }
    }

    /// <summary>
    /// 初始化创建角色ui
    /// </summary>
    /// <param name="curSelectJobIdx"></param>
    /// <param name="clickFunc"></param>
    public void InitCreateRoleUI(List<JobEntity> jobList, int curSelectJobIdx, Action<int> clickJobItemFunc)
    {
        SetCreateRoleGameObjectsActive(true);
        for(int i = 0; i < jobList.Count; ++i)
        {
            m_JobItemList[i].Init(i, jobList[i].Name, $"UI/UISource/HeadImg/{ jobList[i].HeadPic }", clickJobItemFunc);
        }
        m_JobItemList[curSelectJobIdx].PlaceToLeft();
        SetSelectJobDesc(jobList[curSelectJobIdx].Name, jobList[curSelectJobIdx].Desc);
    }
    #endregion

    #region 选择角色
    /// <summary>
    /// 切换成选择角色界面
    /// </summary>
    public void ChangeToSelectRoleView(int curSelectRoleId)
    {
        m_CurViewType = ViewType.SelectRole;
        SetCreateRoleGameObjectsActive(false);
        SetCameraYAngleToZero();
        foreach(var kv in m_RoleDic)
        {
            kv.Value.SetActive(false);
        }
        var tmp = m_RoleItemParent.localPosition.x;
        m_RoleItemParent.anchoredPosition = new Vector2(-154, m_RoleItemParent.anchoredPosition.y);
        m_RoleItemParent.DOLocalMoveX(tmp, 0.2f).OnComplete(() => {
            m_RoleDic[curSelectRoleId].SetActive(true);
            m_RoleDic[curSelectRoleId].transform.SetParent(m_RoleContainers[0], false);
        });
        tmp = m_ToCreateRoleBtn.localPosition.x;
        m_ToCreateRoleBtn.anchoredPosition = new Vector2(-90, m_ToCreateRoleBtn.anchoredPosition.y);
        m_ToCreateRoleBtn.DOLocalMoveX(tmp, 0.2f);
        tmp = m_StartBtn.localPosition.x;
        m_StartBtn.anchoredPosition = new Vector2(-60, m_StartBtn.anchoredPosition.y);
        m_StartBtn.DOLocalMoveX(tmp, 0.2f);
    }

    // 将摄像机y轴欧拉角置零
    private void SetCameraYAngleToZero()
    {
        m_MainCameraParent.localEulerAngles = Vector3.zero;
        m_IsNeedRotate = false;
    }

    /// <summary>
    /// 显示当前选择的角色
    /// </summary>
    public void ShowCurSelectRole(int prevSelectRoleId, int prevSelectRoleItemIdx, int curSelectRoleId, int curSelectRoleItemIdx)
    {
        m_RoleItemList[prevSelectRoleItemIdx].SetLightActive(false);
        m_RoleItemList[curSelectRoleItemIdx].SetLightActive(true);
        m_RoleDic[prevSelectRoleId].SetActive(false);
        m_RoleDic[curSelectRoleId].SetActive(true);
        m_RoleDic[curSelectRoleId].transform.SetParent(m_RoleContainers[0], false);
    }

    //设置新建角色和选择已有角色的ui是否隐藏
    private void SetCreateRoleGameObjectsActive(bool isShow)
    {
        for (int i = 0; i < m_CreateRoleGameObjects.Length; ++i)
        {
            m_CreateRoleGameObjects[i].SetActive(isShow);
        }
        isShow = !isShow;
        for (int i = 0; i < m_SelectRoleGameObjects.Length; ++i)
        {
            m_SelectRoleGameObjects[i].SetActive(isShow);
        }
    }

    /// <summary>
    /// 初始化选择角色ui
    /// </summary>
    /// <param name="roleItemList"></param>
    public void InitSelectRoleUI(List<RoleOperation_LogOnGameServerReturnProto.RoleItem> roleItemList, int curSelectRoleIdx, Action<int> clickFunc)
    {
        SetCreateRoleGameObjectsActive(true);
        m_RoleItemParent.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, RoleItemListHeight(roleItemList.Count));
        for (int i = 0; i < roleItemList.Count; ++i)
        {
            var role = roleItemList[i];
            GameObject obj = Instantiate(m_RoleItem, m_RoleItemParent, false);
            obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -40 - i * 90);
            var roleItemScript = obj.GetComponent<RoleItem>();
            roleItemScript.Init(i, role.RoleNickName, role.RoleJob, role.RoleLevel, clickFunc);
            m_RoleItemList.Add(roleItemScript);
        }
        m_RoleItemList[curSelectRoleIdx].SetLightActive(true);
    }

    /// <summary>
    /// 添加角色项
    /// </summary>
    /// <param name="role"></param>
    /// <param name="clickFunc"></param>
    public void AddRoleItem(List<RoleOperation_LogOnGameServerReturnProto.RoleItem> roleItemList, Action<int> clickFunc)
    {
        m_RoleItemParent.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, RoleItemListHeight(roleItemList.Count));
        GameObject obj = Instantiate(m_RoleItem, m_RoleItemParent, false);
        int idx = roleItemList.Count - 1;
        obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -40 - idx * 90);
        var roleItemScript = obj.GetComponent<RoleItem>();
        roleItemScript.Init(idx, roleItemList[idx].RoleNickName, roleItemList[idx].RoleJob, roleItemList[idx].RoleLevel, clickFunc);
        m_RoleItemList.Add(roleItemScript);
    }

    //计算角色项列表高度
    private int RoleItemListHeight(int count)
    {
        return count * 70 + (count - 1) * 20 + 10;
    }
    #endregion
}