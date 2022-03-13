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

            FingerEvent.Instance.OnFingerUpWithoutDrag += m_MainPlayerCtrl.OnPlayerClickGround;
        }

        public override void OnLeave()
        {
            m_MainPlayerCtrl.m_Animator.SetBool(AnimStateConditionName.ToRun, false);

            FingerEvent.Instance.OnFingerUpWithoutDrag -= m_MainPlayerCtrl.OnPlayerClickGround;
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

        #region OnUpdate
        public override void OnUpdate()
        {
            if (Vector3.Distance(m_MainPlayerCtrl.m_MoveTargetPos, m_MainPlayerCtrl.transform.position) > 0.1f)
            {
                //让角色转身
                if (m_MainPlayerCtrl.m_RotationRatio < 1f)
                {
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
        }
        #endregion
    }
}