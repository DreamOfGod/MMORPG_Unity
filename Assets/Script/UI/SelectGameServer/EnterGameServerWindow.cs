//===============================================
//作    者：
//创建时间：2022-04-10 22:49:51
//备    注：
//===============================================
using UnityEngine;
using UnityEngine.UI;

public class EnterGameServerWindow : WindowBase
{
    [SerializeField]
    private Text m_TextDefaultGameServer;

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.D))
        {
            PlayerPrefs.DeleteAll();
        }
    }
}
