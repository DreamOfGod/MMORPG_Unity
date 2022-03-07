//===============================================
//作    者：
//创建时间：2022-02-24 10:59:37
//备    注：
//===============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UILogOnCtrl : UIWindowBase
{
    [SerializeField]
    private UIInput m_InputNickname;

    [SerializeField]
    private UIInput m_InputPwd;

    [SerializeField]
    private UILabel m_LblTip;

    protected override void OnBtnClick(GameObject go)
    {
        switch(go.name)
        {
            case "btnLogOn":
                BtnLogOn();
                break;
            case "btnToReg":
                BtnToReg();
                break;
        }
    }

    private void BtnLogOn()
    {
        string nickname = m_InputNickname.value.Trim();
        if(nickname.Length == 0)
        {
            m_LblTip.text = "请输入账号";
            return;
        }

        string pwd = m_InputPwd.value.Trim();
        if (pwd.Length == 0)
        {
            m_LblTip.text = "请输入密码";
            return;
        }

        string oldNickname = PlayerPrefs.GetString(RegisterLogonKey.MMO_NICKNAME);
        string oldPwd = PlayerPrefs.GetString(RegisterLogonKey.MMO_PWD);
        if (nickname != oldNickname || pwd != oldPwd)
        {
            m_LblTip.text = "账号或密码错误";
            return;
        }

        LoadingSceneCtrl.NextScene = SceneName.City;
        SceneManager.LoadScene(SceneName.Loading);
    }

    private void BtnToReg()
    {
        TweenScale ts = gameObject.GetComponent<TweenScale>();
        ts.from = Vector2.one;
        ts.to = Vector2.zero;
        ts.duration = 0.2f;
        ts.AddOnFinished(() => {
            Destroy(gameObject);
            OpenRegister();
        });
        ts.PlayForward();
    }

    private void OpenRegister()
    {
        GameObject prefab = Resources.Load("UIPrefab/UIWindows/PanReg") as GameObject;
        GameObject obj = Instantiate(prefab);

        Transform trans = obj.GetComponent<Transform>();
        trans.parent = gameObject.GetComponent<Transform>().parent;
        trans.localPosition = Vector2.zero;

        TweenScale ts = obj.AddComponent<TweenScale>();
        ts.from = Vector2.zero;
        ts.to = Vector2.one;
        ts.duration = 0.2f;
        ts.PlayForward();
    }
}
