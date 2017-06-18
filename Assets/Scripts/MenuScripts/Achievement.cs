using UnityEngine;
using System.Collections;

public class Achievement : MonoBehaviour
{
	public GUITexture btnBackScene, btnQuickRace, btnPayment, btnFreeCoin;
	public Texture2D barDiv;
	public Texture2D[] arIcon;
	public Font customFont1, customFont2, numberFont;
	private Vector2 scrollPosition = Vector2.zero;
	private float sens, sensTime;
	private Color tempGUIColor;
	public Texture fillBackground;
	public AudioSource audioSource;
	//public SunnetPlugin sunnetPlugin;
	bool isShowPayment, isShowFreeCoin;
	Rect confirmRect;
	float lastConfirm;

	void Start ()
	{
		ProfileManager.init ();

		audioSource.volume = ProfileManager.setttings.MusicVolume / 100f;
		audioSource.Play ();

		confirmRect = new Rect (200, 100, 400, 200);

		GoogleAnalytics.LogScreen ("Achievement");
	}

	void Update ()
	{
		if (Input.GetKeyUp (KeyCode.Escape)) {
			AutoFade.LoadLevel ("MainMenu", 0.5f, 0.5f, Color.black);
		}

		if (isShowFreeCoin == true || isShowPayment == true) {
			return;
		} else if (Time.timeSinceLevelLoad - lastConfirm < 0.5f) {
			return;
		}
		
		if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began) {
			if (btnBackScene.GetScreenRect ().Contains (Input.GetTouch (0).position))
				btnBackScene.color = new Color32 (128, 128, 128, 45);
			
			if (btnQuickRace.GetScreenRect ().Contains (Input.GetTouch (0).position))
				btnQuickRace.color = new Color32 (128, 128, 128, 45);

			if (btnPayment.GetScreenRect ().Contains (Input.GetTouch (0).position))
				btnPayment.color = new Color32 (128, 128, 128, 45);
			
			if (btnFreeCoin.GetScreenRect ().Contains (Input.GetTouch (0).position))
				btnFreeCoin.color = new Color32 (128, 128, 128, 45);
		}
		
