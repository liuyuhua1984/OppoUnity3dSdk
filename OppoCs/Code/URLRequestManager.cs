using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using LitJson;
using System;
using ExtensionMethod;
using Facebook.Unity;
using ZXing.PDF417.Internal;

public class URLRequestManager
{
    public enum URL
    {
        AssetBundlePath,
        Login,
        CharList,
        CharAdd,
        CharAddList,
        CharRemove,
        CharClean, //clean char & equip & team
        TeamList,
        TeamCharUpdate,
        TeamEquipUpdate,
        TeamEquipSwap,
        EquipList,
        EquipAdd,
        EquipAddList,
        EquipRemove,
        DropItemList, //items droped
        DropInfo, //if user can get drop items
        ItemList,
        ItemAdd,
        ItemAddList,
        ItemRemove,
        Exp,
        Coin,
        Diamond,
        Coupon,
        BuyCoin,
        BuyPhysic,
        BuyCoupon,
        BuyDiamond, //测试用，实际无用
        MapLevelPass,
        TalentUpdate,
        SkillUpdate,
        TutoUpdate,
        UnlockUpdate,

        ShopGashapon,
        ShopGashaponBuy,

        Shop,
        ShopPvp,
        ShopBuy,
        ShopSell,
        ShopRefresh,

        MailInit,
        MailDelete,
        MailRead,
        MailGet,

        TrialInit,
        TrialPlunder,
        TrialRobBegin,
        TrialRobEnd,
        TrialStatistics,
        TrialGetReward,

        SignIn,
        SignIn_Sign,

        CdKeyCheck,

        AnnounceUrl,

        MatchInfo,

        EquipSell,
        ItemSell,
        ItemCompose,

        UserSign,
        Physic,

        CrusadeInit,
        CrusadeMonster,
        CrusadePass,
        CrusadeReward,
        CrusadePunish,
        CrusadeRemove,

        MissionList,
        MissionReward,

        NicknameSet,
        NicknameGet,

        AnnounceList,

        GameInfo,

        UploadReplay,
        GetReplay,
        MapLevelHighscore,

        SendPower,
        TeamHighscore,
        AllHighscore,
        UserInfo,
        StarHighscore,
		MatchHighscore,
		CaptureList,

        XZPay,

        LoginWithBinding,

        ServerTime,
        FriendList, //好友列表
        FriendRecomList, //推荐好友列表
        FriendRequest_friend_list, //请求申请好友列表
        FriendRecomSearch, //搜索玩家 id
        FriendLocList, //同城好友推荐
        FriendChangedList, //换一批
        FriendRequestAddFriend, //申请好友
        FriendRequestAgree, //同意申请好友
        FriendRequestRefuse, //拒绝请好友
        FriendDetailInfo, //好友info
        FriendDel, //好友del
        FriendGetFrom, //get好友 tili
        FriendPostTo, //gei好友 tili
        FriendInfoPostTo, //好友 mail msg
        FriendProgrees, //好友 progrees

        CharLevelUp,//角色升級
        task_active,//
        game_active_reward,

        NPCInit,
        NPCBattle,
        NPCReward,
        NPCShow,


        game_online,
        game_init,
        onlineReward,

        CheckInit,
        checkin_loop_reward,
        checkin_reward,


		MapLevelSweep,

        WeekCardOpen,
        WeekCardReward,
        WeekCardBuyTest,

		VIP_Test,
		VIP_Info,
		VIP_Reward,

        game_open_lottery,
        game_buy_lottery,

        game_open_space_time_shop,
        game_refresh_space_time_shop,
        game_exchange_item_space_time_shop,


        VERIFY_TOKEN,
        
    }

    

    public long userId;


    private Dictionary<URL, URLRequestCallBackDelegate> callbackDict;
    private Dictionary<URL, string> urlDict;

    public delegate void URLRequestCallBackDelegate(bool success);

    public URLRequestManager()
    {
        callbackDict = new Dictionary<URL, URLRequestCallBackDelegate>();
        urlDict = new Dictionary<URL, string>();

        urlDict.Add(URL.AssetBundlePath, Config.GetHttpRoot() + "?m=excel&a=game_config");

        urlDict.Add(URL.Login, Config.GetHttpRoot() + "?m=user&a=game_login_new");

        urlDict.Add(URL.CharList, Config.GetHttpRoot() + "?m=charactor&a=game_list");
        urlDict.Add(URL.CharAdd, Config.GetHttpRoot() + "?m=charactor&a=game_add");
        urlDict.Add(URL.CharAddList, Config.GetHttpRoot() + "?m=charactor&a=game_add_more");
        urlDict.Add(URL.CharRemove, Config.GetHttpRoot() + "?m=charactor&a=game_remove");
        urlDict.Add(URL.CharClean, Config.GetHttpRoot() + "?m=charactor&a=char_clean");

        urlDict.Add(URL.TeamList, Config.GetHttpRoot() + "?m=team&a=game_list");
        urlDict.Add(URL.TeamCharUpdate, Config.GetHttpRoot() + "?m=team&a=game_update_char");
        urlDict.Add(URL.TeamEquipUpdate, Config.GetHttpRoot() + "?m=team&a=game_update_equip");
        urlDict.Add(URL.TeamEquipSwap, Config.GetHttpRoot() + "?m=team&a=game_update_swap");

        urlDict.Add(URL.EquipList, Config.GetHttpRoot() + "?m=equip&a=game_list");
        urlDict.Add(URL.EquipAdd, Config.GetHttpRoot() + "?m=equip&a=game_add");
        urlDict.Add(URL.EquipRemove, Config.GetHttpRoot() + "?m=equip&a=game_remove");
        urlDict.Add(URL.EquipAddList, Config.GetHttpRoot() + "?m=equip&a=game_add_more");

        urlDict.Add(URL.DropItemList, Config.GetHttpRoot() + "?m=drop&a=game_drop");
        urlDict.Add(URL.DropInfo, Config.GetHttpRoot() + "?m=drop&a=game_info");

        urlDict.Add(URL.ItemList, Config.GetHttpRoot() + "?m=item&a=game_list");
        urlDict.Add(URL.ItemAdd, Config.GetHttpRoot() + "?m=item&a=game_add");
        urlDict.Add(URL.ItemAddList, Config.GetHttpRoot() + "?m=item&a=game_add_more");
        urlDict.Add(URL.ItemRemove, Config.GetHttpRoot() + "?m=item&a=game_minus");

        urlDict.Add(URL.Exp, Config.GetHttpRoot() + "?m=user&a=game_exp");
        urlDict.Add(URL.Coin, Config.GetHttpRoot() + "?m=user&a=game_coin");
        urlDict.Add(URL.Diamond, Config.GetHttpRoot() + "?m=user&a=game_diamond");
        urlDict.Add(URL.Coupon, Config.GetHttpRoot() + "?m=user&a=game_coupon");

        urlDict.Add(URL.BuyCoin, Config.GetHttpRoot() + "?m=user&a=game_buycoin");
        urlDict.Add(URL.BuyPhysic, Config.GetHttpRoot() + "?m=user&a=game_buyenergy");
        urlDict.Add(URL.BuyCoupon, Config.GetHttpRoot() + "?m=user&a=game_buycoupon");
        urlDict.Add(URL.BuyDiamond, Config.GetHttpRoot() + "?m=pay&a=temp");

        urlDict.Add(URL.MapLevelPass, Config.GetHttpRoot() + "?m=maplevel&a=game_list");

        urlDict.Add(URL.TalentUpdate, Config.GetHttpRoot() + "?m=talent&a=game_add");

        urlDict.Add(URL.SkillUpdate, Config.GetHttpRoot() + "?m=skill&a=game_add");

        urlDict.Add(URL.TutoUpdate, Config.GetHttpRoot() + "?m=user&a=game_tuto");

        urlDict.Add(URL.UnlockUpdate, Config.GetHttpRoot() + "?m=user&a=game_lock");

        urlDict.Add(URL.ShopGashapon, Config.GetHttpRoot() + "?m=shop&a=game_gashapon");
        urlDict.Add(URL.ShopGashaponBuy, Config.GetHttpRoot() + "?m=shop&a=game_add_gashapon");
        urlDict.Add(URL.Shop, Config.GetHttpRoot() + "?m=shop&a=game_shopping_list");

        urlDict.Add(URL.ShopBuy, Config.GetHttpRoot() + "?m=shop&a=game_buy_shopping");
        urlDict.Add(URL.ShopSell, Config.GetHttpRoot() + "?m=shop&a=game_sell_shopping");

        urlDict.Add(URL.ShopRefresh, Config.GetHttpRoot() + "?m=shop&a=game_user_refresh");

        urlDict.Add(URL.MailInit, Config.GetHttpRoot() + "?m=message&a=game_list");
        urlDict.Add(URL.MailDelete, Config.GetHttpRoot() + "?m=message&a=game_remove");
        urlDict.Add(URL.MailRead, Config.GetHttpRoot() + "?m=message&a=game_read");
        urlDict.Add(URL.MailGet, Config.GetHttpRoot() + "?m=message&a=game_reward");

        urlDict.Add(URL.TrialInit, Config.GetHttpRoot() + "?m=trial&a=game_init");
        urlDict.Add(URL.TrialPlunder, Config.GetHttpRoot() + "?m=trial&a=game_search");

        urlDict.Add(URL.TrialRobBegin, Config.GetHttpRoot() + "?m=trial&a=game_rob_begin");
        urlDict.Add(URL.TrialRobEnd, Config.GetHttpRoot() + "?m=trial&a=game_rob_end");
        urlDict.Add(URL.TrialStatistics, Config.GetHttpRoot() + "?m=trial&a=game_settlement");
        urlDict.Add(URL.TrialGetReward, Config.GetHttpRoot() + "?m=trial&a=game_reward");



        urlDict.Add(URL.CdKeyCheck, Config.GetHttpRoot() + "?m=cdkey&a=game_reward");

        urlDict.Add(URL.AnnounceUrl, Config.GetHttpRoot() + "?m=message&a=game_notice");

        urlDict.Add(URL.MatchInfo, Config.GetHttpRoot() + "?m=user&a=game_matchinfo");

        urlDict.Add(URL.EquipSell, Config.GetHttpRoot() + "?m=equip&a=game_sell");
        urlDict.Add(URL.ItemSell, Config.GetHttpRoot() + "?m=item&a=game_sell");
        urlDict.Add(URL.ItemCompose, Config.GetHttpRoot() + "?m=item&a=game_compose");

        urlDict.Add(URL.UserSign, Config.GetHttpRoot() + "?m=user&a=game_sign");
        urlDict.Add(URL.Physic, Config.GetHttpRoot() + "?m=user&a=game_energy");

        urlDict.Add(URL.CrusadeInit, Config.GetHttpRoot() + "?m=crusade&a=game_init");
        urlDict.Add(URL.CrusadeMonster, Config.GetHttpRoot() + "?m=crusade&a=game_monster");
        urlDict.Add(URL.CrusadePass, Config.GetHttpRoot() + "?m=crusade&a=game_pass");
        urlDict.Add(URL.CrusadeReward, Config.GetHttpRoot() + "?m=crusade&a=game_reward");
        urlDict.Add(URL.CrusadePunish, Config.GetHttpRoot() + "?m=crusade&a=game_punish");
        urlDict.Add(URL.CrusadeRemove, Config.GetHttpRoot() + "?m=crusade&a=cheat_remove");

        urlDict.Add(URL.MissionList, Config.GetHttpRoot() + "?m=task&a=game_list");
        urlDict.Add(URL.MissionReward, Config.GetHttpRoot() + "?m=task&a=game_reward");

        urlDict.Add(URL.NicknameSet, Config.GetHttpRoot() + "?m=user&a=game_nickname");
        urlDict.Add(URL.NicknameGet, Config.GetHttpRoot() + "?m=user&a=game_randname");

        urlDict.Add(URL.AnnounceList, Config.GetHttpRoot() + "?m=notice&a=game_list");

        urlDict.Add(URL.GameInfo, Config.GetHttpRoot() + "?m=user&a=game_info");

        urlDict.Add(URL.UploadReplay, Config.GetHttpRoot() + "?m=drop&a=game_upload");
        urlDict.Add(URL.GetReplay, Config.GetHttpRoot().Replace("index.php", ""));
        urlDict.Add(URL.MapLevelHighscore, Config.GetHttpRoot().Replace("index.php", "") + "templates/fastmaplevel.html");

        urlDict.Add(URL.SendPower, Config.GetHttpRoot() + "?m=user&a=game_bestpower");
        urlDict.Add(URL.TeamHighscore, Config.GetHttpRoot().Replace("index.php", "") + "templates/bestteam.html");
        urlDict.Add(URL.AllHighscore, Config.GetHttpRoot().Replace("index.php", "") + "templates/leaderboard.html");
        urlDict.Add(URL.StarHighscore, Config.GetHttpRoot().Replace("index.php", "") + "templates/starmap.html");
		urlDict.Add(URL.MatchHighscore, Config.GetHttpRoot() + "?m=matchrank&a=game_open_match_rank");
		urlDict.Add(URL.CaptureList, Config.GetHttpRoot() + "?m=matchrank&a=game_get_before_ten_rank");

        urlDict.Add(URL.UserInfo, Config.GetHttpRoot() + "?m=user&a=game_info");

        urlDict.Add(URL.XZPay, Config.GetHttpRoot() + "?m=pay&a=xz_gp_pay");

        urlDict.Add(URL.LoginWithBinding, Config.GetHttpRoot() + "?m=user&a=game_uid");

        urlDict.Add(URL.ServerTime, Config.GetHttpRoot() + "?m=excel&a=game_time");

        //好友相关
        var str1 = Config.GetHttpRoot();
        str1 += "?m=relation&a=";
        urlDict.Add(URL.FriendList, str1 + "game_my_friend_list");
        //=====推荐好友面板
        urlDict.Add(URL.FriendRecomList, str1 + "game_recommend_list");
        urlDict.Add(URL.FriendRecomSearch, str1 + "game_find_user");
        //todo zjw 如果同城实装，修改这里的链接
        urlDict.Add(URL.FriendLocList, str1 + "game_change_batch");
        urlDict.Add(URL.FriendChangedList, str1 + "game_change_batch");
        urlDict.Add(URL.FriendRequestAddFriend, str1 + "game_request_add_friend");

        //申请列表面板
        urlDict.Add(URL.FriendRequest_friend_list, str1 + "game_request_friend_list");
        urlDict.Add(URL.FriendRequestAgree, str1 + "game_add_friend");
        urlDict.Add(URL.FriendRequestRefuse, str1 + "game_refuse_friend");
        urlDict.Add(URL.FriendDetailInfo, str1 + "game_lookup_team");
        urlDict.Add(URL.FriendDel, str1 + "game_del_friend");
        urlDict.Add(URL.FriendGetFrom, str1 + "game_get_energy");
        urlDict.Add(URL.FriendPostTo, str1 + "game_presented");

        str1 = Config.GetHttpRoot();
        str1 += "?m=message&a=";
        urlDict.Add(URL.FriendInfoPostTo, str1 + "game_mail_new");
        urlDict.Add(URL.FriendProgrees, Config.GetHttpRoot()
            + "?m=maplevel&a=game_friend");

        urlDict.Add(URL.CharLevelUp, Config.GetHttpRoot() + "?m=user&a=game_role_level_up");
        urlDict.Add(URL.task_active, Config.GetHttpRoot() + "?m=task&a=game_active");
        urlDict.Add(URL.game_active_reward, Config.GetHttpRoot() + "?m=task&a=game_active_reward");


        urlDict.Add(URL.NPCInit, Config.GetHttpRoot() + "?m=npc&a=game_init");
        urlDict.Add(URL.NPCBattle, Config.GetHttpRoot() + "?m=npc&a=game_battle");
        urlDict.Add(URL.NPCReward, Config.GetHttpRoot() + "?m=npc&a=game_reward");
        urlDict.Add(URL.NPCShow, Config.GetHttpRoot() + "?m=npc&a=game_show");


        urlDict.Add(URL.game_online, Config.GetHttpRoot() + "?m=online&a=game_online");
        urlDict.Add(URL.game_init, Config.GetHttpRoot() + "?m=online&a=game_init");
        urlDict.Add(URL.onlineReward, Config.GetHttpRoot() + "?m=online&a=game_reward");


        urlDict.Add(URL.SignIn, Config.GetHttpRoot() + "?m=checkin&a=game_init");
        urlDict.Add(URL.SignIn_Sign, Config.GetHttpRoot() + "?m=checkin&a=game_reward");

        urlDict.Add(URL.CheckInit, Config.GetHttpRoot() + "?m=checkin&a=game_init");
        urlDict.Add(URL.checkin_loop_reward, Config.GetHttpRoot() + "?m=checkin&a=game_loop_reward");
        urlDict.Add(URL.checkin_reward, Config.GetHttpRoot() + "?m=checkin&a=game_reward");

		urlDict.Add(URL.MapLevelSweep, Config.GetHttpRoot() + "?m=drop&a=game_sweeping");

		urlDict.Add(URL.WeekCardOpen, Config.GetHttpRoot() + "?m=week_card&a=game_open_week_card");
		urlDict.Add(URL.WeekCardReward, Config.GetHttpRoot() + "?m=week_card&a=game_fetch_week_card");
        urlDict.Add(URL.WeekCardBuyTest, Config.GetHttpRoot() + "?m=week_card&a=test");

        
		urlDict.Add(URL.VIP_Test, Config.GetHttpRoot() + "?m=vip&a=test");
		urlDict.Add(URL.VIP_Info, Config.GetHttpRoot() + "?m=vip&a=game_open_vip");
		urlDict.Add(URL.VIP_Reward, Config.GetHttpRoot() + "?m=vip&a=game_get_vip_reward");

        urlDict.Add(URL.game_open_lottery, Config.GetHttpRoot() + "?m=lottery&a=game_open_lottery");
        urlDict.Add(URL.game_buy_lottery, Config.GetHttpRoot() + "?m=lottery&a=game_buy_lottery");

        urlDict.Add(URL.game_open_space_time_shop, Config.GetHttpRoot() + "?"+ "m=space_time_shop&a=game_open_space_time_shop");
        urlDict.Add(URL.game_refresh_space_time_shop, Config.GetHttpRoot() + "?"+ "m=space_time_shop&a=game_refresh_shop");
        urlDict.Add(URL.game_exchange_item_space_time_shop, Config.GetHttpRoot() + "?"+ "m=space_time_shop&a=game_exchange_item");

        //验证token
        urlDict.Add(URL.VERIFY_TOKEN, Config.GetHttpRoot() + "?" + "m=user&a=verify_token");

    }

    public string GetUrl(URL url)
    {
        return urlDict[url];
    }

    public bool IsLogin()
    {
        return userId > 0;
    }

    public long GetMyUid()
    {
        return userId;
    }

    private void AddCallback(URL url, URLRequestCallBackDelegate callback)
    {
        if (callback == null)
            return;
        if (callbackDict.ContainsKey(url))
            callbackDict[url] = callback;
        else
            callbackDict.Add(url, callback);
    }

    private void DealCallback(URL url, bool success)
    {
        if (callbackDict.ContainsKey(url))
        {
            callbackDict[url](success);
            callbackDict.Remove(url);
        }
    }

    public bool CheckError(JsonData data, bool popupError = false, AlertPanel.AlertPanelCloseDelegate callback = null)
    {
        CheckMission(data);

        if (JsonUtil.ContainKey(data, "error"))
        {
            int error = (int)data["error"];
            if (error < 100)
            {
                return false;
            }
            if (popupError)
            {
                //AlertPanel.Show("[" + error + "]" + Language.GetStr("Error", "e" + error), 4, callback);
                AlertPanel.Show(Language.GetStr("Error", "e" + error), 4, callback);
            }
            return true;
        }
        return false;
    }

    private void CheckMission(JsonData data)
    {
        if (!JsonUtil.ContainKey(data, "task"))
        {
            return;
        }

        List<Mission> esp = new List<Mission>();

        JsonData taskArr = data["task"];
        for (int i = 0; i < taskArr.Count; i++)
        {
            JsonData task = taskArr[i];
            if (task == null)
            {
                Debuger.LogError("wrong data task is null");
                continue;
                
            }
            int id = JsonUtil.ToInt(task["i"]);
            int value = JsonUtil.ToInt(task["s"]);
            Mission mission = MissionManager.GetInstance().GetMission(id);
            if (mission != null)
            {
                var needOpenServerMissionUpdateTip = mission.GetMissionType() == Mission.MissionType.OpenServer
                                                     && !mission.rewarded && !mission.finished;
                mission.SetCurrentValue(value);

                if (needOpenServerMissionUpdateTip)
                {

                    //开服活动任务如果刚刚完成了

                    if (mission.finished)OpenServerRedDotMain.GetInstance().MissionChange(mission);
                }
                if (mission.GetTaskType() == Mission.TaskType.T_10 || mission.GetTaskType() == Mission.TaskType.T_11
                    || mission.GetTaskType() == Mission.TaskType.T_12 || mission.GetTaskType() == Mission.TaskType.T_13 ||
                    mission.GetTaskType() == Mission.TaskType.T_28
                    )
                {
                    esp.Add(mission);
                }
            }
            else
            {
                Debuger.LogError("Mission[" + id + "] could not found");
            }
        }

        if ((Application.loadedLevelName != "Battle" && taskArr.Count > 0)
            || (Application.loadedLevelName == "Battle" && esp.Count > 0)
            )
        {
            MissionLine.instance.Check(esp);
        }

        if (Application.loadedLevelName == "Battle")
        {
            MissionLine.instance.Check(MissionManager.GetInstance().GetDailyMissionList(Mission.TaskType.T_3));
        }
    }



    public void GetAssetBundlePath(bool isBak = false, URLRequestCallBackDelegate callback = null)
    {
        AddCallback(URL.AssetBundlePath, callback);

        URLRequestData data = new URLRequestData();
        if (!isBak)
            data.Add("n", "assetbundles");
        else
            data.Add("n", "assetbundlesbak");

        URLRequest.CreateURLRequest(urlDict[URL.AssetBundlePath], data, URLRequest.Method.POST, OnAssetBundlePath);
    }

