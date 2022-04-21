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

    private void Start()
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
        }
        m_SelectRoleDragAreaController.EndHorizontalDrag += OnEndHorizontalDrag;
    }

    private void OnEndHorizontalDrag(UIDirection dir)
    {
        if(dir == UIDirection.LEFT)
        {
            m_SelectRoleSceneView.RotateCamera(UIDirection.RIGHT);
        }
        else if(dir == UIDirection.RIGHT)
        {
            m_SelectRoleSceneView.RotateCamera(UIDirection.LEFT);
        }
    }

    private void OnChangeScene()
    {
        m_SelectRoleDragAreaController.EndHorizontalDrag -= OnEndHorizontalDrag;
    }
}
