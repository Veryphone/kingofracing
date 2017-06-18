using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour
{
		public GUITexture[] textures;
		public Texture starImage;
		public GUITexture btnQuickRace, btnBackScene, btnShop, btnArchieve, btnSetting, btnPayment, btnFreeCoin;
		public GUIText modeLabel;
		public Font customFont, numberFont;
		private float startPosition;
		private int index = 1;
		private bool slideToLeft = false;
		private bool slideToRight = false;
		public Texture fillBackground;
		public AudioSource audioSource;
		//public SunnetPlugin sunnetPlugin;
		bool isConfirmQuit, isShowPayment, isShowFreeCoin, isLockedLevel;
		Rect confirmRect;
		float lastConfirm;
		public string message;

		void Start ()
		{
       
        //GoogleMobileAdControll.AdmobControll.HideBanner();
				ProfileManager.init ();
//				sunnetPlugin.loadLargeAd (SunnetPlugin.PAYMENT_ALIGN_CENTER_HORIZONTAL, SunnetPlugin.PAYMENT_ALIGN_CENTER_VERTICAL);

				audioSource.volume = ProfileManager.setttings.MusicVolume / 100f;
				audioSource.Play ();

				confirmRect = new Rect (200, 100, 400, 200);

				index = ProfileManager.userProfile.SelectedMap;

				for (int i=0; i<3; i++) {
						textures [i].gameObject.SetActive (false);
				}

				if (index == 0) {
						index = 1;
				}

				Rect tempRect;
				if (index == textures.Length - 1) {
						tempRect = textures [textures.Length - 1].pixelInset;
						tempRect.width = 355;
						tempRect.height = 212;
						tempRect.x = 222.5f;
						tempRect.y = 110;
			
						textures [textures.Length - 1].pixelInset = tempRect;
						textures [textures.Length - 1].gameObject.SetActive (true);
			
						tempRect = textures [textures.Length - 2].pixelInset;
						tempRect.width = 355;
						tempRect.height = 212;
						tempRect.x = -40;
						tempRect.y = 110;
			
						textures [textures.Length - 2].pixelInset = tempRect;
						textures [textures.Length - 2].gameObject.SetActive (true);
				} else {
						tempRect = textures [index].pixelInset;
						tempRect.width = 355;
						tempRect.height = 212;
						tempRect.x = 222.5f;
						tempRect.y = 110;
			
						textures [index].pixelInset = tempRect;
						textures [index].gameObject.SetActive (true);

						tempRect = textures [index - 1].pixelInset;
						tempRect.width = 355;
						tempRect.height = 212;
						tempRect.x = -40;
						tempRect.y = 110;
			
						textures [index - 1].pixelInset = tempRect;
						textures [index - 1].gameObject.SetActive (true);

						tempRect = textures [index + 1].pixelInset;
						tempRect.width = 355;
						tempRect.height = 212;
						tempRect.x = 590;
						tempRect.y = 110;
			
						textures [index + 1].pixelInset = tempRect;
						textures [index + 1].gameObject.SetActive (true);
				}

				ChangeModeLabel ();

				GoogleAnalytics.LogScreen ("Main Menu");
        //ProfileManager.userProfile.Cash += 9999999;
    }

		void OnDestroy ()
		{
//				sunnetPlugin.hideAd ();
		}

		void OnDisable ()
		{
//				sunnetPlugin.hideAd ();
		}

		void Update ()
		{

        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerPrefs.DeleteAll();
            print("show chartboost video");
            //ChartboostController.chartBoost.PlayVideo();
        }
				ScaleAndBlurIMG ();

				if (Input.GetKeyUp (KeyCode.Escape)) {
						this.isConfirmQuit = !this.isConfirmQuit;
				}

				if (isConfirmQuit == true || isShowFreeCoin == true || isShowPayment == true) {
						return;
				} else if (Time.timeSinceLevelLoad - lastConfirm < 0.5f) {
						return;
				}

				#if UNITY_EDITOR
		// PC
		if (Input.GetKeyUp (KeyCode.LeftArrow) && slideToLeft == false && slideToRight == false)
			slideToLeft = true;
				
		if (Input.GetKeyUp (KeyCode.RightArrow) && slideToLeft == false && slideToRight == false)
			slideToRight = true;
		
		if (Input.GetKeyUp (KeyCode.Space)) {
			if ((index >= 4 && index <= 9) || (index >= 22 && index <= 27)) {
				if (ProfileManager.userProfile.Star >= index * 3) {
					this.chooseMap ();
				} else {
					message = "You have to get 3 stars at all lower level to play this!";
					this.isLockedLevel = true;
				}
			} else {
				if (index == 0 || ProfileManager.userProfile.MapProfile [index - 1].LastReward > 0) {
					this.chooseMap ();
				} else {
					message = "You have to finish level " + index + " to play this!";
					this.isLockedLevel = true;
				}
			}
		}

		if (Input.GetKeyUp (KeyCode.Q)) {
			AutoFade.LoadLevel ("QuickRace", 0.5f, 0.5f, Color.black);
		}

		if (Input.GetKeyUp (KeyCode.S)) {
			SceneLoader.scene = "Showroom";
			Application.LoadLevel ("Loading");
		}

		if (Input.GetKeyUp (KeyCode.A)) {
			AutoFade.LoadLevel ("Achievement", 0.5f, 0.5f, Color.black);
		}
				#endif

				//Mobile
				if (Input.touchCount > 0 && slideToLeft == false && slideToRight == false) {
						Vector2 deltaPos = Input.GetTouch (0).deltaPosition;
						Vector2 deltaSlide = Input.GetTouch (0).position;

						// Slide Map
						if (deltaPos.x > 4 && deltaSlide.y <= 325 && deltaSlide.y >= 100)
								slideToRight = true;
					
						if (deltaPos.x < -4 && deltaSlide.y <= 325 && deltaSlide.y >= 100)
								slideToLeft = true;

						// Touch
						if (Input.GetTouch (0).phase == TouchPhase.Ended) {
								if (deltaSlide.y <= 325 && deltaSlide.y >= 100 && deltaSlide.x >= 222.5f && deltaSlide.x <= 577.5f) {
										if ((index >= 4 && index <= 9) || (index >= 22 && index <= 27)) {
												if (ProfileManager.userProfile.Star >= index * 3) {
														this.chooseMap ();
												} else {
														message = "You have to get 3 stars at all lower level to play this!";
														this.isLockedLevel = true;
												}
										} else {
												if (index == 0 || ProfileManager.userProfile.MapProfile [index - 1].LastReward > 0) {
														this.chooseMap ();
												} else {
														message = "You have to finish level " + index + " to play this!";
														this.isLockedLevel = true;
												}
										}
								}
						}
				}
				
				if (slideToLeft)
						ChangeIMG (true);
				
				if (slideToRight)
						ChangeIMG (false);

				if (slideToLeft == false && slideToRight == false)
						ChangeModeLabel ();

				if (Input.touchCount == 1 && Input.GetTouch (0).phase == TouchPhase.Ended) {
						if (btnQuickRace.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
								btnQuickRace.color = new Color32 (128, 128, 128, 128);
								AutoFade.LoadLevel ("QuickRace", 0.5f, 0.5f, Color.black);
//							sunnetPlugin.hideAd ();
						}

						if (btnBackScene.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
								btnBackScene.color = new Color32 (128, 128, 128, 128);
								this.isConfirmQuit = !this.isConfirmQuit;
						}

						if (btnShop.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
								btnShop.color = new Color32 (128, 128, 128, 128);
								SceneLoader.scene = "Showroom";
								Application.LoadLevel ("Loading");
//								sunnetPlugin.hideAd ();
						}

						if (btnArchieve.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
								btnArchieve.color = new Color32 (128, 128, 128, 128);
								AutoFade.LoadLevel ("Achievement", 0.5f, 0.5f, Color.black);
//								sunnetPlugin.hideAd ();
						}

						if (btnSetting.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
								btnSetting.color = new Color32 (128, 128, 128, 128);
								AutoFade.LoadLevel ("Setting", 0.5f, 0.5f, Color.black);
//								sunnetPlugin.hideAd ();
						}

						if (btnPayment.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
								btnPayment.color = new Color32 (128, 128, 128, 128);
								this.openPayment ();
						}
			
						if (btnFreeCoin.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
								btnFreeCoin.color = new Color32 (128, 128, 128, 128);
                //ChartboostController.chartBoost.PlayVideo();
                //								this.openFreecoin ();
            }
				} else if (Input.touchCount == 1 && Input.GetTouch (0).phase == TouchPhase.Began) {
						if (btnQuickRace.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
								btnQuickRace.color = new Color32 (128, 128, 128, 45);
						}
			
						if (btnBackScene.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
								btnBackScene.color = new Color32 (128, 128, 128, 45);
						}
			
						if (btnShop.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
								btnShop.color = new Color32 (128, 128, 128, 45);
						}
			
						if (btnArchieve.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
								btnArchieve.color = new Color32 (128, 128, 128, 45);
						}
			
						if (btnSetting.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
								btnSetting.color = new Color32 (128, 128, 128, 45);
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
			
						if (!btnShop.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
								btnShop.color = new Color32 (128, 128, 128, 128);
						}
			
						if (!btnArchieve.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
								btnArchieve.color = new Color32 (128, 128, 128, 128);
						}
			
						if (!btnSetting.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
								btnSetting.color = new Color32 (128, 128, 128, 128);
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
				largeSize.fontSize = 28;

				GUIStyle number = new GUIStyle ("label");
				number.font = numberFont;
				number.fontSize = 20;

				GUI.Label (new Rect (28, 32, 100, 50), Language.BACK, normalSize);
				GUI.Label (new Rect (123, 32, 200, 50), Language.QUICKRACE, normalSize);
//				GUI.Label (new Rect (685, 28, 200, 50), Language.FREECOIN, normalSize);

				GUI.Label (new Rect (95, 422, 200, 50), Language.SETTING, largeSize);
				GUI.Label (new Rect (315, 422, 300, 50), Language.ACHIEVEMENT, largeSize);
				GUI.Label (new Rect (650, 422, 100, 50), Language.SHOP, largeSize);

				GUI.Label (new Rect (340, 22, 100, 50), ProfileManager.userProfile.Star + string.Empty, number);
				GUI.Label (new Rect (498, 22, 200, 50), ProfileManager.userProfile.Cash + string.Empty, number);

				if (isConfirmQuit) {
						confirmRect = GUI.Window (1, confirmRect, showConfirmQuitWindow, string.Empty);
				} else if (isShowFreeCoin) {
						confirmRect = GUI.Window (1, confirmRect, showFreeCoinWindow, string.Empty);
				} else if (isShowPayment) {
						confirmRect = GUI.Window (1, confirmRect, showPaymentWindow, string.Empty);
				} else if (isLockedLevel) {
						confirmRect = GUI.Window (1, confirmRect, showLockedLevelWindow, string.Empty);
				}

				int numberStar = ProfileManager.userProfile.MapProfile [index].LastReward;
				for (int i=0; i<numberStar; i++) {
						GUI.DrawTexture (new Rect (textures [index].GetScreenRect ().x + i * 50 + 35,
			                           textures [index].GetScreenRect ().y + 50, 43, 43), starImage);
				}

				GUI.Label (new Rect (textures [index].GetScreenRect ().x + 280, textures [index].GetScreenRect ().y + 215, 100, 50),
		           string.Empty + (index + 1), largeSize);
		}
	
		void ChangeIMG (bool slideLeft)
		{
				modeLabel.color = Color.clear;

				Rect tempRect;
				if (slideLeft) {
						if (index == textures.Length - 1) {
								slideToLeft = false;
								return;
						}

						if (index > 0) {
								tempRect = textures [index - 1].pixelInset;
								tempRect.x = Mathf.Lerp (tempRect.x, -249f, 20 * Time.deltaTime);
								textures [index - 1].pixelInset = tempRect;
								textures [index - 1].gameObject.SetActive (false);
						}
			
						tempRect = textures [index].pixelInset;
						tempRect.x = Mathf.Lerp (tempRect.x, -40, 20 * Time.deltaTime);
						textures [index].pixelInset = tempRect;
						textures [index].gameObject.SetActive (true);
			
						tempRect = textures [index + 1].pixelInset;
						tempRect.x = Mathf.Lerp (tempRect.x, 222.5f, 20 * Time.deltaTime);
						textures [index + 1].pixelInset = tempRect;
						textures [index + 1].gameObject.SetActive (true);
			
						if (index + 2 < textures.Length) {
								tempRect = textures [index + 2].pixelInset;
								tempRect.x = Mathf.Lerp (tempRect.x, 590, 20 * Time.deltaTime);
								textures [index + 2].pixelInset = tempRect;
								textures [index + 2].gameObject.SetActive (true);

								if (Mathf.Floor (tempRect.x) == 590) {
										index++;
										slideToLeft = false;
								}
						}

						if (Mathf.Floor (tempRect.x) == 222 && index + 2 == textures.Length) {
								index++;
								slideToLeft = false;
						}
				} else {
						if (index <= 0) {
								slideToRight = false;
								return;
						} else if (index >= 2) {
								tempRect = textures [index - 2].pixelInset;
								tempRect.x = Mathf.Lerp (tempRect.x, -40, 20 * Time.deltaTime);
								textures [index - 2].pixelInset = tempRect;
								textures [index - 2].gameObject.SetActive (true);
						}

						tempRect = textures [index - 1].pixelInset;
						tempRect.x = Mathf.Lerp (tempRect.x, 222.5f, 20 * Time.deltaTime);
						textures [index - 1].pixelInset = tempRect;
						textures [index - 1].gameObject.SetActive (true);
			
						tempRect = textures [index].pixelInset;
						tempRect.x = Mathf.Lerp (tempRect.x, 590, 20 * Time.deltaTime);
						textures [index].pixelInset = tempRect;
						textures [index].gameObject.SetActive (true);

						if (index <= textures.Length - 2) {
								tempRect = textures [index + 1].pixelInset;
								tempRect.x = Mathf.Lerp (tempRect.x, 801, 20 * Time.deltaTime);
								textures [index + 1].pixelInset = tempRect;
								textures [index + 1].gameObject.SetActive (false);

								if (Mathf.Floor (tempRect.x) >= 800) {
										index--;
										slideToRight = false;
								}
						}

						if (Mathf.Floor (tempRect.x) >= 589 && index == textures.Length - 1) {
								index--;
								slideToRight = false;
						}
				}
		}

		void ScaleAndBlurIMG ()
		{
				Rect tempRect;

				if (index == 0) {
						textures [index].color = new Color32 (128, 128, 128, 128);
						textures [index + 1].color = Color32.Lerp (textures [index + 1].color, new Color32 (20, 20, 20, 128), 20 * Time.deltaTime);
						textures [index + 2].color = Color32.Lerp (textures [index + 2].color, new Color32 (20, 20, 20, 128), 20 * Time.deltaTime);

						tempRect = textures [index].pixelInset;
						tempRect.width = Mathf.Lerp (tempRect.width, 355, 20 * Time.deltaTime);
						tempRect.height = Mathf.Lerp (tempRect.height, 212, 20 * Time.deltaTime);
						tempRect.y = Mathf.Lerp (tempRect.y, 110, 20 * Time.deltaTime);
						textures [index].pixelInset = tempRect;

						tempRect = textures [index + 1].pixelInset;
						tempRect.width = Mathf.Lerp (tempRect.width, 248.5f, 20 * Time.deltaTime);
						tempRect.height = Mathf.Lerp (tempRect.height, 148.4f, 20 * Time.deltaTime);
						tempRect.y = Mathf.Lerp (tempRect.y, 141.8f, 20 * Time.deltaTime);
						textures [index + 1].pixelInset = tempRect;

						tempRect = textures [index + 2].pixelInset;
						tempRect.width = Mathf.Lerp (tempRect.width, 248.5f, 20 * Time.deltaTime);
						tempRect.height = Mathf.Lerp (tempRect.height, 148.4f, 20 * Time.deltaTime);
						tempRect.y = Mathf.Lerp (tempRect.y, 141.8f, 20 * Time.deltaTime);
						textures [index + 2].pixelInset = tempRect;
				} else if (index == textures.Length - 1) {
						textures [index].color = new Color32 (128, 128, 128, 128);
						textures [index - 1].color = Color32.Lerp (textures [index - 1].color, new Color32 (20, 20, 20, 128), 20 * Time.deltaTime);
						textures [index - 2].color = Color32.Lerp (textures [index - 2].color, new Color32 (20, 20, 20, 128), 20 * Time.deltaTime);

						tempRect = textures [index].pixelInset;
						tempRect.width = Mathf.Lerp (tempRect.width, 355, 20 * Time.deltaTime);
						tempRect.height = Mathf.Lerp (tempRect.height, 212, 20 * Time.deltaTime);
						tempRect.y = Mathf.Lerp (tempRect.y, 110, 20 * Time.deltaTime);
						textures [index].pixelInset = tempRect;
			
						tempRect = textures [index - 1].pixelInset;
						tempRect.width = Mathf.Lerp (tempRect.width, 248.5f, 20 * Time.deltaTime);
						tempRect.height = Mathf.Lerp (tempRect.height, 148.4f, 20 * Time.deltaTime);
						tempRect.y = Mathf.Lerp (tempRect.y, 141.8f, 20 * Time.deltaTime);
						textures [index - 1].pixelInset = tempRect;
			
						tempRect = textures [index - 2].pixelInset;
						tempRect.width = Mathf.Lerp (tempRect.width, 248.5f, 20 * Time.deltaTime);
						tempRect.height = Mathf.Lerp (tempRect.height, 148.4f, 20 * Time.deltaTime);
						tempRect.y = Mathf.Lerp (tempRect.y, 141.8f, 20 * Time.deltaTime);
						textures [index - 2].pixelInset = tempRect;
				} else {
						textures [index - 1].color = Color32.Lerp (textures [index - 1].color, new Color32 (20, 20, 20, 128), 20 * Time.deltaTime);
						textures [index].color = new Color32 (128, 128, 128, 128);
						textures [index + 1].color = Color32.Lerp (textures [index + 1].color, new Color32 (20, 20, 20, 128), 20 * Time.deltaTime);

						tempRect = textures [index].pixelInset;
						tempRect.width = Mathf.Lerp (tempRect.width, 355, 20 * Time.deltaTime);
						tempRect.height = Mathf.Lerp (tempRect.height, 212, 20 * Time.deltaTime);
						tempRect.y = Mathf.Lerp (tempRect.y, 110, 20 * Time.deltaTime);
						textures [index].pixelInset = tempRect;
			
						tempRect = textures [index - 1].pixelInset;
						tempRect.width = Mathf.Lerp (tempRect.width, 248.5f, 20 * Time.deltaTime);
						tempRect.height = Mathf.Lerp (tempRect.height, 148.4f, 20 * Time.deltaTime);
						tempRect.y = Mathf.Lerp (tempRect.y, 141.8f, 20 * Time.deltaTime);
						textures [index - 1].pixelInset = tempRect;
			
						tempRect = textures [index + 1].pixelInset;
						tempRect.width = Mathf.Lerp (tempRect.width, 248.5f, 20 * Time.deltaTime);
						tempRect.height = Mathf.Lerp (tempRect.height, 148.4f, 20 * Time.deltaTime);
						tempRect.y = Mathf.Lerp (tempRect.y, 141.8f, 20 * Time.deltaTime);
						textures [index + 1].pixelInset = tempRect;
				}
		}

		void ChangeModeLabel ()
		{
				modeLabel.color = Color.Lerp (modeLabel.color, Color.white, 2 * Time.deltaTime);
				switch (index) {
				case 0:
						modeLabel.text = "NORMAL";
						break;
				case 1:
						modeLabel.text = "NORMAL";
						break;
				case 2:
						modeLabel.text = "CHECK POINT";
						break;
				case 3:
						modeLabel.text = "TIME TRIAL";
						break;
				case 4:
						modeLabel.text = "NORMAL";
						break;
				case 5:
						modeLabel.text = "TIME TRIAL";
						break;
				case 6:
						modeLabel.text = "NORMAL";
						break;
				case 7:
						modeLabel.text = "CHECK POINT";
						break;
				case 8:
						modeLabel.text = "NORMAL";
						break;
				case 9:
						modeLabel.text = "TIME TRIAL";
						break;
				case 10:
						modeLabel.text = "TIME TRIAL";
						break;
				case 11:
						modeLabel.text = "CHECK POINT";
						break;
				case 12:
						modeLabel.text = "CHECK POINT";
						break;
				case 13:
						modeLabel.text = "TIME TRIAL";
						break;
				case 14:
						modeLabel.text = "CHECK POINT";
						break;
				case 15:
						modeLabel.text = "NORMAL";
						break;
				case 16:
						modeLabel.text = "CHECK POINT";
						break;
				case 17:
						modeLabel.text = "TIME TRIAL";
						break;
				case 18:
						modeLabel.text = "NORMAL";
						break;
				case 19:
						modeLabel.text = "NORMAL";
						break;
				case 20:
						modeLabel.text = "CHECK POINT";
						break;
				case 21:
						modeLabel.text = "TIME TRIAL";
						break;
				case 22:
						modeLabel.text = "NORMAL";
						break;
				case 23:
						modeLabel.text = "TIME TRIAL";
						break;
				case 24:
						modeLabel.text = "NORMAL";
						break;
				case 25:
						modeLabel.text = "CHECK POINT";
						break;
				case 26:
						modeLabel.text = "NORMAL";
						break;
				case 27:
						modeLabel.text = "TIME TRIAL";
						break;
				case 28:
						modeLabel.text = "TIME TRIAL";
						break;
				case 29:
						modeLabel.text = "CHECK POINT";
						break;
				case 30:
						modeLabel.text = "CHECK POINT";
						break;
				case 31:
						modeLabel.text = "TIME TRIAL";
						break;
				case 32:
						modeLabel.text = "CHECK POINT";
						break;
				case 33:
						modeLabel.text = "NORMAL";
						break;
				case 34:
						modeLabel.text = "CHECK POINT";
						break;
				case 35:
						modeLabel.text = "TIME TRIAL";
						break;
				default:
						modeLabel.text = "NORMAL";
						break;
				}
		}

		void chooseMap ()
		{
				switch (index) {
				case 0:
						GameData.selectedMap = GameData.MAP_NAME.HANOI;
						GameData.selectedMode = GameData.GAME_MODE.NORMAL;
						GameData.numberEnemies = 4;
						GameData.numberRaces = 2;
						break;
			
				case 1:
						GameData.selectedMap = GameData.MAP_NAME.CHINA;
						GameData.selectedMode = GameData.GAME_MODE.NORMAL;
						GameData.numberEnemies = 5;
						GameData.numberRaces = 3;
						break;
			
				case 2:
						GameData.selectedMap = GameData.MAP_NAME.TOKYO;
						GameData.selectedMode = GameData.GAME_MODE.CHECK_POINT;
						GameData.numberEnemies = 3;
						GameData.duration = 90;
						GameData.numberCheckpoints = 3;
						break;

				case 3:
						GameData.selectedMap = GameData.MAP_NAME.PARIS;
						GameData.selectedMode = GameData.GAME_MODE.TIME_TRIAL;
						GameData.numberEnemies = 4;
						GameData.duration = 120;
						break;
			
				case 4:
						GameData.selectedMap = GameData.MAP_NAME.NEWYORK;
						GameData.selectedMode = GameData.GAME_MODE.NORMAL;
						GameData.numberEnemies = 5;
						GameData.numberRaces = 2;
						break;
			
				case 5:
						GameData.selectedMap = GameData.MAP_NAME.NEVADA;
						GameData.selectedMode = GameData.GAME_MODE.TIME_TRIAL;
						GameData.numberEnemies = 3;
						GameData.duration = 90;
						break;

				case 6:
						GameData.selectedMap = GameData.MAP_NAME.HANOI;
						GameData.selectedMode = GameData.GAME_MODE.NORMAL;
						GameData.numberEnemies = 3;
						GameData.numberRaces = 1;
						break;
			
				case 7:
						GameData.selectedMap = GameData.MAP_NAME.CHINA;
						GameData.selectedMode = GameData.GAME_MODE.CHECK_POINT;
						GameData.numberEnemies = 4;
						GameData.duration = 90;
						GameData.numberCheckpoints = 4;
						break;
			
				case 8:
						GameData.selectedMap = GameData.MAP_NAME.NEWYORK;
						GameData.selectedMode = GameData.GAME_MODE.NORMAL;
						GameData.numberEnemies = 1;
						GameData.numberRaces = 2;
						break;
			
				case 9:
						GameData.selectedMap = GameData.MAP_NAME.NEVADA;
						GameData.selectedMode = GameData.GAME_MODE.TIME_TRIAL;
						GameData.numberEnemies = 1;
						GameData.duration = 90;	
						break;
			
				case 10:
						GameData.selectedMap = GameData.MAP_NAME.NEWYORK;
						GameData.selectedMode = GameData.GAME_MODE.TIME_TRIAL;
						GameData.numberEnemies = 4;
						GameData.duration = 120;	
						break;
			
				case 11:
						GameData.selectedMap = GameData.MAP_NAME.TOKYO;
						GameData.selectedMode = GameData.GAME_MODE.CHECK_POINT;
						GameData.numberEnemies = 2;
						GameData.duration = 90;
						GameData.numberCheckpoints = 5;
						break;

				case 12:
						GameData.selectedMap = GameData.MAP_NAME.HANOI;
						GameData.selectedMode = GameData.GAME_MODE.CHECK_POINT;
						GameData.numberEnemies = 3;
						GameData.duration = 90;
						GameData.numberCheckpoints = 5;
						break;
			
				case 13:
						GameData.selectedMap = GameData.MAP_NAME.CHINA;
						GameData.selectedMode = GameData.GAME_MODE.TIME_TRIAL;
						GameData.numberEnemies = 3;
						GameData.duration = 90;
						break;
			
				case 14:
						GameData.selectedMap = GameData.MAP_NAME.PARIS;
						GameData.selectedMode = GameData.GAME_MODE.CHECK_POINT;
						GameData.numberEnemies = 5;
						GameData.duration = 90;
						GameData.numberCheckpoints = 6;
						break;
			
				case 15:
						GameData.selectedMap = GameData.MAP_NAME.NEWYORK;
						GameData.selectedMode = GameData.GAME_MODE.NORMAL;
						GameData.numberEnemies = 5;
						GameData.numberRaces = 3;	
						break;
			
				case 16:
						GameData.selectedMap = GameData.MAP_NAME.NEVADA;
						GameData.selectedMode = GameData.GAME_MODE.CHECK_POINT;
						GameData.numberEnemies = 2;
						GameData.duration = 90;
						GameData.numberCheckpoints = 6;
						break;
			
				case 17:
						GameData.selectedMap = GameData.MAP_NAME.PARIS;
						GameData.selectedMode = GameData.GAME_MODE.TIME_TRIAL;
						GameData.numberEnemies = 4;
						GameData.duration = 90;
						break;

				case 18:
						GameData.selectedMap = GameData.MAP_NAME.HANOI;
						GameData.selectedMode = GameData.GAME_MODE.NORMAL;
						GameData.numberEnemies = 4;
						GameData.numberRaces = 3;
						break;
			
				case 19:
						GameData.selectedMap = GameData.MAP_NAME.CHINA;
						GameData.selectedMode = GameData.GAME_MODE.NORMAL;
						GameData.numberEnemies = 5;
						GameData.numberRaces = 3;
						break;
			
				case 20:
						GameData.selectedMap = GameData.MAP_NAME.TOKYO;
						GameData.selectedMode = GameData.GAME_MODE.CHECK_POINT;
						GameData.numberEnemies = 3;
						GameData.duration = 120;
						GameData.numberCheckpoints = 6;
						break;
			
				case 21:
						GameData.selectedMap = GameData.MAP_NAME.PARIS;
						GameData.selectedMode = GameData.GAME_MODE.TIME_TRIAL;
						GameData.numberEnemies = 4;
						GameData.duration = 120;
						break;
			
				case 22:
						GameData.selectedMap = GameData.MAP_NAME.NEWYORK;
						GameData.selectedMode = GameData.GAME_MODE.NORMAL;
						GameData.numberEnemies = 5;
						GameData.numberRaces = 2;
						break;
			
				case 23:
						GameData.selectedMap = GameData.MAP_NAME.NEVADA;
						GameData.selectedMode = GameData.GAME_MODE.TIME_TRIAL;
						GameData.numberEnemies = 3;
						GameData.duration = 90;
						break;
			
				case 24:
						GameData.selectedMap = GameData.MAP_NAME.HANOI;
						GameData.selectedMode = GameData.GAME_MODE.NORMAL;
						GameData.numberEnemies = 3;
						GameData.numberRaces = 1;
						break;
			
				case 25:
						GameData.selectedMap = GameData.MAP_NAME.CHINA;
						GameData.selectedMode = GameData.GAME_MODE.CHECK_POINT;
						GameData.numberEnemies = 4;
						GameData.duration = 120;
						GameData.numberCheckpoints = 6;
						break;
			
				case 26:
						GameData.selectedMap = GameData.MAP_NAME.NEWYORK;
						GameData.selectedMode = GameData.GAME_MODE.NORMAL;
						GameData.numberEnemies = 1;
						GameData.numberRaces = 2;
						break;
			
				case 27:
						GameData.selectedMap = GameData.MAP_NAME.NEVADA;
						GameData.selectedMode = GameData.GAME_MODE.TIME_TRIAL;
						GameData.numberEnemies = 1;
						GameData.duration = 90;	
						break;
			
				case 28:
						GameData.selectedMap = GameData.MAP_NAME.NEWYORK;
						GameData.selectedMode = GameData.GAME_MODE.TIME_TRIAL;
						GameData.numberEnemies = 4;
						GameData.duration = 120;	
						break;
			
				case 29:
						GameData.selectedMap = GameData.MAP_NAME.TOKYO;
						GameData.selectedMode = GameData.GAME_MODE.CHECK_POINT;
						GameData.numberEnemies = 2;
						GameData.duration = 120;	
						GameData.numberCheckpoints = 7;
						break;
			
				case 30:
						GameData.selectedMap = GameData.MAP_NAME.HANOI;
						GameData.selectedMode = GameData.GAME_MODE.CHECK_POINT;
						GameData.numberEnemies = 3;
						GameData.duration = 120;	
						GameData.numberCheckpoints = 7;
						break;
			
				case 31:
						GameData.selectedMap = GameData.MAP_NAME.CHINA;
						GameData.selectedMode = GameData.GAME_MODE.TIME_TRIAL;
						GameData.numberEnemies = 3;
						GameData.duration = 90;
						break;
			
				case 32:
						GameData.selectedMap = GameData.MAP_NAME.PARIS;
						GameData.selectedMode = GameData.GAME_MODE.CHECK_POINT;
						GameData.numberEnemies = 5;
						GameData.duration = 150;				
						GameData.numberCheckpoints = 8;
						break;
			
				case 33:
						GameData.selectedMap = GameData.MAP_NAME.NEWYORK;
						GameData.selectedMode = GameData.GAME_MODE.NORMAL;
						GameData.numberEnemies = 5;
						GameData.numberRaces = 3;	
						break;
			
				case 34:
						GameData.selectedMap = GameData.MAP_NAME.NEVADA;
						GameData.selectedMode = GameData.GAME_MODE.CHECK_POINT;
						GameData.numberEnemies = 2;
						GameData.duration = 150;			
						GameData.numberCheckpoints = 9;
						break;
			
				case 35:
						GameData.selectedMap = GameData.MAP_NAME.PARIS;
						GameData.selectedMode = GameData.GAME_MODE.TIME_TRIAL;
						GameData.numberEnemies = 4;
						GameData.duration = 90;
						break;

				//---------------------------------------------------
			
				default:
						GameData.selectedMap = GameData.MAP_NAME.HANOI;
						GameData.selectedMode = GameData.GAME_MODE.NORMAL;
						GameData.numberEnemies = 5;
						GameData.numberRaces = 2;
						break;
				}		
				GameData.level = index;
				ProfileManager.userProfile.SelectedMap = GameData.level;
				PlayerPrefs.Save ();

				AutoFade.LoadLevel ("Buy", 0.5f, 0.5f, Color.black);		
//				sunnetPlugin.hideAd ();
		}

		public void openPayment ()
		{
				this.isShowPayment = true;
		}
	
		public void openFreecoin ()
		{
				this.isShowFreeCoin = true;
		}

		void showConfirmQuitWindow (int windowID)
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
				GUI.Label (new Rect (0, -20, 400, 100), "Confirm", centeredTextStyle);
		
				centeredTextStyle.fontSize = 15;
				GUI.Label (new Rect (0, 40, 400, 50), "Do you want to exit game?", centeredTextStyle);
		
				if (GUI.Button (new Rect (60, 120, 100, 40), "Yes", buttonStyle)) {
						Application.Quit ();
						lastConfirm = Time.timeSinceLevelLoad;
				}

				if (GUI.Button (new Rect (250, 120, 100, 40), "No", buttonStyle)) {
						this.isConfirmQuit = false;
						lastConfirm = Time.timeSinceLevelLoad;
				}
		}

		void showLockedLevelWindow (int windowID)
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
				GUI.Label (new Rect (0, -20, 400, 100), "Warning!", centeredTextStyle);
		
				centeredTextStyle.fontSize = 15;
				GUI.Label (new Rect (0, 40, 400, 50), message, centeredTextStyle);
		
				if (GUI.Button (new Rect (150, 120, 100, 40), "OK", buttonStyle)) {
						this.isLockedLevel = false;
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

						this.isShowPayment = false;
						lastConfirm = Time.timeSinceLevelLoad;
				}
		
				if (GUI.Button (new Rect (250, 120, 100, 40), "No", buttonStyle)) {
						this.isShowPayment = false;
						lastConfirm = Time.timeSinceLevelLoad;
				}
		}
}