    private void OnAssetBundlePath(JsonData data)
    {
        if (CheckError(data))
        {
            DealCallback(URL.AssetBundlePath, false);
            return;
        }

        Config.SetHttpBundlesRoot(data["v"].ToString());
        DealCallback(URL.AssetBundlePath, true);
    }



    public void Login(string u, string p, URLRequestCallBackDelegate callback = null, bool inBackground = false,
        bool requestOnce = false)
    {
        AddCallback(URL.Login, callback);

        URLRequestData data = new URLRequestData();
        data.Add("u", u);
        data.Add("p", p);
        data.Add("l", Language.lan.ToString());

        Player myPlayer = Session.GetInstance().myPlayer;
        myPlayer.uid = u;
        myPlayer.password = p;

        URLRequest urlRequest = URLRequest.CreateURLRequest(urlDict[URL.Login], data, URLRequest.Method.POST, OnLogin,
            false);
        if (inBackground)
            urlRequest.SetInBackgroundimmediately();
        if (requestOnce)
            urlRequest.SetRequestOnce();
    }

    private void OnLogin(JsonData data)
    {
        if (CheckError(data, true))
        {
            DealCallback(URL.Login, false);
            return;
        }

        if (JsonUtil.ContainKey(data, "time"))
        {
            Player.GAME_REFRESH_TIME = JsonUtil.ToInt(data["time"]) + 12;
            Player.GAME_START_TIME = Time.realtimeSinceStartup;
        }

        if (JsonUtil.ContainKey(data, "s"))
        {
            Player.serverID = data["s"].ToString();
        }

        if (JsonUtil.ContainKey(data, "i"))
            userId = JsonUtil.ToLong(data["i"]);

        //Debug.LogError("userId " + userId);
        else
        {
            Debug.LogError("err");
            return;
        }
        Player myPlayer = Session.GetInstance().myPlayer;
        myPlayer.nick = data["n"].ToString();
        //myPlayer.coin = JsonUtil.ToInt(data["co"]);
        myPlayer.UpdateResource(Item.PriceType.Coin, JsonUtil.ToInt(data["co"]), true);
        myPlayer.diamond = JsonUtil.ToInt(data["d"]);
        myPlayer.expTotal = JsonUtil.ToInt(data["e"]);

		if(myPlayer.expTotal == 0)
			StaticsManager.GetInstance().LoginComplete();

        //读取发给朋友的邮件
        myPlayer.mailData.LoadSavedFriendMails(userId);

        if (JsonUtil.ContainKey(data, "p"))
            myPlayer.chargeCount = JsonUtil.ToInt(data["p"]);

        TutorialManager.GetInstance().SetTopPassId(data["t"].ToString());
        if (JsonUtil.ContainKey(data, "l") && data["l"] != null)
            UnlockManager.GetInstance().DealUnlockString(data["l"].ToString());

        JsonData arena = data["a"];
        myPlayer.coupon = JsonUtil.ToInt(arena["c"]);

        JsonData match = data["m"];
        myPlayer.matchBattleCount = JsonUtil.ToInt(match["f"]);
        myPlayer.matchScore = JsonUtil.ToInt(match["s"]);
        myPlayer.matchPoint = JsonUtil.ToInt(match["p"]);
        myPlayer.matchDayGet = new List<int>();
        JsonData dayGetData = match["i"];
        for (int i = 0; i < dayGetData.Count; i++)
        {
            int index = JsonUtil.ToInt(dayGetData[i]["m"]);
            if (!myPlayer.matchDayGet.Contains(index))
                myPlayer.matchDayGet.Add(index);
        }
        myPlayer.matchInitDay = System.DateTime.Now.Day;

        JsonData talentList = data["al"]["l"];
        TalentManager.GetInstance().ClearActive();
        for (int i = 0; i < talentList.Count; i++)
        {
            JsonData talent = talentList[i];
            int rowId = JsonUtil.ToInt(talent["r"]);
            int index = JsonUtil.ToInt(talent["i"]);
            TalentManager.GetInstance().SetTalentActive(rowId, index);
        }

        if (JsonUtil.ContainKey(data, "y"))
        {
            JsonData physicData = data["y"];
            int physic = JsonUtil.ToInt(physicData["e"]);
            JsonData date = physicData["t"];
            if (physic >= 0)
            {
                try
                {
                    DateTime dateTime = DateTime.Now;
                    myPlayer.PhysicLastUpdateOverTime = physicData.GetInt("d");
                    if (JsonUtil.ToInt(date["y"]) > 1970)
                        dateTime = new DateTime(JsonUtil.ToInt(date["y"]), JsonUtil.ToInt(date["m"]),
                            JsonUtil.ToInt(date["d"]), JsonUtil.ToInt(date["h"]), JsonUtil.ToInt(date["i"]),
                            JsonUtil.ToInt(date["s"]));
                    UpdatePhysic(dateTime, physic);
                    //long time = long.Parse(physicData["t"].ToString());
                    //myPlayer.physic = physic;
                    //myPlayer.physicLastUpdate = new System.DateTime(time);
                }
                catch (System.Exception e)
                {
                    Debuger.LogException(e);
                }
            }
        }

        SaveManager.GetSaver().LoadPlayer(myPlayer);
        myPlayer.UpdateLevel(false, true);

		myPlayer.vipLevel = JsonUtil.ToInt(data["vip"]);
		myPlayer.diamondBought = JsonUtil.ToInt(data["r_diamond"]);

        OnCharList(data["cl"]);
        OnGetEquipmentList(data["el"]);
        OnGetTeamList(data["tl"]);
        SaveManager.GetSaver().LoadCurrentTeam(myPlayer);
        if (data["sl"] != null)
        {
            JsonData skillList = data["sl"]["l"];
            for (int i = 0; i < skillList.Count; i++)
            {
                JsonData skill = skillList[i];
                if (skill != null)
                {
                    int charId = JsonUtil.ToInt(skill["c"]);
                    int typeId = JsonUtil.ToInt(skill["t"]);
                    int level = JsonUtil.ToInt(skill["l"]);
                    Session.GetInstance().myPlayer.SetSkill(charId, typeId, level);
                }
            }
        }
        SaveManager.GetSaver().LoadSkill(myPlayer);

        OnGetItemList(data["il"]);
        OnGetMapLevelPass(data["ml"]);

        if (JsonUtil.ContainKey(data, "time"))
        {
            int timeLeft = JsonUtil.ToInt(data["time"]);
            OnGetUserSign(data["g"], timeLeft);
        }


        AnnounceManager.GetInstance();

        GetMapLevelHighscore();
        SyncManager.GetInstance().Init();

        if (JsonUtil.ContainKey(data, "w"))
        {
            int w = JsonUtil.ToInt(data["w"]);
            if (w == 1)
            {
                SDKManager.instance.RegisterOK();
//                StaticsManager.GetInstance().ActOrReg(userId.ToString(), DataEyeGA.AccountType._Registered);
            }
        }



		VIPInfo(null);


        DealCallback(URL.Login, true);

        //account statics
        //StaticsManager.GetInstance().SetAccount(userId, myPlayer.nick, anysdk.AccountType.REGISTED, myPlayer.level, anysdk.AccountOperate.LOGIN);
//        StaticsManager.GetInstance().SetAccount(userId.ToString(), DataEyeGA.AccountType._Registered);
    }


    private void OnGetUserSign(JsonData data, int timeLeft)
    {
        string key = "energy"; //Saver.SignType.physic.ToString();
        if (JsonUtil.ContainKey(data, key))
            SaveManager.GetSaver().InitBuyPhysicCount(JsonUtil.ToInt(data[key]), timeLeft);

        key = Saver.SignType.coin.ToString();
        if (JsonUtil.ContainKey(data, key))
            SaveManager.GetSaver().InitBuyCoinCount(JsonUtil.ToInt(data[key]), timeLeft);

        key = Saver.SignType.coupon.ToString();
        if (JsonUtil.ContainKey(data, key))
            SaveManager.GetSaver().InitBuyCouponCount(JsonUtil.ToInt(data[key]), timeLeft);

        key = Saver.SignType.hp.ToString();
        if (JsonUtil.ContainKey(data, key))
            SaveManager.GetSaver().InitBuyHpCount(JsonUtil.ToInt(data[key]), timeLeft);

        key = Saver.SignType.round.ToString();
        if (JsonUtil.ContainKey(data, key))
            SaveManager.GetSaver().InitBuyRoundCount(JsonUtil.ToInt(data[key]), timeLeft);

        key = Saver.SignType.round_hp.ToString();
        if (JsonUtil.ContainKey(data, key))
            SaveManager.GetSaver().InitBuyRoundHpCount(JsonUtil.ToInt(data[key]), timeLeft);
    }


    public void CharList(URLRequestCallBackDelegate callback)
    {
        AddCallback(URL.CharList, callback);

        URLRequestData data = new URLRequestData();
        data.Add("u", userId);

        URLRequest.CreateURLRequest(urlDict[URL.CharList], data, URLRequest.Method.POST, OnCharList);
    }

    private void OnCharList(JsonData data)
    {
        if (CheckError(data, true))
        {
            DealCallback(URL.CharList, false);
            return;
        }

        JsonData arr = data["l"];
        Session.GetInstance().charList.Clear();
        for (int i = 0; i < arr.Count; i++)
        {
            Session.GetInstance().charList.Add(JsonUtil.ToInt(arr[i]));
        }

        DealCallback(URL.CharList, true);
    }






    public void AddChar(int charId, URLRequestCallBackDelegate callback = null)
    {
        AddCallback(URL.CharAdd, callback);

        if (Session.GetInstance().charList.Contains(charId))
        {
            Debuger.LogError("Already has char[" + charId + "]");
            DealCallback(URL.CharAdd, false);
            return;
        }

        CharData charData = CharManager.GetInstance().GetData(charId);
        if (charData != null)
        {
            URLRequestData data = new URLRequestData();
            data.Add("u", userId);
            data.Add("c", charId);
            data.Add("e", charData.weaponId);
            data.Add("o", Config.GetInt(Config.Item.max_team_count));

            URLRequest.CreateURLRequest(urlDict[URL.CharAdd], data, URLRequest.Method.POST, OnAddChar, false);
        }
        else
        {
            DealCallback(URL.CharAdd, false);
        }
    }

    private void OnAddChar(JsonData data)
    {
        if (CheckError(data, true))
        {
            DealCallback(URL.CharAdd, false);
            return;
        }

        int index = JsonUtil.ToInt(data["i"]);
        int charId = JsonUtil.ToInt(data["c"]);
        long weaponUid = JsonUtil.ToLong(data["ei"]);
        int weaponId = JsonUtil.ToInt(data["e"]);

        long uid1 = data.GetLong("rd");
        Player myPlayer = Session.GetInstance().myPlayer;

        myPlayer.GetEquipInventory().AddEquip(weaponUid, weaponId);
        myPlayer.GetEquipInventory().UsedEquip(weaponUid);

        AddCharToTeam(myPlayer, myPlayer.GetTeamDataList(), charId, index, weaponUid,uid1);

        Session.GetInstance().charList.Add(charId);

        DealCallback(URL.CharAdd, true);
    }

    private void AddCharToTeam(Player myPlayer, List<TeamData> teamDataList,
        int charId, int index, long weaponUid,long charUid)
    {

        for (int i = 0; i < teamDataList.Count; i++)
        {
            TeamData teamData = teamDataList[i];
            if (teamData.HasChar(charId))
            {
                Debuger.LogError("AddCharToTeam::already has char[" + charId + "]");
                break;
            }
            AvatarData avatarData = new AvatarData(teamData.index, myPlayer);
            avatarData.charId = charId;
            avatarData.Uid = charUid;
            //TalentManager.GetInstance().FillSkill(avatarData);
            CharData charData = CharManager.GetInstance().GetData(charId);
            avatarData.juniorSkillId = charData.junior_skill;
            avatarData.seniorSkillId = charData.senior_skill;
            avatarData.superSkillId = charData.super_skill;
            avatarData.weaponId = CharManager.GetInstance().GetDefaultWeaponId(charId);
            avatarData.weaponUid = weaponUid;
            avatarData.index = index;
            teamData.Add(avatarData);
        }
    }

    public void AddCharList(int[] charIdArr, URLRequestCallBackDelegate callback = null)
    {
        AddCallback(URL.CharAddList, callback);

        URLRequestData data = new URLRequestData();
        data.Add("u", userId);
        data.Add("o", Config.GetInt(Config.Item.max_team_count));
        JsonData jsonData = JsonMapper.ToObject("[]");

        for (int i = 0; i < charIdArr.Length; i++)
        {
            CharData charData = CharManager.GetInstance().GetData(charIdArr[i]);
            JsonData cj = JsonMapper.ToObject("{}");
            cj["c"] = charIdArr[i];
            cj["e"] = charData.weaponId;
            jsonData.Add(cj);
        }
        data.Add("l", jsonData.ToJson());

        URLRequest.CreateURLRequest(urlDict[URL.CharAddList], data, URLRequest.Method.POST, OnAddCharList, false);
    }

    private void OnAddCharList(JsonData data)
    {
        if (CheckError(data, true))
        {
            DealCallback(URL.CharAddList, false);
            return;
        }

        JsonData arr = data["l"];
        Player myPlayer = Session.GetInstance().myPlayer;
        for (int i = 0; i < arr.Count; i++)
        {
            JsonData o = arr[i];
            int index = JsonUtil.ToInt(o["i"]);
            int charId = JsonUtil.ToInt(o["c"]);
			long weaponUid = JsonUtil.ToLong(o["ei"]);
            int weaponId = JsonUtil.ToInt(o["e"]);
			long uid1 = JsonUtil.ToLong(o["rd"]);

            myPlayer.GetEquipInventory().AddEquip(weaponUid, weaponId);
            myPlayer.GetEquipInventory().UsedEquip(weaponUid);

            AddCharToTeam(myPlayer, myPlayer.GetTeamDataList(), charId, index, weaponUid, uid1);

            Session.GetInstance().charList.Add(charId);
        }

        DealCallback(URL.CharAddList, true);
    }


    public void RemoveChar(int charId, URLRequestCallBackDelegate callback)
    {
        AddCallback(URL.CharRemove, callback);

        if (!Session.GetInstance().myPlayer.GetFirstTeamData().HasChar(charId))
        {
            DealCallback(URL.CharRemove, false);
            return;
        }

        if (Session.GetInstance().myPlayer.GetFirstTeamData().GetAvatarDataList().Count <= 1)
        {
            Debuger.LogError("this is the last charactor in team!!");
            DealCallback(URL.CharRemove, false);
            return;
        }

        URLRequestData data = new URLRequestData();
        data.Add("u", userId);
        data.Add("c", charId);

        URLRequest.CreateURLRequest(urlDict[URL.CharRemove], data, URLRequest.Method.POST, OnRemoveChar);
    }

    private void OnRemoveChar(JsonData data)
    {
        if (CheckError(data, true))
        {
            DealCallback(URL.CharRemove, false);
            return;
        }

        int charId = JsonUtil.ToInt(data["c"]);
        Player myPlayer = Session.GetInstance().myPlayer;
        RemoveCharFromTeam(myPlayer.GetTeamDataList(), charId);
    }

    public void ClearPlayer(URLRequestCallBackDelegate callback = null)
    {
        AddCallback(URL.CharClean, callback);

        URLRequestData data = new URLRequestData();
        data.Add("i", Session.GetInstance().myPlayer.uid);

        URLRequest.CreateURLRequest(urlDict[URL.CharClean], data, URLRequest.Method.POST, OnClearPlayer);
    }

    private void OnClearPlayer(JsonData data)
    {
        if (CheckError(data, true))
        {
            DealCallback(URL.CharClean, false);
            return;
        }

        DealCallback(URL.CharClean, true);
    }


    private void RemoveCharFromTeam(List<TeamData> teamDataList, int charId)
    {
        for (int i = 0; i < teamDataList.Count; i++)
        {
            TeamData teamData = teamDataList[i];
            List<AvatarData> tempAvatarList = teamData.GetAvatarDataList();
            for (int j = tempAvatarList.Count - 1; j >= 0; j--)
            {
                AvatarData avatarData = tempAvatarList[j];
                if (avatarData.charId == charId)
                    tempAvatarList.Remove(avatarData);
            }
            for (int j = 0; j < tempAvatarList.Count; j++)
            {
                AvatarData avatarData = tempAvatarList[j];
                avatarData.index = j;
            }
        }
    }







    public void GetTeamList(URLRequestCallBackDelegate callback = null)
    {
        AddCallback(URL.TeamList, callback);

        URLRequestData data = new URLRequestData();
        data.Add("u", userId);
        data.Add("o", Config.GetInt(Config.Item.max_team_count));

        URLRequest.CreateURLRequest(urlDict[URL.TeamList], data, URLRequest.Method.POST, OnGetTeamList);
    }

    private void OnGetTeamList(JsonData data)
    {
        if (CheckError(data, true))
        {
            DealCallback(URL.TeamList, false);
            return;
        }

        Player player = Session.GetInstance().myPlayer;
        FillTeamDataList(data, TeamData.TeamType.pve, player);

        DealCallback(URL.TeamList, true);
    }

    private void FillTeamDataList(JsonData data, TeamData.TeamType teamType, Player player)
    {
        List<TeamData> teamList = new List<TeamData>();
        JsonData arr = data[teamType.ToString()];
        for (int i = 0; i < arr.Count; i++)
        {
            TeamData teamData = new TeamData(i, new List<AvatarData>(), teamType);
            JsonData avatarArr = arr[i];
            for (int j = 0; j < avatarArr.Count; j++)
            {
                JsonData o = avatarArr[j];
                AvatarData avatarData = new AvatarData(i, player);
                avatarData.charId = JsonUtil.ToInt(o["c"]);
                avatarData.index = JsonUtil.ToInt(o["i"]);
                avatarData.ServerExp = JsonUtil.ToInt(o["ex"]);
                avatarData.Severlevel = JsonUtil.ToInt(o["rl"]);
                avatarData.Uid = JsonUtil.ToLong(o["rd"]);
                //TalentManager.GetInstance().FillSkill(avatarData);
                CharData charData = CharManager.GetInstance().GetData(avatarData.charId);
                avatarData.juniorSkillId = charData.junior_skill;
                avatarData.seniorSkillId = charData.senior_skill;
                avatarData.superSkillId = charData.super_skill;
                long weaponUid = JsonUtil.ToLong(o["e"]);
                int weaponId = player.GetEquipInventory().GetEquitTypeId(weaponUid);
                player.GetEquipInventory().UsedEquip(weaponUid);
                avatarData.weaponId = weaponId;
                avatarData.weaponUid = weaponUid;
                teamData.GetAvatarDataList().Add(avatarData);
            }
            teamData.RefreshEquipUseAvatar();
            teamList.Add(teamData);
        }
        player.SetTeamDataList(teamList);
    }



    public void TeamCharUpdate(TeamData teamData, URLRequestCallBackDelegate callback = null)
    {
        AddCallback(URL.TeamCharUpdate, callback);

        URLRequestData data = new URLRequestData();
        data.Add("u", userId);
        data.Add("t", teamData.teamType.ToString());
        data.Add("o", teamData.index);

        JsonData jsonData = JsonMapper.ToObject("[]");

        List<AvatarData> avatarList = teamData.GetInTeamAvatarDataList();
        for (int i = 0; i < avatarList.Count; i++)
        {
            JsonData aj = JsonMapper.ToObject("{}");
            AvatarData avatarData = avatarList[i];
            aj["i"] = avatarData.index;
            aj["c"] = avatarData.charId;
            jsonData.Add(aj);
        }
        data.Add("l", jsonData.ToJson());

        URLRequest.CreateURLRequest(urlDict[URL.TeamCharUpdate], data, URLRequest.Method.POST, OnTeamCharUpdate);
    }

    private void OnTeamCharUpdate(JsonData data)
    {
        if (CheckError(data, true))
        {
            DealCallback(URL.TeamCharUpdate, false);
            return;
        }

        int teamIndex = JsonUtil.ToInt(data["o"]);
        string teamType = data["t"].ToString();

        TeamData teamData = Session.GetInstance().myPlayer.GetTeamData(teamIndex);
        teamData.RemoveOrder();

        JsonData arr = data["l"];
        for (int i = 0; i < arr.Count; i++)
        {
            JsonData o = arr[i];
            int charId = JsonUtil.ToInt(o["c"]);
            int index = JsonUtil.ToInt(o["i"]);
            teamData.ChangeOrder(charId, index);
        }

        DealCallback(URL.TeamCharUpdate, true);
    }



    public void TeamEquipUpdate(int teamIndex, TeamData.TeamType teamType, int charId, long weaponUid,
        URLRequestCallBackDelegate callback = null)
    {
        AddCallback(URL.TeamEquipUpdate, callback);

        URLRequestData data = new URLRequestData();
        data.Add("u", userId);
        data.Add("t", teamType.ToString());
        data.Add("o", teamIndex);
        data.Add("c", charId);
        data.Add("e", weaponUid);

        URLRequest.CreateURLRequest(urlDict[URL.TeamEquipUpdate], data, URLRequest.Method.POST, OnTeamEquipUpdate);
    }

