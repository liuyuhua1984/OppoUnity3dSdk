using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


    public interface ISdk
    {
         /// <summary>
          /// 初始化
         /// </summary>
        void init();
        /// <summary>
         /// 登录
         /// </summary>
        void login();


        /// <summary>
        /// 支付
        /// </summary>
        /// <param name="jsonString"></param>
        void pay(BuyCoinData data);
        

        /// <summary>
        /// 退出游戏
        /// </summary>
       void exitSdk();
        /// <summary>
        /// onResume 重新进入游戏
        /// </summary>
        void onResume();
        
        /// <summary>
        /// 暂停游戏
        /// </summary>
        void onPause();
    }

