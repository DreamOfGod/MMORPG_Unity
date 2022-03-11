//===============================================
//作    者：
//创建时间：2022-03-10 11:17:24
//备    注：
//===============================================
using UnityEngine;

public class MainPlayerStateIdle : StateBase
{
    private MainPlayerCtrl m_MainPlayerCtrl;

    public MainPlayerStateIdle(MainPlayerCtrl mainPlayerCtrl)
    {
        m_MainPlayerCtrl = mainPlayerCtrl;
    }

    public override void OnEnter()
    {
        m_MainPlayerCtrl.Animator.SetBool(AnimStateConditionName.ToIdleFight, true);

        FingerEvent.Instance.OnFingerUpWithoutDrag += OnPlayerClickGround;
    }

    public override void OnLeave()
    {
        m_MainPlayerCtrl.Animator.SetBool(AnimStateConditionName.ToIdleFight, false);

        FingerEvent.Instance.OnFingerUpWithoutDrag -= OnPlayerClickGround;
    }

    void OnPlayerClickGround(Vector2 screenPos)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPos);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            //点击地面
            if (hitInfo.collider.gameObject.layer == LayerMask.NameToLayer(LayerName.Ground))
            {
                if (Vector3.Distance(hitInfo.point, m_MainPlayerCtrl.transform.position) > 0.1f)
                {
                    m_MainPlayerCtrl.MoveTargetPos = hitInfo.point;
                    m_MainPlayerCtrl.ChangeToRunState();
                }
            }
        }
    }
}