    private void OnTeamEquipUpdate(JsonData data)
    {
        if (CheckError(data, true))
        {
            DealCallback(URL.TeamEquipUpdate, false);
            return;
        }

        Player player = Session.GetInstance().myPlayer;

        int teamIndex = JsonUtil.ToInt(data["o"]);
        string teamType = data["t"].ToString();
        int charId = JsonUtil.ToInt(data["c"]);
        long weaponUid = JsonUtil.ToLong(data["e"]);
        int weaponId = player.GetEquipInventory().GetEquitTypeId(weaponUid);
        TeamData teamData = player.GetTeamData(teamIndex);
        AvatarData avatarData = teamData.GetAvatarData(charId);

        long oldWeaponUid = avatarData.weaponUid;

        avatarData.weaponId = weaponId;
        avatarData.weaponUid = weaponUid;

        player.GetEquipInventory().UsedEquip(weaponUid);
        teamData.RefreshEquipUseAvatar();

        if (!EquipInventory.CheckEquipped(player, oldWeaponUid))
        {
            player.GetEquipInventory().UnuseEquip(oldWeaponUid);
        }

        if (avatarData.index == 0 && teamData == player.GetCurrentTeamData())
        {
            Session.GetInstance().GetMessageManager().LeaderChange();
        }

        DealCallback(URL.TeamEquipUpdate, true);
    }







    public void TeamEquipSwap(int teamIndex, TeamData.TeamType teamType, int charId1, int charId2,
        URLRequestCallBackDelegate callback = null)
    {
        AddCallback(URL.TeamEquipUpdate, callback);

        URLRequestData data = new URLRequestData();
        data.Add("u", userId);
        data.Add("t", teamType.ToString());
        data.Add("o", teamIndex);
        data.Add("c1", charId1);
        data.Add("c2", charId2);

        URLRequest.CreateURLRequest(urlDict[URL.TeamEquipSwap], data, URLRequest.Method.POST, OnTeamEquipSwap);
    }

    private void OnTeamEquipSwap(JsonData data)
    {
        if (CheckError(data, true))
        {
            DealCallback(URL.TeamEquipSwap, false);
            return;
        }

        Player player = Session.GetInstance().myPlayer;

        string teamType = data["t"].ToString();
        int teamIndex = JsonUtil.ToInt(data["o"]);
        int charId1 = JsonUtil.ToInt(data["c1"]);
        int charId2 = JsonUtil.ToInt(data["c2"]);
        long equip1 = JsonUtil.ToLong(data["e1"]);
        int weaponId1 = player.GetEquipInventory().GetEquitTypeId(equip1);
        long equip2 = JsonUtil.ToLong(data["e2"]);
        int weaponId2 = player.GetEquipInventory().GetEquitTypeId(equip2);

        TeamData teamData = player.GetTeamData(teamIndex);
        AvatarData avatarData = teamData.GetAvatarData(charId1);
        avatarData.weaponId = weaponId1;
        avatarData.weaponUid = equip1;

        avatarData = teamData.GetAvatarData(charId2);
        avatarData.weaponId = weaponId2;
        avatarData.weaponUid = equip2;

        teamData.RefreshEquipUseAvatar();

        if (avatarData.index == 0 && teamData == player.GetCurrentTeamData())
        {
            Session.GetInstance().GetMessageManager().LeaderChange();
        }

        DealCallback(URL.TeamEquipSwap, true);
    }









    public void GetEquipmentList(URLRequestCallBackDelegate callback)
    {
        AddCallback(URL.EquipList, callback);

        URLRequestData data = new URLRequestData();
        data.Add("u", userId);

        URLRequest.CreateURLRequest(urlDict[URL.EquipList], data, URLRequest.Method.POST, OnGetEquipmentList);
    }

    private void OnGetEquipmentList(JsonData data)
    {
        if (CheckError(data, true))
        {
            DealCallback(URL.EquipList, false);
            return;
        }

        EquipInventory inventory = Session.GetInstance().myPlayer.GetEquipInventory();
        inventory.Clear();

        JsonData arr = data["r"];
        for (int i = 0; i < arr.Count; i++)
        {
            JsonData d = arr[i];
            long id = JsonUtil.ToLong(d["i"]);
            int equipId = JsonUtil.ToInt(d["e"]);
            inventory.AddEquip(id, equipId, true);
        }

        DealCallback(URL.EquipList, true);
    }



    public void AddEquipment(int equipId, URLRequestCallBackDelegate callback)
    {
        AddCallback(URL.EquipAdd, callback);

        URLRequestData data = new URLRequestData();
        data.Add("u", userId);
        data.Add("e", equipId);

        URLRequest.CreateURLRequest(urlDict[URL.EquipAdd], data, URLRequest.Method.POST, OnAddEquipment);
    }

    private void OnAddEquipment(JsonData data)
    {
        if (CheckError(data, true))
        {
            DealCallback(URL.EquipAdd, false);
            return;
        }

        long id = JsonUtil.ToLong(data["i"]);
        int equipId = JsonUtil.ToInt(data["e"]);
        Session.GetInstance().myPlayer.GetEquipInventory().AddEquip(id, equipId);

        DealCallback(URL.EquipAdd, true);
    }

    public void RemoveEquipment(int id, URLRequestCallBackDelegate callback)
    {
        AddCallback(URL.EquipRemove, callback);

        URLRequestData data = new URLRequestData();
        data.Add("u", userId);
        data.Add("i", id);

        URLRequest.CreateURLRequest(urlDict[URL.EquipRemove], data, URLRequest.Method.POST, OnRemoveEquipment);
    }

    private void OnRemoveEquipment(JsonData data)
    {
        if (CheckError(data, true))
        {
            DealCallback(URL.EquipRemove, false);
            return;
        }

        int id = JsonUtil.ToInt(data["i"]);
        Session.GetInstance().myPlayer.GetEquipInventory().RemoveEquip(id);

        DealCallback(URL.EquipRemove, true);
    }

    public void AddEquipmentList(int[] equipIdArr, URLRequestCallBackDelegate callback)
    {
        AddCallback(URL.EquipAddList, callback);

        URLRequestData data = new URLRequestData();
        data.Add("u", userId);
        JsonData jsonData = JsonMapper.ToObject("[]");

        for (int i = 0; i < equipIdArr.Length; i++)
        {
            JsonData ej = JsonMapper.ToObject("{}");
            ej["e"] = equipIdArr[i];
            jsonData.Add(ej);
        }
        data.Add("l", jsonData.ToJson());

        URLRequest.CreateURLRequest(urlDict[URL.EquipAddList], data, URLRequest.Method.POST, OnAddEquipmentList);
    }

    private void OnAddEquipmentList(JsonData data)
    {
        if (CheckError(data, true))
        {
            DealCallback(URL.EquipAddList, false);
            return;
        }

        JsonData arr = data["l"];
        for (int i = 0; i < arr.Count; i++)
        {
            JsonData o = arr[i];
            int uid = JsonUtil.ToInt(o["i"]);
            int equipId = JsonUtil.ToInt(o["e"]);
            Session.GetInstance().myPlayer.GetEquipInventory().AddEquip(uid, equipId);
        }

        DealCallback(URL.EquipAddList, true);
    }


    public void UploadReplayData(string id, string replay, URLRequestCallBackDelegate callback = null)
    {
        if (!Application.isEditor)
        {
            AddCallback(URL.UploadReplay, callback);
            URLRequestData data = new URLRequestData();
            data.Add("i", id);
            data.Add("r", replay);

            SaveManager.GetSaver().AddReplay(id, replay);

            URLRequest urlRequest = URLRequest.CreateURLRequest(urlDict[URL.UploadReplay], data, URLRequest.Method.POST,
                OnUploadReplayData, false);
            urlRequest.SetInBackgroundimmediately();
        }
    }

    private void OnUploadReplayData(JsonData data)
    {
        if (CheckError(data, true))
        {
            DealCallback(URL.GetReplay, false);
            return;
        }

        string id = data["i"].ToString();
        SaveManager.GetSaver().RemoveReplay(id);

        DealCallback(URL.GetReplay, true);
    }



    public void GetReplayData(string path, URLRequestCallBackDelegate callback = null)
    {
        AddCallback(URL.GetReplay, callback);
        string fullPath = urlDict[URL.GetReplay] + path;
        URLRequest urlRequest = URLRequest.CreateURLRequest(fullPath, new URLRequestData(), URLRequest.Method.GET,
            OnGetReplayData, true);
    }

    private void OnGetReplayData(JsonData data)
    {
        if (CheckError(data, true))
        {
            DealCallback(URL.GetReplay, false);
            return;
        }

        //		string replayStr = data["r"].ToString();
        //#if UNITY_EDITOR
        //		replayStr = Resources.Load<TextAsset>("Record/2").text;
        //#endif
        Session.GetInstance().replaySource = data.ToJson();
        Session.GetInstance().GetMessageManager().BattlePveStart(100, BattleUtil.BattleType.NpcChallenge);

        DealCallback(URL.GetReplay, true);
    }



    public void GetMapLevelHighscore(URLRequestCallBackDelegate callback = null)
    {
        AddCallback(URL.MapLevelHighscore, callback);

        URLRequestData data = new URLRequestData();
        URLRequest urlRequest = URLRequest.CreateURLRequest(urlDict[URL.MapLevelHighscore], data, URLRequest.Method.POST,
            OnGetMapLevelHighscore, false);
        urlRequest.SetInBackgroundimmediately();
        urlRequest.SetRequestOnce();
    }

    private void OnGetMapLevelHighscore(JsonData data)
    {
        if (CheckError(data, true))
        {
            DealCallback(URL.MapLevelHighscore, false);
            return;
        }

        MapLevelManager.GetInstance().InitHighscore(data);
        DealCallback(URL.MapLevelHighscore, true);
    }



    public void GetDropItemList(int mapId, URLRequestCallBackDelegate callback = null)
    {
        AddCallback(URL.DropItemList, callback);

        URLRequestData data = new URLRequestData();
        data.Add("m", mapId);
        data.Add("u", userId);

        URLRequest urlRequest = URLRequest.CreateURLRequest(urlDict[URL.DropItemList], data, URLRequest.Method.POST,
            OnGetDropItemList, false);
        urlRequest.useBackground = true;
        urlRequest.errorPostfix = "_drop";
    }

    private void OnGetDropItemList(JsonData data)
    {
        if (CheckError(data, true))
        {
            DealCallback(URL.DropItemList, false);
            return;
        }

        BattleSession battleSession = Session.GetInstance().GetBattleSession();
        battleSession.mapLevelPassCount = JsonUtil.ToInt(data["c"]);
        battleSession.coin = JsonUtil.ContainKey(data, "o") ? JsonUtil.ToInt(data["o"]) : 0;
        battleSession.exp = JsonUtil.ContainKey(data, "e") ? JsonUtil.ToInt(data["e"]) : 0;
        battleSession.InitDropItemDict();
        JsonData arr = data["l"];
        if (arr.Count > 0)
        {
            for (int i = 0; i < arr.Count; i++)
            {
                JsonData o = arr[i];
                int itemId = JsonUtil.ToInt(o["i"]);
                int count = JsonUtil.ToInt(o["c"]);
                battleSession.SetDropItemDict(itemId, count);
            }
        }

        SavePhysic(Session.GetInstance().myPlayer);
        DealCallback(URL.DropItemList, true);
    }

    public void SetDropInfo(int mapId, int passCount, bool isWin, int stars, int coin,
        URLRequestCallBackDelegate callback = null)
    {
        AddCallback(URL.DropInfo, callback);

        Player player = Session.GetInstance().myPlayer;
        int power = player.GetInTeamPower(player.GetCurrentTeamData());
        string info = HighScorePlayer.GetInTeamJson(player, player.GetCurrentTeamData()).ToJson();

        URLRequestData data = new URLRequestData();
        data.Add("m", mapId);
        data.Add("u", userId);
        data.Add("c", passCount);
        data.Add("w", isWin);
        data.Add("s", stars);
        data.Add("o", coin);
        data.Add("r", power);
        //		data.Add("r", 720);
        data.Add("i", info);

        URLRequest.CreateURLRequest(urlDict[URL.DropInfo], data, URLRequest.Method.POST, OnSetDropInfo, true);
    }

    private void OnSetDropInfo(JsonData data)
    {
        if (CheckError(data, true))
        {
            DealCallback(URL.DropInfo, false);
            return;
        }

        if (JsonUtil.ContainKey(data, "i"))
        {
            Session.GetInstance().GetBattleSession().GetRecord().uploadReplayId = data["i"].ToString();
        }

        Player player = Session.GetInstance().myPlayer;
        if (JsonUtil.ContainKey(data, "o"))
            player.UpdateResource(Item.PriceType.Coin, JsonUtil.ToInt(data["o"]), true);
        if (JsonUtil.ContainKey(data, "e"))
        {
            player.expTotal = JsonUtil.ToInt(data["e"]);
            //player.UpdateLevel();
        }

        JsonData arr = data["l"];
        for (int i = 0; i < arr.Count; i++)
        {
            JsonData o = arr[i];
            HandlerRewardItem(o, -1, StaticsManager.ConsumeType.Reward, StaticsManager.ConsumeModule.MapLevel);
        }
        //player.UpdateLevel();

        UnlockManager.GetInstance().UnlockFunction(player);
        DealCallback(URL.DropInfo, true);
    }





    public void GetItemList(URLRequestCallBackDelegate callback)
    {
        AddCallback(URL.ItemList, callback);

        URLRequestData data = new URLRequestData();
        data.Add("u", userId);

        URLRequest.CreateURLRequest(urlDict[URL.ItemList], data, URLRequest.Method.POST, OnGetItemList);
    }

    private void OnGetItemList(JsonData data)
    {
        if (CheckError(data, true))
        {
            DealCallback(URL.ItemList, false);
            return;
        }

        Player myPlayer = Session.GetInstance().myPlayer;
        myPlayer.ClearItemInventory();
        JsonData arr = data["l"];
        for (int i = 0; i < arr.Count; i++)
        {
            JsonData obj = arr[i];
            long uid = JsonUtil.ToLong(obj["i"]);
            int amount = JsonUtil.ToInt(obj["c"]);
            int itemId = JsonUtil.ToInt(obj["m"]);
            Debuger.Log("OnGetItemList:[uid]" + uid + ", [itemid]" + itemId + ", [amount]" + amount);
            myPlayer.SetItem(uid, itemId, amount);
        }

        DealCallback(URL.ItemList, true);
    }


    public void AddItem(int itemId, int amount, URLRequestCallBackDelegate callback)
    {
        AddCallback(URL.ItemAdd, callback);
        URLRequestData data = new URLRequestData();
        data.Add("u", userId);
        data.Add("m", itemId);
        data.Add("c", amount);

        URLRequest.CreateURLRequest(urlDict[URL.ItemAdd], data, URLRequest.Method.POST, OnItemAdd);
    }

    private void OnItemAdd(JsonData data)
    {
        if (CheckError(data, true))
        {
            DealCallback(URL.ItemAdd, false);
            return;
        }

        long uid = JsonUtil.ToLong(data["i"]);
        int itemId = JsonUtil.ToInt(data["m"]);
        int amount = JsonUtil.ToInt(data["c"]);
        Session.GetInstance().myPlayer.SetItem(uid, itemId, amount);

        DealCallback(URL.ItemAdd, true);
    }


    public void AddItemList(List<Item> list, URLRequestCallBackDelegate callback)
    {
        AddCallback(URL.ItemAddList, callback);

        JsonData arr = JsonMapper.ToObject("[]");
        for (int i = 0; i < list.Count; i++)
        {
            Item item = list[i];
            JsonData o = JsonMapper.ToObject("{}");
            o["m"] = item.GetId();
            o["c"] = item.GetAmount();
            arr.Add(o);
        }

        URLRequestData data = new URLRequestData();
        data.Add("u", userId);
        data.Add("l", arr.ToString());

        URLRequest.CreateURLRequest(urlDict[URL.ItemAddList], data, URLRequest.Method.POST, OnItemAddList);
    }

    private void OnItemAddList(JsonData data)
    {
        if (CheckError(data, true))
        {
            DealCallback(URL.ItemAddList, false);
            return;
        }

        Player myPlayer = Session.GetInstance().myPlayer;
        JsonData arr = data["l"];
        for (int i = 0; i < arr.Count; i++)
        {
            JsonData o = arr[i];
            long uid = JsonUtil.ToLong(o["i"]);
            int itemId = JsonUtil.ToInt(o["m"]);
            int amount = JsonUtil.ToInt(o["c"]);
            myPlayer.SetItem(uid, itemId, amount);
        }

        DealCallback(URL.ItemAddList, true);
    }



    public void ItemRemove(long uid, int itemId, int amount, URLRequestCallBackDelegate callback)
    {
        AddCallback(URL.ItemRemove, callback);

        URLRequestData data = new URLRequestData();
        data.Add("u", userId);
        data.Add("i", uid);
        data.Add("m", itemId);
        data.Add("c", amount);

        URLRequest.CreateURLRequest(urlDict[URL.ItemRemove], data, URLRequest.Method.POST, OnItemRemove);
    }

    private void OnItemRemove(JsonData data)
    {
        if (CheckError(data, true))
        {
            DealCallback(URL.ItemRemove, false);
            return;
        }

        Player myPlayer = Session.GetInstance().myPlayer;
        long uid = JsonUtil.ToLong(data["i"]);
        int itemId = JsonUtil.ToInt(data["m"]);
        int amount = JsonUtil.ToInt(data["c"]);
        myPlayer.SetItem(uid, itemId, amount);

        DealCallback(URL.ItemRemove, true);
    }

    public void SetCoin(int coin, URLRequestCallBackDelegate callback)
    {
        AddCallback(URL.Coin, callback);

        URLRequestData data = new URLRequestData();
        data.Add("u", userId);
        data.Add("c", coin);

        URLRequest.CreateURLRequest(urlDict[URL.Coin], data, URLRequest.Method.POST, OnSetCoin);
    }

    private void OnSetCoin(JsonData data)
    {
        if (CheckError(data, true))
        {
            DealCallback(URL.Coin, false);
            return;
        }

        Player myPlayer = Session.GetInstance().myPlayer;
        //myPlayer.coin = JsonUtil.ToInt(data["c"]);
        myPlayer.UpdateResource(Item.PriceType.Coin, JsonUtil.ToInt(data["c"]), true);
        DealCallback(URL.Coin, true);
    }

    public void SetDiamond(int diamond, URLRequestCallBackDelegate callback)
    {
        AddCallback(URL.Diamond, callback);

        URLRequestData data = new URLRequestData();
        data.Add("u", userId);
        data.Add("d", diamond);

        URLRequest.CreateURLRequest(urlDict[URL.Diamond], data, URLRequest.Method.POST, OnSetDiamond);
    }

    private void OnSetDiamond(JsonData data)
    {
        if (CheckError(data, true))
        {
            DealCallback(URL.Diamond, false);
            return;
        }

        Player myPlayer = Session.GetInstance().myPlayer;
        myPlayer.diamond = JsonUtil.ToInt(data["d"]);

        DealCallback(URL.Diamond, true);
    }

    public void SetExp(int exp, URLRequestCallBackDelegate callback)
    {
        AddCallback(URL.Exp, callback);

        URLRequestData data = new URLRequestData();
        data.Add("u", userId);
        data.Add("e", exp);

        URLRequest.CreateURLRequest(urlDict[URL.Exp], data, URLRequest.Method.POST, OnSetExp);
    }

    private void OnSetExp(JsonData data)
    {
        if (CheckError(data, true))
        {
            DealCallback(URL.Exp, false);
            return;
        }

        Player myPlayer = Session.GetInstance().myPlayer;
        myPlayer.expTotal = JsonUtil.ToInt(data["e"]);
        myPlayer.UpdateLevel();
        if (TopUserInfo.GetTopUserInfo() != null)
            TopUserInfo.GetTopUserInfo().UpdateExp();

        DealCallback(URL.Exp, true);
    }

    public void SetCoupon(int coupon, URLRequestCallBackDelegate callback)
    {
        AddCallback(URL.Coupon, callback);

        URLRequestData data = new URLRequestData();
        data.Add("u", userId);
        data.Add("c", coupon);

        URLRequest.CreateURLRequest(urlDict[URL.Coupon], data, URLRequest.Method.POST, OnSetCoupon);
    }

    private void OnSetCoupon(JsonData data)
    {
        if (CheckError(data, true))
        {
            DealCallback(URL.Coupon, false);
            return;
        }

        Player myPlayer = Session.GetInstance().myPlayer;
        myPlayer.coupon = JsonUtil.ToInt(data["c"]);

        DealCallback(URL.Coupon, true);
    }




    public void BuyCoin(URLRequestCallBackDelegate callback, bool showPending = true)
    {
        AddCallback(URL.BuyCoin, callback);

        URLRequestData data = new URLRequestData();
        data.Add("u", userId);

        URLRequest.CreateURLRequest(urlDict[URL.BuyCoin], data, URLRequest.Method.POST, OnBuyCoin, showPending).SetRequestOnce();
    }

    private void OnBuyCoin(JsonData data)
    {
        if (CheckError(data, true))
        {
            DealCallback(URL.BuyCoin, false);
            return;
        }

        if (HotfixManager.GetInstance().CheckHotfix())
        {
            return;
        }

        Player myPlayer = Session.GetInstance().myPlayer;
        myPlayer.diamond = JsonUtil.ToInt(data["d"]);
        myPlayer.UpdateResource(Item.PriceType.Coin, JsonUtil.ToInt(data["c"]), true);

        int count = JsonUtil.ToInt(data["n"]);
        int time = JsonUtil.ToInt(data["m"]);
        SaveManager.GetSaver().InitBuyCoinCount(count, time);

        //statics iap - purchase item
        StaticsManager.GetInstance()
            .PurchaseItem(Item.SpecialType.Coin.GetHashCode(), ItemType.Coin, JsonUtil.ToInt(data["ac"]),
                JsonUtil.ToInt(data["ld"]), StaticsManager.ConsumeModule.None);

        DealCallback(URL.BuyCoin, true);
    }

    public void BuyPhysic(URLRequestCallBackDelegate callback,bool showPending = true)
    {
        AddCallback(URL.BuyPhysic, callback);

        URLRequestData data = new URLRequestData();
        data.Add("u", userId);

        URLRequest.CreateURLRequest(urlDict[URL.BuyPhysic], data, URLRequest.Method.POST, OnBuyPhysic, showPending).SetRequestOnce();
    }

