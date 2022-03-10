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

    [SerializeField]
    private CameraCtrl m_CameraCtrl;

    void Awake()
    {
        GameObject mainPlayerPrefab = Resources.Load<GameObject>("RolePrefab/Player/Role_MainPlayer");
        GameObject mainPlayer = Instantiate(mainPlayerPrefab);
        mainPlayer.transform.position = m_PlayerBornPos.position;

        MainPlayerCtrl mainPlayerCtrl = mainPlayer.GetComponent<MainPlayerCtrl>();
        RoleInfoMainPlayer roleInfoMainPlayer = new RoleInfoMainPlayer();
        roleInfoMainPlayer.Nickname = UserInfo.nickname;
        mainPlayerCtrl.Init(roleInfoMainPlayer, new RoleMainPlayerCityAI());

        m_CameraCtrl.Init(mainPlayer.transform);
        m_UICtrl.InitHeadBar(mainPlayerCtrl.HeadBarPos, roleInfoMainPlayer.Nickname, false);

        GameObject monsterPrefab = Resources.Load<GameObject>("RolePrefab/Monster/Role_Monster_1");
        GameObject monster = Instantiate(monsterPrefab);

        monster.transform.position = m_PlayerBornPos.position;
        //Vector3 pos = new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f));
        //Debug.Log("1:" + pos);
        //pos = transform.TransformPoint(pos);
        //Debug.Log("2:" + pos);
        //monster.transform.localPosition = new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f));

        MonsterCtrl monsterCtrl = monster.GetComponent<MonsterCtrl>();
        RoleInfoBase roleInfo = new RoleInfoMonster();
        roleInfo.Nickname = "小怪";
        roleInfo.MaxHP = 100;
        monsterCtrl.Init(roleInfo, new RoleMonsterAI());

        m_UICtrl.InitHeadBar(monsterCtrl.HeadBarPos, roleInfo.Nickname, true);

        m_CameraCtrl.Init(monster.transform);
    }
}
