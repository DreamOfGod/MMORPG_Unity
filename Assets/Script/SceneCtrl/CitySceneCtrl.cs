//===============================================
//作    者：
//创建时间：2022-03-09 10:41:22
//备    注：
//===============================================
using UnityEngine;

public class CitySceneCtrl : MonoBehaviour
{
    [SerializeField]
    private Transform m_PlayerBornPos;

    /// <summary>
    /// UI控制器
    /// </summary>
    [SerializeField]
    private UISceneCityCtrl m_UICtrl;

    [SerializeField]
    private CameraCtrl m_CameraCtrl;

    public MainPlayerCtrl MainPlayerCtrl { get; private set; }

    void Awake()
    {
        LoadMainPlayer();
    }

    private void LoadMainPlayer() 
    {
        GameObject mainPlayerPrefab = Resources.Load<GameObject>("RolePrefab/Player/Role_MainPlayer");
        GameObject mainPlayer = Instantiate(mainPlayerPrefab);

        Vector3 pos;
        RaycastHit hitInfo;
        if (Physics.Raycast(m_PlayerBornPos.position, Vector3.down, out hitInfo)
            && hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            pos = hitInfo.point;
        }
        else
        {
            Debug.LogError("主角出生点没有位于地面之上");
            pos = m_PlayerBornPos.position;
        }
        mainPlayer.transform.position = pos;

        MainPlayerCtrl = mainPlayer.GetComponent<MainPlayerCtrl>();

        m_UICtrl.InitHeadBar(MainPlayerCtrl.HeadBarPos, UserInfo.nickname, false);

        m_CameraCtrl.SetTarget(mainPlayer.transform);
    }
}
