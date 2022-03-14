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
    /// 主角跑动状态
    /// </summary>
    private class MainPlayerStateRun : StateBase
    {
        private MainPlayerCtrl m_MainPlayerCtrl;

        public MainPlayerStateRun(MainPlayerCtrl mainPlayerCtrl)
        {
            m_MainPlayerCtrl = mainPlayerCtrl;
        }

        #region OnEnter OnLeave
        public override void OnEnter()
        {
            m_MainPlayerCtrl.m_Animator.SetBool(AnimStateConditionName.ToRun, true);

            calcuMoveDir();

            FingerEvent.Instance.OnFingerUpWithoutDrag += OnPlayerClick;
        }

        public override void OnLeave()
        {
            m_MainPlayerCtrl.m_Animator.SetBool(AnimStateConditionName.ToRun, false);

            FingerEvent.Instance.OnFingerUpWithoutDrag -= OnPlayerClick;
        }
        #endregion

        #region calcuMoveDir 计算移动方向
        /// <summary>
        /// 计算移动方向
        /// </summary>
        private void calcuMoveDir()
        {
            //计算移动方向
            m_MainPlayerCtrl.m_MoveDirection = m_MainPlayerCtrl.m_MoveTargetPos - m_MainPlayerCtrl.transform.position;
            m_MainPlayerCtrl.m_MoveDirection = m_MainPlayerCtrl.m_MoveDirection.normalized;//归一化
            m_MainPlayerCtrl.m_MoveDirection.y = 0;

            m_MainPlayerCtrl.m_BeginQuaternion = m_MainPlayerCtrl.transform.rotation;

            //计算目标四元数
            //欧拉角旋转产生的万向锁现象：https://www.cnblogs.com/trio/p/13696560.html
            //比如按xyz顺序旋转，如果绕y轴旋转90度，导致z轴转到x轴位置，那么接下来绕z轴旋转实际上和第一次绕x轴旋转，都是同一个轴向
            m_MainPlayerCtrl.m_TargetQuaternion = Quaternion.LookRotation(m_MainPlayerCtrl.m_MoveDirection);

            m_MainPlayerCtrl.m_RotationRatio = 0;
        }
        #endregion

        #region OnPlayerClick 玩家点击屏幕回调
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
            int targetLayer = groundLayer | monsterLayer;
            if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, targetLayer))
            {
                //检测到点击地面或怪物
                int colliderLayer = hitInfo.collider.gameObject.layer;
                if (colliderLayer == monsterLayer)
                {
                    //点击怪物
                    m_MainPlayerCtrl.m_TargetMonster = hitInfo.collider.GetComponent<MonsterCtrl>();
                    float distance = Vector3.Distance(m_MainPlayerCtrl.transform.position, m_MainPlayerCtrl.m_TargetMonster.transform.position);
                    if (distance <= m_MainPlayerCtrl.m_AttackDistance)
                    {
                        //在攻击范围内，转为攻击状态
                        m_MainPlayerCtrl.ChangeToAttackState();
                    }
                    else
                    {
                        //在攻击范围外，跑向目标怪物
                        m_MainPlayerCtrl.m_MoveTargetPos = m_MainPlayerCtrl.m_TargetMonster.transform.position;
                        calcuMoveDir();
                    }
                }
                else if (colliderLayer == groundLayer)
                {
                    //点击地面
                    m_MainPlayerCtrl.m_TargetMonster = null;
                    if (Vector3.Distance(hitInfo.point, m_MainPlayerCtrl.transform.position) > 0.1f)
                    {
                        //目标位置离当前位置足够远
                        m_MainPlayerCtrl.m_MoveTargetPos = hitInfo.point;
                        calcuMoveDir();
                    }
                    else
                    {
                        //目标位置离当前位置太近，转为休闲状态
                        m_MainPlayerCtrl.ChangeToIdleState();
                    }
                }
            }
        }
        #endregion

        #region OnUpdate
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
                    else
                    {
                        //未达到攻击时间，转为待机状态
                        m_MainPlayerCtrl.ChangeToIdleState();
                    }
                }
                else
                {
                    //不在攻击范围内，跑向目标怪物
                    m_MainPlayerCtrl.m_MoveTargetPos = m_MainPlayerCtrl.m_TargetMonster.transform.position;
                    calcuMoveDir();
                    Move();
                }
                return;
            }

            //没有目标怪物或怪物已死亡
            Move();
        }
        #endregion

        #region Move 向目标移动
        /// <summary>
        /// 向目标移动
        /// </summary>
        private void Move()
        {
            if (m_MainPlayerCtrl.m_RotationRatio < 1f)
            {
                //角色未旋转到目标方向
                //更新插值比例
                m_MainPlayerCtrl.m_RotationRatio += m_MainPlayerCtrl.m_RotationSpeed * Time.deltaTime;
                m_MainPlayerCtrl.m_RotationRatio = Mathf.Min(m_MainPlayerCtrl.m_RotationRatio, 1f);
                //对旋转插值
                m_MainPlayerCtrl.transform.rotation = Quaternion.Lerp(m_MainPlayerCtrl.m_BeginQuaternion, m_MainPlayerCtrl.m_TargetQuaternion, m_MainPlayerCtrl.m_RotationRatio);
            }

            Vector3 offset = m_MainPlayerCtrl.m_MoveDirection * Time.deltaTime * m_MainPlayerCtrl.m_MoveSpeed;
            m_MainPlayerCtrl.m_CharacterController.Move(offset);
            if (Vector3.Distance(m_MainPlayerCtrl.m_MoveTargetPos, m_MainPlayerCtrl.transform.position) <= 0.1f)
            {
                m_MainPlayerCtrl.ChangeToIdleState();
            }
        }
        #endregion
    }
}