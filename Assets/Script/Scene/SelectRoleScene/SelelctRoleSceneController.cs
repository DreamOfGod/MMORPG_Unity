//===============================================
//作    者：
//创建时间：2022-04-20 21:58:50
//备    注：
//===============================================
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

    //当前选择的职业ID
    private int m_SelectJobId;

    private void Start()
    {
        GameServerModel.Instance.LogonGameServerReturn += OnLogonGameServerReturn;
        GameServerModel.Instance.ReqLogonGameServer();
    }

    private void OnDestroy()
    {
        GameServerModel.Instance.LogonGameServerReturn -= OnLogonGameServerReturn;
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
            m_JobItemList[i].Init(jobList[i].Id, jobList[i].Name, jobList[i].JobPic);
            m_JobItemList[i].ClickJobItem += OnClickJobItem;
        }
        m_SelectJobId = jobList[0].Id;
        m_JobItemList[m_SelectJobId - 1].MoveToRight();
        var job = JobConfig.Instance.List[m_SelectJobId - 1];
        m_SelectRoleSceneView.SetSelectJobDesc(job.Name, job.Desc);
        m_SelectRoleDragAreaController.EndHorizontalDrag += OnEndHorizontalDrag;
    }

    private void OnClickJobItem(int jobId)
    {
        if(m_SelectJobId == jobId)
        {
            return;
        }
        var targetEulerAngleY = (jobId - 1) * -90;
        if(targetEulerAngleY <= -180)
        {
            targetEulerAngleY += 360;
        }
        m_SelectRoleSceneView.RotateCamera(targetEulerAngleY);
        m_JobItemList[m_SelectJobId - 1].MoveToOrigin();
        m_SelectJobId = jobId;
        m_JobItemList[m_SelectJobId - 1].MoveToRight();
        var job = JobConfig.Instance.List[m_SelectJobId - 1];
        m_SelectRoleSceneView.SetSelectJobDesc(job.Name, job.Desc);
    }

    private void OnEndHorizontalDrag(UIDirection dir)
    {
        m_SelectRoleSceneView.RotateCamera(dir == UIDirection.LEFT ? UIDirection.RIGHT : UIDirection.LEFT);
    }
}
