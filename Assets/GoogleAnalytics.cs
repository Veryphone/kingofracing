using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GoogleAnalytics : MonoBehaviour {
	
	public static string propertyID="UA-51748862-1";
	
	public static string bundleID="com.dev.game.kingofracing";
	public static string appName="King of Racing";
	public static string appVersion="1.0";
	
	public static string screenRes=Screen.width + "x" + Screen.height;
	public static string clientID=SystemInfo.deviceUniqueIdentifier;

	public static void LogScreen(string title)
	{
		
		title = WWW.EscapeURL(title);
		
		var url = "http://www.google-analytics.com/collect?v=1&ul=en-us&t=appview&sr="+screenRes+"&an="+WWW.EscapeURL(appName)+"&a=448166238&tid="+propertyID+"&aid="+bundleID+"&cid="+WWW.EscapeURL(clientID)+"&_u=.sB&av="+appVersion+"&_v=ma1b3&cd="+title+"&qt=2500&z=185";
		
		WWW request = new WWW(url);		
	}
	
}