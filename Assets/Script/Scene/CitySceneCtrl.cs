//===============================================
//作    者：
//创建时间：2022-03-09 10:41:22
//备    注：
//===============================================
using UnityEngine;

/// <summary>
/// 主城控制器
/// </summary>
public class CitySceneCtrl : MonoBehaviour
{
    /// <summary>
    /// 主角出生点
    /// </summary>
    [SerializeField]
    private Transform m_PlayerBornPos;

    /// <summary>
    /// UI控制器
    /// </summary>
    [SerializeField]
    private UISceneCityCtrl m_UICtrl;

    /// <summary>
    /// 摄像机控制器
    /// </summary>
    [SerializeField]
    private CameraCtrl m_CameraCtrl;

    /// <summary>
    /// 主角控制器
    /// </summary>
    public MainPlayerCtrl MainPlayerCtrl { get; private set; }

    void Awake()
    {
        LoadMainPlayer();
    }

    /// <summary>
    /// 加载主角
    /// </summary>
    private void LoadMainPlayer() 
    {
        string fullPath = $"{ AssetBundlePath.RoleRootPath }Role_MainPlayer_Cike{ AssetBundlePath.OtherExpandedName }";
        AssetBundleCreateRequest request = AssetBundle.LoadFromFileAsync(fullPath);
        request.completed += (AsyncOperation ao) => {
            GameObject obj = request.assetBundle.LoadAsset<GameObject>("Role_MainPlayer");
            request.assetBundle.Unload(false);
            GameObject mainPlayer = Instantiate(obj);

            Vector3 pos;
            RaycastHit hitInfo;
            if (Physics.Raycast(m_PlayerBornPos.position, Vector3.down, out hitInfo)
                && hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                pos = hitInfo.point;
            }
            else
            {
                Debug.LogError("主角出生点没有位于地面上方");
                pos = m_PlayerBornPos.position;
            }
            mainPlayer.transform.position = pos;

            MainPlayerCtrl = mainPlayer.GetComponent<MainPlayerCtrl>();

            RoleHeadBarCtrl headBarCtrl = m_UICtrl.AddHeadBar(MainPlayerCtrl.HeadBarPos, UserInfo.nickname, false);
            MainPlayerCtrl.SetHeadBarCtrl(headBarCtrl);
            MainPlayerCtrl.SetCityUICtrl(m_UICtrl);

            m_CameraCtrl.SetTarget(mainPlayer.transform);
        };
    }
}