    private void OnBuyPhysic(JsonData data)
    {
        if (CheckError(data, true))
        {
            DealCallback(URL.BuyPhysic, false);
            return;
        }

        if (HotfixManager.GetInstance().CheckHotfix())
        {
            return;
        }

        Player myPlayer = Session.GetInstance().myPlayer;
        myPlayer.physic = JsonUtil.ToInt(data["e"]);
        myPlayer.diamond = JsonUtil.ToInt(data["d"]);
        myPlayer.UpdateResource(Item.PriceType.Diamond, myPlayer.diamond, true);

        int count = JsonUtil.ToInt(data["n"]);
        int time = JsonUtil.ToInt(data["m"]);
        SaveManager.GetSaver().InitBuyPhysicCount(count, time);

        //statics iap - purchase item
        StaticsManager.GetInstance()
            .PurchaseItem(Item.SpecialType.Physic.GetHashCode(), ItemType.Physic, JsonUtil.ToInt(data["ae"]),
                JsonUtil.ToInt(data["ld"]), StaticsManager.ConsumeModule.None);

        DealCallback(URL.BuyPhysic, true);
    }

    public void BuyCoupon(URLRequestCallBackDelegate callback,bool showPanding  = true)
    {
        AddCallback(URL.BuyCoupon, callback);

        URLRequestData data = new URLRequestData();
        data.Add("u", userId);

        URLRequest.CreateURLRequest(urlDict[URL.BuyCoupon], data, URLRequest.Method.POST, OnBuyCoupon, showPanding).SetRequestOnce();
    }

    private void OnBuyCoupon(JsonData data)
    {
        if (CheckError(data, true))
        {
            DealCallback(URL.BuyCoupon, false);
            return;
        }

        if (HotfixManager.GetInstance().CheckHotfix())
        {
            return;
        }

        Player myPlayer = Session.GetInstance().myPlayer;
        myPlayer.diamond = JsonUtil.ToInt(data["d"]);
        myPlayer.UpdateResource(Item.PriceType.Coupon, JsonUtil.ToInt(data["c"]), true);

        int count = JsonUtil.ToInt(data["n"]);
        int time = JsonUtil.ToInt(data["m"]);
        SaveManager.GetSaver().InitBuyCouponCount(count, time);

        //statics iap - purchase item
        StaticsManager.GetInstance()
            .PurchaseItem(Item.SpecialType.Coupon.GetHashCode(), ItemType.Coupon, JsonUtil.ToInt(data["ac"]),
                JsonUtil.ToInt(data["ld"]), StaticsManager.ConsumeModule.None);

        DealCallback(URL.BuyCoupon, true);
    }

    public void BuyDiamond(URLRequestCallBackDelegate callback)
    {
        AddCallback(URL.BuyDiamond, callback);

        URLRequestData data = new URLRequestData();
        data.Add("u", userId);

        URLRequest.CreateURLRequest(urlDict[URL.BuyDiamond], data, URLRequest.Method.POST, OnBuyDiamond).SetRequestOnce();
    }

    private void OnBuyDiamond(JsonData data)
    {
        if (CheckError(data, true))
        {
            DealCallback(URL.BuyDiamond, false);
            return;
        }

        if (HotfixManager.GetInstance().CheckHotfix())
        {
            return;
        }

        DealCallback(URL.BuyDiamond, true);
    }





    public void GetMapLevelPass(URLRequestCallBackDelegate callback)
    {
        AddCallback(URL.MapLevelPass, callback);

        URLRequestData data = new URLRequestData();
        data.Add("u", userId);

        URLRequest.CreateURLRequest(urlDict[URL.MapLevelPass], data, URLRequest.Method.POST, OnGetMapLevelPass);
    }

    private void OnGetMapLevelPass(JsonData data)
    {
        if (CheckError(data, true))
        {
            DealCallback(URL.Coupon, false);
            return;
        }

        MapLevelManager.GetInstance().ClearStarCache();

        JsonData arr = data["l"];
        for (int i = 0; i < arr.Count; i++)
        {
            JsonData o = arr[i];
            int id = JsonUtil.ToInt(o["m"]);
            int star = JsonUtil.ToInt(o["v"]);
            MapLevelManager.GetInstance().PassMap(id, star, false);
        }
        MapLevelManager.GetInstance().UpdateTopLevelId();

        DealCallback(URL.MapLevelPass, true);
    }


    public void UpdateTalent(int charId, int rowId, int index, URLRequestCallBackDelegate callback)
    {
        AddCallback(URL.TalentUpdate, callback);

        URLRequestData data = new URLRequestData();
        data.Add("u", userId);
        data.Add("c", charId);
        data.Add("r", rowId);
        data.Add("i", index);

        URLRequest.CreateURLRequest(urlDict[URL.TalentUpdate], data, URLRequest.Method.POST, OnUpdateTalent);
    }

    private void OnUpdateTalent(JsonData data)
    {
        if (CheckError(data, true))
        {
            DealCallback(URL.TalentUpdate, false);
            return;
        }

        int rowId = JsonUtil.ToInt(data["r"]);
        int index = JsonUtil.ToInt(data["i"]);

        DealCallback(URL.TalentUpdate, true);
    }



    public void UpdateSkill(int charId, int typeId, int level, URLRequestCallBackDelegate callback)
    {
        AddCallback(URL.SkillUpdate, callback);

        URLRequestData data = new URLRequestData();
        data.Add("u", userId);
        data.Add("c", charId);
        data.Add("t", typeId);
        data.Add("l", level);

        URLRequest.CreateURLRequest(urlDict[URL.SkillUpdate], data, URLRequest.Method.POST, OnUpdateSkill);
    }

    private void OnUpdateSkill(JsonData data)
    {
        if (CheckError(data, true))
        {
            DealCallback(URL.SkillUpdate, false);
            return;
        }

        Player player = Session.GetInstance().myPlayer;
        int charId = JsonUtil.ToInt(data["c"]);
        int typeId = JsonUtil.ToInt(data["t"]);
        int level = JsonUtil.ToInt(data["l"]);
        JsonData arr = data["a"];
        for (int i = 0; i < arr.Count; i++)
        {
            JsonData o = arr[i];
            string strId = o["m"].ToString();
            if (strId == "coin")
            {
                int coinLeft = JsonUtil.ToInt(o["c"]);
                player.UpdateResource(Item.PriceType.Coin, coinLeft, true);
                //statics iap - use item
                StaticsManager.GetInstance()
                    .UseItem(Item.SpecialType.Coin.GetHashCode(), ItemType.Coin, JsonUtil.ToInt(o["lc"]),
                        StaticsManager.ConsumeModule.SkillUpdate);
            }
            else
            {
                int id = int.Parse(strId);
                int count = JsonUtil.ToInt(o["c"]);
                long uid = JsonUtil.ToLong(o["i"]);
                player.SetItem(uid, id, count);
                //statics iap - use item
                StaticsManager.GetInstance()
                    .UseItem(id, ItemType.Item, JsonUtil.ToInt(o["lc"]), StaticsManager.ConsumeModule.SkillUpdate);
            }
        }

        player.SetSkill(charId, typeId, level);

        DealCallback(URL.SkillUpdate, true);
    }



    public void UpdateTutorial(string info, int index, URLRequestCallBackDelegate callback)
    {
        AddCallback(URL.TutoUpdate, callback);

        URLRequestData data = new URLRequestData();
        data.Add("u", userId);
        data.Add("i", info);

        URLRequest.CreateURLRequest(urlDict[URL.TutoUpdate], data, URLRequest.Method.POST, OnUpdateTutorial, false);

		StaticsManager.GetInstance().TutoComplete(index);
	}
	
    private void OnUpdateTutorial(JsonData data)
    {
        if (CheckError(data, true))
        {
            DealCallback(URL.TutoUpdate, false);
            return;
        }

//        int index = JsonUtil.ToInt(data["i"]);

        DealCallback(URL.TutoUpdate, true);
    }


    public void UpdateUnlock(string unlockString, URLRequestCallBackDelegate callback)
    {
        AddCallback(URL.UnlockUpdate, callback);

        URLRequestData data = new URLRequestData();
        data.Add("u", userId);
        data.Add("l", unlockString);

        URLRequest.CreateURLRequest(urlDict[URL.UnlockUpdate], data, URLRequest.Method.POST, OnUpdateUnlock, false);
    }

    private void OnUpdateUnlock(JsonData data)
    {
        if (CheckError(data, true))
        {
            DealCallback(URL.UnlockUpdate, false);
            return;
        }

        if (data["l"] != null)
        {
            string unlockString = data["l"].ToString();
            UnlockManager.GetInstance().DealUnlockString(unlockString);
        }

        DealCallback(URL.UnlockUpdate, true);
    }



    #region 商店通讯部分 杨朝智

    public void RequestShopSell(JsonData context, URLRequestCallBackDelegate callback = null)
    {
        AddCallback(URL.ShopSell, callback);

        URLRequestData data = new URLRequestData();
        data.Add("l", context.ToJson());
        data.Add("u", userId);

        URLRequest.CreateURLRequest(urlDict[URL.ShopSell], data, URLRequest.Method.POST, OnHandlerShopSell);
    }

    private void OnHandlerShopSell(JsonData data)
    {
        if (CheckError(data, true))
        {
            DealCallback(URL.ShopSell, false);
            return;
        }

        if (HotfixManager.GetInstance().CheckHotfix())
        {
            return;
        }

        JsonData arr = data["l"];
        for (int i = 0; i < arr.Count; i++)
        {
            Item.PriceType type = (Item.PriceType)JsonUtil.ToInt(arr[i]["t"]);
            int priceAdd = JsonUtil.ToInt(arr[i]["c"]);

            //statics iap - use item
            StaticsManager.GetInstance()
                .UseItem(JsonUtil.ToInt(arr[i]["m"]), ItemType.Item, JsonUtil.ToInt(arr[i]["n"]),
                    StaticsManager.ConsumeModule.ShopSell);
            //statics iap - reward item
            Item.SpecialType specialType = Item.GetSpecialTypeByPriceType(type);
            ItemType itemType = Item.GetItemTypeBySpecialType(specialType);
            StaticsManager.GetInstance()
                .RewardItem(specialType.GetHashCode(), itemType, priceAdd, StaticsManager.ConsumeModule.ShopSell);

            Player player = Session.GetInstance().myPlayer;
            player.UpdateResource(type, priceAdd);
        }
        ItemInventory inventory = Session.GetInstance().myPlayer.GetItemInventory(Item.Type.Item);
        inventory.Clear();

        DealCallback(URL.ShopSell, true);
    }

    public void RequestShopBuy(ItemType type, long uid, URLRequestCallBackDelegate callback = null)
    {
        AddCallback(URL.ShopBuy, callback);

        URLRequestData data = new URLRequestData();
        data.Add("u", userId);
        data.Add("i", uid);
        data.Add("t", (int)type);
        URLRequest.CreateURLRequest(urlDict[URL.ShopBuy], data, URLRequest.Method.POST, OnHanlderShopBuy);
    }

    private void OnHanlderShopBuy(JsonData data)
    {
        if (CheckError(data, true))
        {
            DealCallback(URL.ShopBuy, false);
            return;
        }

        if (HotfixManager.GetInstance().CheckHotfix())
        {
            return;
        }

        Player player = Session.GetInstance().myPlayer;

        Item.PriceType type = (Item.PriceType)JsonUtil.ToInt(data["t"]);
        int curent = JsonUtil.ToInt(data["c"]);
        player.UpdateResource(type, curent, true);
        int cost = JsonUtil.ToInt(data["lc"]);
        bool isPurchase = (type == Item.PriceType.Diamond) ? true : false;
        //statics iap - use
        if (!isPurchase)
        {
            Item.SpecialType specialType = Item.GetSpecialTypeByPriceType(type);
            StaticsManager.GetInstance()
                .UseItem(specialType.GetHashCode(), Item.GetItemTypeBySpecialType(specialType), cost,
                    StaticsManager.ConsumeModule.Shop);
        }

        ShopData shopData = player.shopData;
        JsonData arr = data["l"];
        for (int i = 0; i < arr.Count; i++)
        {
            //statics iap - purchase or reward
            if (isPurchase)
                PopSpecialReminder.Pop(HandlerRewardItem(arr[i], cost, StaticsManager.ConsumeType.Purchase,
                    StaticsManager.ConsumeModule.Shop));
            else
                PopSpecialReminder.Pop(HandlerRewardItem(arr[i], -1, StaticsManager.ConsumeType.Reward,
                    StaticsManager.ConsumeModule.Shop));
        }

        DealCallback(URL.ShopBuy, true);
    }

    public void InitShopList(ShoppingPanel.ShopType type, URLRequestCallBackDelegate callback = null,
        bool inBackground = false, bool requestOnce = false)
    {
        AddCallback(URL.Shop, callback);
        Session.GetInstance().myPlayer.shopData.type = type;
        URLRequestData data = new URLRequestData();
        data.Add("u", userId);
        data.Add("t", (int)type);
        URLRequest urlRequest = URLRequest.CreateURLRequest(urlDict[URL.Shop], data, URLRequest.Method.POST,
            OnHanlderInitShopList);
        if (inBackground)
            urlRequest.SetInBackgroundimmediately();
        if (requestOnce)
            urlRequest.SetRequestOnce();
    }

    public void RequestShopRefresh(ShoppingPanel.ShopType type, URLRequestCallBackDelegate callback = null)
    {
        AddCallback(URL.ShopRefresh, callback);
        Session.GetInstance().myPlayer.shopData.type = type;
        URLRequestData data = new URLRequestData();
        data.Add("t", (int)type);
        data.Add("u", userId);
        URLRequest.CreateURLRequest(urlDict[URL.ShopRefresh], data, URLRequest.Method.POST, OnHandlerShopRefresh);
    }

    private void OnHandlerShopRefresh(JsonData data)
    {
        if (CheckError(data, true))
        {
            DealCallback(URL.ShopRefresh, false);
            return;
        }

        if (HotfixManager.GetInstance().CheckHotfix())
        {
            return;
        }

        ParserShopRefreshData(data);
        DealCallback(URL.ShopRefresh, true);
    }

    private void ParserShopRefreshData(JsonData data)
    {
        Player player = Session.GetInstance().myPlayer;
        ShopData shopData = player.shopData;
        ShoppingData shoppingData = new ShoppingData();
        JsonData arr = data["l"];
        List<ShoppingItemData> shoppingItemDatas = new List<ShoppingItemData>();
        ShoppingItemData item = null;
        for (int i = 0; i < arr.Count; i++)
        {
            item = new ShoppingItemData();
            item.uid = JsonUtil.ToLong(arr[i]["i"]);
            item.index = JsonUtil.ToInt(arr[i]["n"]);
            item.id = JsonUtil.ToInt(arr[i]["t"]);
            item.type = (ItemType)JsonUtil.ToInt(arr[i]["p"]); //ShopItemType.Item;
            item.cost = JsonUtil.ToInt(arr[i]["c"]);
            item.costType = (Item.PriceType)JsonUtil.ToInt(arr[i]["s"]);
            item.count = JsonUtil.ToInt(arr[i]["tc"]);
            item.sell = JsonUtil.ContainKey(arr[i], "b") ? 1 : 0;
            shoppingItemDatas.Add(item);
        }

        shoppingData.datas = shoppingItemDatas;
        shoppingData.refreshCost = JsonUtil.ToInt(data["rc"]);
        shoppingData.refreshCostType = (Item.PriceType)JsonUtil.ToInt(data["rt"]);

        if (JsonUtil.ContainKey(data, "rd"))
        {
            shoppingData.nextRefreshTime = JsonUtil.ToInt(data["rd"]);
        }
        else if (shopData.data[shopData.type] != null)
        {
            shoppingData.nextRefreshTime = shopData.data[shopData.type].nextRefreshTime;
        }

        if (JsonUtil.ContainKey(data, "rs"))
        {
            shoppingData.cd = JsonUtil.ToInt(data["rs"]);
        }
        else if (shopData.data[shopData.type] != null)
        {
            shoppingData.cd = shopData.data[shopData.type].cd;
        }

        if (JsonUtil.ContainKey(data, "c"))
        {
            Item.PriceType type = shopData.data[shopData.type].refreshCostType;
            shoppingData.refreshCostType = type;
            int curent = JsonUtil.ToInt(data["c"]);
            player.UpdateResource(type, curent, true);

            //static iap - purchase or use item
            Item.SpecialType specialType = Item.GetSpecialTypeByPriceType(type);
            ItemType itemType = Item.GetItemTypeBySpecialType(specialType);
            if (type == Item.PriceType.Diamond)
                StaticsManager.GetInstance()
                    .PurchaseItem(-9999, ItemType.None, 0, JsonUtil.ToInt(data["lc"]),
                        StaticsManager.ConsumeModule.ShopRefresh);
            else
                StaticsManager.GetInstance()
                    .UseItem(specialType.GetHashCode(), itemType, JsonUtil.ToInt(data["lc"]),
                        StaticsManager.ConsumeModule.ShopRefresh);
        }
        shopData.data[shopData.type] = shoppingData;
    }

    private void OnHanlderInitShopList(JsonData data)
    {
        if (CheckError(data, true))
        {
            DealCallback(URL.Shop, false);
            return;
        }
        ParserShopRefreshData(data);
        DealCallback(URL.Shop, true);
    }

    public void ShopGashaponBuy(GashaponCard.Type type, int amont, bool isFree,
        URLRequestCallBackDelegate callback = null)
    {
        AddCallback(URL.ShopGashaponBuy, callback);
        URLRequestData data = new URLRequestData();
        data.Add("u", userId);
        data.Add("t", (int)type);
        data.Add("c", amont);
        data.Add("f", isFree ? 0 : 1);
        URLRequest.CreateURLRequest(urlDict[URL.ShopGashaponBuy], data, URLRequest.Method.POST, OnHanlderShopGashaponBuy,
            false);
    }

    public List<T> RandomSortList<T>(List<T> ListT)
    {
        System.Random random = new System.Random();
        List<T> newList = new List<T>();
        foreach (T item in ListT)
        {
            newList.Insert(random.Next(newList.Count + 1), item);
        }
        return newList;
    }

    private void OnHanlderShopGashaponBuy(JsonData data)
    {
        if (CheckError(data, true))
        {
            DealCallback(URL.ShopGashaponBuy, false);
            return;
        }

        Player player = Session.GetInstance().myPlayer;
        ShopData shopData = player.shopData;

        Item.PriceType type = (Item.PriceType)JsonUtil.ToInt(data["t"]);
        int curent = JsonUtil.ToInt(data["c"]);

        bool isFree = false;

        if (type == Item.PriceType.None)
            isFree = true;
        else
            player.UpdateResource(type, curent, true);

        int priceTotal = JsonUtil.ToInt(data["lc"]);
        int buyCount = JsonUtil.ToInt(data["tc"]);
        int priceSingle = priceTotal / buyCount;

        JsonData arr = data["l"];
        List<GetRewardPanelItemData> rewards = new List<GetRewardPanelItemData>();
        for (int i = 0; i < arr.Count; i++)
            rewards.Add(HandlerRewardItem(arr[i], -1, StaticsManager.ConsumeType.Reward,
                StaticsManager.ConsumeModule.Gashapon));

        rewards = RandomSortList(rewards);

        JsonData dropArr = data["g"];
        for (int k = 0; k < dropArr.Count; k++)
            HandlerRewardItem(dropArr[k], priceSingle, StaticsManager.ConsumeType.Purchase,
                StaticsManager.ConsumeModule.Gashapon);

        shopData.reward = rewards;
        shopData.isFree = isFree;
        DealCallback(URL.ShopGashaponBuy, true);
    }

