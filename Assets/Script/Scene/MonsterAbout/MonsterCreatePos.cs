//===============================================
//作    者：
//创建时间：2022-03-10 15:42:56
//备    注：
//===============================================
using UnityEngine;

public class MonsterCreatePos: MonoBehaviour
{
    [SerializeField]
    private CitySceneCtrl m_CitySceneCtrl;

    [SerializeField]
    private UISceneCityCtrl m_UICtrl;

    private const int MAX_COUNT = 1;
    private int m_CurrCount = 0;
    private float m_NextCreateTime = 0;
    private GameObject m_MonsterPrefab;

    private void Awake()
    {
        AssetBundle ab = AssetBundle.LoadFromFile(AssetBundlePath.MonsterPath("Monster_1"));
        m_MonsterPrefab = ab.LoadAsset<GameObject>("Monster_1");
        ab.Unload(false);
    }

    void Update()
    {
        if(m_CitySceneCtrl.RoleCtrl == null)
        {
            return;
        }
        if (m_CurrCount < MAX_COUNT)
        {
            if (Time.time >= m_NextCreateTime)
            {
                m_NextCreateTime = Time.time + Random.Range(1.5f, 3.5f);

                GameObject monster = Instantiate(m_MonsterPrefab);

                monster.transform.parent = transform;
                Vector3 pos = transform.TransformPoint(new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f)));
                RaycastHit hitInfo;
                if (Physics.Raycast(pos, Vector3.down, out hitInfo)
                    && hitInfo.collider.gameObject.layer == LayerMask.NameToLayer(LayerName.Ground))
                {
                    pos.y = hitInfo.point.y;
                }
                else
                {
                    Debug.LogError("小怪出生点没有位于地面之上");
                }
                monster.transform.position = pos;

                MonsterCtrl monsterCtrl = monster.GetComponent<MonsterCtrl>();
                monsterCtrl.SetMainPlayerCtrl(m_CitySceneCtrl.RoleCtrl);

                RoleHeadBarCtrl headBarCtrl = m_UICtrl.AddHeadBar(monsterCtrl.HeadBarPos, "小怪", true);
                monsterCtrl.SetHeadBarCtrl(headBarCtrl);

                m_CurrCount++;
            }
        }
    }
}
