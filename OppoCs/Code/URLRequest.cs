using UnityEngine;
using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;

public class URLRequest : MonoBehaviour
{


    public enum Method
    {
        GET,
        POST,
    }

    public string url;
    public Method method;
    public WWWForm form;
    public bool useBackground;
    public string errorPostfix = "";
    private URLRequestCallBackDelegate callBack;
    private URLRequestJsonCallBackDelegate jsonCallBack;
    private URLRequestStreamCallBackDelegate streamCallBack;
    private URLRequestAssetBundlesCallBackDelegate assetBundleCallBack;

    private AlertPanel alertPanel;
    private float startTime;
    private bool isInBackground;
    private float reconnectDelay = 15f;
    private bool requestOnce;
	private int tryCount;
	private const int TRY_COUNT_MAX = 3;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    IEnumerator Start()
    {
		tryCount++;
        startTime = GameTime.time;

        WWW www = (form == null ? new WWW(url) : new WWW(url, form));
        yield return www;

        //CancelShowPendingLog(www.url);

        string data = null;
        if (www.error != null)
        {
            Debuger.LogError("[GetUrlResult]"+ url + ", WWW error : " + www.error);
            data = www.error;
        }
        else
        {
            Debuger.LogWarning("[GetUrlResult]" + url + ", WWW text : " + www.text);
            data = www.text;
        }

        if (www.error == null && CheckResult(data))
        {
            DealCallBack(www);

            callBack = null;
            jsonCallBack = null;
            streamCallBack = null;
            assetBundleCallBack = null;
            if (alertPanel != null)
                Destroy(alertPanel.gameObject);
            Destroy(this.gameObject);
        }
        else
        {
            if (Session.GetInstance().IsInBattle() && streamCallBack != null)
            {
                DealCallBack(www);
                Destroy(gameObject);
            }
            else
            {

                if (requestOnce)
                {
                    DealCallBack(www);
                    Destroy(this.gameObject);
                }

                if (alertPanel == null)
                {
                    if (isInBackground)
                        OnErrorAlertYes(null);
                    else if (useBackground)
                    {

                    }
                    else
                    {
                        //showLoading = true;
                        if (AnnouncePanel.currentPanel != null)
                            AnnouncePanel.currentPanel.Close();

						uint btns = tryCount > TRY_COUNT_MAX ? AlertPanel.CANCEL : AlertPanel.OK | AlertPanel.CANCEL;

                        if (Language.IsLanguageInit())
						{
							alertPanel = AlertPanel.Show(Language.GetStr("Error", "e" + 1), btns, OnErrorAlertYes);
							alertPanel.cancelBtnText.text = Language.GetStr("Public", "give_up");
						}
                        else
						{
                            if (Language.lan == Language.Enabled.Chinese.ToString())
                            {
                                alertPanel = AlertPanel.Show("<color=orange>网络连接出现问题</color>\n点击确认重试.", btns, OnErrorAlertYes);
                                alertPanel.cancelBtnText.text = "取消";
                                alertPanel.yesBtnText.text = "确定";
                            }
                            else if (Language.lan == Language.Enabled.ChineseTraditional.ToString())
                            {
                                alertPanel = AlertPanel.Show("<color=orange>網路連接出現問題</color>\n點擊確認重試.", btns, OnErrorAlertYes);
                                alertPanel.cancelBtnText.text = "取消";
                                alertPanel.yesBtnText.text = "確認";
                            }
                            else
                            {
                                alertPanel = AlertPanel.Show("<color=orange>Network issues </color>\nPlease try reconnection", btns, OnErrorAlertYes);
                                alertPanel.cancelBtnText.text = "Cancel";
                                alertPanel.yesBtnText.text = "Yes";
                            }
                        }
                    }

                }
            }
        }

		www.Dispose();
    }