		if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Stationary) {
			sens = 0;
			sensTime = 0;
		}

		if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Moved) {
			sens = 2 * Input.GetTouch (0).deltaPosition.y;
			
			if (!btnBackScene.GetScreenRect ().Contains (Input.GetTouch (0).position))
				btnBackScene.color = new Color32 (128, 128, 128, 128);
			
			if (!btnQuickRace.GetScreenRect ().Contains (Input.GetTouch (0).position))
				btnQuickRace.color = new Color32 (128, 128, 128, 128);

			if (!btnPayment.GetScreenRect ().Contains (Input.GetTouch (0).position))
				btnPayment.color = new Color32 (128, 128, 128, 128);
			
			if (!btnFreeCoin.GetScreenRect ().Contains (Input.GetTouch (0).position))
				btnFreeCoin.color = new Color32 (128, 128, 128, 128);
		}

		if (Input.touchCount == 1 && Input.GetTouch (0).phase == TouchPhase.Ended) {
			if (btnBackScene.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
				btnBackScene.color = new Color32 (128, 128, 128, 128);
				AutoFade.LoadLevel ("MainMenu", 0.5f, 0.5f, Color.black);
			}
			
			if (btnQuickRace.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
				btnQuickRace.color = new Color32 (128, 128, 128, 128);
				AutoFade.LoadLevel ("QuickRace", 0.5f, 0.5f, Color.black);
			}

			if (btnPayment.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
				btnPayment.color = new Color32 (128, 128, 128, 128);
				this.openPayment ();
			}
			
			if (btnFreeCoin.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
				btnFreeCoin.color = new Color32 (128, 128, 128, 128);
                //				this.openFreecoin ();
                //sunnetPlugin.openDownloadLink ();
                //ChartboostController.chartBoost.PlayVideo();
            }
		}
		
		// Smooth scrolling
		sensTime += 0.1f * Time.deltaTime;
		if (sens != 0) {
			sens = Mathf.Lerp (sens, 0, sensTime);
			scrollPosition.y += sens;
		} else {
			sens = 0;
			sensTime = 0;
		}
	}
	
	void OnGUI ()
	{
		GUI.matrix = Matrix4x4.TRS (Vector3.zero, Quaternion.identity, new Vector3 (Screen.width / 800f, Screen.height / 480f, 1f));

		GUIStyle largeText = new GUIStyle ("label");
		largeText.font = customFont1;
		largeText.fontSize = 25;
		
		GUIStyle normalSize = new GUIStyle ("label");
		normalSize.font = customFont1;
		normalSize.fontSize = 17;
		
		GUIStyle smallText = new GUIStyle ("label");
		smallText.font = customFont2;
		smallText.fontSize = 15;

		GUIStyle number = new GUIStyle ("label");
		number.font = numberFont;
		number.fontSize = 20;
		
		GUI.Label (new Rect (28, 32, 100, 50), Language.BACK, normalSize);
		GUI.Label (new Rect (123, 32, 200, 50), Language.QUICKRACE, normalSize);
		GUI.Label (new Rect (685, 28, 200, 50), Language.FREECOIN, normalSize);

		GUI.Label (new Rect (340, 22, 100, 50), ProfileManager.userProfile.Star + string.Empty, number);
		GUI.Label (new Rect (498, 22, 200, 50), ProfileManager.userProfile.Cash + string.Empty, number);
		
		// BEGIN SCROLL
		scrollPosition = GUI.BeginScrollView (new Rect (0, 67, 800, 475), scrollPosition, new Rect (0, 0, 800, 1224), new GUIStyle (), new GUIStyle ());
		
		for (int i=0; i<arIcon.Length; i++) {
			GUI.DrawTexture (new Rect (0, barDiv.height * i, barDiv.width, barDiv.height), barDiv);

			if (ProfileManager.achievementProfile.isReachAchievement (i) == true) {
				GUI.color = Color.white;
			} else {
				GUI.color = Color.gray;
			}

			GUI.DrawTexture (new Rect (130, barDiv.height * i + 1, arIcon [i].width, arIcon [i].height), arIcon [i]);
			GUI.color= Color.white;

			if (ProfileManager.achievementProfile.isReachAchievement (i) == true) {
				GUI.color = Color.yellow;
			} else {
				GUI.color = Color.gray;
			}
			GUI.Label (new Rect (220, 10 + barDiv.height * i, 570, 59), getAchievementName (i), largeText);
			GUI.Label (new Rect (220, 35 + barDiv.height * i, 570, 59), getAchievementDescription (i), smallText);
			GUI.color = Color.white;
		}
		
		GUI.EndScrollView ();

		if (isShowFreeCoin) {
			confirmRect = GUI.Window (1, confirmRect, showFreeCoinWindow, string.Empty);
		} else if (isShowPayment) {
			confirmRect = GUI.Window (1, confirmRect, showPaymentWindow, string.Empty);
		}
	}

	string getAchievementName (int id)
	{
		switch (id) {
		case 0:
			return "FIRST BLOOD";
		case 1:
			return "NEW-CAR SMELL";
		case 2:
			return "CAR COLLECTOR";
		case 3:
			return "CAR NUT";
		case 4:
			return "ALL-STAR";
		case 5:
			return "BLAZIN' FAST";
		case 6:
			return "HANDYMAN";
		case 7:
			return "THE MECHANIC";
		case 8:
			return "HOT HAND";
		case 9:
			return "FURIOUS FIFTY";
		case 10:
			return "CENTURION";
		case 11:
			return "SHARE THE PAIN";
		case 12:
			return "SLOW AND STEADY";
		case 13:
			return "PLUGGED IN";
		case 14:
			return "MAX POWER";
		case 15:
			return "CRANK IT UP";
		case 16:
			return "ULTIMATE UPGRADE";
		default:
			return string.Empty;
		}
	}

	string getAchievementDescription (int id)
	{
		switch (id) {
		case 0:
			return "WIN 1 ROUND IN CAREER MODE";
		case 1:
			return "BUY A NEW CAR";
		case 2:
			return "HAVE 5 CARS";
		case 3:
			return "BUY ALL CARS";
		case 4:
			return "EARN ALL STARS IN CAREER";
		case 5:
			return "REACH A TOP SPEED OVER 300KM/H";
		case 6:
			return "BUY YOUR FIRST CAR UPGRADE";
		case 7:
			return "BUY ALL UPGRADES OF 1 CAR";
		case 8:
			return "WIN 10 ROUND IN QUICK RACE MODE";
		case 9:
			return "WIN 50 ROUND IN QUICK RACE MODE";
		case 10:
			return "WIN 100 ROUND IN QUICK RACE MODE";
		case 11:
			return "SHARE ON FACEBOOK 3 TIMES";
		case 12:
			return "WIN A CAREER MODE WITHOUT USING NITRO";
		case 13:
			return "CONNECT WITH FACEBOOK";
		case 14:
			return "FULLY UPGRADE 2 CAR";
		case 15:
			return "FULLY UPGRADE 5 CAR";
		case 16:
			return "FULLY UPGRADE 10 CAR";
		default:
			return string.Empty;
		}
	}

	public void openPayment ()
	{
		this.isShowPayment = true;
	}
	
	public void openFreecoin ()
	{
		this.isShowFreeCoin = true;
	}

	void showFreeCoinWindow (int windowID)
	{
		GUI.FocusWindow (1);
		
		GUI.DrawTexture (new Rect (4, 4, 393, 193), fillBackground);
		
		GUIStyle centeredTextStyle = new GUIStyle ("label");
		centeredTextStyle.alignment = TextAnchor.MiddleCenter;
		centeredTextStyle.font = numberFont;
		
		GUIStyle buttonStyle = new GUIStyle ("button");
		buttonStyle.alignment = TextAnchor.MiddleCenter;
		buttonStyle.font = numberFont;
		
		centeredTextStyle.fontSize = 40;
		GUI.Label (new Rect (0, -20, 400, 100), "Free coins", centeredTextStyle);
		
		centeredTextStyle.fontSize = 15;
		GUI.Label (new Rect (0, 40, 400, 50), "Do you want to get free dollar?", centeredTextStyle);
		
		if (GUI.Button (new Rect (60, 120, 100, 40), "Yes", buttonStyle)) {
            //sunnetPlugin.openPayment ();
            //ChartboostController.chartBoost.PlayVideo();
            this.isShowFreeCoin = false;
			lastConfirm = Time.timeSinceLevelLoad;
		}
		
		if (GUI.Button (new Rect (250, 120, 100, 40), "No", buttonStyle)) {
			this.isShowFreeCoin = false;
			lastConfirm = Time.timeSinceLevelLoad;
		}
	}
	
	void showPaymentWindow (int windowID)
	{
		GUI.FocusWindow (1);
		
		GUI.DrawTexture (new Rect (4, 4, 393, 193), fillBackground);
		
		GUIStyle centeredTextStyle = new GUIStyle ("label");
		centeredTextStyle.alignment = TextAnchor.MiddleCenter;
		centeredTextStyle.font = numberFont;
		
		GUIStyle buttonStyle = new GUIStyle ("button");
		buttonStyle.alignment = TextAnchor.MiddleCenter;
		buttonStyle.font = numberFont;
		
		centeredTextStyle.fontSize = 40;
		GUI.Label (new Rect (0, -20, 400, 100), "Payment", centeredTextStyle);
		
		centeredTextStyle.fontSize = 15;
		GUI.Label (new Rect (0, 40, 400, 50), "Do you want to buy more dollar?", centeredTextStyle);
		
		if (GUI.Button (new Rect (60, 120, 100, 40), "Yes", buttonStyle)) {
            //sunnetPlugin.openPayment ();
           // ChartboostController.chartBoost.PlayVideo();
            this.isShowPayment = false;
			lastConfirm = Time.timeSinceLevelLoad;
		}
		
		if (GUI.Button (new Rect (250, 120, 100, 40), "No", buttonStyle)) {
			this.isShowPayment = false;
			lastConfirm = Time.timeSinceLevelLoad;
		}
	}
}
