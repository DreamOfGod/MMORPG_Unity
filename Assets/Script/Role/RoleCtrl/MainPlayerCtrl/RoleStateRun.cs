//===============================================
//作    者：
//创建时间：2022-03-10 11:17:24
//备    注：
//===============================================
using Pathfinding;
using UnityEngine;

public partial class RoleCtrl
{
    // 主角跑动状态
    private class RoleStateRun : StateBase
    {
        private RoleCtrl m_RoleCtrl;
        private CapsuleCollider m_Collider;
        //地面法向量
        private Vector3 m_GroundNormal;
        //地面所在层的掩码
        private readonly int m_GroundMask;
        //碰撞信息
        private RaycastHit m_HitInfo;

        public RoleStateRun(RoleCtrl roleCtrl, CapsuleCollider collider)
        {
            m_RoleCtrl = roleCtrl;
            m_Collider = collider;
            m_GroundMask = 1 << (LayerMask.NameToLayer(LayerName.Ground));
            if (Physics.Raycast(m_RoleCtrl.transform.position, Vector3.down, out m_HitInfo, Mathf.Infinity, m_GroundMask))
            {
                m_GroundNormal = m_HitInfo.normal;
            }
            else
            {
                DebugLogger.LogError("没检测到地面");
                m_GroundNormal = Vector3.up;
            }
        }

        #region OnEnter OnLeave
        public override void OnEnter()
        {
            m_RoleCtrl.m_Animator.SetBool(AnimStateConditionName.ToRun, true);
            calcuMoveDir();
        }

        public override void OnLeave()
        {
            m_RoleCtrl.m_Animator.SetBool(AnimStateConditionName.ToRun, false);
        }
        #endregion

        #region calcuMoveDir 计算移动方向
        /// <summary>
        /// 计算移动方向
        /// </summary>
        private void calcuMoveDir()
        {
            //移动方向
            var targetPos = m_RoleCtrl.m_TargetPos;
            m_RoleCtrl.m_MoveDirection = targetPos - m_RoleCtrl.transform.position;
            m_RoleCtrl.m_MoveDirection.y = 0;
            m_RoleCtrl.m_MoveDirection = m_RoleCtrl.m_MoveDirection.normalized;//归一化
            //四元数
            m_RoleCtrl.m_BeginQuaternion = m_RoleCtrl.transform.rotation;
            //欧拉角旋转产生的万向锁现象：https://www.cnblogs.com/trio/p/13696560.html
            //比如按xyz顺序旋转，如果绕y轴旋转90度，导致z轴转到x轴位置，那么接下来绕z轴旋转实际上和第一次绕x轴旋转，都是同一个轴向
            m_RoleCtrl.m_TargetQuaternion = Quaternion.LookRotation(m_RoleCtrl.m_MoveDirection);
            m_RoleCtrl.m_RotationRatio = 0;
        }
        #endregion

        #region OnUpdate
        public override void OnUpdate()
        {
            if (m_RoleCtrl.m_TargetMonster != null && !m_RoleCtrl.m_TargetMonster.isDieState())
            {
                //有目标怪物且怪物未死亡
                float distance = Vector3.Distance(m_RoleCtrl.transform.position, m_RoleCtrl.m_TargetMonster.transform.position);
                if(distance <= m_RoleCtrl.m_AttackDistance)
                {
                    //目标怪物在攻击范围内
                    if(Time.time >= m_RoleCtrl.m_NextAttackTime)
                    {
                        //达到攻击时间，转为攻击状态
                        m_RoleCtrl.ChangeToAttackState();
                    }
                    else
                    {
                        //未达到攻击时间，转为待机状态
                        m_RoleCtrl.ChangeToIdleState();
                    }
                }
                else
                {
                    //不在攻击范围内，跑向目标怪物
                    m_RoleCtrl.m_Seeker.StartPath(m_RoleCtrl.transform.position, m_RoleCtrl.m_TargetMonster.transform.position, (Path path) => {
                        if (path.error)
                        {
                            DebugLogger.LogError($"寻路出错，error：{ path.errorLog }");
                        }
                        else
                        {
                            m_RoleCtrl.m_AStarPath = path as ABPath;
                            var originalEndPoint = m_RoleCtrl.m_AStarPath.originalEndPoint;
                            if (Vector3.Distance(m_RoleCtrl.m_AStarPath.endPoint, new Vector3(originalEndPoint.x, m_RoleCtrl.m_AStarPath.endPoint.y, originalEndPoint.z)) > 0.5f)
                            {
                                DebugLogger.LogWarning("A星计算出来的目标点与原始目标点距离太远，忽略本次点击");
                                return;
                            }
                            m_RoleCtrl.m_AStarPathIdx = 1;
                            calcuMoveDir();
                            Move();
                        }
                    });
                }
                return;
            }

            //没有目标怪物或怪物已死亡，继续移动
            Move();
        }
        #endregion