    public GetRewardPanelItemData HandlerRewardItem(JsonData reward,
        int costAmount = -1,
        StaticsManager.ConsumeType consumeType = StaticsManager.ConsumeType.None,
        StaticsManager.ConsumeModule consumeModule = StaticsManager.ConsumeModule.None)
    {
        Player player = Session.GetInstance().myPlayer;
        ItemType type = (ItemType)JsonUtil.ToInt(reward["t"]);
        JsonData info = reward["a"]["s"][0];
        GetRewardPanelItemData data = new GetRewardPanelItemData();
        data.type = type;

        bool isPurchase = (costAmount >= 0 && consumeType == StaticsManager.ConsumeType.Purchase) ? true : false;

        switch (type)
        {
            case ItemType.Char:
                if (JsonUtil.ContainKey(reward["a"], "ch"))
                {
                    int cid = JsonUtil.ToInt(reward["a"]["ch"]);
                    long ch_uid = JsonUtil.ToLong(info["i"]);
                    int ch_item_id = JsonUtil.ToInt(info["m"]);
                    int ch_count = JsonUtil.ToInt(info["c"]);
                    data.id = cid;
                    data.decompose = new DecomposeItemData();
                    data.decompose.id = ch_item_id;
                    ItemData decomposeItemData = ItemManager.GetInstance().GetData(data.decompose.id);
                    Item decomposeItem = new Item(decomposeItemData);
                    data.decompose.count =
                        data.count =
                            ch_count - player.GetItemInventory(decomposeItem.GetType()).GetItemAmount(data.decompose.id);
                    player.SetItem(ch_uid, ch_item_id, ch_count);
                    //statics iap - purchase & reward
                    if (isPurchase)
                        StaticsManager.GetInstance()
                            .PurchaseItem(ch_item_id, ItemType.Item, ch_count, costAmount, consumeModule);
                    else
                        StaticsManager.GetInstance().RewardItem(ch_item_id, ItemType.Item, ch_count, consumeModule);
                }
                else
                {
                    int index = JsonUtil.ToInt(info["i"]);
                    int cid = JsonUtil.ToInt(info["c"]);
                    int wid = JsonUtil.ToInt(info["ei"]);
                    int wtid = JsonUtil.ToInt(info["e"]);
                    //角色uid
                    long uid1 = info.GetLong("rd");
                    AddCharToTeam(player, player.GetTeamDataList(), cid, index, wid, uid1);
                    player.GetEquipInventory().AddEquip(wid, wtid, true);
                    player.GetEquipInventory().UsedEquip(wid);
                    data.id = cid;
                    //statics iap - purchase & reward
                    if (isPurchase)
                        StaticsManager.GetInstance().PurchaseItem(cid, type, 1, costAmount, consumeModule);
                    else
                        StaticsManager.GetInstance().RewardItem(cid, type, 1, consumeModule);
                }
                break;
            case ItemType.Item:
                long item_uid = JsonUtil.ToLong(info["i"]);
                int count = JsonUtil.ToInt(info["c"]);
                int id = JsonUtil.ToInt(info["m"]);
                data.id = id;
                ItemData itemData = ItemManager.GetInstance().GetData(data.id);
                Item item = new Item(itemData);
                data.count = count - player.GetItemInventory(item.GetType()).GetItemAmount(id);
                player.SetItem(item_uid, id, count);
                //statics iap - purchase & reward
                if (isPurchase)
                    StaticsManager.GetInstance().PurchaseItem(id, type, count, costAmount, consumeModule);
                else
                    StaticsManager.GetInstance().RewardItem(id, type, count, consumeModule);
                break;
            case ItemType.Equip:
                long uid = JsonUtil.ToLong(info["i"]);
                int equipid = JsonUtil.ToInt(info["e"]);
                player.GetEquipInventory().AddEquip(uid, equipid);
                data.id = equipid;
                //statics iap - purchase & reward
                if (isPurchase)
                    StaticsManager.GetInstance().PurchaseItem(equipid, type, 1, costAmount, consumeModule);
                else
                    StaticsManager.GetInstance().RewardItem(equipid, type, 1, consumeModule);
                break;
            case ItemType.Coin:
                data.count = JsonUtil.ToInt(info["c"]) - player.coin;
                player.UpdateResource(Item.PriceType.Coin, data.count, false);
                //statics iap - purchase & reward
                if (isPurchase)
                    StaticsManager.GetInstance()
                        .PurchaseItem(Item.GetSpecialTypeByItemType(type).GetHashCode(), type, data.count, costAmount,
                            consumeModule);
                else
                    StaticsManager.GetInstance()
                        .RewardItem(Item.GetSpecialTypeByItemType(type).GetHashCode(), type, data.count, consumeModule);
                break;
            case ItemType.Coupon:
                data.count = JsonUtil.ToInt(info["c"]) - player.coupon;
                player.UpdateResource(Item.PriceType.Coupon, data.count, false);
                //statics iap - purchase & reward
                if (isPurchase)
                    StaticsManager.GetInstance()
                        .PurchaseItem(Item.GetSpecialTypeByItemType(type).GetHashCode(), type, data.count, costAmount,
                            consumeModule);
                else
                    StaticsManager.GetInstance()
                        .RewardItem(Item.GetSpecialTypeByItemType(type).GetHashCode(), type, data.count, consumeModule);
                break;
            case ItemType.Diamond:
                data.count = JsonUtil.ToInt(info["d"]) - player.diamond;
                player.UpdateResource(Item.PriceType.Diamond, data.count, false);
                //statics iap - purchase & reward
                if (isPurchase)
                    StaticsManager.GetInstance()
                        .PurchaseItem(Item.GetSpecialTypeByItemType(type).GetHashCode(), type, data.count, costAmount,
                            consumeModule);
                else
                    StaticsManager.GetInstance()
                        .RewardItem(Item.GetSpecialTypeByItemType(type).GetHashCode(), type, data.count, consumeModule);
                break;
            case ItemType.Physic:
                data.count = JsonUtil.ToInt(info["e"]) - player.physic;
                player.UpdateResource(Item.PriceType.Physic, data.count, false);
                //statics iap - purchase & reward
                if (isPurchase)
                    StaticsManager.GetInstance()
                        .PurchaseItem(Item.GetSpecialTypeByItemType(type).GetHashCode(), type, data.count, costAmount,
                            consumeModule);
                else
                    StaticsManager.GetInstance()
                        .RewardItem(Item.GetSpecialTypeByItemType(type).GetHashCode(), type, data.count, consumeModule);
                break;
            case ItemType.Exp:
                data.count = JsonUtil.ToInt(info["e"]["e"]) - player.expTotal;
                player.expTotalOld = player.expTotal;
                player.expTotal += data.count;
                player.levelOld = player.level;
                int[] playerlevels = player.UpdateLevel();
                if (!Session.GetInstance().IsInBattle())
                {    
                    if (playerlevels[0] != playerlevels[1])
                    {
                        AssetBundleUtil.GetInstance()
                            .GetEffectPrefab(AssetBundleUtil.Url.Effect, "tongyong/tongyong_DWshengji/RankUpAnim",
                                (GameObject obj) =>
                                {
                                    GameObject rankUpObj = GameObject.Instantiate(obj);
                                    //RankUpAnim rankUpAnim = rankUpObj.AddComponent<RankUpAnim>();
                                    RankUpAnim rankUpAnim = DllManager.instance.AddCompotent<RankUpAnim>(rankUpObj,
                                        "RankUpAnim");
                                    PopUpManager.AddToForwardCanvas(rankUpObj);
                                    rankUpAnim.SetRank(playerlevels[1]);
                                    UISound.Play(UISound.Type.RankUp);
                                });
                    }
                }
                if (TopUserInfo.GetTopUserInfo() != null)
                    TopUserInfo.GetTopUserInfo().UpdateExp();
                //statics iap - purchase & reward
                if (isPurchase)
                    StaticsManager.GetInstance()
                        .PurchaseItem(Item.GetSpecialTypeByItemType(type).GetHashCode(), type, data.count, costAmount,
                            consumeModule);
                else
                    StaticsManager.GetInstance()
                        .RewardItem(Item.GetSpecialTypeByItemType(type).GetHashCode(), type, data.count, consumeModule);
                break;
        }
        return data;
    }

    public void InitGashapon(URLRequestCallBackDelegate callback = null, bool inBackground = false,
        bool requestOnce = false)
    {
        AddCallback(URL.ShopGashapon, callback);
        URLRequestData data = new URLRequestData();
        data.Add("u", userId);
        URLRequest urlRequest = URLRequest.CreateURLRequest(urlDict[URL.ShopGashapon], data, URLRequest.Method.POST,
            OnHanlderInitGashapon, false);
        if (inBackground)
            urlRequest.SetInBackgroundimmediately();
        if (requestOnce)
            urlRequest.SetRequestOnce();
    }

    private void OnHanlderInitGashapon(JsonData data)
    {
        if (CheckError(data, true))
        {
            DealCallback(URL.ShopGashapon, false);
            return;
        }
        UpdateUrlTime(URL.ShopGashapon);
        Dictionary<GashaponCard.Type, GashaponData> gashaponData = new Dictionary<GashaponCard.Type, GashaponData>();
        Session.GetInstance().myPlayer.shopData.gashaponData = gashaponData;

        GashaponData item = null;
        JsonData arr = data["l"];
        for (int i = 0; i < arr.Count; i++)
        {
            item = new GashaponData();
            item.type = (GashaponCard.Type)JsonUtil.ToInt(arr[i]["p"]);
            item.discount = JsonUtil.ToInt(arr[i]["d"]);
            if (JsonUtil.ContainKey(arr[i], "dt"))
                item.disTxt = JsonUtil.ToInt(arr[i]["dt"]).ToString();
            item.buyOnePrice = JsonUtil.ToInt(arr[i]["c"]);
            item.priceType = (Item.PriceType)JsonUtil.ToInt(arr[i]["ct"]);
            item.maxFreeCount = JsonUtil.ToInt(arr[i]["f"]);
            item.buyTimes = JsonUtil.ToInt(arr[i]["fc"]);
            item.cdTotal = JsonUtil.ToInt(arr[i]["ft"]) * 60;
            item.cd = item.cdTotal - JsonUtil.ToInt(arr[i]["t"]);
            item.time = Time.realtimeSinceStartup;
            gashaponData.Add(item.type, item);
        }

        DealCallback(URL.ShopGashapon, true);
    }

    #endregion

    #region 邮箱系统

    public void InitMail(URLRequestCallBackDelegate callback = null, long id = -1, bool inBackground = false,
        bool requestOnce = false)
    {
        AddCallback(URL.MailInit, callback);
        URLRequestData data = new URLRequestData();
        data.Add("u", userId);
        data.Add("i", id);

        URLRequest urlRequest = URLRequest.CreateURLRequest(urlDict[URL.MailInit], data, URLRequest.Method.POST,
            OnHanlderInitMail, false);
        if (inBackground)
            urlRequest.SetInBackgroundimmediately();
        if (requestOnce)
            urlRequest.SetRequestOnce();
    }

    private void OnHanlderInitMail(JsonData data)
    {
        if (CheckError(data, true))
        {
            DealCallback(URL.MailInit, false);
            return;
        }

        if (!JsonUtil.ContainKey(data, "l"))
        {
            DealCallback(URL.MailInit, true);
            return;
        }

        MailData mailData = Session.GetInstance().myPlayer.mailData;
        JsonData arr = data["l"];
        List<MailItemData> rewards = new List<MailItemData>();
        for (int i = 0; i < arr.Count; i++)
        {
            MailItemData itemData = new MailItemData();
            itemData.id = JsonUtil.ToLong(arr[i]["i"]);
            itemData.type = JsonUtil.ToInt(arr[i]["t"]);
            itemData.name = arr[i]["l"].ToString();
            itemData.context = arr[i]["c"].ToString();
            //== for friend  msg
            string strId = arr[i]["si"].ToString();
            itemData.FriendId = JsonUtil.ToLong(strId);
            itemData.FriendName = arr[i]["sn"].ToString();
            itemData.timeStr = arr[i]["n"].ToString();

            itemData.isReaded = JsonUtil.ToInt(arr[i]["r"]) == 0 ? false : true;

            if (JsonUtil.ContainKey(arr[i], "w"))
            {
                itemData.isGeted = JsonUtil.ToInt(arr[i]["w"]) == 0 ? false : true;

                JsonData gifts = arr[i]["a"];

                if (gifts.Count > 0)
                {
                    itemData.rewards = new List<MailItemRewardData>();
                    for (int k = 0; k < gifts.Count; k++)
                    {
                        MailItemRewardData gift = new MailItemRewardData();
                        gift.type = (ItemType)JsonUtil.ToInt(gifts[k]["t"]);
                        gift.count = JsonUtil.ToInt(gifts[k]["c"]);
                        if (JsonUtil.ContainKey(gifts[k], "m"))
                        {
                            gift.id = JsonUtil.ToInt(gifts[k]["m"]);
                        }
                        itemData.rewards.Add(gift);
                    }
                }
            }

            mailData.AddMailItemData(itemData);
        }
        mailData.SortMail();
        DealCallback(URL.MailInit, true);

        CheckData();
    }

    public void CheckData()
    {
        int current = DateTime.Now.Day;
        Debug.Log("check SignIn" + current + " ==== >>  " + GetDataDay(URL.SignIn));
        if (GetDataDay(URL.SignIn) != -1 && GetDataDay(URL.SignIn) != current)
        {
            InitSignIn();
        }

        Debug.Log("check ShopGashapon" + current + " ==== >>  " + GetDataDay(URL.ShopGashapon));
        if (GetDataDay(URL.ShopGashapon) != -1 && GetDataDay(URL.ShopGashapon) != current)
        {
            InitGashapon();
        }

        Debug.Log("check ActivityCrusadeInit" + current + " ==== >>  " + GetDataDay(URL.CrusadeInit));
        if (GetDataDay(URL.CrusadeInit) != -1 && GetDataDay(URL.CrusadeInit) != current)
        {
            ActivityCrusadeInit();
        }

    }

    public void MailDelete(long id, URLRequestCallBackDelegate callback = null)
    {
        if (id == -1)
        {
            MailDeleteReal(new long[] { }, callback);

            return;
        }
        MailDeleteReal(new[] { id }, callback);
    }

    private void MailDeleteReal(long[] ids, URLRequestCallBackDelegate callback = null)
    {
        AddCallback(URL.MailDelete, callback);
        URLRequestData data = new URLRequestData();
        data.Add("u", userId);
        data.Add("i", JsonUtil.LongArrToJson(ids));
        //data.Add("li", lastId);
        URLRequest.CreateURLRequest(urlDict[URL.MailDelete], data, URLRequest.Method.POST, OnHanlderMailDelete);

    }

    public void MailDeletFriend(long fid, string fname, Action<JsonData> OnData)
    {
        var url = URL.MailDelete;
        var t = Session.GetInstance().myPlayer.mailData.FriendsMails.GetFriendMailData(fid, fname);
        var ids = t.GetFriendToMeIds();
        EasyRequest(url, OnData, "i", JsonUtil.LongArrToJson(ids));



    }

    private void OnHanlderMailDelete(JsonData data)
    {
        if (CheckError(data, true))
        {
            DealCallback(URL.MailDelete, false);
            return;
        }
        DealCallback(URL.MailDelete, true);
    }

    public void MailRead(long id)
    {
        URLRequestData data = new URLRequestData();
        data.Add("u", userId);
        data.Add("i", id);
        URLRequest.CreateURLRequest(urlDict[URL.MailRead], data, URLRequest.Method.POST, OnHanlderMailRead);
    }

    private void OnHanlderMailRead(JsonData data)
    {

    }

    public void MailGet(long id, URLRequestCallBackDelegate callback = null)
    {
        //OnHanlderMailGet(null);return;
        AddCallback(URL.MailGet, callback);
        URLRequestData data = new URLRequestData();
        data.Add("u", userId);
        data.Add("i", id);
        URLRequest.CreateURLRequest(urlDict[URL.MailGet], data, URLRequest.Method.POST, OnHanlderMailGet);
    }

    private void OnHanlderMailGet(JsonData data)
    {
        //string d = "{ \"i\":\"4\",\"l\":[{\"a\":{\"s\":[{\"i\":\"4876\",\"c\":82,\"m\":\"7001\"}]},\"t\":3},{\"a\":{\"s\":[{\"c\":10200}]},\"t\":4},{\"a\":{\"s\":[{\"i\":\"4883\",\"c\":151,\"m\":\"7007\"}],\"ch\":\"2\"},\"t\":1}]}";

        //data = JsonMapper.ToObject(d);
        if (CheckError(data, true))
        {
            DealCallback(URL.MailGet, false);
            return;
        }

        MailData mailData = Session.GetInstance().myPlayer.mailData;
        mailData.rewawrdID = JsonUtil.ToInt(data["i"]);
        mailData.rewards = new List<GetRewardPanelItemData>();
        JsonData arr = data["l"];
        for (int i = 0; i < arr.Count; i++)
        {
            mailData.rewards.Add(HandlerRewardItem(arr[i], -1, StaticsManager.ConsumeType.Reward,
                StaticsManager.ConsumeModule.Mail));
        }
        DealCallback(URL.MailGet, true);

        if (HotfixManager.GetInstance().CheckHotfix())
        {
            return;
        }
    }

    #endregion

    #region 试炼

    public void TrialInit(URLRequestCallBackDelegate callback = null, int time = 0, bool inBackground = false,
        bool requestOnce = false)
    {
        if (callback != null)
            AddCallback(URL.TrialInit, callback);

        if (time == 0)
            Session.GetInstance().myPlayer.trialData.infos.Clear();

        Session.GetInstance().myPlayer.trialData.lastRequestTime = Time.realtimeSinceStartup;

        URLRequestData data = new URLRequestData();
        data.Add("u", userId);
        data.Add("t", time);
        URLRequest urlRequest = URLRequest.CreateURLRequest(urlDict[URL.TrialInit], data, URLRequest.Method.POST,
            OnHandlerTrialInit, false);
        if (inBackground)
            urlRequest.SetInBackgroundimmediately();
        if (requestOnce)
            urlRequest.SetRequestOnce();
    }

    private void OnHandlerTrialInit(JsonData data)
    {
        if (CheckError(data, true))
        {
            DealCallback(URL.TrialInit, false);
            return;
        }
        TrialData.INIT = true;
        TrialData trialData = Session.GetInstance().myPlayer.trialData;
        trialData.safeCD = JsonUtil.ToInt(data["st"]);
        trialData.safeTime = Time.realtimeSinceStartup;

        trialData.plunderTimes = JsonUtil.ToInt(data["rt"]);
        trialData.plunderCD = JsonUtil.ToInt(data["rtt"]);
        trialData.plunderTime = Time.realtimeSinceStartup;
        trialData.lastTime = JsonUtil.ToInt(data["lt"]);

        JsonData arr = data["l"];
        for (int i = 0; i < arr.Count; i++)
        {
            trialData.AddInfo(PaserTrialInfoItemData(arr[i]));
        }
        trialData.plunderDatas = GetCommonItemDatas(data["al"]);
        trialData.remainRewardDatas = GetCommonItemDatas(data["dl"]);

        trialData.infos.Sort((a, b) =>
        {
            if (a.type == b.type)
            {
                return a.id > b.id ? -1 : 1;
            }
            else
            {
                int type1 = (int)a.type;
                int type2 = (int)b.type;
                return type1 > type2 ? 1 : -1;
            }
        }
            );


        DealCallback(URL.TrialInit, true);
    }

    private TrialInfoItemData PaserTrialInfoItemData(JsonData data)
    {
        TrialInfoItemData item = new TrialInfoItemData();
        item.type = (TrialInfoItemEnun)JsonUtil.ToInt(data["t"]);
        item.id = JsonUtil.ToLong(data["i"]);
        item.rid = JsonUtil.ToLong(data["r"]);
        if (JsonUtil.ContainKey(data, "m"))
        {
            item.items = Inventory.ParserCommonItemData(data["m"].ToString());
        }

        string pinfo = data["ri"].ToString();

        string[] parr = StringUtil.Split(pinfo, '|');
        if (parr.Length > 1)
        {
            item.name = parr[0];
            item.level = Player.GetCharLevelByExp(int.Parse(parr[1]));
            item.starCount = int.Parse(parr[2]);
            item.attack = int.Parse(parr[3]);
            item.teamInfo = parr[4];
            item.weaponIds = StringUtil.SplitToInt(parr[5], ':');
        }
        else
        {
            try
            {
                JsonData json = JsonMapper.ToObject(pinfo);
                PaserTrialInfoItemDataByJsonData(json, item);
            }
            catch (System.Exception e)
            {
                //TODO exception
            }
        }
        return item;
    }

    public void TrailPlunder(URLRequestCallBackDelegate callback = null)
    {
        AddCallback(URL.TrialPlunder, callback);
        URLRequestData data = new URLRequestData();
        data.Add("u", userId);
        URLRequest.CreateURLRequest(urlDict[URL.TrialPlunder], data, URLRequest.Method.POST, OnHandlerTrailPlunder,
            false);
    }

    private void UpdatePlayerResouce(JsonData data)
    {
        Item.PriceType type = (Item.PriceType)JsonUtil.ToInt(data["t"]);
        int curent = JsonUtil.ToInt(data["c"]);
        Session.GetInstance().myPlayer.UpdateResource(type, curent, true);
    }

    private void OnHandlerTrailPlunder(JsonData data)
    {
        if (CheckError(data, true))
        {
            DealCallback(URL.TrialPlunder, false);
            return;
        }
        UpdatePlayerResouce(data);
        Session.GetInstance().myPlayer.trialData.searchPlunderPlayer = PaserTrialInfoItemDataByJsonData(data);

        DealCallback(URL.TrialPlunder, true);
    }

    private TrialInfoItemData PaserTrialInfoItemDataByJsonData(JsonData data, TrialInfoItemData item = null)
    {
        if (item == null)
            item = new TrialInfoItemData();
        item.rid = JsonUtil.ToLong(data["u"]);
        item.name = data["n"].ToString();
        item.level = Player.GetCharLevelByExp(JsonUtil.ToInt(data["x"]));
        item.starCount = JsonUtil.ToInt(data["r"]);

        JsonData arr = data["m"];
        int length = arr.Count > 4 ? 4 : arr.Count;
        item.chares = new int[length];
        item.weaponIds = new int[length];
        for (int i = 0; i < length; i++)
        {
            if (JsonUtil.ContainKey(arr[i], "i"))
            {
                int index = JsonUtil.ToInt(arr[i]["i"]);
            }
            int cid = JsonUtil.ToInt(arr[i]["c"]);
            int wid = JsonUtil.ToInt(arr[i]["e"]);

            item.chares[i] = cid;
            item.weaponIds[i] = wid;
            item.attack += GetPower(item.level, cid, wid);
        }
        item.charId = item.chares[0];
        return item;
    }

    private int GetPower(int level, int cid, int wid)
    {
        CharWrap charWrap = CharManager.GetInstance().CreateWrap(cid, level);
        EquipmentData weaponData = EquipmentManager.GetInstance().GetData(wid);
        return
            (int)
                Mathf.Floor(charWrap.GetPower(Session.GetInstance().myPlayer.GetCurrentTeamData().GetAvatarData(cid)) +
                            weaponData.GetPower(true));
    }

    public void TrailRobBegin(long rid, long uid, URLRequestCallBackDelegate callback = null)
    {
        AddCallback(URL.TrialRobBegin, callback);

        URLRequestData data = new URLRequestData();
        data.Add("u", userId);
        data.Add("d", rid);
        data.Add("v", uid);
        data.Add("r", Session.GetInstance().myPlayer.GetTrialRobInfo());
        URLRequest urlRequest = URLRequest.CreateURLRequest(urlDict[URL.TrialRobBegin], data, URLRequest.Method.POST,
            OnHandlerTrailRobBegin);
        urlRequest.useBackground = true;
        urlRequest.errorPostfix = "_drop";
    }

    private void OnHandlerTrailRobBegin(JsonData data)
    {
        if (CheckError(data, true))
        {
            DealCallback(URL.TrialRobBegin, false);
            return;
        }
        ActivityTrailManager.GetInstance().trailBattleData.SetReward(JsonUtil.ToLong(data["i"]), data["r"].ToString());
        DealCallback(URL.TrialRobBegin, true);
    }

