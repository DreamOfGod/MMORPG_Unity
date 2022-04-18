//===============================================
//作    者：
//创建时间：2022-04-11 21:23:10
//备    注：
//===============================================
using UnityEngine;
using UnityEngine.UI;

public class GameServerItem : MonoBehaviour
{
    [SerializeField]
    private Text m_GameServerName;
    [SerializeField]
    private Image m_Status;
    [SerializeField]
    private Sprite[] m_SpriteStatusArray;

    private GameServerBean m_Data;
    private SelectGameServerController m_SelectGameServerController;

    public void Init(GameServerBean data, SelectGameServerController selectGameServerController)
    {
        m_Data = data;
        m_SelectGameServerController = selectGameServerController;
        m_GameServerName.text = data.Name;
        m_Status.overrideSprite = m_SpriteStatusArray[data.RunStatus];
    }

    public void OnBtnClick()
    {
        m_SelectGameServerController.EnterGameServerController.SetCurSelectGameServer(m_Data);
        m_SelectGameServerController.SelectGameServerWindow.ZoomOutClose();
    }
}