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
    /// 角色控制器
    /// </summary>
    public RoleCtrl RoleCtrl { get; private set; }

    void Awake()
    {
        LoadRole();

        ShowWall();
    }

    /// <summary>
    /// 加载角色
    /// </summary>
    private async void LoadRole() 
    {
        var abcr =  AssetBundle.LoadFromFileAsync(AssetBundlePath.RolePath("Role_Cike"));
        await abcr;
        var abr = abcr.assetBundle.LoadAssetAsync<GameObject>("Role_Cike");
        await abr;
        abcr.assetBundle.Unload(false);
        GameObject role = Instantiate((GameObject)abr.asset);

        Vector3 pos;
        RaycastHit hitInfo;
        if (Physics.Raycast(m_PlayerBornPos.position, Vector3.down, out hitInfo) && hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            pos = hitInfo.point;
        }
        else
        {
            Debug.LogError("主角出生点没有位于地面上方");
            pos = m_PlayerBornPos.position;
        }
        role.transform.position = pos;

        RoleCtrl = role.GetComponent<RoleCtrl>();

        RoleHeadBarCtrl headBarCtrl = m_UICtrl.AddHeadBar(RoleCtrl.HeadBarPos, UserInfo.nickname, false);
        RoleCtrl.SetHeadBarCtrl(headBarCtrl);
        RoleCtrl.SetCityUICtrl(m_UICtrl);

        FingerEvent.Instance.OnFingerUpWithoutDrag += OnClickScreen;

        m_CameraCtrl.SetTarget(role.transform);
    }

    private void OnClickScreen(Vector2 pos)
    {
        DebugLogger.LogError("OnClickScreen");
        RoleCtrl.OnPlayerClick(pos);

        ShowTargePosFlag(pos);
    }

    private void OnDestroy()
    {
        FingerEvent.Instance.OnFingerUpWithoutDrag -= OnClickScreen;
    }

    #region Debug
    [SerializeField]
    private Transform m_ClickGroundPosFlag;
    [SerializeField]
    private GameObject m_Wall;

    //显示目标位置标记
    private void ShowTargePosFlag(Vector3 screenPos)
    {
        var ray = Camera.main.ScreenPointToRay(screenPos);
        RaycastHit hitInfo;
        var groundLayer = LayerMask.NameToLayer(LayerName.Ground);
        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, 1 << groundLayer))
        {
            m_ClickGroundPosFlag.gameObject.SetActive(true);
            m_ClickGroundPosFlag.position = hitInfo.point;
        }
    }

    //显示墙
    private void ShowWall()
    {
        m_Wall.SetActive(true);
    }
    #endregion
}
