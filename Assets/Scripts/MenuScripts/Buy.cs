using UnityEngine;
using System.Collections;

public class Buy : MonoBehaviour
{
	public Font customFont, numberFont;
	public GUITexture btnQuickRace, btnBackScene, btnFullNitro, btnFirstPos, btnX2, 
		btnStartRace, image, btnPayment, btnFreeCoin;
	public Texture tokyo, china, hanoi, nevada, newyork, france;
	public GUITexture firstRacer;
	public GUITexture nitroFull;
	public GUITexture rewardDouble;
	public Texture fillBackground;
	public AudioSource audioSource;
	//public SunnetPlugin sunnetPlugin;
	bool isShowPayment, isShowFreeCoin;
	Rect confirmRect;
	float lastConfirm;

	void Start ()
	{
		ProfileManager.init ();
		GameData.resetItem ();
//		sunnetPlugin.hideAd ();

		audioSource.volume = ProfileManager.setttings.MusicVolume / 100f;
		audioSource.Play ();

		confirmRect = new Rect (200, 100, 400, 200);

		switch (GameData.selectedMap) {
		case GameData.MAP_NAME.TOKYO:
			image.texture = tokyo;
			break;
		case GameData.MAP_NAME.CHINA:
			image.texture = china;
			break;
		case GameData.MAP_NAME.HANOI:
			image.texture = hanoi;
			break;
		case GameData.MAP_NAME.NEVADA:
			image.texture = nevada;
			break;
		case GameData.MAP_NAME.NEWYORK:
			image.texture = newyork;
			break;
		case GameData.MAP_NAME.PARIS:
			image.texture = france;
			break;
		default:
			image.texture = newyork;
			break;
		}

		GoogleAnalytics.LogScreen ("Buy Screen");
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

		if (Input.GetKeyUp (KeyCode.Space)) {
			SceneLoader.scene = "SLevel_" + GameData.getMapID (GameData.selectedMap);
			Application.LoadLevel ("Loading");
		}
		
		if (Input.touchCount == 1 && Input.GetTouch (0).phase == TouchPhase.Ended) {
			if (btnQuickRace.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
				btnQuickRace.color = new Color32 (128, 128, 128, 128);
				AutoFade.LoadLevel ("QuickRace", 0.5f, 0.5f, Color.black);
			}
			
			if (btnBackScene.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
				btnBackScene.color = new Color32 (128, 128, 128, 128);
				AutoFade.LoadLevel ("MainMenu", 0.5f, 0.5f, Color.black);
			}
			
			if (btnFullNitro.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
				btnFullNitro.color = new Color32 (128, 128, 128, 128);

				if (ProfileManager.userProfile.Cash >= 1800) {
					ProfileManager.userProfile.Cash -= 1800;
					PlayerPrefs.Save ();
					GameData.isNitroFull = true;
				} else {
					this.openPayment ();
				}
			}
			
			if (btnFirstPos.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
				btnFirstPos.color = new Color32 (128, 128, 128, 128);

				if (ProfileManager.userProfile.Cash >= 1500) {
					ProfileManager.userProfile.Cash -= 1500;
					PlayerPrefs.Save ();
					GameData.isFirstRacer = true;
				} else {
					this.openPayment ();
				}
			}
			
			if (btnX2.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
				btnX2.color = new Color32 (128, 128, 128, 128);

				if (ProfileManager.userProfile.Cash >= 2000) {
					ProfileManager.userProfile.Cash -= 2000;
					PlayerPrefs.Save ();
					GameData.isDoubleReward = true;
				} else {
					this.openPayment ();
				}
			}

			if (btnStartRace.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
				btnStartRace.color = new Color32 (128, 128, 128, 128);
				SceneLoader.scene = "SLevel_" + GameData.getMapID (GameData.selectedMap);
				Application.LoadLevel ("Loading");
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
			
			if (btnFullNitro.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
				btnFullNitro.color = new Color32 (128, 128, 128, 45);
			}
			
			if (btnFirstPos.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
				btnFirstPos.color = new Color32 (128, 128, 128, 45);
			}
			
			if (btnX2.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
				btnX2.color = new Color32 (128, 128, 128, 45);
			}

			if (btnStartRace.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
				btnStartRace.color = new Color32 (128, 128, 128, 45);
			}

			if (btnPayment.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
				btnPayment.color = new Color32 (128, 128, 128, 45);
			}
			
			if (btnFreeCoin.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
				btnFreeCoin.color = new Color32 (128, 128, 128, 45);
			}
		} else if (Input.touchCount == 1 && Input.GetTouch (0).phase == TouchPhase.Moved) {
			if (!btnQuickRace.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
				btnQuickRace.color = new Color32 (128, 128, 128, 128);
			}
			
			if (!btnBackScene.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
				btnBackScene.color = new Color32 (128, 128, 128, 128);
			}
			
			if (!btnFullNitro.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
				btnFullNitro.color = new Color32 (128, 128, 128, 128);
			}
			
			if (!btnFirstPos.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
				btnFirstPos.color = new Color32 (128, 128, 128, 128);
			}
			
			if (!btnX2.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
				btnX2.color = new Color32 (128, 128, 128, 128);
			}

			if (!btnStartRace.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
				btnStartRace.color = new Color32 (128, 128, 128, 128);
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

		GUI.Label (new Rect (35, 280, 300, 50), "Start in the 1st position", normalSize);
		if (GameData.isFirstRacer == false) {
			firstRacer.enabled = true;
			GUI.Label (new Rect (110, 390, 300, 50), "1500", largeSize);
		} else {
			firstRacer.enabled = false;
			GUI.Label (new Rect (100, 370, 300, 50), "Bought", largeSize);
		}

		GUI.Label (new Rect (310, 280, 300, 50), "Start with full nitro", normalSize);
		if (GameData.isNitroFull == false) {
			nitroFull.enabled = true;
			GUI.Label (new Rect (370, 390, 300, 50), "1800", largeSize);
		} else {
			nitroFull.enabled = false;
			GUI.Label (new Rect (360, 370, 300, 50), "Bought", largeSize);
		}

		GUI.Label (new Rect (560, 280, 300, 50), "Double reward received", normalSize);
		if (GameData.isDoubleReward == false) {
			rewardDouble.enabled = true;
			GUI.Label (new Rect (630, 390, 300, 50), "2000", largeSize);
		} else {
			rewardDouble.enabled = false;
			GUI.Label (new Rect (620, 370, 300, 50), "Bought", largeSize);
		}
		
		GUI.Label (new Rect (340, 22, 100, 50), ProfileManager.userProfile.Star + string.Empty, number);
		GUI.Label (new Rect (498, 22, 200, 50), ProfileManager.userProfile.Cash + string.Empty, number);

		if (isShowFreeCoin) {
			confirmRect = GUI.Window (1, confirmRect, showFreeCoinWindow, string.Empty);
		} else if (isShowPayment) {
			confirmRect = GUI.Window (1, confirmRect, showPaymentWindow, string.Empty);
		}

		switch (GameData.selectedMode) {
		case GameData.GAME_MODE.NORMAL:
			GUI.Label (new Rect (10, 75, 550, 50), "Finish in 3 lead position to win", number);
			GUI.Label (new Rect (10, 215, 200, 50), GameData.numberEnemies + " opponents", number);
			GUI.Label (new Rect (650, 215, 200, 50), GameData.numberRaces + " laps", number);
			break;
			
		case GameData.GAME_MODE.CHECK_POINT:
			GUI.Label (new Rect (10, 75, 550, 50), "Get enough check points within allowed time", number);
			GUI.Label (new Rect (10, 215, 200, 50), GameData.numberEnemies + " opponents", number);
			GUI.Label (new Rect (650, 215, 200, 50), GameData.duration + " seconds", number);

			break;
			
		case GameData.GAME_MODE.TIME_TRIAL:
			GUI.Label (new Rect (10, 75, 550, 50), "Finish in 3 lead position within allowed time", number);
			GUI.Label (new Rect (10, 215, 200, 50), GameData.numberEnemies + " opponents", number);
			GUI.Label (new Rect (650, 215, 200, 50), GameData.duration + " seconds", number);
			break;
			
		default:
			break;
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
            //sunnetPlugin.openPayment();
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
            //sunnetPlugin.openPayment();\
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
