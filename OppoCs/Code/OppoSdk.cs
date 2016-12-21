using UnityEngine;
using System.Collections;
using System;
using LitJson;



/// <summary>
/// opposdk接口类
/// </summary>
public class OppoSdk : MonoBehaviour ,ISdk {
    private const string SDK_JAVA_CLASS = "com.lyh.oppo.sdk.OppoSdk";
    private static string GAME_OBJECT =  "MainCamera";

    public static OppoSdk instance;

    public static OppoSdk getInstance()
    {
        return instance;
    }

    void Awake()
    {
        instance = this;
        GAME_OBJECT = GetType().Name;


    }
    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    /// <summary>
    /// oppo初始化
    /// </summary>
    /// <param name="appSecret"></param>
    public void init()
    {
        string appSecret = "A9B8958B746d4a3d0501Ee0A194fc9fB";
        callSdkApi("init", appSecret);

        SDKHandler.instance.InitCallback("Init_Success");
    }

    /// <summary>
    /// 登录
    /// </summary>
    public void login()
    {

        // callSdkApi("login", GAME_OBJECT);

        SDKHandler.instance.LoginCallback(SDKManager.Instance.GetPlatform() + "_"+
            SystemInfo.deviceUniqueIdentifier);
    }

    /// <summary>
    /// (支付)
    /// </summary>
    /// <param name="gameObject"></param>
    /// <param name="amount"></param>金额(分)
    /// <param name="goodsName"></param>(商品名称)
    /// <param name="userId"></param>
    /// <param name="callback"></param>(服务端回调地址)
    public void pay(BuyCoinData bcData)
    {
        //Debug.LogError("oppo充10001");

        int amount = (int)bcData.currency * 100;//分
        //amount = amount/100;
        //string userId = Session.GetInstance().myPlayer.uid;
        string userId = ObjUtil.GetUrlRequestManager().userId.ToString();
        //string callback = "http://218.244.129.189/qwsk1/index.php?m=opposdk&a=oppo_pay";
        string callback = "http://218.244.129.189/qwsk1/index.php?m=sdk&a=oppo_pay";

        callSdkApi("pay", GAME_OBJECT, amount, "充值", "描述", userId, callback);
        //Debug.LogError("oppo充10002");

    }
   
    /// <summary>
    /// onResume & onPause（控制浮标的显示和隐藏，需成对调用）
    /// </summary>
    public void onResume()
    {
        callSdkApi("onResume");
    }

    public void onPause()
    {
        callSdkApi("onPause");
    }


    public void exitSdk()
    {
        try
        {
            callSdkApi("exitSdk", GAME_OBJECT);

        }
        catch (Exception)
        {
            Application.Quit();
            throw;
        }
        
    }
    /// <summary>
    /// 反射调用androidapi
    /// </summary>
    /// <param name="apiName"></param>
    /// <param name="args"></param>
    private void callSdkApi(string apiName, params object[] args)
    {
        log("Unity3D " + apiName + " calling...");

        using (AndroidJavaClass cls = new AndroidJavaClass(SDK_JAVA_CLASS))
        {
            Debuger.Log("AndroidJavaClass11 ["+cls+"]");
            cls.CallStatic(apiName, args);
            Debuger.Log("CallStatic [" + apiName + "] "+ args);
        }
    }

    public void log(string logContent)
    {
        print(logContent);

    }


    /////////////////////////////////////////回调///////////////////////////////////////
    /// <summary>
    /// 登录成功回调
    /// </summary>
    /// <param name="msg"></param>
    public void OnLoginSuccess(string msg)
    {
        log("登录成功");
        callSdkApi("getTokenAndSsoid",GAME_OBJECT);
    }

    /// <summary>
    ///登录失败回调
    /// </summary>
    /// <param name="msg"></param>
    public void OnLoginFail(string msg)
    {

    }



    /// <summary>
    /// 支付成功
    /// </summary>
    /// <param name="msg"></param>
    public void OnPaySuccess(string msg)
    {
        var t = ObjUtil.GetUrlRequestManager();
        t.VIPInfo(null);
        t.OnSynPlyerD();
        //var t = "";
        print("充值成功");
    }
    /// <summary>
    /// 支付失败
    /// </summary>
    /// <param name="msg"></param>
    public void OnPayFail(string msg)
    {

    }

    /// <summary>
    /// 支付取消
    /// </summary>
    /// <param name="msg"></param>
    public void OnPayCancel(string msg)
    {

    }


    public void OnExitGame(string msg)
    {
        log("退出成功:" + msg);
        Application.Quit();
    }

    public void getToken(string jsonString)
    {
        log("获取token:"+ jsonString);
        JsonData json = JsonMapper.ToObject(jsonString);
        string token = (string)json["token"];
        string ssoid = (string)json["ssoid"];

        Session.GetInstance().GetURLRequestManager().VerifyToken(WWW.EscapeURL(token), WWW.EscapeURL(ssoid), OnLoginCallBack);


    }

    public void OnLoginCallBack(bool callBack)
    {
        if (callBack)
        {
            log("登录回调成功");
            string u_id = Session.GetInstance().myPlayer.uid;
            SDKHandler.instance.LoginCallback(u_id);
        }
    }

 

}
