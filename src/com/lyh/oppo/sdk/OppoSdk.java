package com.lyh.oppo.sdk;

import java.util.Random;

import com.nearme.game.sdk.GameCenterSDK;
import com.nearme.game.sdk.callback.ApiCallback;
import com.nearme.game.sdk.callback.GameExitCallback;
import com.nearme.game.sdk.callback.SinglePayCallback;
import com.nearme.game.sdk.common.model.biz.PayInfo;
import com.nearme.game.sdk.common.model.biz.ReportUserGameInfoParam;
import com.nearme.game.sdk.common.model.biz.ReqUserInfoParam;
import com.nearme.game.sdk.common.util.AppUtil;
import com.nearme.platform.opensdk.pay.PayResponse;
import com.unity3d.player.UnityPlayer;

import android.util.Log;

/**
 * ClassName: OppoSdk <br/>
 * Function: TODO (). <br/>
 * Reason: TODO (). <br/>
 * date: 2016年12月13日 下午12:05:32 <br/>
 * 
 * @author lyh
 * @version
 */
public class OppoSdk {
	/**
	 * init:(). <br/>
	 * TODO().<br/>
	 * oppo初始化
	 * 
	 * @author lyh
	 * @param appSecret
	 */
	public static void init(final String appSecret) {
		/// Toast.makeText(DemoActivity.this, "token = " + token + "ssoid = " +
		/// ssoid, Toast.LENGTH_LONG).show();
		Log.e("初始化", "int calling...");
		UnityPlayer.currentActivity.runOnUiThread(new Runnable() {

			@Override
			public void run() {
				// TODO Auto-generated method stub
				GameCenterSDK.init(appSecret, UnityPlayer.currentActivity);
				Log.e("初始化成功", "int calling...");
			}
		});
	}

	/**
	 * login:(). <br/>
	 * TODO().<br/>
	 * 登录
	 * 
	 * @author lyh
	 * @param gameObject
	 */
	public static void login(final String gameObject) {
		Log.e("登录", "login calling...");

		UnityPlayer.currentActivity.runOnUiThread(new Runnable() {

			@Override
			public void run() {
				// TODO Auto-generated method stub
				GameCenterSDK.getInstance().doLogin(UnityPlayer.currentActivity, new ApiCallback() {

					@Override
					public void onSuccess(String msg) {
						// TODO Auto-generated method stub
						Log.e("登录成功", "login calling...");
						UnityPlayer.UnitySendMessage(gameObject, "OnLoginSuccess", msg);
					}

					@Override
					public void onFailure(String msg, int code) {
						// TODO Auto-generated method stub
						UnityPlayer.UnitySendMessage(gameObject, "OnLoginFail", msg);
					}
				});
			}
		});
	}

	/**
	 * getTokenAndSsoid:(). <br/>
	 * TODO().<br/>
	 * 获取用户信息
	 * 
	 * @author lyh
	 * @param gameObject
	 */
	public static void getTokenAndSsoid(final String gameObject) {
		Log.e("获取ssoid", "getTokenAndSsoid calling...");
		UnityPlayer.currentActivity.runOnUiThread(new Runnable() {

			@Override
			public void run() {
				// TODO Auto-generated method stub
				GameCenterSDK.getInstance().doGetTokenAndSsoid(new ApiCallback() {

					@Override
					public void onSuccess(String resultMsg) {
						try {
							// JSONObject json = new JSONObject(resultMsg);
							// String token = json.getString("token");
							// String ssoid = json.getString("ssoid");
							UnityPlayer.UnitySendMessage(gameObject, "OnGetToken", resultMsg);
							Log.e("获取ssoid成功", "getTokenAndSsoid calling...");
							//
							//
							//
							// Toast.makeText(DemoActivity.this,
							// "token = " + token + "ssoid = " + ssoid,
							// Toast.LENGTH_LONG).show();

						} catch (Exception e) {
							e.printStackTrace();
						}
					}

					@Override
					public void onFailure(String content, int resultCode) {

					}
				});
			}
		});
	}

	private static void doGetUserInfoByCpClient(final String gameObject, String token, String ssoid) {
		GameCenterSDK.getInstance().doGetUserInfo(new ReqUserInfoParam(token, ssoid), new ApiCallback() {

			@Override
			public void onSuccess(String resultMsg) {// json格式
				// Toast.makeText(DemoActivity.this, resultMsg,
				// Toast.LENGTH_LONG).show();
				UnityPlayer.UnitySendMessage(gameObject, "OnGetUserInfoSuccess", resultMsg);
			}

			@Override
			public void onFailure(String resultMsg, int resultCode) {

			}
		});
	}

