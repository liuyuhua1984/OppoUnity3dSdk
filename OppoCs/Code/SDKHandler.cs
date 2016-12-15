using UnityEngine;
using System.Collections;

public class SDKHandler : MonoBehaviour
{
    public static SDKHandler instance = null;
    void Awake()
    {
        instance = this;
        
    }

    public void InitCallback(string result)
    {
        Debug.LogError("InitCallback  CALL   " + result);
        if (result == "Init_Success")
        {
            SDKManager.INIT_COMPELTE = true;

            GameObject loginPanel = GameObject.Find("LoginPanel");
            LoginPanel c = loginPanel.GetComponent<LoginPanel>();

            if (c == null) return;
            GameObject loginButton = loginPanel.transform.FindChild("LoginButton").gameObject;
            loginButton.SetActive(true);
        }
    }

    public void LoginCallback(string result)
    {
        Debug.LogError("LoginCallback " + result);
        if (result == "onCancel")
        {

        }
        else if (result == "error")
        {

        }
        else
        {
#if R2
                    R2SDK.LoginHelp(result);
                    GameObject.Find("LoginPanel").GetComponent<LoginPanel>().Login(SDKManager.Instance.GetPlatform()+"_"+ result) ;
#elif shinezone || qhjgoogle
                    GameObject.Find("LoginPanel").GetComponent<LoginPanel>().Login(SDKManager.Instance.GetPlatform() + "_" + result);
#else
            GameObject.Find("LoginPanel").GetComponent<LoginPanel>().Login(result);
#endif
        }
    }

    public void PayCallback(string context)
    {
        Debug.LogError("PayCallback " + context);

#if R2
        Session.GetInstance().GetURLRequestManager().OnSynPlyerD();

#elif shinezone
        Session.GetInstance().GetURLRequestManager().OnXZPay(context);
#else

#endif

		Session.GetInstance().GetURLRequestManager().VIPInfo(null);
    }


}
