using UnityEngine;

public class SDKManager
{

    public static bool INIT_COMPELTE = false;

    public static SDKManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new SDKManager();
                SDKManager.Instance.Init();
            }
            return _instance;
        }
    }
    private static SDKManager _instance;
    public ISdk mGameSdk = null;
    private bool isSdk = true;
    public SDKManager()
    {
        //var t1 = "{\"sss\":\"dd\",\"uId\":999999232}";
        //var dd1 = JsonMapper.ToObject(t1);
        //Debug.LogError(dd1.GetLong("uId"));

        GameObject sdkObj = new GameObject("SDKHandler");
        SDKHandler sdkhandler = DllManager.instance.AddCompotent<SDKHandler>(sdkObj, "SDKHandler");
        Debug.LogError("sdkHandler == NULL   " + (sdkObj == null));
        
        GameObject.DontDestroyOnLoad(sdkObj);
        GameObject obj1 = new GameObject("MiSdk");
        GameObject.DontDestroyOnLoad(obj1);


#if R2
        
     
#elif shinezone
     
#elif qhjgoogle

#elif oppo
        Debug.LogError("sdkObj == NULL   " + (sdkObj == null));

        mGameSdk = DllManager.instance.AddCompotent<OppoSdk>(obj1, "OppoSdk");
        Debug.LogError("mGameSdk == NULL   " + (mGameSdk == null));

        //mGameSdk = OppoSdk.getInstance();
#elif mi
        Debug.LogError("sdkObj == NULL   " + (sdkObj == null));
        
        mGameSdk = DllManager.instance.AddCompotent<MiSdk>(obj1, "MiSdk");
        Debug.LogError("mGameSdk == NULL   " + (mGameSdk == null));

#elif uc
        Debug.LogError("sdkObj == NULL   " + (sdkObj == null));
        
        mGameSdk = DllManager.instance.AddCompotent<UcSdk>(obj1, "UcSdk");
        Debug.LogError("mGameSdk == NULL   " + (mGameSdk == null));
#else


        isSdk = false;
          mGameSdk = DllManager.instance.AddCompotent<NoSdk>(obj1, "NoSdk");
        //  mGameSdk = DllManager.instance.AddCompotent<MiSdk>(sdkObj, "MiSdk");
#endif

    }
    public string GetPlatform()
    {

#if R2
        
                return "R2";
#elif shinezone
        return "shinezone";
#elif qhjgoogle
		return "qhjgoogle";
#elif oppo
		return "oppo";
#elif mi
		return "mi";
#else
        return "qhsk";
#endif
    }

    public bool IsSDKLogin()
    {
//#if R2 || shinezone || qhjgoogle
//        return true;
//#else
//        return false;
//#endif
        return isSdk;
    }

    public void Init()
    {
        /***
        INIT_COMPELTE = false;
#if R2
            R2SDK.InitSDK();
#elif shinezone || qhjgoogle 
            FacebookSDK.Init();
#else

#endif
***/
        mGameSdk.init();
    }

    public void Login()
    {
#if UNITY_EDITOR
        SDKHandler.instance.LoginCallback("NoSdk" + "_" + SystemInfo.deviceUniqueIdentifier);
        return;
#endif
        /***
#if R2
            R2SDK.LoginSDK();
#elif shinezone || qhjgoogle
            FacebookSDK.Login();
#else

#endif**/
        mGameSdk.login();
    }

   
    public void CheckPay()
    {
        #if R2
        
		#elif shinezone || qhjgoogle
                FacebookSDK.CheckPay();
        #else

        #endif
    }

    public void Pay(BuyCoinData data)
    {

        mGameSdk.pay(data);

 
/***
#if R2
            R2SDK.PaySDK(data.id+"", Player.serverID, Session.GetInstance().myPlayer.uid, Session.GetInstance().GetURLRequestManager().GetMyUid()+"",data.token, data.currency);
#elif shinezone || qhjgoogle
            FacebookSDK.Pay(data.code, data.currency);
		StaticsManager.GetInstance().RequestPayment(data.code, data.coin, data.currency);
#else

#endif
**/
    }




    public void Exit()
    {
        /***
        #if R2
                      R2SDK.LoginoutHelp();
		#elif shinezone || qhjgoogle
                      FacebookSDK.CallFBLogout();
        #endif
        **/
        Debug.LogError("game Exit ...");
        mGameSdk.exitSdk();
    }



    public void RegisterOK()
    {
        #if R2

		#elif shinezone || qhjgoogle
                FacebookSDK.RegisterOK();
        #endif
    }

    public void TutorialOK()
    {
        #if R2

		#elif shinezone || qhjgoogle
                FacebookSDK.TutorialOK();
        #endif
    }

    public void AchievementOK()
    {
        #if R2

		#elif shinezone || qhjgoogle
                FacebookSDK.AchievementOK();
        #endif
    }


    public void LevelUp(int level)
    {
        #if R2

		#elif shinezone || qhjgoogle
                FacebookSDK.LevelUp(level);
        #endif
    }

    /// <summary>
    /// 
    /// 重新进入游戏
    /// </summary>
    public void onResume()
    {
        mGameSdk.onResume();
    }

    public void onPause()
    {
        mGameSdk.onPause();
    }

}
