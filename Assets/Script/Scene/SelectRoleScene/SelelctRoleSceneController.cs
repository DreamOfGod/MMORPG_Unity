//===============================================
//作    者：
//创建时间：2022-04-20 21:58:50
//备    注：
//===============================================
using System;
using UnityEngine;

public class SelelctRoleSceneController : MonoBehaviour
{
    //视图
    [SerializeField]
    private SelectRoleSceneView m_SelectRoleSceneView;
    //检测拖拽区域的物体控制器
    [SerializeField]
    private SelectRoleDragAreaController m_SelectRoleDragAreaController;
    //弹窗容器
    [SerializeField]
    private Transform m_WindowParent;

    //当前选择的职业项序号
    private int m_SelectJobItemIdx = 0;
    //当前选择的角色项序号
    private int m_SelectRoleItemIdx = 0;

    private void Awake()
    {
        GameServerModel.Instance.CreateRoleReturn += OnCreateRoleReturn;
        m_SelectRoleDragAreaController.EndHorizontalDrag += OnEndHorizontalDrag;
    }

    private void OnDestroy()
    {
        GameServerModel.Instance.CreateRoleReturn -= OnCreateRoleReturn;
        m_SelectRoleDragAreaController.EndHorizontalDrag -= OnEndHorizontalDrag;
    }

    private async void Start()
    {
        await m_SelectRoleSceneView.LoadRolePrefab(JobConfig.Instance.List);
        m_SelectRoleSceneView.InitCreateRoleUI(JobConfig.Instance.List, m_SelectJobItemIdx, OnClickJobItem);
        m_SelectRoleSceneView.InitSelectRoleUI(GameServerModel.Instance.RoleList, m_SelectRoleItemIdx, OnClickRoleItem);
        if (GameServerModel.Instance.RoleList.Count == 0)
        {
            m_SelectRoleSceneView.ChangeToCreateRoleView(JobConfig.Instance.List, m_SelectJobItemIdx);
        }
        else
        {
            m_SelectRoleSceneView.ChangeToSelectRoleView(GameServerModel.Instance.RoleList[m_SelectRoleItemIdx].RoleJob);
        }
    }

    //点击创建角色项回调
    private void OnClickJobItem(int idx)
    {
        if(m_SelectJobItemIdx == idx)
        {
            return;
        }
        m_SelectRoleSceneView.SetSelectJobItem(m_SelectJobItemIdx, idx, JobConfig.Instance.List[idx]);
        m_SelectJobItemIdx = idx;
    }

    //点击角色项回调
    private void OnClickRoleItem(int idx)
    {
        m_SelectRoleSceneView.ShowCurSelectRole(GameServerModel.Instance.RoleList[m_SelectRoleItemIdx].RoleJob, m_SelectRoleItemIdx, GameServerModel.Instance.RoleList[idx].RoleJob, idx);
        m_SelectRoleItemIdx = idx;
    }

    //拖拽屏幕结束事件回调
    private void OnEndHorizontalDrag(UIDirection dir)
    {
        if(m_SelectRoleSceneView.IsRotating)
        {
            return;
        }
        int targetJobIdx;
        if(dir == UIDirection.LEFT)
        {
            targetJobIdx = m_SelectJobItemIdx + 1;
            if(targetJobIdx > 3)
            {
                targetJobIdx = 0;
            }
        }
        else if(dir == UIDirection.RIGHT)
        {
            targetJobIdx = m_SelectJobItemIdx - 1;
            if(targetJobIdx < 0)
            {
                targetJobIdx = 3;
            }
        }
        else
        {
            throw new ArgumentException("参数dir只能是左和右");
        }
        m_SelectRoleSceneView.SetSelectJobItem(m_SelectJobItemIdx, targetJobIdx, JobConfig.Instance.List[targetJobIdx]);
        m_SelectJobItemIdx = targetJobIdx;
    }

    /// <summary>
    /// 开始按钮回调
    /// </summary>
    public void OnClickStartBtn()
    {
        //GameServerModel.Instance.ReqStartGame();
        LoadingSceneController.LoadSceneFromAssetBundle(AssetBundlePath.HuPaoCunScene, SceneName.HuPaoCun);
    }

    /// <summary>
    /// 切换创建角色按钮回调
    /// </summary>
    public void OnClickToCreateRoleBtn()
    {
        m_SelectRoleSceneView.ChangeToCreateRoleView(JobConfig.Instance.List, m_SelectJobItemIdx);
    }

    /// <summary>
    /// 创建角色按钮回调
    /// </summary>
    public void OnClickCreateRoleBtn()
    {
        string nickname = m_SelectRoleSceneView.GetNickname();
        if (string.IsNullOrEmpty(nickname))
        {
            MessageWindow.Show(m_WindowParent, "提示", "请输入2至7位昵称", true, false);
        }
        else if(nickname.Length < 2 || nickname.Length > 7)
        {
            MessageWindow.Show(m_WindowParent, "提示", "昵称长度必须是2至7位", true, false);
        }
        else
        {
            GameServerModel.Instance.ReqCreateRole((byte)JobConfig.Instance.List[m_SelectJobItemIdx].Id, nickname);
        }
    }

    //创建角色返回消息回调
    private void OnCreateRoleReturn(RoleOperation_CreateRoleReturnProto proto)
    {
        if(proto.IsSuccess)
        {
            MessageWindow.Show(m_WindowParent, "提示", "创建角色成功", true, false, null, null, () => {
                m_SelectRoleSceneView.AddRoleItem(GameServerModel.Instance.RoleList, OnClickRoleItem);
                m_SelectRoleSceneView.ChangeToSelectRoleView(GameServerModel.Instance.RoleList[m_SelectRoleItemIdx].RoleJob);
            });
        }
        else
        {
            MessageWindow.Show(m_WindowParent, "提示", proto.MsgCode == 2? "昵称重复": "创建失败", true, false);
        }
    }

    /// <summary>
    /// 返回按钮回调
    /// </summary>
    public void OnClickReturnBtn()
    {
        if(m_SelectRoleSceneView.CurViewType == SelectRoleSceneView.ViewType.CreateRole && GameServerModel.Instance.RoleList.Count > 0)
        {
            m_SelectRoleSceneView.ChangeToSelectRoleView(GameServerModel.Instance.RoleList[m_SelectRoleItemIdx].RoleJob);
        }
        else
        {
            SocketHelper.Instance.Close();
            LoadingSceneController.LoadSceneFromAssetBundle(AssetBundlePath.LogonScene, SceneName.Logon);
        }
    }

    /// <summary>
    /// 随机昵称按钮回调
    /// </summary>
    public void OnClickRandomName()
    {
        m_SelectRoleSceneView.SetNickname(StringUtility.RandomChinese(7));
    }
}