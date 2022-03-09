//===============================================
//作    者：
//创建时间：2022-03-09 10:41:22
//备    注：
//===============================================
using UnityEngine;

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

    void Awake()
    {
        GameObject mainPlayerPrefab = Resources.Load<GameObject>("RolePrefab/Player/Role_MainPlayer");
        GameObject mainPlayerObj = Instantiate(mainPlayerPrefab);
        mainPlayerObj.transform.position = m_PlayerBornPos.position;

        RoleCtrl roleCtrl = mainPlayerObj.GetComponent<RoleCtrl>();
        RoleInfoMainPlayer roleInfoMainPlayer = new RoleInfoMainPlayer();
        roleInfoMainPlayer.Nickname = UserInfo.nickname;
        roleCtrl.Init(roleInfoMainPlayer, new RoleMainPlayerCityAI());

        m_UICtrl.InitHeadBar(roleCtrl.HeadBarPos, roleInfoMainPlayer.Nickname);
    }
}