    public void TrailRobEnd(bool win, long infoId, long revengeId, int coin, URLRequestCallBackDelegate callback = null)
    {
        AddCallback(URL.TrialRobEnd, callback);

        URLRequestData data = new URLRequestData();
        data.Add("u", userId);
        data.Add("w", win ? 1 : 0);
        data.Add("i", infoId);
        data.Add("v", revengeId);
        data.Add("c", coin);
        URLRequest urlrequest = URLRequest.CreateURLRequest(urlDict[URL.TrialRobEnd], data, URLRequest.Method.POST,
            OnHandlerTrailRobEnd, false);
        if (!win)
            urlrequest.SetInBackgroundimmediately();
    }

    private void OnHandlerTrailRobEnd(JsonData data)
    {
        if (CheckError(data, true))
        {
            DealCallback(URL.TrialRobEnd, false);
            return;
        }
        if (JsonUtil.ContainKey(data, "l"))
        {
            JsonData arr = data["l"];
            for (int i = 0; i < arr.Count; i++)
            {
                HandlerRewardItem(arr[i], -1, StaticsManager.ConsumeType.Reward, StaticsManager.ConsumeModule.Trial);
            }
        }

        if (JsonUtil.ContainKey(data, "c"))
        {
            Player player = Session.GetInstance().myPlayer;
            player.UpdateResource(Item.PriceType.Coin, JsonUtil.ToInt(data["c"]), true);
        }

        DealCallback(URL.TrialRobEnd, true);
    }


    public void TrialStatistics(URLRequestCallBackDelegate callback = null, bool inBackground = false,
        bool requestOnce = false)
    {
        AddCallback(URL.TrialStatistics, callback);
        URLRequestData data = new URLRequestData();
        data.Add("u", userId);
        URLRequest urlRequest = URLRequest.CreateURLRequest(urlDict[URL.TrialStatistics], data, URLRequest.Method.POST,
            OnHandlerTrialStatistics);
        if (inBackground)
            urlRequest.SetInBackgroundimmediately();
        if (requestOnce)
            urlRequest.SetRequestOnce();
    }

    private void OnHandlerTrialStatistics(JsonData data)
    {
        if (CheckError(data, true))
        {
            DealCallback(URL.TrialStatistics, false);
            return;
        }

        TrialStatisticsData trialStatisticsData = new TrialStatisticsData();
        Session.GetInstance().myPlayer.trialStatisticsData = trialStatisticsData;

        trialStatisticsData.isGet = JsonUtil.ToInt(data["r"]) == 0 ? false : true;
        trialStatisticsData.defFailTotalTimes = JsonUtil.ToInt(data["dc"]);
        trialStatisticsData.canGetRewards = GetCommonItemDatas(data["dl"]);
        trialStatisticsData.plunderTotalTimes = JsonUtil.ToInt(data["ac"]);
        if (trialStatisticsData.plunderTotalTimes > 0)
            trialStatisticsData.plunderRewards = GetCommonItemDatas(data["al"]);
        DealCallback(URL.TrialStatistics, true);
    }

    public void TrialGetReward()
    {
        //AddCallback(URL.TrialGetReward, callback);
        URLRequestData data = new URLRequestData();
        data.Add("u", userId);
        URLRequest.CreateURLRequest(urlDict[URL.TrialGetReward], data, URLRequest.Method.POST, OnHandlerTrialGetReward);
    }

    private void OnHandlerTrialGetReward(JsonData data)
    {
        if (CheckError(data, true))
        {
            //DealCallback(URL.TrialGetReward, false);
            return;
        }

        JsonData arr = data["l"];
        for (int i = 0; i < arr.Count; i++)
        {
            HandlerRewardItem(arr[i], -1, StaticsManager.ConsumeType.Reward, StaticsManager.ConsumeModule.TrialReward);
        }
        Session.GetInstance().myPlayer.trialStatisticsData.isGet = true;

        if (HotfixManager.GetInstance().CheckHotfix())
        {
            return;
        }
    }

    private List<CommonItemData> GetCommonItemDatas(JsonData arr)
    {
        List<CommonItemData> datas = new List<CommonItemData>();
        for (int i = 0; i < arr.Count; i++)
        {
            CommonItemData data = new CommonItemData();

            if (JsonUtil.ContainKey(arr[i], "i"))
                data.index = JsonUtil.ToInt(arr[i]["i"]);
            data.type = (ItemType)JsonUtil.ToInt(arr[i]["t"]);
            data.count = JsonUtil.ToInt(arr[i]["c"]);
            if (JsonUtil.ContainKey(arr[i], "m"))
                data.id = JsonUtil.ToInt(arr[i]["m"]);
            datas.Add(data);
        }
        return datas;
    }



    #endregion


    #region 签到

    public void InitSignIn(URLRequestCallBackDelegate callback = null, bool inBackground = false,
        bool requestOnce = false)
    {
        AddCallback(URL.SignIn, callback);
        URLRequestData data = new URLRequestData();
        data.Add("u", userId);
        URLRequest urlRequest = URLRequest.CreateURLRequest(urlDict[URL.SignIn], data, URLRequest.Method.POST,
            OnHanlderInitSignIn, false);
        if (inBackground)
            urlRequest.SetInBackgroundimmediately();
        if (requestOnce)
            urlRequest.SetRequestOnce();
    }

    private void OnHanlderInitSignIn(JsonData data)
    {
        if (CheckError(data, true))
        {
            DealCallback(URL.SignIn, false);
            return;
        }

        UpdateUrlTime(URL.SignIn);

        SignInData signInData = new SignInData();
        signInData.SetSignSerialData(data);

        JsonData arr = data["l"];
        for (int i = 0; i < arr.Count; i++)
        {
            SignInUIItemData item = new SignInUIItemData();
            CommonItemData commonItem = new CommonItemData();
            commonItem.type = (ItemType)JsonUtil.ToInt(arr[i]["t"]);
            commonItem.count = JsonUtil.ToInt(arr[i]["c"]);
            commonItem.id = JsonUtil.ToInt(arr[i]["m"]);
            item.itemData = commonItem;
            item.isGeted = JsonUtil.ToInt(arr[i]["f"]) > 0;
            item.isCurrent = JsonUtil.ContainKey(arr[i], "d");
            item.id = JsonUtil.ToLong(arr[i]["i"]);
            item.newId = JsonUtil.ToLong(arr[i]["id"]);
            if (item.isCurrent)
            {
                signInData.day = i;
                signInData.isGeted = item.isGeted;
            }
            signInData.datas.Add(item);
        }
        Session.GetInstance().myPlayer.signInData = signInData;
        DealCallback(URL.SignIn, true);
    }

    private SignInUIItemData signInUIItemData;

    public void InitSignInSign(SignInUIItemData item, URLRequestCallBackDelegate callback = null)
    {
        signInUIItemData = item;
        AddCallback(URL.SignIn_Sign, callback);
        URLRequestData data = new URLRequestData();
        data.Add("u", userId);
        data.Add("i", item.newId);
        URLRequest.CreateURLRequest(urlDict[URL.SignIn_Sign], data, URLRequest.Method.POST, OnHanlderInitSignInSign);
    }

    private void OnHanlderInitSignInSign(JsonData data)
    {
        if (CheckError(data, true))
        {
            DealCallback(URL.SignIn_Sign, false);
            return;
        }
        JsonData arr = data["l"];
        for (int i = 0; i < arr.Count; i++)
        {
            PopSpecialReminder.Pop(HandlerRewardItem(arr[i], -1, StaticsManager.ConsumeType.Reward,
                StaticsManager.ConsumeModule.Checkin));
        }
        DealCallback(URL.SignIn_Sign, true);

        HotfixManager.GetInstance().CheckHotfix();
    }

    #endregion


    public void CheckCDKey(string key, URLRequestCallBackDelegate callback = null)
    {
        AddCallback(URL.CdKeyCheck, callback);
        URLRequestData data = new URLRequestData();

        data.Add("k", key);
        data.Add("u", userId);

        URLRequest.CreateURLRequest(urlDict[URL.CdKeyCheck], data, URLRequest.Method.POST, OnCheckCDKey);
    }

    private void OnCheckCDKey(JsonData data)
    {
        if (CheckError(data, true))
        {
            DealCallback(URL.CdKeyCheck, false);
            return;
        }

        JsonData arr = data["l"];
        List<GetRewardPanelItemData> rewards = new List<GetRewardPanelItemData>();
        for (int i = 0; i < arr.Count; i++)
            rewards.Add(HandlerRewardItem(arr[i], -1, StaticsManager.ConsumeType.Reward,
                StaticsManager.ConsumeModule.Cdkey));

        GetRewardAnimManager.GetInstance().ShowRewardPanel(rewards);

        DealCallback(URL.CdKeyCheck, true);
    }



    public void GetAnnounceUrl(URLRequestCallBackDelegate callback = null)
    {
        AddCallback(URL.AnnounceUrl, callback);
        URLRequestData data = new URLRequestData();

        data.Add("u", userId);

        URLRequest urlRequest = URLRequest.CreateURLRequest(urlDict[URL.AnnounceUrl], data, URLRequest.Method.POST,
            OnGetAnnounceUrl);
        urlRequest.SetRequestOnce();
        urlRequest.SetInBackgroundimmediately();
    }

    private void OnGetAnnounceUrl(JsonData data)
    {
        if (CheckError(data, true))
        {
            DealCallback(URL.AnnounceUrl, false);
            return;
        }

        string url = data["m"].ToString();
        AnnouncePanel.Show(url);

        DealCallback(URL.AnnounceUrl, true);
    }




    public void GetMatchInfo(URLRequestCallBackDelegate callback = null)
    {
        AddCallback(URL.MatchInfo, callback);
        URLRequestData data = new URLRequestData();
        data.Add("u", userId);
        URLRequest.CreateURLRequest(urlDict[URL.MatchInfo], data, URLRequest.Method.POST, OnGetMatchInfo);
    }

    private void OnGetMatchInfo(JsonData data)
    {
        if (CheckError(data, true))
        {
            DealCallback(URL.MatchInfo, false);
            return;
        }

        JsonData arr = data["m"];
        Player myPlayer = Session.GetInstance().myPlayer;
        myPlayer.matchBattleCount = JsonUtil.ToInt(data["f"]);
        myPlayer.matchScore = JsonUtil.ToInt(data["s"]);
        myPlayer.matchPoint = JsonUtil.ToInt(data["p"]);
        myPlayer.matchInitDay = System.DateTime.Now.Day;

        DealCallback(URL.SignIn, true);
    }



    public void SellEquip(long uid, URLRequestCallBackDelegate callback = null)
    {
        AddCallback(URL.EquipSell, callback);
        URLRequestData data = new URLRequestData();

        data.Add("u", userId);
        data.Add("i", uid);

        URLRequest.CreateURLRequest(urlDict[URL.EquipSell], data, URLRequest.Method.POST, OnSellEquip);
    }

    private void OnSellEquip(JsonData data)
    {
        if (CheckError(data, true))
        {
            DealCallback(URL.EquipSell, false);
            return;
        }

        Player player = Session.GetInstance().myPlayer;
        long uid = JsonUtil.ToLong(data["i"]);
        int price = JsonUtil.ToInt(data["c"]);

        //statics iap - use item
        StaticsManager.GetInstance()
            .UseItem(Session.GetInstance().myPlayer.GetEquipInventory().GetEquipByUid(uid).equipId, ItemType.Equip, 1,
                StaticsManager.ConsumeModule.Sell);

        //statics iap - reward item
        StaticsManager.GetInstance()
            .RewardItem(Item.SpecialType.Coin.GetHashCode(), ItemType.Coin, JsonUtil.ToInt(data["ac"]),
                StaticsManager.ConsumeModule.Sell);

        player.GetEquipInventory().RemoveEquip(uid);
        player.UpdateResource(Item.PriceType.Coin, price, true);

        DealCallback(URL.EquipSell, true);

        if (HotfixManager.GetInstance().CheckHotfix())
        {
            return;
        }
    }





    public void SellItem(long uid, int id, int count, URLRequestCallBackDelegate callback = null)
    {
        AddCallback(URL.ItemSell, callback);
        URLRequestData data = new URLRequestData();

        data.Add("u", userId);
        data.Add("i", uid);
        data.Add("m", id);
        data.Add("c", count);

        URLRequest.CreateURLRequest(urlDict[URL.ItemSell], data, URLRequest.Method.POST, OnSellItem);
    }

    private void OnSellItem(JsonData data)
    {
        if (CheckError(data, true))
        {
            DealCallback(URL.ItemSell, false);
            return;
        }

        Player player = Session.GetInstance().myPlayer;
        long itemUid = JsonUtil.ToLong(data["i"]);
        int itemId = JsonUtil.ToInt(data["m"]);
        int itemAmount = JsonUtil.ToInt(data["c"]);
        Item.PriceType priceType = Item.GetPriceTypeByHashCode(JsonUtil.ToInt(data["t"]));
        int priceCount = JsonUtil.ToInt(data["tc"]);
        player.SetItem(itemUid, itemId, itemAmount);
        player.UpdateResource(priceType, priceCount, true);

        //statics iap - use item
        StaticsManager.GetInstance()
            .UseItem(itemId, ItemType.Item, JsonUtil.ToInt(data["n"]), StaticsManager.ConsumeModule.Sell);
        //statics iap - reward item
        Item.SpecialType specialType = Item.GetSpecialTypeByPriceType(priceType);
        ItemType itemType = Item.GetItemTypeBySpecialType(specialType);
        StaticsManager.GetInstance()
            .RewardItem(specialType.GetHashCode(), itemType, JsonUtil.ToInt(data["ac"]),
                StaticsManager.ConsumeModule.Sell);

        DealCallback(URL.ItemSell, true);

        if (HotfixManager.GetInstance().CheckHotfix())
        {
            return;
        }
    }




    public void ComposeItem(long uid, int id, int targetCount, URLRequestCallBackDelegate callback = null)
    {
        AddCallback(URL.ItemCompose, callback);
        URLRequestData data = new URLRequestData();

        data.Add("u", userId);
        data.Add("i", uid);
        data.Add("m", id);
        data.Add("c", targetCount);

        URLRequest.CreateURLRequest(urlDict[URL.ItemCompose], data, URLRequest.Method.POST, OnComposeItem);
    }

    private void OnComposeItem(JsonData data)
    {
        if (CheckError(data, true))
        {
            DealCallback(URL.ItemCompose, false);
            return;
        }
        OnCompose1(data);
        //GetTeamList();
    }

    private void OnCompose1(JsonData data)
    {
        Player player = Session.GetInstance().myPlayer;
        long itemUid = JsonUtil.ToLong(data["mi"]);
        int itemId = JsonUtil.ToInt(data["m"]);
        int itemAmount = JsonUtil.ToInt(data["mc"]);
        Item.PriceType priceType = Item.GetPriceTypeByHashCode(JsonUtil.ToInt(data["t"]));
        int priceCount = JsonUtil.ToInt(data["c"]);
        player.SetItem(itemUid, itemId, itemAmount);
        player.UpdateResource(priceType, priceCount, true);

        //statics iap - use item
        Item.SpecialType specialType = Item.GetSpecialTypeByPriceType(priceType);
        ItemType itemType = Item.GetItemTypeBySpecialType(specialType);
        StaticsManager.GetInstance()
            .UseItem(specialType.GetHashCode(), itemType, JsonUtil.ToInt(data["lc"]),
                StaticsManager.ConsumeModule.Compose);
        StaticsManager.GetInstance()
            .UseItem(itemId, ItemType.Item, JsonUtil.ToInt(data["ml"]), StaticsManager.ConsumeModule.Compose);

        JsonData arr = data["l"];
        for (int i = 0; i < arr.Count; i++)
        {
            JsonData o = arr[i];
            HandlerRewardItem(o, -1, StaticsManager.ConsumeType.Reward, StaticsManager.ConsumeModule.Compose);
        }

        DealCallback(URL.ItemCompose, true);
    }

    public void GetAssetBundlesXML(string v, string f, URLRequestCallBackDelegate callBack = null)
    {

    }




    public void UserSign(string type)
    {
        URLRequestData data = new URLRequestData();
        data.Add("u", userId);
        data.Add("t", type);
        URLRequest.CreateURLRequest(urlDict[URL.UserSign], data, URLRequest.Method.POST, OnUserSign);
    }

    private void OnUserSign(JsonData data)
    {
        if (CheckError(data, true))
        {
            DealCallback(URL.ItemCompose, false);
            return;
        }

        string type = data["t"].ToString();
        int count = JsonUtil.ToInt(data["n"]);
        int timeLeft = JsonUtil.ToInt(data["m"]);
        SaveManager.GetSaver().OnUserSign(type, count, timeLeft);
    }


    public void SavePhysic(Player player)
    {
        URLRequestData data = new URLRequestData();
        data.Add("u", userId);
        //data.Add("e", player.physic);
        //data.Add("t", player.physicLastUpdate.Ticks.ToString());
        URLRequest urlRequest = URLRequest.CreateURLRequest(urlDict[URL.Physic], data, URLRequest.Method.POST, OnPhysic,
            false);
        urlRequest.SetInBackgroundimmediately();
    }
    public void TopBarTryUpdatePhysic()
    {
        SavePhysic(null);
    }

    private void OnPhysic(JsonData data)
    {
        CheckError(data, true);

        if (data == null)
            return;

        JsonData date = data["t"];
        Session.GetInstance().myPlayer.PhysicLastUpdateOverTime = data.GetInt("d");
        int physicTotal = JsonUtil.ToInt(data["e"]);
        DateTime dateTime = DateTime.Now;
        if (JsonUtil.ToInt(date["y"]) > 1970)
            dateTime = new DateTime(JsonUtil.ToInt(date["y"]), JsonUtil.ToInt(date["m"]), JsonUtil.ToInt(date["d"]),
                JsonUtil.ToInt(date["h"]), JsonUtil.ToInt(date["i"]), JsonUtil.ToInt(date["s"]));
        if (physicTotal >= 0)
            UpdatePhysic(dateTime, physicTotal);
    }

    private void UpdatePhysic(DateTime dateTime, int physicTotal)
    {
        Player player = Session.GetInstance().myPlayer;
        player.physic = physicTotal;
        player.physicLastUpdate = dateTime;
    }


    public void ActivityCrusadeInit(URLRequestCallBackDelegate callback = null, bool inBackground = false,
        bool requestOnce = false)
    {
        AddCallback(URL.CrusadeInit, callback);

        URLRequestData data = new URLRequestData();
        data.Add("u", userId);
        data.Add("l", Session.GetInstance().myPlayer.level);

        URLRequest urlRequest = URLRequest.CreateURLRequest(urlDict[URL.CrusadeInit], data, URLRequest.Method.POST,
            OnActivityCrusadeInit, false);
        if (inBackground)
            urlRequest.SetInBackgroundimmediately();
        if (requestOnce)
            urlRequest.SetRequestOnce();
    }

    private Dictionary<URL, int> requestTimeDay = new Dictionary<URL, int>();

    private void UpdateUrlTime(URL url)
    {
        requestTimeDay[url] = System.DateTime.Now.Day;
    }


    public int GetDataDay(URL url)
    {
        int day = -1;
        requestTimeDay.TryGetValue(url, out day);
        return day;
    }

    public bool NeedRequestData(URL url)
    {
        int day = -1;
        if (requestTimeDay.TryGetValue(url, out day))
        {
            if (day == DateTime.Now.Day)
            {
                return false;
            }
        }

        return true;
    }

    private void OnActivityCrusadeInit(JsonData data)
    {
        if (CheckError(data, true))
        {
            DealCallback(URL.CrusadeInit, false);
            return;
        }
        UpdateUrlTime(URL.CrusadeInit);

        int level = Session.GetInstance().myPlayer.level;
        int week = JsonUtil.ToInt(data["d"]);
        int passed = JsonUtil.ToInt(data["p"]);
        long punish = JsonUtil.ToLong(data["c"]);
        int[] mons = StringUtil.SplitToInt(data["m"].ToString(), '|');

        List<CommonItemData> itemList = GetCommonItemDatas(data["l"]);

        ActivityCrusadeManager.GetInstance().CreateMapLevel(level, week, passed, punish, mons, itemList);

        DealCallback(URL.CrusadeInit, true);

        if (mons.Length == 0)
            ActivityCrusadeSaveMonster();
    }

    private void ActivityCrusadeSaveMonster()
    {
        URLRequestData data = new URLRequestData();
        data.Add("u", userId);

        string mons = StringUtil.Join(ActivityCrusadeManager.GetInstance().GetMapLevelByIndex(0).GetNpcs(), "|");
        data.Add("m", mons);

        URLRequest.CreateURLRequest(urlDict[URL.CrusadeMonster], data, URLRequest.Method.POST,
            OnActivityCrusadeSaveMonster);
    }

    private void OnActivityCrusadeSaveMonster(JsonData data)
    {
        if (CheckError(data, true))
        {
            DealCallback(URL.CrusadeMonster, false);
            return;
        }

        DealCallback(URL.CrusadeMonster, true);
    }



    public void ActivityCrusadePass(int pass, long punish, int coin, URLRequestCallBackDelegate callback = null)
    {
        AddCallback(URL.CrusadePass, callback);

        URLRequestData data = new URLRequestData();
        data.Add("u", userId);
        data.Add("p", pass);
        data.Add("t", punish);
        data.Add("c", coin);

        bool found = false;
        int start = punish == 0 ? pass + 1 : pass;
        List<ActivityCrusadeMapLevel> mapLevelList = ActivityCrusadeManager.GetInstance().GetMapLevelList();
        for (int i = start; i < mapLevelList.Count; i++)
        {
            ActivityCrusadeMapLevel mapLevel = mapLevelList[i];
            if (mapLevel.mapLevel != null)
            {
                string mons = StringUtil.Join(mapLevel.mapLevel.GetNpcs(), "|");
                data.Add("m", mons);
                found = true;
                break;
            }
        }

        if (!found)
            data.Add("m", "");


        URLRequest.CreateURLRequest(urlDict[URL.CrusadePass], data, URLRequest.Method.POST, OnActivityCrusadePass, false);
    }