	/**
	 * pay:(). <br/>
	 * TODO().<br/>
	 * oppo支付
	 * 
	 * @author lyh
	 * @param gameObject
	 * @param amount
	 *            金额(分)
	 * @param goodsName(商品名称)
	 * @param callback
	 *            (服务端回调地址)
	 */
	public static void pay(final String gameObject, final int amount, final String goodsName,final String goodsDes, final String userId,
		final String callback) {
		Log.e("充值", "pay calling...");
		UnityPlayer.currentActivity.runOnUiThread(new Runnable() {

			@Override
			public void run() {
				// TODO Auto-generated method stub
				// CP 支付参数
				// int amount = 1; // 支付金额，单位分
				PayInfo payInfo = new PayInfo(System.currentTimeMillis() + new Random().nextInt(1000) + "", "自定义字段",
					amount);
				payInfo.setProductDesc(goodsDes);
				payInfo.setProductName(goodsName);
				payInfo.setCallbackUrl(callback);
				payInfo.setAttach( userId);

				GameCenterSDK.getInstance().doSinglePay(UnityPlayer.currentActivity, payInfo, new SinglePayCallback() {
					// add OPPO 支付成功处理逻辑~
					public void onSuccess(String resultMsg) {

						UnityPlayer.UnitySendMessage(gameObject, "OnPaySuccess", "支付成功");
					}

					// add OPPO 支付失败处理逻辑~
					public void onFailure(String resultMsg, int resultCode) {
						if (PayResponse.CODE_CANCEL != resultCode) {
							UnityPlayer.UnitySendMessage(gameObject, "OnPayFail", "支付失败");
						} else { // 取消支付处理
							UnityPlayer.UnitySendMessage(gameObject, "OnPayCancel", "支付取消");
						}
					}

					/*
					 * bySelectSMSPay 为true表示回调来自于支付渠道列表选择短信支付，false表示没有
					 * 网络等非主动选择短信支付时候的回调
					 */
					public void onCallCarrierPay(PayInfo payInfo, boolean bySelectSMSPay) {
						// add 运营商支付逻辑~
						// Toast.makeText(DemoActivity.this,
						// "运营商支付",Toast.LENGTH_SHORT).show();
					}
				});

			}
		});
	}

	/**
	 * onResume:(). <br/>
	 * TODO().<br/>
	 * onResume & onPause（控制浮标的显示和隐藏，需成对调用）
	 * 
	 * @author lyh
	 */
	public static void onResume() {
		UnityPlayer.currentActivity.runOnUiThread(new Runnable() {

			@Override
			public void run() {
				// TODO Auto-generated method stub
				GameCenterSDK.getInstance().onResume(UnityPlayer.currentActivity);
			}
		});

	}

	public static void onPause() {
		UnityPlayer.currentActivity.runOnUiThread(new Runnable() {

			@Override
			public void run() {
				// TODO Auto-generated method stub
				GameCenterSDK.getInstance().onPause();
			}
		});

	}

	/**
	 * exitSdk:(). <br/>
	 * TODO().<br/>
	 * 退出游戏
	 * 
	 * @author lyh
	 */
	public static void exitSdk(final String gameObject) {
		
		UnityPlayer.currentActivity.runOnUiThread(new Runnable() {
			
			@Override
			public void run() {
				// TODO Auto-generated method stub
				GameCenterSDK.getInstance().onExit(UnityPlayer.currentActivity, new GameExitCallback() {

					@Override
					public void exitGame() {
						// TODO Auto-generated method stub
						// CP 实现游戏退出操作，也可以直接调用
						// AppUtil工具类里面的实现直接强杀进程~
//						 AppUtil.exitGameProcess(UnityPlayer.currentActivity);
						UnityPlayer.UnitySendMessage(gameObject, "OnExitGame","退出成功");
					}
				});
			}
		});
		
		
		
	
	}

	/**
	 * sendUserInfo:(). <br/>
	 * TODO().<br/>
	 * 上传角色信息
	 * 
	 * @author lyh
	 * @param gameObject
	 * @param appId
	 * @param serverId
	 * @param roleName
	 * @param lv
	 */
	public static void sendUserInfo(final String gameObject, final String appId, final String serverId,
		final String roleName, final String lv) {
		UnityPlayer.currentActivity.runOnUiThread(new Runnable() {

			@Override
			public void run() {
				// TODO Auto-generated method stub
				GameCenterSDK.getInstance().doReportUserGameInfoData(
					new ReportUserGameInfoParam(appId, serverId, roleName, lv), new ApiCallback() {

						@Override
						public void onSuccess(String resultMsg) {
							UnityPlayer.UnitySendMessage(gameObject, "OnUserInfoSuccess", "上传成功");
							// Toast.makeText(DemoActivity.this, "success",
							// Toast.LENGTH_LONG).show();
						}

						@Override
						public void onFailure(String resultMsg, int resultCode) {
							UnityPlayer.UnitySendMessage(gameObject, "OnUserInfoFail", resultMsg);
							// Toast.makeText(DemoActivity.this, resultMsg,
							// Toast.LENGTH_LONG).show();
						}
					});
			}
		});
	}
}
