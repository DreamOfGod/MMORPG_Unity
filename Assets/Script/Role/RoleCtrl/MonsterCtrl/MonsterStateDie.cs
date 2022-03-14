//===============================================
//作    者：
//创建时间：2022-03-14 11:25:21
//备    注：
//===============================================
using System.Collections;
using UnityEngine;

public partial class MonsterCtrl
{
    /// <summary>
    /// 死亡状态
    /// </summary>
    private class MonsterStateDie : StateBase
    {
        private MonsterCtrl m_MonsterCtrl;

        public MonsterStateDie(MonsterCtrl monsterCtrl)
        {
            m_MonsterCtrl = monsterCtrl;
        }

        public override void OnEnter()
        {
            m_MonsterCtrl.m_Animator.SetBool(AnimStateConditionName.ToDie, true);

            m_MonsterCtrl.m_HeadBarCtrl.gameObject.SetActive(false);
            m_MonsterCtrl.StartCoroutine(DestroyMonster());
        }

        /// <summary>
        /// 延迟销毁怪物
        /// </summary>
        /// <returns></returns>
        private IEnumerator DestroyMonster()
        {
            yield return new WaitForSeconds(5f);
            Destroy(m_MonsterCtrl.gameObject);
            Destroy(m_MonsterCtrl.m_HeadBarCtrl.gameObject);
        }

        public override void OnLeave()
        {
            m_MonsterCtrl.m_Animator.SetBool(AnimStateConditionName.ToDie, false);
        }
    }
}