    private void OnActivityCrusadePass(JsonData data)
    {
        if (CheckError(data, true, (AlertCloseEvent evt) =>
        {
            if (Session.GetInstance().IsInBattle())
                Session.GetInstance().GetMessageManager().ExitBattle();
        }))
        {
            DealCallback(URL.CrusadePass, false);
            return;
        }

        int passed = JsonUtil.ToInt(data["p"]);
        long punish = JsonUtil.ToLong(data["t"]);

        if (JsonUtil.ContainKey(data, "c"))
        {
            Player player = Session.GetInstance().myPlayer;
            player.UpdateResource(Item.PriceType.Coin, JsonUtil.ToInt(data["c"]), true);
        }

        try
        {
            ActivityCrusadeMapLevel mapLevel = ActivityCrusadeManager.GetInstance().GetMapLevelList()[passed];
            mapLevel.passed = (punish == 0);
            mapLevel.punish = punish;
            if (mapLevel.passed)
                ActivityCrusadeManager.GetInstance().passed++;
        }
        catch (System.Exception e)
        {
            Debuger.LogException(e);
            DealCallback(URL.CrusadePass, false);
            return;
        }

        DealCallback(URL.CrusadePass, true);
    }

    public void ActivityCrusadeReward(int levelId, int pass, URLRequestCallBackDelegate callback = null)
    {
        AddCallback(URL.CrusadeReward, callback);

        URLRequestData data = new URLRequestData();
        data.Add("u", userId);
        data.Add("i", levelId);
        data.Add("p", pass);
        URLRequest.CreateURLRequest(urlDict[URL.CrusadeReward], data, URLRequest.Method.POST, OnActivityCrusadeReward);
    }

    private void OnActivityCrusadeReward(JsonData data)
    {
        if (CheckError(data, true, (AlertCloseEvent evt) =>
        {
            if (Session.GetInstance().IsInMain() || Session.GetInstance().IsInTown())
            {
                EmigratedPanel.instatnce.Close();
                DelayCall.Call(() =>
                {
                    EmigratedPanel.Show();
                }, 0.6f);
            }
        }))
        {
            DealCallback(URL.CrusadeReward, false);
            return;
        }
        JsonData arr = data["l"];
        List<GetRewardPanelItemData> rewards = new List<GetRewardPanelItemData>();
        for (int i = 0; i < arr.Count; i++)
        {
            rewards.Add(HandlerRewardItem(arr[i], -1, StaticsManager.ConsumeType.Reward,
                StaticsManager.ConsumeModule.Crusade));
        }

        EmigratedPanel.rewards = rewards;

        DealCallback(URL.CrusadeReward, true);

        HotfixManager.GetInstance().CheckHotfix();
    }

    public void ActivityCrusadePunish(int pass, URLRequestCallBackDelegate callback = null)
    {
        AddCallback(URL.CrusadePunish, callback);
        URLRequestData data = new URLRequestData();
        data.Add("u", userId);
        data.Add("p", pass);
        URLRequest.CreateURLRequest(urlDict[URL.CrusadePunish], data, URLRequest.Method.POST, OnActivityCrusadePunish);
    }

    private void OnActivityCrusadePunish(JsonData data)
    {
        if (CheckError(data, true))
        {
            DealCallback(URL.CrusadePunish, false);
            return;
        }

        Session.GetInstance()
            .myPlayer.UpdateResource((Item.PriceType)JsonUtil.ToInt(data["t"]), JsonUtil.ToInt(data["c"]), true);
        DealCallback(URL.CrusadePunish, true);
    }

    public void ActivityCrusadeRemove()
    {
        URLRequestData data = new URLRequestData();
        data.Add("u", userId);
        URLRequest.CreateURLRequest(urlDict[URL.CrusadeRemove], data, URLRequest.Method.POST, OnActivityCrusadeRemove);
    }

    private void OnActivityCrusadeRemove(JsonData data)
    {
        if (CheckError(data, true))
        {
            DealCallback(URL.CrusadeRemove, false);
            return;
        }
        DealCallback(URL.CrusadeRemove, true);
    }

    public void GetMissionListOnlyOpenSever()
    {
        URLRequestData data = new URLRequestData();
        data.Add("u", userId);
        URLRequest urlRequest = URLRequest.CreateURLRequest(urlDict[URL.MissionList], data, URLRequest.Method.POST,
            delegate(JsonData jsonData)
            {
                //Debug.LogError(jsonData.ToString());
                JsonData list = jsonData["l"];
                for (int i = 0; i < list.Count; i++)
                {
                    JsonData m = list[i];
                    int id = JsonUtil.ToInt(m["i"]);
                    Mission mission = MissionManager.GetInstance().GetMission(id);

                    if (mission == null)
                    {
                        Debug.LogError("没有这个任务.. id =" + id);
                        continue;
                    }
                    if (!mission.data.IsOpenServerMission) continue;
                    int value = JsonUtil.ToInt(m["s"]);
                    int times = 1;
                    if (JsonUtil.ContainKey(m, "c"))
                    {
                        times = JsonUtil.ToInt(m["c"]);
                    }
                    //Debug.LogError("开服任务"+mission.data.name+"_"+ value);

                    mission.times = times;
                    if (mission != null)
                    {
                        if (value >= 0)
                            mission.SetCurrentValue(value);
                        else
                            mission.rewarded = true;
                    }

                }
                MissionManager.INIT_OPENSERVER = true;
                OpenServerRedDotMain.GetInstance().SetMissions(MissionManager.GetInstance().
                    GetOpenServerListAllDay());
            }, false);
        urlRequest.SetInBackgroundimmediately();
    }
    public void GetMissionList(URLRequestCallBackDelegate callback = null, bool isInBackground = false,
        bool isRequestOnce = false)
    {
        AddCallback(URL.MissionList, callback);
        URLRequestData data = new URLRequestData();
        data.Add("u", userId);
        URLRequest urlRequest = URLRequest.CreateURLRequest(urlDict[URL.MissionList], data, URLRequest.Method.POST,
            OnMissionList, false);
        if (isInBackground)
            urlRequest.SetInBackgroundimmediately();
        if (isRequestOnce)
            urlRequest.SetRequestOnce();
    }

    private void OnMissionList(JsonData data)
    {
        if (CheckError(data, true))
        {
            DealCallback(URL.MissionList, false);
            return;
        }
        //Debug.LogError("OnMissionList" + data.ToJson());
        MissionManager.GetInstance().dailyList.ForEach(mission => { mission.ClearData(); });
        UpdateUrlTime(URL.MissionList);

        JsonData list = data["l"];

        MissionManager.GetInstance().needUpdateMission.ForEach(mission =>
        {
            mission.rewarded = false;
            mission.startTime = Time.realtimeSinceStartup;
        });

        for (int i = 0; i < list.Count; i++)
        {
            JsonData m = list[i];
            int id = JsonUtil.ToInt(m["i"]);
            Mission mission = MissionManager.GetInstance().GetMission(id);

            if (mission == null)
            {
                Debug.LogError("没有这个任务.. id =" + id);
                continue;

            }
            int value = JsonUtil.ToInt(m["s"]);
            int times = 1;
            if (JsonUtil.ContainKey(m, "c"))
            {
                times = JsonUtil.ToInt(m["c"]);
                //Debug.LogError(id + " times   " + times);
            }     
            mission.times = times;
            if (mission != null)
            {
                if (value >= 0)
                    mission.SetCurrentValue(value);
                else
                    mission.rewarded = true;
            }
            else
            {
                Debuger.LogError("Mission[" + id + "] could not found");
            }
        }

        DealCallback(URL.MissionList, true);
        MissionManager.INIT = true;
        MissionManager.INIT_OPENSERVER = true;
    }

    public void GetMissionReward(int id, int type, 
        URLRequestCallBackDelegate callback = null,Action<JsonData> onDataAction=null )
    {
        AddCallback(URL.MissionReward, callback);
        URLRequestData data = new URLRequestData();
        data.Add("u", userId);
        data.Add("i", id);
        data.Add("t", type);

        URLRequest.CreateURLRequest(urlDict[URL.MissionReward], data, URLRequest.Method.POST,
            delegate(JsonData jsonData)
            {

               var noErr =  OnGetMissionReward(jsonData);
                if (noErr && onDataAction != null) onDataAction(jsonData);

            });
    }

    private bool OnGetMissionReward(JsonData data)
    {
        if (CheckError(data, true))
        {
            DealCallback(URL.MissionReward, false);
            return false;
        }

        JsonData arr = data["l"];
        List<GetRewardPanelItemData> rewards = new List<GetRewardPanelItemData>();
        for (int i = 0; i < arr.Count; i++)
            rewards.Add(HandlerRewardItem(arr[i], -1, StaticsManager.ConsumeType.Reward,
                StaticsManager.ConsumeModule.Quest));
        int id = JsonUtil.ToInt(data["i"]);
        bool b = JsonUtil.ToInt(data["d"]) == 1 ? true : false;
        MissionManager.GetInstance().GetMission(id).rewarded = b;

        MissionManager.GetInstance().GetMission(id).times--;

        if (JsonUtil.ContainKey(data, "c"))
        {
            MissionManager.GetInstance().GetMission(id).times = JsonUtil.ToInt(data["c"]);
            MissionManager.GetInstance().GetMission(id).rewarded = false;

            Debug.LogError(id + "   " + MissionManager.GetInstance().GetMission(id).times);
        }
        //statics mission complete
        StaticsManager.GetInstance().FinishTask(id);

        GetRewardAnimManager.GetInstance().ShowRewardPanel(rewards);

        DealCallback(URL.MissionReward, true);

        HotfixManager.GetInstance().CheckHotfix();
        return true;
    }




    public void SetNickname(string nick, Item.PriceType type, int cost, URLRequestCallBackDelegate callback = null)
    {
        AddCallback(URL.NicknameSet, callback);
        URLRequestData data = new URLRequestData();
        data.Add("u", userId);
        data.Add("n", nick);
        data.Add("t", type.GetHashCode());
        data.Add("c", cost);

        URLRequest.CreateURLRequest(urlDict[URL.NicknameSet], data, URLRequest.Method.POST, OnSetNickname);
    }

    private void OnSetNickname(JsonData data)
    {
        if (CheckError(data, true))
        {
            DealCallback(URL.NicknameSet, false);
            return;
        }

        if (HotfixManager.GetInstance().CheckHotfix())
        {
            return;
        }

        string nick = data["n"].ToString();
        Item.PriceType type = (Item.PriceType)JsonUtil.ToInt(data["t"]);
        int count = JsonUtil.ToInt(data["c"]);
        Player player = Session.GetInstance().myPlayer;
        player.nick = nick;
        player.UpdateResource(type, count, true);

        if (Session.GetInstance().IsInTown())
        {
            Session.GetInstance().GetMessageManager().SendTownNick();
        }

        DealCallback(URL.NicknameSet, true);
    }




    public void GetNickname(URLRequestCallBackDelegate callback = null)
    {
        AddCallback(URL.NicknameGet, callback);
        URLRequestData data = new URLRequestData();
        data.Add("u", userId);

        URLRequest.CreateURLRequest(urlDict[URL.NicknameGet], data, URLRequest.Method.POST, OnGetNickname);
    }

    private void OnGetNickname(JsonData data)
    {
        if (CheckError(data, true))
        {
            DealCallback(URL.NicknameGet, false);
            return;
        }

        string nick = data["n"].ToString();
        SettingPanel.randname = nick;

        DealCallback(URL.NicknameGet, true);
    }



    public void GetAnnouceList(string date, URLRequestCallBackDelegate callback = null)
    {
        AddCallback(URL.AnnounceList, callback);
        URLRequestData data = new URLRequestData();
        data.Add("u", userId);
        data.Add("t", date);

        URLRequest urlrequest = URLRequest.CreateURLRequest(urlDict[URL.AnnounceList], data, URLRequest.Method.POST,
            OnGetAnnouceList, false);
        urlrequest.SetInBackgroundimmediately();
        urlrequest.SetRequestOnce();
    }

    private void OnGetAnnouceList(JsonData data)
    {
        if (CheckError(data, true))
        {
            DealCallback(URL.AnnounceList, false);
            return;
        }

        AnnounceManager.GetInstance().Add(data);

        DealCallback(URL.AnnounceList, true);
    }

    /// <summary>
    /// 检测跑马灯公告，但是不加入显示列表
    /// </summary>
    /// <param name="date"></param>
    public void CheckAnnounceList(string date)
    {
        AddCallback(URL.AnnounceList, null);
        URLRequestData data = new URLRequestData();
        data.Add("u", userId);
        data.Add("t", date);

        URLRequest urlrequest = URLRequest.CreateURLRequest(urlDict[URL.AnnounceList], data, URLRequest.Method.POST,
            OnCheckAnnounceList, false);
        urlrequest.SetInBackgroundimmediately();
        urlrequest.SetRequestOnce();
    }

    private void OnCheckAnnounceList(JsonData data)
    {
        if (CheckError(data, true))
        {
            DealCallback(URL.AnnounceList, false);
            return;
        }

		AnnounceManager.GetInstance().Add(data);
//        AnnounceManager.GetInstance().Check(data);

        DealCallback(URL.AnnounceList, true);
    }

    public void PassLevelTest(int level)
    {
        for (int i = 1; i <= level; i++)
        {

        }

        URLRequestData data = new URLRequestData();
        data.Add("u", userId);
        data.Add("m", level);
        data.Add("v", 3);
        URLRequest.CreateURLRequest(Config.GetHttpRoot() + "?m=maplevel&a=fraud_cross", data, URLRequest.Method.POST,
            OnPassLevelTest);

    }

    private void OnPassLevelTest(JsonData data)
    {
        if (CheckError(data, true))
        {
            Debug.Log("Pass OK");
        }
    }



    public void OnXZPay(string info)
    {
        string[] xx = info.Split(':');

        URLRequestData data = new URLRequestData();
        data.Add("un", userId);
        data.Add("si", Player.serverID);
        data.Add("sp", xx[0]);
        data.Add("pi", xx[1]);
        data.Add("pt", xx[2]);
        URLRequest.CreateURLRequest(urlDict[URL.XZPay], data, URLRequest.Method.POST, OnHandlerXZPay);
    }

    private void OnHandlerXZPay(JsonData data)
    {
        if (CheckError(data, true))
        {
            return;
        }

        OnSynPlyerD();


        string sku = data["pi"].ToString();
		BuyCoinData buyCoinData = BuyCoinManager.GetInstance().GetData(JsonUtil.ToInt(data["pi"]));
		StaticsManager.GetInstance().RequestPayment(buyCoinData.code, buyCoinData.coin, buyCoinData.currency);
		if (buyCoinData.type == 2)
        {
            if (WeekCardPanel.currentPanel != null)
            {
                WeekCardPanel.currentPanel.TryGetData();
            }
        }
        FacebookSDK.ConsumeAsync(sku);
    }


    public void OnSynPlyerD()
    {
        URLRequestData data = new URLRequestData();
        data.Add("u", userId);
        URLRequest.CreateURLRequest(urlDict[URL.GameInfo], data, URLRequest.Method.POST, OnHandlerSynPlyer);
    }

    private void OnHandlerSynPlyer(JsonData data)
    {
        if (CheckError(data, true))
        {
            return;
        }

        int t = JsonUtil.ToInt(data["d"]);
        Player player = Session.GetInstance().myPlayer;
        player.UpdateResource(Item.PriceType.Diamond, t, true);

        SpecialReminder.Show(Language.GetStr("Public", "diamond"), t.ToString());
        StaticsManager.GetInstance()
            .ChargeSuccess(BuyCoinItem.GetRequestTimestamp(), "钻石", (int)BuyCoinItem.GetCurrencyAmount(),
                StaticsManager.currencyType, "测试", t);

        player.chargeCount++;

        if (BuyCoinPanel.currentPanel != null)
            BuyCoinPanel.currentPanel.SetData();
        if (TopUserInfo.GetTopUserInfo() != null)
        {
            TopUserInfo.UpdateInfo();
            TopUserInfo.GetTopUserInfo().EnableFirstCharge(false);
        }
    }









    public void SendPower(URLRequestData data, URLRequestCallBackDelegate callback = null)
    {
        AddCallback(URL.SendPower, callback);

        data.Add("u", userId);

        URLRequest urlRequest = URLRequest.CreateURLRequest(urlDict[URL.SendPower], data, URLRequest.Method.POST,
            OnSendPower, false);
        urlRequest.SetInBackgroundimmediately();
        urlRequest.SetRequestOnce();
    }

    private void OnSendPower(JsonData data)
    {
    }


    public void TeamHighScore(URLRequestCallBackDelegate callback = null)
    {
        AddCallback(URL.TeamHighscore, callback);
        URLRequest.CreateURLRequest(urlDict[URL.TeamHighscore], new URLRequestData(), URLRequest.Method.GET,
            OnTeamHighScore, true);
    }

    private void OnTeamHighScore(JsonData data)
    {
        if (CheckError(data, true))
        {
            return;
        }
        HighScoreManager.GetInstance().SetTeamHighScore(data);
    }

    public void AllHighScore(URLRequestCallBackDelegate callback = null)
    {
        AddCallback(URL.AllHighscore, callback);
        URLRequest.CreateURLRequest(urlDict[URL.AllHighscore], new URLRequestData(), URLRequest.Method.GET,
            OnAllHighScore, true);
    }

    private void OnAllHighScore(JsonData data)
    {
        if (CheckError(data, true))
        {
            return;
        }
        HighScoreManager.GetInstance().SetAllHighScore(data);
    }

    public void GetMyHighscore(URLRequestCallBackDelegate callback = null)
    {
        AddCallback(URL.UserInfo, callback);
        URLRequestData data = new URLRequestData();
        data.Add("u", userId);
        URLRequest.CreateURLRequest(urlDict[URL.UserInfo], data, URLRequest.Method.POST, OnGetMyHighscore, true);
    }

    private void OnGetMyHighscore(JsonData data)
    {
        if (CheckError(data, true))
        {
            return;
        }
        HighScoreManager.GetInstance().SetMyPowerPlayer(userId, data);
    }



	public void GetMatchHighscore(URLRequestCallBackDelegate callback = null)
	{
		AddCallback(URL.MatchHighscore, callback);
		URLRequestData data = new URLRequestData();
		data.Add("u", userId);
		URLRequest.CreateURLRequest(urlDict[URL.MatchHighscore], data, URLRequest.Method.POST, OnGotMatchHighscore, true);
	}

	private void OnGotMatchHighscore(JsonData data)
	{
		if (CheckError(data, true))
		{
			DealCallback(URL.MatchHighscore, false);
			return;
		}
		HighScoreManager.GetInstance().SetMatchHighscore(userId, data);
		DealCallback(URL.MatchHighscore, true);
	}


	public void GetCaptureList(URLRequestCallBackDelegate callBack = null)
	{
		AddCallback(URL.CaptureList, callBack);
		URLRequestData data = new URLRequestData();
		data.Add("u", userId);
		int myIndex = HighScoreManager.GetInstance().GetMyMatchPlayer().index;
		if(myIndex > HighScoreManager.MATCH_LAST_RANK)
			myIndex = 2000;
		else if(myIndex > 0)
			myIndex -= 1;

		data.Add("s", myIndex);
		URLRequest.CreateURLRequest(urlDict[URL.CaptureList], data, URLRequest.Method.POST, OnGotCaptureList, true);
	}

	private void OnGotCaptureList(JsonData data)
	{
		if (CheckError(data, true))
		{
			DealCallback(URL.CaptureList, false);
			return;
		}
		HighScoreManager.GetInstance().SetCaptureList(data);
		DealCallback(URL.CaptureList, true);
	}






    public void StarHighscore(URLRequestCallBackDelegate callBack = null)
    {
        AddCallback(URL.StarHighscore, callBack);
        URLRequestData data = new URLRequestData();
        URLRequest.CreateURLRequest(urlDict[URL.StarHighscore], data, URLRequest.Method.POST, OnStarHighscore, true);
    }

    private void OnStarHighscore(JsonData data)
    {
        if (CheckError(data, true))
        {
            return;
        }
        HighScoreManager.GetInstance().SetStarHighscore(data);
        DealCallback(URL.StarHighscore, true);
    }

    public void LoginWithBinding(string token, URLRequestCallBackDelegate callBack = null)
    {
        AddCallback(URL.LoginWithBinding, callBack);
        URLRequestData data = new URLRequestData();
        data.Add("u", userId);
        data.Add("t", token);
        URLRequest.CreateURLRequest(urlDict[URL.LoginWithBinding], data, URLRequest.Method.POST, OnLoginWithBinding,
            true);
    }

    private void OnLoginWithBinding(JsonData data)
    {
        if (CheckError(data, true))
        {
            DealCallback(URL.LoginWithBinding, false);
            FacebookSDK.CallFBLogout();
            return;
        }
        DealCallback(URL.LoginWithBinding, true);
    }

    public void GetServerTime(URLRequestCallBackDelegate callBack = null)
    {
        AddCallback(URL.LoginWithBinding, callBack);
        URLRequestData data = new URLRequestData();
        URLRequest urlRequest = URLRequest.CreateURLRequest(urlDict[URL.ServerTime], data, URLRequest.Method.POST,
            OnGetServerTime, false);
        urlRequest.SetInBackgroundimmediately();
        urlRequest.SetRequestOnce();
    }

