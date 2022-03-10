//===============================================
//作    者：
//创建时间：2022-03-10 15:42:56
//备    注：
//===============================================
using UnityEngine;

public class MonsterCreatePos: MonoBehaviour
{
    [SerializeField]
    private UISceneCityCtrl m_UICtrl;

    private const int MAX_COUNT = 3;
    private int m_CurrCount = 0;
    private float m_NextCreateTime = 0;

    private void Start()
    {
        GameObject monsterPrefab = Resources.Load<GameObject>("RolePrefab/Monster/Role_Monster_1");
        GameObject monster = Instantiate(monsterPrefab);

        monster.transform.position = transform.position;
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
    }

    void Update()
    {
        //if (m_CurrCount < MAX_COUNT)
        //{
        //    if (Time.time >= m_NextCreateTime)
        //    {
        //        m_NextCreateTime = Time.time + Random.Range(1.5f, 3.5f);

        //        GameObject monsterPrefab = Resources.Load<GameObject>("RolePrefab/Monster/Role_Monster_1");
        //        GameObject monster = Instantiate(monsterPrefab);

        //        monster.transform.parent = transform;
        //        monster.transform.position = transform.position;
        //        //Vector3 pos = new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f));
        //        //Debug.Log("1:" + pos);
        //        //pos = transform.TransformPoint(pos);
        //        //Debug.Log("2:" + pos);
        //        //monster.transform.localPosition = new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f));

        //        MonsterCtrl monsterCtrl = monster.GetComponent<MonsterCtrl>();
        //        RoleInfoBase roleInfo = new RoleInfoMonster();
        //        roleInfo.Nickname = "小怪";
        //        roleInfo.MaxHP = 100;
        //        monsterCtrl.Init(roleInfo, new RoleMonsterAI());

        //        m_UICtrl.InitHeadBar(monsterCtrl.HeadBarPos, roleInfo.Nickname, true);



        //        m_CurrCount++;
        //    }
        //}
    }
}
