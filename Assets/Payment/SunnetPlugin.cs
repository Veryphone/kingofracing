using UnityEngine;
using System.Collections;

public class SunnetPlugin : MonoBehaviour
{
		//public static AndroidJavaClass payment;
		////
		//[HideInInspector]
		//public string
		//		message = string.Empty;
		////
		//public const int PAYMENT_ALIGN_TOP = 0;
		//public const int PAYMENT_ALIGN_BOTTOM = 1;
		//public const int PAYMENT_ALIGN_CENTER_VERTICAL = 2;
		////
		//public const int PAYMENT_ALIGN_LEFT = 3;
		//public const int PAYMENT_ALIGN_RIGHT = 4;
		//public const int PAYMENT_ALIGN_CENTER_HORIZONTAL = 5;

		//void Start ()
		//{
		//		this.init ("com.dev.game.kingofracing", "King Of Racing 3D", "638", "2", string.Empty);
		//		this.initFacebook ("King of Racing 3D", "690909877593974");
		//}
	
		//AndroidJavaObject getUnityPlayerObject ()
		//{
		//		AndroidJavaClass parentClass = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
		//		AndroidJavaObject activityObject = parentClass.GetStatic<AndroidJavaObject> ("currentActivity");
		
		//		return activityObject;
		//}
	
		//void init (string packageName, string appName, string appID, string channelID, string tapjoySecretCode)
		//{
		//		this.preparePayment ();
		//		payment.CallStatic ("init", packageName, appName, appID, channelID, tapjoySecretCode);
		//}

		//void initFacebook (string gameName, string facebookAppID)
		//{
		//		this.preparePayment ();
		//		payment.CallStatic ("initFacebook", gameName, facebookAppID);
		//}
	
		//void preparePayment ()
		//{
		//		if (payment == null) {
		//				payment = new AndroidJavaClass ("vn.sunnet.lib.Payment");
		//		}
		//}
	
		//public void onPaymentSuccess (string coin)
		//{
		//		this.message = coin;
		//		//Process Coin here
		//		showToast ("You received " + (int.Parse (coin) / 2) + " coins");

		//		ProfileManager.userProfile.Cash += int.Parse (coin) / 2;
		//		PlayerPrefs.Save ();
		//}

		//public void onPaymentFailure (string type)
		//{
		//		this.message = type;
		//}

		//public void onPaymentDeny (string type)
		//{
		//		this.message = type;
		//}

		//public void OnFacebookShareSuccess (string mes)
		//{
		//		showToast ("Share successful!");
		//		ProfileManager.achievementProfile.ShareFacebookTime++;
		//		PlayerPrefs.Save ();
		//}

		//public void openPayment ()
		//{
		//		this.preparePayment ();
		//		//payment.CallStatic ("showPayment", getUnityPlayerObject ());
		//}

		//public void postScoreToFacebook (int score)
		//{
		//		this.preparePayment ();
		//		payment.CallStatic ("postScoreToFacebook", getUnityPlayerObject (), score);
		//}

		//public void loadLargeAd (int alignHorizontal, int alignVertical)
		//{
		//		this.preparePayment ();

		//		//payment.CallStatic ("loadLargeAd", getUnityPlayerObject (), alignHorizontal, alignVertical);
		//}

		//public void loadSmallAd (int alignHorizontal, int alignVertical)
		//{
		//		this.preparePayment ();
		
		//		//payment.CallStatic ("loadSmallAd", getUnityPlayerObject (), alignHorizontal, alignVertical);
		//}

		//public void loadFullscreenAd (int alignHorizontal, int alignVertical)
		//{
		//		this.preparePayment ();
		
		//		//payment.CallStatic ("loadFullScreenAd", getUnityPlayerObject (), alignHorizontal, alignVertical);
		//}

		//public void hideAd ()
		//{
		//		this.preparePayment ();

		//		payment.CallStatic ("closeAd", getUnityPlayerObject ());
		//}

		//public void openDownloadLink ()
		//{
		//		this.preparePayment ();
		
		//		//payment.CallStatic ("openDownloadLink", getUnityPlayerObject ());
		//}
	
		//public void showToast (string message)
		//{
		//		this.preparePayment ();
		
		//		payment.CallStatic ("showToast", getUnityPlayerObject (), message);
		//}

		//public void logError (string tag, string text)
		//{
		//		this.preparePayment ();

		//		payment.CallStatic ("logError", getUnityPlayerObject (), tag, text);
		//}

		//public void logDebug (string tag, string text)
		//{
		//		this.preparePayment ();
		
		//		payment.CallStatic ("logDebug", getUnityPlayerObject (), tag, text);
		//}

		//public void logWarning (string tag, string text)
		//{
		//		this.preparePayment ();
		
		//		payment.CallStatic ("logWarning", getUnityPlayerObject (), tag, text);
		//}

		//public void logInfo (string tag, string text)
		//{
		//		this.preparePayment ();
		
		//		payment.CallStatic ("logInfo", getUnityPlayerObject (), tag, text);
		//}

		//public void logVerbose (string tag, string text)
		//{
		//		this.preparePayment ();
		
		//		payment.CallStatic ("logVerbose", getUnityPlayerObject (), tag, text);
		//}

		//public static bool CheckForInternetConnection ()
		//{
		//		System.Net.WebClient client = null;
		//		System.IO.Stream stream = null;

		//		try {
		//				client = new System.Net.WebClient ();
		//				stream = client.OpenRead ("http://www.google.com");
		//				return true;
		//		} catch {
		//				return false;
		//		} finally {
		//				if (client != null) {
		//						client.Dispose ();
		//				}
		//				if (stream != null) {
		//						stream.Dispose ();
		//				}
		//		}
		//}
}