    private void OnGetServerTime(JsonData data)
    {
        if (CheckError(data, true))
        {
            DealCallback(URL.LoginWithBinding, false);
            return;
        }

        JsonData date = data["t"];
        DateTime dateTime = DateTime.Now;
        if (JsonUtil.ToInt(date["y"]) > 1970)
            dateTime = new DateTime(JsonUtil.ToInt(date["y"]), JsonUtil.ToInt(date["m"]), JsonUtil.ToInt(date["d"]),
                JsonUtil.ToInt(date["h"]), JsonUtil.ToInt(date["i"]), JsonUtil.ToInt(date["s"]));
        DateUtil.SetStandardTime(dateTime);
        DealCallback(URL.ServerTime, true);
    }

    #region 好友相关协议

    public void GetFriendList(Action<JsonData> onDataAction)
    {
        //AddCallback(URL.FriendList, callback);
        var url = URL.FriendList;
        URLRequestData sdata = new URLRequestData();
        sdata.Add("u", userId);
        URLRequest urlRequest = URLRequest.CreateURLRequest(urlDict[url], sdata,
            URLRequest.Method.POST, delegate (JsonData rdata)
            {
                if (CheckError(rdata, true))
                {
                    DealCallback(url, false);
                    return;
                }
                UpdateUrlTime(url);
                //LogT.t(rdata.ToJson());
                onDataAction(rdata);
                DealCallback(url, true);

                //OnFriendList(jsonData);
            });

    }

    //private void OnFriendList(JsonData data)
    //{
    //    if (CheckError(data, true))
    //    {
    //        DealCallback(URL.FriendList, false);
    //        return;
    //    }
    //    //MissionManager.GetInstance().dailyList.ForEach(mission => { mission.ClearData(); });
    //    UpdateUrlTime(URL.FriendList);

    //    JsonData list = data["l"];

    //    //MissionManager.GetInstance().needUpdateMission.ForEach(mission =>
    //    //{
    //    //    mission.rewarded = false;
    //    //    mission.startTime = Time.realtimeSinceStartup;
    //    //});
    //    Debuger.Log("=============好友列表=========" + data);

    //}
    /// <summary>
    /// 推荐好友列表
    /// </summary>
    /// <param name="onData"></param>
    public void getRecommend_list(Action<JsonData> onData)
    {

        var url = URL.FriendRecomList;
        URLRequestData sdata = new URLRequestData();
        sdata.Add("u", userId);
        URLRequest urlRequest = URLRequest.CreateURLRequest(urlDict[url], sdata,
            URLRequest.Method.POST, delegate (JsonData rdata)
            {
                if (CheckError(rdata, true))
                {
                    DealCallback(url, false);
                    return;
                }
                UpdateUrlTime(url);
                //JsonData list = rdata["l"];
                onData(rdata);
                DealCallback(url, true);

                //OnFriendList(jsonData);
            });
    }
    //申请列表
    public void getRequest_friend_list(Action<JsonData> onData)
    {

        var url = URL.FriendRequest_friend_list;
        URLRequestData sdata = new URLRequestData();
        sdata.Add("u", userId);
        URLRequest urlRequest = URLRequest.CreateURLRequest(urlDict[url], sdata,
            URLRequest.Method.POST, delegate (JsonData rdata)
            {
                if (CheckError(rdata, true))
                {
                    DealCallback(url, false);
                    return;
                }
                //JsonData list = rdata["l"];
                onData(rdata);
                DealCallback(url, true);

                //OnFriendList(jsonData);
            });
    }

    /// <summary>
    /// find player
    /// </summary>
    /// <param name="onData"></param>
    public void FindFriendById(string fid, Action<JsonData> onData)
    {
        var url = URL.FriendRecomSearch;
        URLRequestData sdata = new URLRequestData();
        sdata.Add("u", userId);
        sdata.Add("ci", fid);
        URLRequest urlRequest = URLRequest.CreateURLRequest(urlDict[url], sdata,
            URLRequest.Method.POST, delegate (JsonData rdata)
            {
                if (CheckError(rdata, true))
                {
                    DealCallback(url, false);
                    return;
                }
                UpdateUrlTime(url);
                //JsonData list = rdata["l"];
                onData(rdata);
                DealCallback(url, true);

                //OnFriendList(jsonData);
            }, false);
    }

    public void getFriendLoc_list(Action<JsonData> onData)
    {
        EasyRequest(URL.FriendLocList, onData);
    }

    public void getChanged_list(Action<JsonData> onData)
    {
        EasyRequest(URL.FriendChangedList, onData);
    }

    //public void RequestApplyFriends(long[] ids,Action<JsonData> onData)
    //{
    //    //URL.FriendList; todo
    //    //EasyRequest(URL.FriendChangedList, onData);
    //    URL url = URL.FriendRequestAddFriend;
    //    EasyRequest(ids,onData,url);
    //}

    public void RequestAddFriend(long[] users, Action<JsonData> onData)
    {
        URL url = URL.FriendRequestAddFriend;
        EasyRequest(users, onData, url);
    }

    private void EasyRequest(long[] users, Action<JsonData> onData, URL url)
    {
        URLRequestData sdata = new URLRequestData();
        sdata.Add("u", userId);
        sdata.Add("l", JsonUtil.LongArrToJson(users));
        URLRequest urlRequest = URLRequest.CreateURLRequest(urlDict[url], sdata,
            URLRequest.Method.POST, delegate (JsonData rdata)
            {
                if (CheckError(rdata, true))
                {
                    DealCallback(url, false);
                    return;
                }
                UpdateUrlTime(url);
                onData(rdata);
                DealCallback(url, true);
            }, false);
    }


    public void EasyRequest(URL url, Action<JsonData> onData = null)
    {
        URLRequestData sdata = new URLRequestData();
        sdata.Add("u", userId);
        URLRequest urlRequest = URLRequest.CreateURLRequest(urlDict[url], sdata,
            URLRequest.Method.POST, delegate (JsonData rdata)
            {
                if (CheckError(rdata, true))
                {
                    return;
                }
                UpdateUrlTime(url);
                if (onData != null) onData(rdata);
                //OnFriendList(jsonData);
            });
    }
    private void EasyRequest(URL url, Action<JsonData> onData, params object[] param)
    {
        URLRequestData sdata = new URLRequestData();
        sdata.Add("u", userId);
        for (int i = 0; i < param.Length; i = i + 2)
        {
            sdata.Add(param[i].ToString(), param[i + 1]);
        }

        URLRequest urlRequest = URLRequest.CreateURLRequest(urlDict[url], sdata,
            URLRequest.Method.POST, delegate (JsonData rdata)
            {
                if (CheckError(rdata, true))
                {
                    return;
                }
                UpdateUrlTime(url);
                onData(rdata);
                //OnFriendList(jsonData);
            }, true);
        //urlRequest.SetInBackgroundimmediately();
    }
    //==================好友申请列表页面
    public void RequestAgreeFriend(long[] users, Action<JsonData> onData)
    {
        URL url = URL.FriendRequestAgree;
        // JsonData arr = JsonMapper.ToObject("[]");
        EasyRequest(users, onData, url);
    }



    public void RequestAgreeFriend(long id, Action<JsonData> onDataAction)
    {
        RequestAgreeFriend(new[] { id }, onDataAction);
    }

    public void RequestRefuseFriend(long id, Action<JsonData> onDataAction)
    {
        RequestRefuseFriend(new[] { id }, onDataAction);
    }

    public void RequestRefuseFriend(long[] users, Action<JsonData> onDataAction)
    {
        var url = URL.FriendRequestRefuse;
        EasyRequest(users, onDataAction, url);
    }

    public void RequestFriendDetailInfo(long id, Action<JsonData> onDataAction)
    {
        var url = URL.FriendDetailInfo;
        EasyRequest(url, onDataAction, "cu", id.ToString());
    }
    public void RequestDelFriend(long id, Action<JsonData> onDataAction)
    {
        var url = URL.FriendDel;
        EasyRequest(url, onDataAction, "di", id.ToString());
    }
    public void RequestGetFromFriend(long id, Action<JsonData> onDataAction)
    {

        long[] ids = { id };
        RequestGetFromFriend(ids, onDataAction);
    }

    public void RequestGetFromFriend(long[] ids, Action<JsonData> onDataAction)
    {
        var url = URL.FriendGetFrom;
        Action<JsonData> a = delegate (JsonData data)
        {
            var physic = JsonUtil.ToInt(data["en"]);
            UIFastSet.UpdatePlayerPhysic(physic);
            onDataAction(data);
        };
        EasyRequest(url, a, "l", JsonUtil.LongArrToJson(ids));
    }

    public void RequestPostToFriend(long id, Action<JsonData> onDataAction)
    {
        long[] ids = { id };
        RequestPostToFriend(ids, onDataAction);
    }
    public void RequestPostToFriend(long[] ids, Action<JsonData> onDataAction)
    {
        if (ids == null) return;
        if (ids.Length == 0) return;
        var url = URL.FriendPostTo;
        Action<JsonData> a = delegate (JsonData data)
        {
            //var physic = JsonUtil.ToInt(data["en"]);
            //UIFastSet.UpdatePlayerPhysic(physic);
            onDataAction(data);
        };
        EasyRequest(url, a, "l", JsonUtil.LongArrToJson(ids));
    }
    public void PostInfoToFriend(long id, string info, Action<JsonData> onDataAction)
    {
        var url = URL.FriendInfoPostTo;
        //var str ="\""+info+"\"";
        var str = WWW.EscapeURL(info);
        EasyRequest(url, onDataAction, "ru", id.ToString(), "t", 3, "c", str);
    }
    public void GetFriendProgrees(Action<JsonData> onDataAction)
    {
        var url = URL.FriendProgrees;
        //return;

        EasyRequest(url, onDataAction);

    }

    #endregion
    #region 角色升级

    public void CharLevelUpCoatItems(Dictionary<long, int> items, long charUId, Action<JsonData> onDataAction)
    {
        var url = URL.CharLevelUp;

        var json = dictToServerCountArray(items);
        EasyRequest(url, onDataAction, "it", json, "rd", charUId);
    }
    #endregion
    #region 开服活动

    public void task_active(Action<JsonData> onDataAction, bool isDayTast = false)
    {
        var url = URL.task_active;
        EasyRequest(url, onDataAction, "t", isDayTast?1:3);
    }
    public void game_active_reward(int id, Action<JsonData> onDataAction, bool isDayTast = false)
    {
        var url = URL.game_active_reward;
        EasyRequest(url, onDataAction, "t", isDayTast ? 1 : 3, "i", id);

    }
    #endregion
    #region 在线奖励
    internal void OnlineHeartBeat()
    {
        URL url = URL.game_online;

        EasyRequest(url, (d) =>
        {
            var now = TimeUtil.Str1ToDateTime(d.GetString("t"));
            TimeUtil.Now = now;
        });
    }
    internal void OnlineData(Action<JsonData> onDataAction)
    {
        URL url = URL.game_init;

        EasyRequest(url, onDataAction);
    }
    internal void onlineReward(int id, Action<JsonData> onDataAction)
    {
        //m=online&a=game_reward
        URL url = URL.onlineReward;

        EasyRequest(url, onDataAction, "i", id);
    }
    #endregion
    #region 新签到
    internal void CheckInit(Action<JsonData> onDataAction)
    {

        URL url = URL.CheckInit;

        EasyRequest(url, onDataAction);

    }
    internal void checkin_loop_reward(long id, Action<JsonData> onDataAction)
    {

        URL url = URL.checkin_loop_reward;

        EasyRequest(url, onDataAction, "i", id.ToString());

    }
    internal void checkin_reward(long id, Action<JsonData> onDataAction)
    {

        URL url = URL.checkin_reward;

        EasyRequest(url, onDataAction, "i", id.ToString());

    }
    #endregion
    #region  周卡
    internal void OpenWeekCard(Action<JsonData> onDataAction)
    {
        URL url = URL.WeekCardOpen;
        EasyRequest(url, onDataAction);

    }
    internal void RewardWeekCard(Action<JsonData> onDataAction)
    {
        URL url = URL.WeekCardReward;
        EasyRequest(url, onDataAction);

    }
    #endregion
    #region 大乐透
    internal void game_open_lottery(Action<JsonData> onDataAction)
    {
        URL url = URL.game_open_lottery;
        EasyRequest(url, onDataAction);

    }
    internal void game_buy_lottery(Action<JsonData> onDataAction)
    {
        URL url = URL.game_buy_lottery;
        EasyRequest(url, onDataAction);
    }
    #endregion

    #region 时空商人
    public void TimeTraderGetOpenData(Action<JsonData> onDataAction)
    {
        URL url = URL.game_open_space_time_shop;
        EasyRequest(url, onDataAction);
    }
    public void TimeTraderRefresh(Action<JsonData> onDataAction)
    {
        URL url = URL.game_refresh_space_time_shop;
        EasyRequest(url, onDataAction);
    }
    /*
    u 用户名id
    s_id 源道具id(背包里的道具)
    t_id 目标道具id
    e_t 兑换方式(0表示5:1,1表示10:3)
    */
    public void TimeTraderExchangeItem(int s_id,int t_id, Action<JsonData> onDataAction,int e_t = 0)
    {
        URL url = URL.game_exchange_item_space_time_shop;
        EasyRequest(url,onDataAction, "s_id", s_id, "t_id", t_id, "e_t", e_t);
    }
    #endregion

    private string dictToServerCountArray(Dictionary<long, int> id_countDict)
    {
        var jsonstr = "";

        //var arr = new JsonData[id_countDict.Count];
        var arr = new JsonData();
        var i = 0;
        foreach (var item in id_countDict)
        {
            var jd = new JsonData();
            jd[item.Key.ToString()] = item.Value;
            arr.Add(jd);
            i++;
            // Console.WriteLine(item.Key + item.Value);
        }
        jsonstr = JsonMapper.ToJson(arr);
        return jsonstr;
    }

    public void NPCInit()
    {
        URLRequestData data = new URLRequestData();
        data.Add("u", userId);

        URLRequest urlRequest = URLRequest.CreateURLRequest(urlDict[URL.NPCInit], data, URLRequest.Method.POST, OnNPCInit);
        urlRequest.SetRequestOnce();
        urlRequest.SetInBackgroundimmediately();
    }

    private void OnNPCInit(JsonData json)
    {
        if (CheckError(json))
        {
            RandomNPCManager.GetInstance().Init(null);
        }
        else
        {
            RandomNPCManager.GetInstance().Init(json["l"]);
        }
    }



    public void NPCBattle(long uid, int id, URLRequestCallBackDelegate callBack)
    {
        AddCallback(URL.NPCBattle, callBack);

        URLRequestData data = new URLRequestData();
        data.Add("u", userId);
        data.Add("i", uid);
        data.Add("d", id);

        URLRequest urlRequest = URLRequest.CreateURLRequest(urlDict[URL.NPCBattle], data, URLRequest.Method.POST, OnNPCBattle);
    }

    private void OnNPCBattle(JsonData json)
    {
        if (CheckError(json))
        {
            DealCallback(URL.NPCBattle, false);
            return;
        }
        DealCallback(URL.NPCBattle, true);
    }

    public void NPCReward(URLRequestCallBackDelegate callBack)
    {
        AddCallback(URL.NPCReward, callBack);

        RandomBossData bossData = RandomNPCManager.GetInstance().currentNpcData;

        URLRequestData data = new URLRequestData();
        data.Add("u", userId);
        data.Add("i", bossData.uid);
        data.Add("d", bossData.id);

        URLRequest.CreateURLRequest(urlDict[URL.NPCReward], data, URLRequest.Method.POST, OnNPCReward);

        BattleSession battleSession = Session.GetInstance().GetBattleSession();

        RandomNPCData npcData = RandomNPCManager.GetInstance().GetData(bossData.id);
        for (int i = 0; i < npcData.dropItemArr.Length; i++)
        {
            CommonItemData item = npcData.dropItemArr[i];
            battleSession.SetDropItemDict(item.id, item.count);
        }
    }

    private void OnNPCReward(JsonData json)
    {
        if (CheckError(json))
        {
            DealCallback(URL.NPCReward, false);
            return;
        }

        JsonData arr = json["l"];
        for (int i = 0; i < arr.Count; i++)
        {
            HandlerRewardItem(arr[i], -1, StaticsManager.ConsumeType.Reward, StaticsManager.ConsumeModule.RandomNPC);
        }
        DealCallback(URL.NPCReward, true);
    }

    public void NPCShow(long uid, URLRequestCallBackDelegate callBack)
    {
        AddCallback(URL.NPCShow, callBack);

        URLRequestData data = new URLRequestData();
        data.Add("u", userId);
        data.Add("i", uid);

        URLRequest.CreateURLRequest(urlDict[URL.NPCShow], data, URLRequest.Method.POST, OnNPCShow);
    }

    private void OnNPCShow(JsonData json)
    {
        if (CheckError(json, true))
        {
            DealCallback(URL.NPCShow, false);
            return;
        }

        int count = JsonUtil.ToInt(json["s"]);
        long uid = JsonUtil.ToLong(json["i"]);
        int id = JsonUtil.ToInt(json["d"]);
        RandomNPCManager.GetInstance().SetRequestShow(id, uid, count);
        DealCallback(URL.NPCShow, true);
    }



	public void MapLevelSweep(int mapLevelId, int count, URLRequestCallBackDelegate callBack)
	{
		AddCallback(URL.MapLevelSweep, callBack);
		
		URLRequestData data = new URLRequestData();
		data.Add("u", userId);
		data.Add("m", mapLevelId);
		data.Add("c", count);
		
		URLRequest.CreateURLRequest(urlDict[URL.MapLevelSweep], data, URLRequest.Method.POST, OnMapLevelSweep);
	}

	private void OnMapLevelSweep(JsonData json)
	{
		if (CheckError(json, true))
		{
			DealCallback(URL.MapLevelSweep, false);
			return;
		}

//		int itemCount = JsonUtil.ToInt(json["i"]);
		int physics = JsonUtil.ToInt(json["c"]);

		JsonData dropArr = json["m"];

		List<AutoCombatData> autoCombatList = new List<AutoCombatData>();

		int index = 0;
		while(index < dropArr.Count)
		{
			JsonData arr = dropArr[index]["l"];
			List<GetRewardPanelItemData> dataList = new List<GetRewardPanelItemData>();
			for (int i = 0; i < arr.Count; i++)
			{
				JsonData o = arr[i];
				dataList.Add(HandlerRewardItem(o, -1, StaticsManager.ConsumeType.Reward, StaticsManager.ConsumeModule.MapLevel));
			}
			autoCombatList.Add(new AutoCombatData(dataList));
			index++;
		}

		Player player = Session.GetInstance().myPlayer;
		player.physic = physics;
		TopUserInfo.UpdateInfo();

		AutoCombatReminder.Show(autoCombatList);
		
		DealCallback(URL.MapLevelSweep, true);
	}



	public void VIPTest(URLRequestCallBackDelegate callBack)
	{
		AddCallback(URL.VIP_Test, callBack);
		
		URLRequestData data = new URLRequestData();
		data.Add("u", userId);
		
		URLRequest.CreateURLRequest(urlDict[URL.VIP_Test], data, URLRequest.Method.POST, null, false);
	}



	public void VIPInfo(URLRequestCallBackDelegate callBack)
	{
		AddCallback(URL.VIP_Info, callBack);
		
		URLRequestData data = new URLRequestData();
		data.Add("u", userId);
		
		URLRequest.CreateURLRequest(urlDict[URL.VIP_Info], data, URLRequest.Method.POST, OnVIPInfo);
	}

	private void OnVIPInfo(JsonData json)
	{
		if (CheckError(json, true))
		{
			DealCallback(URL.VIP_Info, false);
			return;
		}

		int level = JsonUtil.ToInt(json["v"]);
		int diamond = JsonUtil.ToInt(json["r_d"]);
		Player myPlayer = Session.GetInstance().myPlayer;
		int oldLevel = myPlayer.vipLevel;
		myPlayer.vipLevel = level;
		myPlayer.diamondBought = diamond;
		VIPManager.GetInstance().SetReward(json["r_fetch"]);

		if(oldLevel != level)
		{
			VIPUpgradePanel.Show(myPlayer);
			TopUserInfo.UpdateInfo();

			if(BuyDiamondPanel.currentPanel != null)
			{
				BuyDiamondPanel.currentPanel.SetData();
			}
		}

		DealCallback(URL.VIP_Info, true);
	}

	public void VIPReward(int level, URLRequestCallBackDelegate callBack=null)
	{
		AddCallback(URL.VIP_Reward, callBack);
		
		URLRequestData data = new URLRequestData();
		data.Add("u", userId);
		data.Add("lv", level);
		
		URLRequest.CreateURLRequest(urlDict[URL.VIP_Reward], data, URLRequest.Method.POST, OnVIPReward);
	}

	private void OnVIPReward(JsonData json)
	{
		if (CheckError(json, true))
		{
			DealCallback(URL.VIP_Reward, false);
			return;
		}

		VIPManager.GetInstance().SetReward(json["r_fetch"]);

		List<GetRewardPanelItemData> rewards = new List<GetRewardPanelItemData>();
		JsonData rewardList = json["l"];
		for(int i=0; i<rewardList.Count; i++)
		{
			rewards.Add(HandlerRewardItem(rewardList[i]));
		}

		RewardPanel rewardPanel = RewardPanel.Show();
		rewardPanel.InitData(rewards);

		DealCallback(URL.VIP_Reward, true);
	}

    public void VerifyToken(string token, string ssoid, URLRequestCallBackDelegate callBack)
    {
        AddCallback(URL.VERIFY_TOKEN, callBack);

        URLRequestData data = new URLRequestData();
        data.Add("token", token);
        data.Add("ssoid", ssoid);

        URLRequest urlRequest = URLRequest.CreateURLRequest(urlDict[URL.VERIFY_TOKEN], data, URLRequest.Method.POST, OnVerifyToken);
    }

    public void OnVerifyToken(JsonData json)
    {
        //
        if (CheckError(json, true))
        {
            DealCallback(URL.VERIFY_TOKEN, false);
            return;
        }
        Session.GetInstance().myPlayer.uid = json["u"].ToString();
        DealCallback(URL.VERIFY_TOKEN, true);

    }
}

