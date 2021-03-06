﻿using UnityEngine;
using System.Collections;
using ExtensionMethod;
using LitJson;
public class UcSdk : MonoBehaviour,ISdk{

    private const string SDK_JAVA_CLASS = "com.lyh.sdk.uc.UcSdk";

    private string _objName;
    public static UcSdk instance;

    public static UcSdk getInstance()
    {
        return instance;
    }


    void Awake()
    {
        instance = this;
        _objName = gameObject.name = GetType().Name;

        //GAME_OBJECT = gameObject.name;


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
        /***
        string appSecret = "A9B8958B746d4a3d0501Ee0A194fc9fB";
        string appId = "2882303761517531891";
        string appKey = "5361753184891";
        **/
        callSdkApi("ucInit",_objName);

        SDKHandler.instance.InitCallback("Init_Success");
    }

    public void OnCallHello()
    {
        AlertPanel.Show("测试回调！1");
    }

    /// <summary>
    /// 登录
    /// </summary>
    public void login()
    {
        //AlertPanel.Show("回调名字 " + _objName);
      //  callSdkApi("miLogin", _objName);
        //AlertPanel.Show("测试","德国队")
        SDKHandler.instance.LoginCallback(SDKManager.Instance.GetPlatform() + "_"+ SystemInfo.deviceUniqueIdentifier);
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
        //   final String gameObject,final String appName,final String goodsName,final String amount,final String user_id
        string amount = ""+bcData.currency;

        // int amount = 1;
        string userId = "" + Session.GetInstance().GetURLRequestManager().GetMyUid();

        callSdkApi("ucPay", _objName,"qwsk","商品", amount, userId);
    }


    /// <summary>
    /// onResume & onPause（控制浮标的显示和隐藏，需成对调用）
    /// </summary>
    public void onResume()
    {
        // callSdkApi("onResume");
    }

    public void onPause()
    {
        //  callSdkApi("onPause");
    }


    //public void exitSdk()
    //{
    //    callSdkApi("exitSdk", _objName);

    //}
    public void exitSdk()
    {
        callSdkApi("exitSdk", _objName);
        // AlertPanel.Show(Language.GetStr("Public", "quit_game_confirm"), AlertPanel.YES | AlertPanel.NO, OnQuit, null);
    }
    public void OnQuit(AlertCloseEvent evt)
    {
        if (evt.detail == AlertPanel.YES)
        {
            //SDKManager.Instance.Exit();
            Application.Quit();

        }
        else
        {
            //isAlertShow = false;
        }
    }
    /// <summary>
    /// 反射调用androidapi
    /// </summary>
    /// <param name="apiName"></param>
    /// <param name="args"></param>
    private void callSdkApi(string apiName, params object[] args)
    {
        //log("Unity3D " + apiName + " calling...");

        using (AndroidJavaClass cls = new AndroidJavaClass(SDK_JAVA_CLASS))
        {
            //Debuger.Log("AndroidJavaClass11 ["+cls+"]");
            cls.CallStatic(apiName, args);
            //Debuger.Log("CallStatic [" + apiName + "] "+ args);
        }
    }

    public void log(string logContent)
    {
        print(logContent);

    }


    /////////////////////////////////////////回调///////////////////////////////////////
    public void OnInitSuccess(string msg)
    {
        log("初始化成功");
    }
    
    public void OnPayInitSuccess(string msg)
    {
        log("充值初始化成功");
    }
    /// <summary>
    /// 登录成功回调
    /// </summary>
    /// <param name="msg"></param>
    public void OnLoginSuccess(string msg)
    {
        //AlertPanel.Show(msg);
        //Debug.LogError("msg "+ msg);
        JsonData json = JsonMapper.ToObject(msg);

        string uId = json.GetString("uId");

        //("登录成功");
        //  string u_id = Session.GetInstance().myPlayer.uid;
        SDKHandler.instance.LoginCallback(SDKManager.Instance.GetPlatform() + "_" + uId);


        //  callSdkApi("getTokenAndSsoid",GAME_OBJECT);
    }

    /// <summary>
    ///登录失败回调
    /// </summary>
    /// <param name="msg"></param>
    public void OnLoginFail(string msg)
    {
        log("登录失败");
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
        log("充值成功");
    }

    /// <summary>
    /// 支付失败
    /// </summary>
    /// <param name="msg"></param>
    public void OnPayFail(string msg)
    {
        log("充值:" + msg);
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

    /***
    public void getToken(String jsonString)
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
    ***/

}
