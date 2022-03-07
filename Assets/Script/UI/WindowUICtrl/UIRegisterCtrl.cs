//===============================================
//作    者：
//创建时间：2022-02-24 11:00:18
//备    注：
//===============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIRegisterCtrl : UIWindowBase
{
    [SerializeField]
    private UIInput m_InputNickname;

    [SerializeField]
    private UIInput m_InputPwd1;

    [SerializeField]
    private UIInput m_InputPwd2;

    [SerializeField]
    private UILabel m_LblTip;

    protected override void OnBtnClick(GameObject go)
    {
        switch (go.name)
        {
            case "btnReg":
                BtnRegister();
                break;
            case "btnToLogOn":
                BtnToLogOn();
                break;
        }
    }

    private void BtnRegister()
    {
        string nickname = m_InputNickname.value.Trim();
        if(nickname.Length == 0)
        {
            m_LblTip.text = "请输入昵称";
            return;
        }

        string pwd1 = m_InputPwd1.value.Trim();
        if (pwd1.Length == 0)
        {
            m_LblTip.text = "请输入密码";
            return;
        }

        string pwd2 = m_InputPwd2.value.Trim();
        if (pwd2.Length == 0)
        {
            m_LblTip.text = "请输入确认密码";
            return;
        }

        if(pwd1 != pwd2)
        {
            m_LblTip.text = "两次输入密码不一致";
            return;
        }

        PlayerPrefs.SetString(RegisterLogonKey.MMO_NICKNAME, nickname);
        PlayerPrefs.SetString(RegisterLogonKey.MMO_PWD, pwd1);

        LoadingSceneCtrl.NextScene = SceneName.City;
        SceneManager.LoadScene(SceneName.Loading);
    }

    private void BtnToLogOn()
    {
        TweenScale ts = gameObject.GetComponent<TweenScale>();
        ts.from = Vector2.one;
        ts.to = Vector2.zero;
        ts.duration = 0.2f;
        ts.AddOnFinished(() => {
            Destroy(gameObject);
            OpenLogon();
        });
        ts.PlayForward();
    }

    private void OpenLogon()
    {
        GameObject prefab = Resources.Load("UIPrefab/UIWindows/PanLogOn") as GameObject;
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
