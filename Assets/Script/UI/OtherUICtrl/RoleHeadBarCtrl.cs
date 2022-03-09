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

    private Transform m_NicknameTarget;

    public void Init(Transform nicknameTarget, string nickname)
    {
        m_NicknameTarget = nicknameTarget;
        m_LblNickname.text = nickname;
    }

    void Update()
    {
        Vector3 viewportPos = Camera.main.WorldToViewportPoint(m_NicknameTarget.position);
        Vector3 screenPos = UICamera.mainCamera.ViewportToWorldPoint(viewportPos);
        transform.position = screenPos;
    }

    public void SetHUDText(int hurtVal)
    {
        m_HUDText.Add(string.Format("-{0}", hurtVal), Color.red, 0.1f);
    }
}