        #region Move 移动
        private void Move()
        {
            if (m_RoleCtrl.m_RotationRatio < 1f)//角色未旋转到目标方向
            {
                //更新插值比例
                m_RoleCtrl.m_RotationRatio += m_RoleCtrl.m_RotationSpeed * Time.deltaTime;
                m_RoleCtrl.m_RotationRatio = Mathf.Min(m_RoleCtrl.m_RotationRatio, 1f);
                //对旋转插值
                m_RoleCtrl.transform.rotation = Quaternion.Lerp(m_RoleCtrl.m_BeginQuaternion, m_RoleCtrl.m_TargetQuaternion, m_RoleCtrl.m_RotationRatio);
            }

            Debug.DrawLine(m_RoleCtrl.transform.position, m_RoleCtrl.transform.position + m_GroundNormal, Color.black, Mathf.Infinity);

            //计算方向
            var targetPos = m_RoleCtrl.m_TargetPos;
            m_RoleCtrl.m_MoveDirection = targetPos - m_RoleCtrl.transform.position;
            Debug.DrawLine(m_RoleCtrl.transform.position, targetPos, Color.blue, 2f);
            m_RoleCtrl.m_MoveDirection = Vector3.ProjectOnPlane(m_RoleCtrl.m_MoveDirection, m_GroundNormal);//方向向量投影到地面上
            m_RoleCtrl.m_MoveDirection = m_RoleCtrl.m_MoveDirection.normalized;//归一化
            //计算位移
            var offset = m_RoleCtrl.m_MoveDirection * Time.deltaTime * m_RoleCtrl.m_MoveSpeed;
            m_RoleCtrl.transform.Translate(offset, Space.World);
            //检测重叠
            var point1 = m_RoleCtrl.transform.position + 0.5f * m_GroundNormal;
            var point0 = point1 + m_GroundNormal;
            var collider = Physics.OverlapCapsule(point0, point1, 0.5f, m_GroundMask);
            for (int i = 0; i < collider.Length; ++i)
            {
                if (collider[i].transform != m_Collider.transform)
                {
                    Vector3 dir;
                    float dis;
                    if (Physics.ComputePenetration(m_Collider, m_Collider.transform.position, m_Collider.transform.rotation, collider[i], collider[i].transform.position, collider[i].transform.rotation, out dir, out dis))
                    {
                        m_Collider.transform.Translate(dis * dir, Space.World);
                    }
                }
            }
            //更新地面法线
            if (Physics.Raycast(m_RoleCtrl.transform.position, Vector3.down, out m_HitInfo, Mathf.Infinity, m_GroundMask))
            {
                m_GroundNormal = m_HitInfo.normal;
                if(m_HitInfo.distance > 0.5)
                {
                    //m_RoleCtrl.transform.Translate((m_HitInfo.distance - 0.5f) * Vector3.down);
                }
            }
            else
            {
                DebugLogger.LogError("没检测到地面");
                m_GroundNormal = Vector3.up;
            }

            if (Vector3.Distance(new Vector3(targetPos.x, m_RoleCtrl.transform.position.y, targetPos.z), m_RoleCtrl.transform.position) <= 0.1f)
            {
                //if(++m_RoleCtrl.m_AStarPathIdx >= m_RoleCtrl.m_AStarPath.vectorPath.Count)
                //{
                //    m_RoleCtrl.m_AStarPath = null;
                //    m_RoleCtrl.ChangeToIdleState();
                //}
                m_RoleCtrl.ChangeToIdleState();
            }
        }
        #endregion
    }
}