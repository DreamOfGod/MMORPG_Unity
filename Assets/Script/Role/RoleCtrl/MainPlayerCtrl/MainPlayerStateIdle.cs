//===============================================
//作    者：
//创建时间：2022-03-10 11:17:24
//备    注：
//===============================================

using UnityEngine;
/// <summary>
/// 主角控制器
/// </summary>
public partial class MainPlayerCtrl
{
    /// <summary>
    /// 主角休闲状态
    /// </summary>
    private class MainPlayerStateIdle : StateBase
    {
        private MainPlayerCtrl m_MainPlayerCtrl;

        public MainPlayerStateIdle(MainPlayerCtrl mainPlayerCtrl)
        {
            m_MainPlayerCtrl = mainPlayerCtrl;
        }

        public override void OnEnter()
        {
            m_MainPlayerCtrl.m_Animator.SetBool(AnimStateConditionName.ToIdleNormal, true);

            FingerEvent.Instance.OnFingerUpWithoutDrag += OnPlayerClick;
        }

        public override void OnLeave()
        {
            m_MainPlayerCtrl.m_Animator.SetBool(AnimStateConditionName.ToIdleNormal, false);

            FingerEvent.Instance.OnFingerUpWithoutDrag -= OnPlayerClick;
        }

        #region OnPlayerClickGround 玩家点击屏幕回调
        /// <summary>
        /// 玩家点击屏幕回调
        /// </summary>
        /// <param name="screenPos">屏幕坐标点</param>
        private void OnPlayerClick(Vector2 screenPos)
        {
            Ray ray = Camera.main.ScreenPointToRay(screenPos);
            RaycastHit hitInfo;
            int groundLayer = LayerMask.NameToLayer(LayerName.Ground);
            int monsterLayer = LayerMask.NameToLayer(LayerName.Monster);
            int targetLayerMask = (1 << groundLayer) | (1 << monsterLayer);
            if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, targetLayerMask))
            {
                //点击了怪物或地面
                int colliderLayer = hitInfo.collider.gameObject.layer;
                if (colliderLayer == monsterLayer)
                {
                    //点击了怪物，设置目标怪物，等OnUpdate执行时再处理
                    m_MainPlayerCtrl.m_TargetMonster = hitInfo.collider.GetComponent<MonsterCtrl>();
                }
                else if (colliderLayer == groundLayer)
                {
                    //点击了地面
                    m_MainPlayerCtrl.m_TargetMonster = null;
                    if (Vector3.Distance(hitInfo.point, m_MainPlayerCtrl.transform.position) > 0.1f)
                    {
                        //目标点离当前位置足够远，转入移动状态
                        m_MainPlayerCtrl.ChangeToRunState(hitInfo.point);
                    }
                }
            }
        }
        #endregion

        public override void OnUpdate()
        {
            if (m_MainPlayerCtrl.m_TargetMonster != null && !m_MainPlayerCtrl.m_TargetMonster.isDisState())
            {
                //有目标怪物且怪物未死亡
                float distance = Vector3.Distance(m_MainPlayerCtrl.transform.position, m_MainPlayerCtrl.m_TargetMonster.transform.position);
                if(distance <= m_MainPlayerCtrl.m_AttackDistance)
                {
                    //目标怪物在攻击范围内
                    if(Time.time >= m_MainPlayerCtrl.m_NextAttackTime)
                    {
                        //达到攻击时间，转为攻击状态
                        m_MainPlayerCtrl.ChangeToAttackState();
                    }
                }
                else
                {
                    //不在攻击范围内，跑向目标怪物
                    m_MainPlayerCtrl.ChangeToRunState(m_MainPlayerCtrl.m_TargetMonster.transform.position);
                }
                return;
            }
        }
    }
}