    private void DealCallBack(WWW www)
    {
        if (callBack != null)
        {
            Debuger.Log("RecieveData : " + www.text);
            callBack(www.text);
        }
        if (jsonCallBack != null)
        {
            PendingPanel.Hide(this);
            jsonCallBack(ParseRecieveData(www.text));
        }
        if (streamCallBack != null)
        {
            if (www.bytes != null && www.bytes.Length > 0)
                streamCallBack(www.bytes);
            else
                streamCallBack(null);
        }
        if (assetBundleCallBack != null)
        {
            assetBundleCallBack(www);
        }
        return;
        try
        {
            if (callBack != null)
            {
                Debuger.Log("RecieveData : " + www.text);
                callBack(www.text);
            }
            if (jsonCallBack != null)
            {
                PendingPanel.Hide(this);
                jsonCallBack(ParseRecieveData(www.text));
            }
            if (streamCallBack != null)
            {
                if (www.bytes != null && www.bytes.Length > 0)
                    streamCallBack(www.bytes);
                else
                    streamCallBack(null);
            }
            if (assetBundleCallBack != null)
            {
                assetBundleCallBack(www);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError(e);
            //Debuger.LogException(e);
            //if (callBack != null)
            //{
            //    callBack(null);
            //}
            //if (jsonCallBack != null)
            //{
            //    PendingPanel.Hide(this);
            //    jsonCallBack(null);
            //}
            //if (streamCallBack != null)
            //{
            //    streamCallBack(null);
            //}
            //if (assetBundleCallBack != null)
            //{
            //    assetBundleCallBack(null);
            //}
        }
    }

    void FixedUpdate()
    {
        if (GameTime.time - startTime >= reconnectDelay && alertPanel == null)
        {
            if (alertPanel == null && !isInBackground)
            {
                if (useBackground)
                    isInBackground = true;
                if (AnnouncePanel.currentPanel != null)
                    AnnouncePanel.currentPanel.Close();

				uint btns = tryCount > TRY_COUNT_MAX ? AlertPanel.CANCEL : AlertPanel.OK | AlertPanel.CANCEL;

                if (Language.IsLanguageInit())
				{
					alertPanel = AlertPanel.Show(Language.GetStr("Error", "e" + 2 + errorPostfix), btns, OnErrorAlertYes);
					alertPanel.cancelBtnText.text = Language.GetStr("Public", "give_up");
				}
                else
				{
                    if (Language.lan == Language.Enabled.Chinese.ToString())
                    {
						alertPanel = AlertPanel.Show("<color=orange>网络连接出现问题</color>\n点击确认重试.", btns, OnErrorAlertYes);
                        alertPanel.cancelBtnText.text = "取消";
                        alertPanel.yesBtnText.text = "确定";
                    }
                    else if (Language.lan == Language.Enabled.ChineseTraditional.ToString())
                    {
                        alertPanel = AlertPanel.Show("<color=orange>網路連接出現問題</color>\n點擊確認重試.", btns, OnErrorAlertYes);
                        alertPanel.cancelBtnText.text = "取消";
                        alertPanel.yesBtnText.text = "確認";
                    }
                    else
                    {
						alertPanel = AlertPanel.Show("<color=orange>Network issues </color>\nPlease try reconnection", btns, OnErrorAlertYes);
                        alertPanel.cancelBtnText.text = "Cancel";
                        alertPanel.yesBtnText.text = "Yes";
                    }
                }
            }
            else if (isInBackground)
            {
                startTime = GameTime.time;
                StartCoroutine("Start");
            }

        }
    }

    private bool CheckResult(string str)
    {
        if (jsonCallBack == null)
            return true;

        JsonData data = ParseRecieveData(str);
        if (((IDictionary)data).Contains("error") && (int)data["error"] == 1)
            return false;
        return true;
    }

    private void OnErrorAlertYes(AlertCloseEvent evt)
    {
        if (evt == null)
        {
            DelayCall.Call(delegate
            {
                startTime = GameTime.time;
                try
                {
                    StartCoroutine("Start");
                }
                catch(System.Exception e)
                {
                    Debuger.LogException(e);
                }
            }, reconnectDelay);
        }
        else
        {
			if(evt.detail == AlertPanel.OK)
			{
	            startTime = GameTime.time;
	            try
	            {
	                StartCoroutine("Start");
	            }
	            catch (System.Exception e)
	            {
	                Debuger.LogException(e);
	            }
	        }
			else if(evt.detail == AlertPanel.CANCEL)
			{
				SettingPanel.RestartGame();
			}
		}
    }

    public delegate void URLRequestCallBackDelegate(string data);
    public delegate void URLRequestJsonCallBackDelegate(JsonData data);
    public delegate void URLRequestStreamCallBackDelegate(byte[] data);
    public delegate void URLRequestAssetBundlesCallBackDelegate(WWW www);

    public static URLRequest CreateURLRequestString(string url, URLRequestData requestData, Method method, URLRequestCallBackDelegate callBack)
    {
        URLRequest urlRequest = NewURLRequest(url, requestData, method);
        urlRequest.callBack = callBack;
        return urlRequest;
    }

    public static URLRequest CreateURLRequest(string url, URLRequestData requestData, Method method, URLRequestJsonCallBackDelegate callBack, bool showPending = true)
    {
        URLRequest urlRequest = NewURLRequest(url, requestData, method);
        urlRequest.jsonCallBack = callBack;

        //if(showPending)
        //{
        //    HandlerShowPendingLog(urlRequest, 15);
        //}

        if (showPending)
            PendingPanel.Show(false, urlRequest);
        return urlRequest;
    }

    //public static Dictionary<string, vp_Timer.Handle> timerURLDic = new Dictionary<string, vp_Timer.Handle>();
    //public static void HandlerShowPendingLog(URLRequest urlRequest, float delay)
    //{
    //    vp_Timer.Handle timer = new vp_Timer.Handle();
    //    vp_Timer.In(delay, () => { Destroy(urlRequest);  }, timer); //TODO  
    //    timerURLDic[urlRequest.url] = timer;
    //}

    //public static void CancelShowPendingLog(string url)
    //{
    //    if (timerURLDic.ContainsKey(url))
    //    {
    //        timerURLDic[url].Cancel();
    //        timerURLDic.Remove(url);
    //    }
    //}

    public static URLRequest CreateURLRequestByte(string url, URLRequestData requestData, Method method, URLRequestStreamCallBackDelegate callBack)
    {
        URLRequest urlRequest = NewURLRequest(url, requestData, method);
        urlRequest.streamCallBack = callBack;
        return urlRequest;
    }

    public static URLRequest CreateURLRequestWWW(string url, URLRequestData requestData, Method method, URLRequestAssetBundlesCallBackDelegate callBack)
    {
        URLRequest urlRequest = NewURLRequest(url, requestData, method);
        urlRequest.assetBundleCallBack = callBack;
        return urlRequest;
    }

    public static URLRequest NewURLRequest(string url, URLRequestData requestData, Method method)
    {
        url = url.Replace("\\", "/");

        //========= add random value pervent cache start =========
        if (url.IndexOf("http") > -1)
        {
            if (url.IndexOf("?") == -1)
                url += "?";

            if (url.Substring(url.Length - 1, 1) != "?")
                url += "&";

            url += "" + UnityEngine.Random.Range(0, 99999);
        }
        
        //========= add random value pervent cache end =========

        Debuger.Log("CreateURLRequest Method[" + method.ToString() + "]" + url);

        GameObject gameObj = new GameObject("URLRequest");
        //URLRequest urlRequest = gameObj.AddComponent<URLRequest>();
        URLRequest urlRequest = DllManager.instance.AddCompotent<URLRequest>(gameObj, "URLRequest");
        List<KeyValuePair<string, object>> data = requestData != null ? requestData.GetDataList() : null;
        if (data != null && data.Count > 0)
        {
            if (method == Method.GET)
            {
                string[] strArr = new string[data.Count];
                for (int i = 0; i < data.Count; i++)
                {
                    KeyValuePair<string, object> kvp = data[i];
                    strArr[i] = kvp.Key + "=" + kvp.Value;
                    Debuger.Log(kvp.Key + " : " + kvp.Value);
                }

                if (url.IndexOf("?") == -1)
                    url += "?";

                if (url.Substring(url.Length - 1, 1) != "?")
                    url += "&";

                url += string.Join("&", strArr);
                urlRequest.url = url;
                Debuger.Log("[url]" + url);
            }
            else if (method == Method.POST)
            {
				WWWForm form = new WWWForm();
				bool encryption = true;
				if(encryption)
				{
					JsonData json = JsonMapper.ToObject("{}");
					for (int i = 0; i < data.Count; i++)
					{
						KeyValuePair<string, object> kvp = data[i];
//						form.AddField(kvp.Key, kvp.Value.ToString());
						json[kvp.Key] = kvp.Value.ToString();
						Debuger.Log(kvp.Key + " : " + kvp.Value);
					}
					string cryptionStr = DecryptionUtil.Encryption(json.ToJson());
//                    Debuger.Log("json.ToJson()"+json.ToJson());

                    form.AddField("x", cryptionStr);
				}
				else
				{
	                for (int i = 0; i < data.Count; i++)
	                {
	                    KeyValuePair<string, object> kvp = data[i];
	                    form.AddField(kvp.Key, kvp.Value.ToString());
	                    Debuger.Log(kvp.Key + " : " + kvp.Value);
	                }
				}
                urlRequest.form = form;
                urlRequest.url = url;
            }
        }
        else
        {
            urlRequest.url = url;
        }

        

        return urlRequest;
    }

    private JsonData ParseRecieveData(string data)
    {
        Debuger.Log("ParseRecieveData : " + data);
        int index = data.IndexOf("{");
        if (index >= 0)
        {
            data = data.Substring(index);
        }

        try
        {
            return JsonMapper.ToObject(data);
        }
        catch (JsonException e)
        {
            Debug.LogException(e);
        }

        JsonData json = new JsonData();
        json["error"] = 1;
        return json;
    }

    public void SetInBackgroundimmediately()
    {
        isInBackground = true;
    }

    public void SetRequestOnce()
    {
        requestOnce = true;
    }
}

