using UnityEngine;
using System.Collections;

public class NoSdk : MonoBehaviour,ISdk {
    private string _objName;

    void Awake()
    {
        _objName = gameObject.name = GetType().Name;


    }
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void init()
    {
        SDKHandler.instance.InitCallback("Init_Success");
    }

    public void login()
    {
        SDKHandler.instance.LoginCallback("NoSdk"+"_"+SystemInfo.deviceUniqueIdentifier);
        
    }

    public void pay(BuyCoinData data)
    {
    }

    public void exitSdk()
    {
        AlertPanel.Show(Language.GetStr("Public", "quit_game_confirm"), AlertPanel.YES | AlertPanel.NO, OnQuit, null);
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
    public void onResume()
    {
        
    }

    public void onPause()
    {
        
    }
}
