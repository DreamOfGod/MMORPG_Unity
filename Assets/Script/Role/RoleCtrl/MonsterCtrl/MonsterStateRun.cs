//===============================================
//作    者：
//创建时间：2022-03-10 14:57:02
//备    注：
//===============================================
using UnityEngine;

/// <summary>
/// 怪物控制器
/// </summary>
public partial class MonsterCtrl
{
    /// <summary>
    /// 怪物跑动状态
    /// </summary>
    private class MonsterStateRun : StateBase
    {
        private MonsterCtrl m_MonsterCtrl;

        public MonsterStateRun(MonsterCtrl monsterCtrl)
        {
            m_MonsterCtrl = monsterCtrl;
        }

        #region OnEnter OnLeave
        public override void OnEnter()
        {
            m_MonsterCtrl.m_Animator.SetBool(AnimStateConditionName.ToRun, true);

            calcuMoveDir();
        }

        public override void OnLeave()
        {
            m_MonsterCtrl.m_Animator.SetBool(AnimStateConditionName.ToRun, false);
        }
        #endregion

        #region calcuMoveDir 计算移动方向
        /// <summary>
        /// 计算移动方向
        /// </summary>
        private void calcuMoveDir()
        {
            //计算移动方向
            m_MonsterCtrl.m_MoveDirection = m_MonsterCtrl.m_MoveTargetPos - m_MonsterCtrl.transform.position;
            m_MonsterCtrl.m_MoveDirection = m_MonsterCtrl.m_MoveDirection.normalized;//归一化
            m_MonsterCtrl.m_MoveDirection.y = 0;

            m_MonsterCtrl.m_BeginQuaternion = m_MonsterCtrl.transform.rotation;

            //计算目标四元数
            //欧拉角旋转产生的万向锁现象：https://www.cnblogs.com/trio/p/13696560.html
            //比如按xyz顺序旋转，如果绕y轴旋转90度，导致z轴转到x轴位置，那么接下来绕z轴旋转实际上和第一次绕x轴旋转，都是同一个轴向
            m_MonsterCtrl.m_TargetQuaternion = Quaternion.LookRotation(m_MonsterCtrl.m_MoveDirection);

            m_MonsterCtrl.m_RotationRatio = 0;
        }
        #endregion

        #region OnUpdate
        public override void OnUpdate()
        {
            if(m_MonsterCtrl.m_MainPlayerCtrl.IsDieState())
            {
                //主角死亡
                if(m_MonsterCtrl.m_LockedEnemy)
                {
                    //已锁定主角，返回出生点
                    m_MonsterCtrl.m_LockedEnemy = false;
                    m_MonsterCtrl.m_MoveTargetPos = m_MonsterCtrl.m_BornPos;
                    calcuMoveDir();
                }
                else
                {
                    //没锁定主角，继续移动
                    Move();
                }
                return;
            }

            float distance = Vector3.Distance(m_MonsterCtrl.m_MainPlayerCtrl.transform.position, m_MonsterCtrl.transform.position);
            if (distance <= m_MonsterCtrl.m_AttackDistance)
            {
                //主角在攻击范围内
                m_MonsterCtrl.m_LockedEnemy = true;
                if (Time.time >= m_MonsterCtrl.m_NextAttackTime)
                {
                    //达到攻击时间，攻击主角
                    m_MonsterCtrl.ChangeToAttackState();
                }
                else
                {
                    //未达到攻击时间，转入待机
                    m_MonsterCtrl.ChangeToIdleState();
                }
                return;
            }
            
            if (distance <= m_MonsterCtrl.m_ViewRadius)
            {
                //主角在视野范围，跑向主角
                m_MonsterCtrl.m_LockedEnemy = true;
                m_MonsterCtrl.m_MoveTargetPos = m_MonsterCtrl.m_MainPlayerCtrl.transform.position;
                calcuMoveDir();
            }

            Move();
        }
        #endregion

        #region Move 移动
        /// <summary>
        /// 移动
        /// </summary>
        private void Move()
        {
            if (m_MonsterCtrl.m_RotationRatio < 1f)
            {
                //转身未完成
                //更新插值比例
                m_MonsterCtrl.m_RotationRatio += m_MonsterCtrl.m_RotationSpeed * Time.deltaTime;
                m_MonsterCtrl.m_RotationRatio = Mathf.Min(m_MonsterCtrl.m_RotationRatio, 1f);
                //对旋转插值
                m_MonsterCtrl.transform.rotation = Quaternion.Lerp(m_MonsterCtrl.m_BeginQuaternion, m_MonsterCtrl.m_TargetQuaternion, m_MonsterCtrl.m_RotationRatio);
            }

            Vector3 offset = m_MonsterCtrl.m_MoveDirection * Time.deltaTime * m_MonsterCtrl.m_MoveSpeed;
            m_MonsterCtrl.m_CharacterController.Move(offset);
            if (Vector3.Distance(m_MonsterCtrl.m_MoveTargetPos, m_MonsterCtrl.transform.position) < 0.1f)
            {
                m_MonsterCtrl.ChangeToIdleState();
            }
        }
        #endregion
    }
}