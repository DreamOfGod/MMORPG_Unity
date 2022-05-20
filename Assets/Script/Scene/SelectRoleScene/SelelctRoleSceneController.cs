//===============================================
//作    者：
//创建时间：2022-04-20 21:58:50
//备    注：
//===============================================
using System;
using System.Collections.Generic;
using UnityEngine;

public class SelelctRoleSceneController : MonoBehaviour
{
    [SerializeField]
    private SelectRoleSceneView m_SelectRoleSceneView;
    [SerializeField]
    private Transform[] m_RoleContainers;
    [SerializeField]
    private SelectRoleDragAreaController m_SelectRoleDragAreaController;
    //职业列表
    [SerializeField]
    private JobItem[] m_JobItemList;

    //当前选择的职业项序号
    private int m_SelectJobIdx;

    private void Start()
    {
        GameServerModel.Instance.LogonGameServerReturn += OnLogonGameServerReturn;
        GameServerModel.Instance.CreateRoleReturn += OnCreateRoleReturn;
        GameServerModel.Instance.ReqLogonGameServer();
    }

    private void OnDestroy()
    {
        GameServerModel.Instance.LogonGameServerReturn -= OnLogonGameServerReturn;
        GameServerModel.Instance.CreateRoleReturn -= OnCreateRoleReturn;
    }

    private void OnLogonGameServerReturn(List<RoleOperation_LogOnGameServerReturnProto.RoleItem> roleItemList)
    {
        LoadRoles();
    }

    private async void LoadRoles()
    {
        var jobList = JobConfig.Instance.List;
        var assetBundleCreateRequestList = new List<AssetBundleCreateRequest>();
        for(int i = 0; i < jobList.Count; ++i)
        {
            assetBundleCreateRequestList.Add(AssetBundle.LoadFromFileAsync($"{ AssetBundlePath.RoleRootPath }{ jobList[i].PrefabName }{ AssetBundlePath.OtherExpandedName }"));
        }
        for(int i = 0; i < assetBundleCreateRequestList.Count; ++i)
        {
            await assetBundleCreateRequestList[i];
        }
        var assetBundleRequestList = new List<AssetBundleRequest>();
        for(int i = 0; i < assetBundleCreateRequestList.Count; ++i)
        {
            assetBundleRequestList.Add(assetBundleCreateRequestList[i].assetBundle.LoadAssetAsync(jobList[i].PrefabName));
        }
        for(int i = 0; i < assetBundleRequestList.Count; ++i)
        {
            await assetBundleRequestList[i];
        }
        for(int i = 0; i < assetBundleRequestList.Count; ++i)
        {
            GameObject prefab = assetBundleRequestList[i].asset as GameObject;
            GameObject obj = Instantiate(prefab, m_RoleContainers[i], false);
            m_JobItemList[i].Init(i, jobList[i].Name, $"UI/UISource/HeadImg/{ jobList[i].HeadPic }");
            m_JobItemList[i].ClickJobItem += OnClickJobItem;
        }
        m_SelectJobIdx = 0;
        m_JobItemList[m_SelectJobIdx].MoveToRight();
        var job = JobConfig.Instance.List[m_SelectJobIdx];
        m_SelectRoleSceneView.SetSelectJobDesc(job.Name, job.Desc);
        m_SelectRoleDragAreaController.EndHorizontalDrag += OnEndHorizontalDrag;
    }

    private void OnClickJobItem(int idx)
    {
        if(m_SelectJobIdx == idx)
        {
            return;
        }
        var targetEulerAngleY = 360 - idx * 90;
        if(targetEulerAngleY == 360)
        {
            targetEulerAngleY = 0;
        }
        m_SelectRoleSceneView.RotateCamera(targetEulerAngleY);
        m_JobItemList[m_SelectJobIdx].MoveToOrigin();
        m_SelectJobIdx = idx;
        m_JobItemList[m_SelectJobIdx].MoveToRight();
        var job = JobConfig.Instance.List[m_SelectJobIdx];
        m_SelectRoleSceneView.SetSelectJobDesc(job.Name, job.Desc);
    }

    private void OnEndHorizontalDrag(UIDirection dir)
    {
        if(m_SelectRoleSceneView.IsRotating)
        {
            return;
        }
        int targetJobIdx;
        if(dir == UIDirection.LEFT)
        {
            targetJobIdx = m_SelectJobIdx + 1;
            if(targetJobIdx > 3)
            {
                targetJobIdx = 0;
            }
            m_SelectRoleSceneView.RotateCamera(UIDirection.RIGHT);

        }
        else if(dir == UIDirection.RIGHT)
        {
            targetJobIdx = m_SelectJobIdx - 1;
            if(targetJobIdx < 0)
            {
                targetJobIdx = 3;
            }
            m_SelectRoleSceneView.RotateCamera(UIDirection.LEFT);
        }
        else
        {
            throw new ArgumentException("参数dir只能是左和右");
        }
        m_JobItemList[m_SelectJobIdx].MoveToOrigin();
        m_SelectJobIdx = targetJobIdx;
        m_JobItemList[m_SelectJobIdx].MoveToRight();
        var job = JobConfig.Instance.List[m_SelectJobIdx];
        m_SelectRoleSceneView.SetSelectJobDesc(job.Name, job.Desc);
    }

    public void OnClickStartBtn()
    {
        GameServerModel.Instance.ReqCreateRole(1, "test1");
    }

    private void OnCreateRoleReturn(int result)
    {
        DebugLogger.LogError("OnCreateRoleReturn:" + result);
    }
}