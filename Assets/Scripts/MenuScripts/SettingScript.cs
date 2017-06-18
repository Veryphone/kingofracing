using UnityEngine;
using System.Collections;

public class SettingScript : MonoBehaviour
{
	public GUISkin skin;
	public Font customFont, numberFont;
	public Texture vibrateOn, vibrateOff, lowOn, lowDisable, mediumOn, mediumDisable, highOn, highDisable;
	public GUITexture btnQuickRace, btnBackScene, btnMore, btnFacebook, btnCredit, btnVibrate,
		btnLow, btnMedium, btnHigh, btnPayment, btnFreeCoin;
	private GUIStyle slider, thumb;
	public Texture fillBackground, creditBG;
	public AudioSource audioSource;
	//public SunnetPlugin sunnetPlugin;
	bool isShowCredit, isShowPayment, isShowFreeCoin;
	Rect confirmRect, paymentRect;
	float lastConfirm;

	void Start ()
	{
		ProfileManager.init ();

		audioSource.volume = ProfileManager.setttings.MusicVolume / 100f;
		audioSource.Play ();

		slider = skin.GetStyle ("horizontalslider");
		thumb = skin.GetStyle ("horizontalsliderthumb");
		confirmRect = new Rect (100, 90, 600, 300);
		paymentRect = new Rect (200, 100, 400, 200);

		switch (ProfileManager.setttings.GraphicsQualitiy) {
		case 0:
			btnLow.texture = lowOn;
			btnMedium.texture = mediumDisable;
			btnHigh.texture = highDisable;
			break;

		case 1:
			btnLow.texture = lowDisable;
			btnMedium.texture = mediumOn;
			btnHigh.texture = highDisable;
			break;

		case 2:
			btnLow.texture = lowDisable;
			btnMedium.texture = mediumDisable;
			btnHigh.texture = highOn;
			break;

		default:
			break;
		}

		if (ProfileManager.setttings.IsVibrate == true) {
			btnVibrate.texture = vibrateOn;
		} else {
			btnVibrate.texture = vibrateOff;
		}

		GoogleAnalytics.LogScreen ("Setting Screen");
	}

