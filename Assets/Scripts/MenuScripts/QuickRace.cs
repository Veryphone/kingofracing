using UnityEngine;
using System.Collections;

public class QuickRace : MonoBehaviour
{
		public GUITexture[] textures;
		public GUITexture btnCareerMode, btnBackScene, btnArchieve, btnSetting, btnShop, btnNormal,
				btnCheckPoint, btnTimeTrial, btnPayment, btnFreeCoin;
		public Font customFont, numberFont;
		private float startPosition;
		private int index = 0;
		private bool slideToLeft = false;
		private bool slideToRight = false;
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
				GameData.selectedMode = GameData.GAME_MODE.NORMAL;

		GoogleAnalytics.LogScreen ("Quick Race");
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

				ScaleAndBlurIMG ();
				
				#if UNITY_EDITOR
		if (Input.GetKeyUp (KeyCode.LeftArrow) && slideToLeft == false && slideToRight == false)
			slideToLeft = true;
				
		if (Input.GetKeyUp (KeyCode.RightArrow) && slideToLeft == false && slideToRight == false)
			slideToRight = true;

		if (Input.GetKeyUp (KeyCode.Space)) {
			this.chooseMap ();
		}

		if(Input.GetKeyUp(KeyCode.N)){
			btnNormal.color = new Color32 (128, 128, 128, 128);
			btnCheckPoint.color = new Color32 (128, 128, 128, 45);
			btnTimeTrial.color = new Color32 (128, 128, 128, 45);
			GameData.selectedMode = GameData.GAME_MODE.NORMAL;
		}

		if(Input.GetKeyUp(KeyCode.C)){
			btnNormal.color = new Color32 (128, 128, 128, 45);
			btnCheckPoint.color = new Color32 (128, 128, 128, 128);
			btnTimeTrial.color = new Color32 (128, 128, 128, 45);
			GameData.selectedMode = GameData.GAME_MODE.CHECK_POINT;
		}

		if(Input.GetKeyUp(KeyCode.T)){
			btnNormal.color = new Color32 (128, 128, 128, 45);
			btnCheckPoint.color = new Color32 (128, 128, 128, 45);
			btnTimeTrial.color = new Color32 (128, 128, 128, 128);
			GameData.selectedMode = GameData.GAME_MODE.TIME_TRIAL;
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
										this.chooseMap ();
								}
						}
				}
				
				if (slideToLeft)
						ChangeIMG (true);
				
				if (slideToRight)
						ChangeIMG (false);

				if (Input.touchCount == 1 && Input.GetTouch (0).phase == TouchPhase.Ended) {
						if (btnCareerMode.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
								btnCareerMode.color = new Color32 (128, 128, 128, 128);
								AutoFade.LoadLevel ("MainMenu", 0.5f, 0.5f, Color.black);
						}
			
						if (btnBackScene.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
								btnBackScene.color = new Color32 (128, 128, 128, 128);
								AutoFade.LoadLevel ("MainMenu", 0.5f, 0.5f, Color.black);
						}
			
						if (btnShop.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
								btnShop.color = new Color32 (128, 128, 128, 128);
								SceneLoader.scene = "Showroom";
								Application.LoadLevel ("Loading");
						}
			
						if (btnArchieve.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
								btnArchieve.color = new Color32 (128, 128, 128, 128);
								AutoFade.LoadLevel ("Achievement", 0.5f, 0.5f, Color.black);
						}
			
						if (btnSetting.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
								btnSetting.color = new Color32 (128, 128, 128, 128);
								AutoFade.LoadLevel ("Setting", 0.5f, 0.5f, Color.black);
						}

						if (btnPayment.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
								btnPayment.color = new Color32 (128, 128, 128, 128);
								this.openPayment ();
						}
			
						if (btnFreeCoin.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
								btnFreeCoin.color = new Color32 (128, 128, 128, 128);
                //								this.openFreecoin ();
                //sunnetPlugin.openDownloadLink ();
                //ChartboostController.chartBoost.PlayVideo();
            }

						if (btnNormal.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
								btnNormal.color = new Color32 (128, 128, 128, 128);
								btnCheckPoint.color = new Color32 (128, 128, 128, 45);
								btnTimeTrial.color = new Color32 (128, 128, 128, 45);
								GameData.selectedMode = GameData.GAME_MODE.NORMAL;
						}

						if (btnCheckPoint.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
								btnNormal.color = new Color32 (128, 128, 128, 45);
								btnCheckPoint.color = new Color32 (128, 128, 128, 128);
								btnTimeTrial.color = new Color32 (128, 128, 128, 45);
								GameData.selectedMode = GameData.GAME_MODE.CHECK_POINT;
						}

						if (btnTimeTrial.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
								btnNormal.color = new Color32 (128, 128, 128, 45);
								btnCheckPoint.color = new Color32 (128, 128, 128, 45);
								btnTimeTrial.color = new Color32 (128, 128, 128, 128);
								GameData.selectedMode = GameData.GAME_MODE.TIME_TRIAL;
						}
				} else if (Input.touchCount == 1 && Input.GetTouch (0).phase == TouchPhase.Began) {
						if (btnCareerMode.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
								btnCareerMode.color = new Color32 (128, 128, 128, 45);
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
						if (!btnCareerMode.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
								btnCareerMode.color = new Color32 (128, 128, 128, 128);
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
				GUI.Label (new Rect (112, 32, 200, 50), Language.CAREERMODE, normalSize);
				GUI.Label (new Rect (685, 28, 200, 50), Language.FREECOIN, normalSize);

				GUI.Label (new Rect (95, 422, 200, 50), Language.SETTING, largeSize);
				GUI.Label (new Rect (315, 422, 300, 50), Language.ACHIEVEMENT, largeSize);
				GUI.Label (new Rect (650, 422, 100, 50), Language.SHOP, largeSize);

				GUI.Label (new Rect (340, 22, 100, 50), ProfileManager.userProfile.Star + string.Empty, number);
				GUI.Label (new Rect (498, 22, 200, 50), ProfileManager.userProfile.Cash + string.Empty, number);

				if (isShowFreeCoin) {
						confirmRect = GUI.Window (1, confirmRect, showFreeCoinWindow, string.Empty);
				} else if (isShowPayment) {
						confirmRect = GUI.Window (1, confirmRect, showPaymentWindow, string.Empty);
				}
		}
	
		void ChangeIMG (bool slideLeft)
		{
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
						textures [index].gameObject.SetActive (false);
			
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
								textures [index - 2].gameObject.SetActive (false);
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

		void chooseMap ()
		{
				switch (index) {
				case 0:
						GameData.selectedMap = GameData.MAP_NAME.HANOI;
						break;
			
				case 1:
						GameData.selectedMap = GameData.MAP_NAME.CHINA;
						break;
			
				case 2:
						GameData.selectedMap = GameData.MAP_NAME.TOKYO;
						break;
			
				case 3:
						GameData.selectedMap = GameData.MAP_NAME.PARIS;
						break;
			
				case 4:
						GameData.selectedMap = GameData.MAP_NAME.NEWYORK;
						break;
			
				case 5:
						GameData.selectedMap = GameData.MAP_NAME.NEVADA;
						break;
			
				default:
						GameData.selectedMap = GameData.MAP_NAME.HANOI;
						break;
				}

				GameData.level = -1;
				GameData.numberRaces = 2;
				GameData.numberEnemies = 5;
				GameData.numberCheckpoints = 4;
				GameData.duration = 120;

				AutoFade.LoadLevel ("Buy", 0.5f, 0.5f, Color.black);
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