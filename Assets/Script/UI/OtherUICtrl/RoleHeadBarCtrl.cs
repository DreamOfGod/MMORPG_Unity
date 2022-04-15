//===============================================
//作    者：
//创建时间：2022-03-09 17:07:46
//备    注：
//===============================================
using UnityEngine;

public class RoleHeadBarCtrl : MonoBehaviour
{
    [SerializeField]
    private UILabel m_LblNickname;

    [SerializeField]
    private HUDText m_HUDText;

    [SerializeField]
    private UIProgressBar m_HPProgress;

    private Transform m_NicknameTarget;

    public void SetFollowTarget(Transform nicknameTarget)
    {
        m_NicknameTarget = nicknameTarget;
    }

    public void SetNickname(string nickname)
    {
        m_LblNickname.text = nickname;
    }

    public void SetHPBarVisible(bool visible)
    {
        m_HPProgress.gameObject.SetActive(visible);
    }

    void Update()
    {
        Vector3 viewportPos = Camera.main.WorldToViewportPoint(m_NicknameTarget.position);
        Vector3 screenPos = UICamera.mainCamera.ViewportToWorldPoint(viewportPos);
        transform.position = screenPos;
    }

    public void Hurt(int hurtVal, float HPPercent)
    {
        m_HUDText.Add($"-{ hurtVal }", Color.red, 0.1f);
        m_HPProgress.value = HPPercent;

    }
}