	void Update ()
	{
		if (Input.GetKeyUp (KeyCode.Escape)) {
//			this.openCredit();
			AutoFade.LoadLevel ("MainMenu", 0.5f, 0.5f, Color.black);
			PlayerPrefs.Save ();
		}

		if (isShowCredit == true || isShowFreeCoin == true || isShowPayment == true) {
			return;
		} else if (Time.timeSinceLevelLoad - lastConfirm < 0.5f) {
			return;
		}

		if (Input.touchCount == 1 && Input.GetTouch (0).phase == TouchPhase.Ended) {
			if (btnQuickRace.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
				btnQuickRace.color = new Color32 (128, 128, 128, 128);
				PlayerPrefs.Save ();
				AutoFade.LoadLevel ("QuickRace", 0.5f, 0.5f, Color.black);
			}
			
			if (btnBackScene.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
				btnBackScene.color = new Color32 (128, 128, 128, 128);
				PlayerPrefs.Save ();
				AutoFade.LoadLevel ("MainMenu", 0.5f, 0.5f, Color.black);
			}
			
			if (btnMore.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
				btnMore.color = new Color32 (128, 128, 128, 128);
				this.openMoregames ();
			}
			
			//if (btnFacebook.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
			//	btnFacebook.color = new Color32 (128, 128, 128, 128);
			//	this.openFacebook ();
			//}
			
			if (btnCredit.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
				btnCredit.color = new Color32 (128, 128, 128, 128);
				this.openCredit ();
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
		} else if (Input.touchCount == 1 && Input.GetTouch (0).phase == TouchPhase.Began) {
			if (btnQuickRace.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
				btnQuickRace.color = new Color32 (128, 128, 128, 45);
			}
			
			if (btnBackScene.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
				btnBackScene.color = new Color32 (128, 128, 128, 45);
			}
			
			if (btnMore.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
				btnMore.color = new Color32 (128, 128, 128, 45);
			}
			
			//if (btnFacebook.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
			//	btnFacebook.color = new Color32 (128, 128, 128, 45);
			//}
			
			if (btnCredit.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
				btnCredit.color = new Color32 (128, 128, 128, 45);
			}

			if (btnPayment.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
				btnPayment.color = new Color32 (128, 128, 128, 45);
			}
			
			if (btnFreeCoin.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
				btnFreeCoin.color = new Color32 (128, 128, 128, 45);
			}

			if (btnVibrate.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
				if (ProfileManager.setttings.IsVibrate == true) {
					btnVibrate.texture = vibrateOff;
					ProfileManager.setttings.IsVibrate = false;
				} else {
					btnVibrate.texture = vibrateOn;
					ProfileManager.setttings.IsVibrate = true;
				}
			}

			if (btnLow.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
				btnLow.texture = lowOn;
				btnMedium.texture = mediumDisable;
				btnHigh.texture = highDisable;
				ProfileManager.setttings.GraphicsQualitiy = 0;
			}
			if (btnMedium.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
				btnLow.texture = lowDisable;
				btnMedium.texture = mediumOn;
				btnHigh.texture = highDisable;
				ProfileManager.setttings.GraphicsQualitiy = 1;
			}
			if (btnHigh.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
				btnLow.texture = lowDisable;
				btnMedium.texture = mediumDisable;
				btnHigh.texture = highOn;
				ProfileManager.setttings.GraphicsQualitiy = 2;
			}

		} else if (Input.touchCount == 1 && Input.GetTouch (0).phase == TouchPhase.Moved) {
			if (!btnQuickRace.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
				btnQuickRace.color = new Color32 (128, 128, 128, 128);
			}
			
			if (!btnBackScene.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
				btnBackScene.color = new Color32 (128, 128, 128, 128);
			}
			
			if (!btnMore.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
				btnMore.color = new Color32 (128, 128, 128, 128);
			}
			
			//if (!btnFacebook.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
			//	btnFacebook.color = new Color32 (128, 128, 128, 128);
			//}
			
			if (!btnCredit.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
				btnCredit.color = new Color32 (128, 128, 128, 128);
			}

			if (!btnPayment.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
				btnPayment.color = new Color32 (128, 128, 128, 128);
			}
			
			if (!btnFreeCoin.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
				btnFreeCoin.color = new Color32 (128, 128, 128, 128);
			}
		}
	}

	void OnGUI ()
	{
		GUI.matrix = Matrix4x4.TRS (Vector3.zero, Quaternion.identity, new Vector3 (Screen.width / 800f, Screen.height / 480f, 1f));
		
		GUIStyle normalSize = new GUIStyle ("label");
		normalSize.font = customFont;
		normalSize.fontSize = 17;
		
		GUIStyle largeSize = new GUIStyle ("label");
		largeSize.font = customFont;
		largeSize.fontSize = 20;
		
		GUIStyle number = new GUIStyle ("label");
		number.font = numberFont;
		number.fontSize = 20;
		
		GUI.Label (new Rect (28, 32, 100, 50), Language.BACK, normalSize);
		GUI.Label (new Rect (123, 32, 200, 50), Language.QUICKRACE, normalSize);
		GUI.Label (new Rect (685, 28, 200, 50), Language.FREECOIN, normalSize);
		
		GUI.Label (new Rect (99, 430, 300, 50), Language.MOREGAME, largeSize);
		//GUI.Label (new Rect (348, 430, 300, 50), Language.FACEBOOK, largeSize);
		GUI.Label (new Rect (595, 430, 300, 50), Language.CREDIT, largeSize);
		
		GUI.Label (new Rect (340, 22, 100, 50), ProfileManager.userProfile.Star + string.Empty, number);
		GUI.Label (new Rect (498, 22, 200, 50), ProfileManager.userProfile.Cash + string.Empty, number);

		ProfileManager.setttings.SoundVolume = GUI.HorizontalSlider (new Rect (370, 158, 249, 20), ProfileManager.setttings.SoundVolume, 0, 100, slider, thumb);

		ProfileManager.setttings.MusicVolume = GUI.HorizontalSlider (new Rect (370, 203, 249, 20), ProfileManager.setttings.MusicVolume, 0, 100, slider, thumb);
		audioSource.volume = ProfileManager.setttings.MusicVolume / 100f;

		ProfileManager.setttings.Sensitivity = GUI.HorizontalSlider (new Rect (370, 248, 249, 20), ProfileManager.setttings.Sensitivity, 0, 100, slider, thumb);

		if (isShowCredit) {
			confirmRect = GUI.Window (1, confirmRect, showCreditWindow, string.Empty);
		} else if (isShowFreeCoin) {
			paymentRect = GUI.Window (1, paymentRect, showFreeCoinWindow, string.Empty);
		} else if (isShowPayment) {
			paymentRect = GUI.Window (1, paymentRect, showPaymentWindow, string.Empty);
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

	public void openMoregames ()
	{
        //sunnetPlugin.openDownloadLink ();
        //ChartboostController.chartBoost.MoreApp();
    }

 //   public void openFacebook ()
	//{
	//	//sunnetPlugin.postScoreToFacebook (ProfileManager.userProfile.Star);
	//	ProfileManager.achievementProfile.IsConnectFacebook = true;
	//	PlayerPrefs.Save ();
	//}

	public void openCredit ()
	{
		this.isShowCredit = true;
	}

	void showCreditWindow (int windowID)
	{
		GUI.FocusWindow (1);
		
		GUI.DrawTexture (new Rect (4, 4, 593, 293), creditBG);

        //GUIStyle centeredTextStyle = new GUIStyle ("label");
        //centeredTextStyle.alignment = TextAnchor.MiddleCenter;
        //centeredTextStyle.font = numberFont;

        //GUIStyle leftTextStyle = new GUIStyle ("label");
        //leftTextStyle.alignment = TextAnchor.MiddleLeft;
        //leftTextStyle.font = numberFont;


        //centeredTextStyle.fontSize = 40;
        //GUI.Label (new Rect (50, -20, 500, 100), "Information", centeredTextStyle);

        //centeredTextStyle.fontSize = 15;
        //GUI.color = Color.yellow;
        //GUI.Label (new Rect (50, 40, 500, 50), "Project Manager", leftTextStyle);
        //GUI.color = Color.white;
        //GUI.Label (new Rect (50, 70, 500, 50), "Do Son Tung", leftTextStyle);

        //GUI.color = Color.yellow;
        //GUI.Label (new Rect (50, 130, 500, 50), "Developers", leftTextStyle);
        //GUI.color = Color.white;
        //GUI.Label (new Rect (50, 160, 500, 50), "Do Anh Tuan", leftTextStyle);
        //GUI.Label (new Rect (50, 190, 500, 50), "Trinh Viet Van", leftTextStyle);

        //GUI.color = Color.yellow;
        //GUI.Label (new Rect (220, 40, 500, 50), "Audio and soundtrack", leftTextStyle);
        //GUI.color = Color.white;
        //GUI.Label (new Rect (220, 70, 500, 50), "Nguyen Hoang Anh", leftTextStyle);

        //GUI.color = Color.yellow;
        //GUI.Label (new Rect (220, 130, 500, 50), "Quality Assurance", leftTextStyle);
        //GUI.color = Color.white;
        //GUI.Label (new Rect (220, 160, 500, 50), "Luong Duc Thuan", leftTextStyle);

        //GUI.color = Color.yellow;
        //GUI.Label (new Rect (450, 40, 500, 50), "Graphics Artists", leftTextStyle);
        //GUI.color = Color.white;
        //GUI.Label (new Rect (450, 70, 500, 50), "Dinh Ngoc Tan", leftTextStyle);
        //GUI.Label (new Rect (450, 100, 500, 50), "Nguyen Tuan Vu", leftTextStyle);
        //GUI.Label (new Rect (450, 130, 500, 50), "Phan Trung Kien", leftTextStyle);
        //GUI.Label (new Rect (450, 160, 500, 50), "Do Dinh Tuan", leftTextStyle);

        GUIStyle buttonStyle = new GUIStyle("button");
        buttonStyle.alignment = TextAnchor.MiddleCenter;
        buttonStyle.font = numberFont;

        if (GUI.Button (new Rect (250, 250, 100, 40), "OK", buttonStyle)) {
			isShowCredit = false;
			lastConfirm = Time.timeSinceLevelLoad;
		}
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
            //ChartboostController.chartBoost.PlayVideo();
            this.isShowPayment = false;
			lastConfirm = Time.timeSinceLevelLoad;
		}
		
		if (GUI.Button (new Rect (250, 120, 100, 40), "No", buttonStyle)) {
			this.isShowPayment = false;
			lastConfirm = Time.timeSinceLevelLoad;
		}
	}
}
