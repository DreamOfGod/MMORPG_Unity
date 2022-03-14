//===============================================
//作    者：
//创建时间：2022-03-13 01:19:03
//备    注：
//===============================================
using UnityEngine;

/// <summary>
/// 主角控制器
/// </summary>
public partial class MainPlayerCtrl
{
    /// <summary>
    /// 主角受伤状态
    /// </summary>
    private class MainPlayerStateHurt : StateBase
    {
        private MainPlayerCtrl m_MainPlayerCtrl;

        public MainPlayerStateHurt(MainPlayerCtrl mainPlayerCtrl)
        {
            m_MainPlayerCtrl = mainPlayerCtrl;
        }

        public override void OnEnter()
        {
            m_MainPlayerCtrl.m_Animator.SetBool(AnimStateConditionName.ToHurt, true);

            FingerEvent.Instance.OnFingerUpWithoutDrag += OnPlayerClick;
        }

        public override void OnLeave()
        {
            m_MainPlayerCtrl.m_Animator.SetBool(AnimStateConditionName.ToHurt, false);

            FingerEvent.Instance.OnFingerUpWithoutDrag -= OnPlayerClick;
        }

        #region OnPlayerClickGround 玩家点击屏幕回调
        /// <summary>
        /// 玩家点击屏幕回调
        /// </summary>
        /// <param name="screenPos">屏幕坐标点</param>
        private void OnPlayerClick(Vector2 screenPos)
        {
            if(m_MainPlayerCtrl.HP <= 0)
            {
                return;
            }
            Ray ray = Camera.main.ScreenPointToRay(screenPos);
            RaycastHit hitInfo;
            int groundLayer = LayerMask.NameToLayer(LayerName.Ground);
            int monsterLayer = LayerMask.NameToLayer(LayerName.Monster);
            int targetLayerMask = (1 << groundLayer) | (1 << monsterLayer);
            if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, targetLayerMask))
            {
                //点击了地面或怪物
                int colliderLayer = hitInfo.collider.gameObject.layer;
                if (colliderLayer == monsterLayer)
                {
                    //点击怪物
                    m_MainPlayerCtrl.m_TargetMonster = hitInfo.collider.GetComponent<MonsterCtrl>();
                    float distance = Vector3.Distance(m_MainPlayerCtrl.m_TargetMonster.transform.position, m_MainPlayerCtrl.transform.position);
                    if(distance <= m_MainPlayerCtrl.m_AttackDistance)
                    {
                        //怪物在攻击范围，转为攻击
                        if(Time.time >= m_MainPlayerCtrl.m_NextAttackTime)
                        {
                            //达到攻击时间，转为攻击状态
                            m_MainPlayerCtrl.ChangeToAttackState();
                        }
                    }
                    else
                    {
                        //怪物不在攻击范围，跑向怪物
                        m_MainPlayerCtrl.ChangeToRunState(m_MainPlayerCtrl.m_TargetMonster.transform.position);
                    }
                }
                else if (colliderLayer == groundLayer)
                {
                    //点击地面
                    m_MainPlayerCtrl.m_TargetMonster = null;
                    if (Vector3.Distance(hitInfo.point, m_MainPlayerCtrl.transform.position) > 0.1f)
                    {
                        m_MainPlayerCtrl.ChangeToRunState(hitInfo.point);
                    }
                }
            }
        }
        #endregion

        public override void OnUpdate()
        {
            if(m_MainPlayerCtrl.m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            {
                if(m_MainPlayerCtrl.HP > 0)
                {
                    m_MainPlayerCtrl.ChangeToIdleState();
                }
                else
                {
                    m_MainPlayerCtrl.ChangeToDieState();
                }
            }
        }
    }
}
