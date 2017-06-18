using UnityEngine;
using System.Collections;

public class ShowRoom : MonoBehaviour
{
    public float rotateSpeed = 0.2f;
    public Font customFont, numberFont;
    public Texture stats;
    public GUITexture btnNextCar, btnBackCar, btnBackScene, btnQuickRace, SELECTEDCOLOR,
        btnAddSpeed, btnAddHand, btnAddAcc, btnAddNitro, btnPayment, btnFreeCoin;
    public GameObject[] cars;
    public GUITexture[] colorTab;
    public Material[] colorMaterial;
    public Color[] colors;
    private int carIndex = 0;
    public Texture fillBackground;
    public Texture buyCarFrame;
    public AudioSource audioSource;
   //    public SunnetPlugin sunnetPlugin;
    bool isShowPayment, isShowFreeCoin;
    Rect confirmRect;
    float lastConfirm;

    void Start()
    {
        ProfileManager.init();

        audioSource.volume = ProfileManager.setttings.MusicVolume / 100f;
        audioSource.Play();

        confirmRect = new Rect(200, 100, 400, 200);
        carIndex = ProfileManager.userProfile.SelectedCar;

        cars[carIndex].SetActive(true);
        Rect tmp = SELECTEDCOLOR.pixelInset;
        tmp.x = SetTMP(carIndex);
        print(colorMaterial.Length + "_________carindex:" + carIndex + "_________CarProfile" + colors.Length);
        colorMaterial[carIndex].SetColor("_Color", colors[ProfileManager.userProfile.CarProfile[carIndex].Color]);
        SELECTEDCOLOR.pixelInset = tmp;

        QualitySettings.antiAliasing = 2;

        GoogleAnalytics.LogScreen("Shop");
    }

   

    float SetTMP(int carIndex)
    {
         return( 55 + ProfileManager.userProfile.CarProfile[carIndex].Color * 70);
    }

	void Update ()
	{
		if (Input.GetKeyUp (KeyCode.Escape)) {
			ProfileManager.userProfile.SelectedCar = carIndex;
			PlayerPrefs.Save ();
			AutoFade.LoadLevel ("MainMenu", 0.5f, 0.5f, Color.black);
		}

		if (isShowFreeCoin == true || isShowPayment == true) {
			return;
		} else if (Time.timeSinceLevelLoad - lastConfirm < 0.5f) {
			return;
		}

		transform.Rotate (new Vector3 (0f, rotateSpeed, 0f));

		#if UNITY_EDITOR
		if(Input.GetKeyUp(KeyCode.LeftArrow))
			// btnBackCar
			PrevCar();
		else if(Input.GetKeyUp(KeyCode.RightArrow))
			// btnNextCar
			NextCar();
		#endif

		if (Input.touchCount == 1 && Input.GetTouch (0).phase == TouchPhase.Ended) {
			// btnNextCar
			if (btnNextCar.GetScreenRect ().Contains (Input.GetTouch (0).position))
				NextCar ();

			// btnBackCar
			if (btnBackCar.GetScreenRect ().Contains (Input.GetTouch (0).position))
				PrevCar ();

			// btnBackScene
			if (btnBackScene.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
				btnBackScene.color = new Color32 (128, 128, 128, 128);
				ProfileManager.userProfile.SelectedCar = carIndex;
				PlayerPrefs.Save ();
				AutoFade.LoadLevel ("MainMenu", 0.5f, 0.5f, Color.black);
			}

			// btnQuickRace
			if (btnQuickRace.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
				btnQuickRace.color = new Color32 (128, 128, 128, 128);
				ProfileManager.userProfile.SelectedCar = carIndex;
				PlayerPrefs.Save ();
				AutoFade.LoadLevel ("QuickRace", 0.5f, 0.5f, Color.black);
			}

			if (btnPayment.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
					btnPayment.color = new Color32 (128, 128, 128, 128);
					this.openPayment ();
				}
			
				if (btnFreeCoin.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
					btnFreeCoin.color = new Color32 (128, 128, 128, 128);
                //					this.openFreecoin ();
                //sunnetPlugin.openDownloadLink ();
                //ChartboostController.chartBoost.PlayVideo();
            }
			
			if (ProfileManager.userProfile.CarProfile [carIndex].IsBought == true) {
				// btnChoose color
				for (int i=0; i<colorTab.Length; i++) {
					if (colorTab [i].GetScreenRect ().Contains (Input.GetTouch (0).position)) {
						Rect tmp = SELECTEDCOLOR.pixelInset;
						tmp.x = colorTab [i].pixelInset.x - 5;
						tmp.y = colorTab [i].pixelInset.y - 4;
						SELECTEDCOLOR.pixelInset = tmp;
						colorMaterial [carIndex].SetColor ("_Color", colors [i]);

						ProfileManager.userProfile.CarProfile [carIndex].Color = i;
						PlayerPrefs.Save ();
						break;
					}
				}

				// btnSTat
				if (btnAddAcc.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
					btnAddAcc.color = new Color32 (128, 128, 128, 128);

					if (ProfileManager.userProfile.Cash >= 1000) {
						if (ProfileManager.userProfile.CarProfile [carIndex].Acceleration < 5) {
							ProfileManager.userProfile.CarProfile [carIndex].Acceleration += 1;
							ProfileManager.userProfile.Cash -= 1000;
							PlayerPrefs.Save ();
						}
					} else {
						this.openPayment ();
					}
				}

				if (btnAddHand.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
					btnAddHand.color = new Color32 (128, 128, 128, 128);

					if (ProfileManager.userProfile.Cash >= 1000) {
						if (ProfileManager.userProfile.CarProfile [carIndex].Handling < 5) {
							ProfileManager.userProfile.CarProfile [carIndex].Handling += 1;
							ProfileManager.userProfile.Cash -= 1000;
							PlayerPrefs.Save ();
						}
					} else {
						this.openPayment ();
					}
				}

				if (btnAddNitro.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
					btnAddNitro.color = new Color32 (128, 128, 128, 128);

					if (ProfileManager.userProfile.Cash >= 1000) {
						if (ProfileManager.userProfile.CarProfile [carIndex].Nitro < 5) {
							ProfileManager.userProfile.CarProfile [carIndex].Nitro += 1;
							ProfileManager.userProfile.Cash -= 1000;
							PlayerPrefs.Save ();
						}
					} else {
						this.openPayment ();
					}
				}

				if (btnAddSpeed.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
					btnAddSpeed.color = new Color32 (128, 128, 128, 128);

					if (ProfileManager.userProfile.Cash >= 1000) {
						if (ProfileManager.userProfile.CarProfile [carIndex].Speed < 5) {
							ProfileManager.userProfile.CarProfile [carIndex].Speed += 1;
							ProfileManager.userProfile.Cash -= 1000;
							PlayerPrefs.Save ();
						}
					} else {
						this.openPayment ();
					}
				}				
			} else if (Input.touchCount == 1 && Input.GetTouch (0).phase == TouchPhase.Began) {
				if (btnQuickRace.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
					btnQuickRace.color = new Color32 (128, 128, 128, 45);
				}
			
				if (btnBackScene.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
					btnBackScene.color = new Color32 (128, 128, 128, 45);
				}

				// btnSTat
				if (btnAddAcc.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
					btnAddAcc.color = new Color32 (128, 128, 128, 45);
				}
			
				if (btnAddHand.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
					btnAddHand.color = new Color32 (128, 128, 128, 45);
				}
			
				if (btnAddNitro.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
					btnAddNitro.color = new Color32 (128, 128, 128, 45);
				}
			
				if (btnAddSpeed.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
					btnAddSpeed.color = new Color32 (128, 128, 128, 45);
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

				// btnSTat
				if (!btnAddAcc.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
					btnAddAcc.color = new Color32 (128, 128, 128, 128);
				}
			
				if (!btnAddHand.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
					btnAddHand.color = new Color32 (128, 128, 128, 128);
				}
			
				if (!btnAddNitro.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
					btnAddNitro.color = new Color32 (128, 128, 128, 128);
				}
			
				if (!btnAddSpeed.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
					btnAddSpeed.color = new Color32 (128, 128, 128, 128);
				}

				if (!btnPayment.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
					btnPayment.color = new Color32 (128, 128, 128, 128);
				}
			
				if (!btnFreeCoin.GetScreenRect ().Contains (Input.GetTouch (0).position)) {
					btnFreeCoin.color = new Color32 (128, 128, 128, 128);
				}
			}
		}
	}

	void OnGUI ()
	{
		GUI.matrix = Matrix4x4.TRS (Vector3.zero, Quaternion.identity, new Vector3 (Screen.width / 800f, Screen.height / 480f, 1f));

		GUIStyle normalSize = new GUIStyle ("label");
		normalSize.font = customFont;
		normalSize.fontSize = 17;

		GUIStyle number = new GUIStyle ("label");
		number.font = numberFont;
		number.fontSize = 20;

		GUI.Label (new Rect (28, 32, 100, 50), Language.BACK, normalSize);
		GUI.Label (new Rect (123, 32, 200, 50), Language.QUICKRACE, normalSize);
		GUI.Label (new Rect (685, 28, 200, 50), Language.FREECOIN, normalSize);

		GUI.Label (new Rect (500, 175, 100, 50), "1000$", number);

		GUI.Label (new Rect (340, 22, 100, 50), ProfileManager.userProfile.Star + string.Empty, number);
		GUI.Label (new Rect (498, 22, 200, 50), ProfileManager.userProfile.Cash + string.Empty, number);

		GUI.Label (new Rect (498, 90, 300, 50), GameData.getCarName(carIndex), number);

		if (ProfileManager.userProfile.CarProfile [carIndex].IsBought == true) {
			// max speed
			for (int i=0; i<ProfileManager.userProfile.CarProfile[carIndex].Speed; i++) {
				GUI.DrawTexture (new Rect (585 + 18 * i, 216, stats.width, stats.height), stats);
			}

			// handing
			for (int i=0; i<ProfileManager.userProfile.CarProfile[carIndex].Handling; i++) {
				GUI.DrawTexture (new Rect (585 + 18 * i, 252, stats.width, stats.height), stats);
			}
		
			// acc
			for (int i=0; i<ProfileManager.userProfile.CarProfile[carIndex].Acceleration; i++) {
				GUI.DrawTexture (new Rect (585 + 18 * i, 286, stats.width, stats.height), stats);
			}
		
			// nitro
			for (int i=0; i<ProfileManager.userProfile.CarProfile[carIndex].Nitro; i++) {
				GUI.DrawTexture (new Rect (585 + 18 * i, 322, stats.width, stats.height), stats);
			}
		} else {
			if (GUI.Button (new Rect (100, 200, buyCarFrame.width, buyCarFrame.height), buyCarFrame, new GUIStyle ())) {
				if (ProfileManager.userProfile.Cash >= getCarPrice (carIndex)) {
					ProfileManager.userProfile.Cash -= getCarPrice (carIndex);
					ProfileManager.userProfile.CarProfile [carIndex].IsBought = true;
					PlayerPrefs.Save ();
					return;
				} else {
					this.openPayment ();
				}
			}

			GUI.Label (new Rect (180, 230, 200, 100), "Price: " + getCarPrice (carIndex), number);
		}

		if (isShowFreeCoin) {
			confirmRect = GUI.Window (1, confirmRect, showFreeCoinWindow, string.Empty);
		} else if (isShowPayment) {
			confirmRect = GUI.Window (1, confirmRect, showPaymentWindow, string.Empty);
		}
	}

	void NextCar ()
	{
		transform.eulerAngles = new Vector3 (0, 0, 0);
		cars [carIndex].SetActive (false);
		carIndex++;
		carIndex %= cars.Length;
		cars [carIndex].SetActive (true);

		Rect tmp = SELECTEDCOLOR.pixelInset;
		tmp.x = SetTMP(carIndex);
        SELECTEDCOLOR.pixelInset = tmp;
		colorMaterial [carIndex].SetColor ("_Color", colors [ProfileManager.userProfile.CarProfile [carIndex].Color]);
	}

	void PrevCar ()
	{
		transform.eulerAngles = new Vector3 (0, 0, 0);
		cars [carIndex].SetActive (false);
		carIndex--;
		if (carIndex < 0)
			carIndex = cars.Length - 1;
		carIndex %= cars.Length;
		cars [carIndex].SetActive (true);

		Rect tmp = SELECTEDCOLOR.pixelInset;
		tmp.x = SetTMP(carIndex);
        SELECTEDCOLOR.pixelInset = tmp;
		colorMaterial [carIndex].SetColor ("_Color", colors [ProfileManager.userProfile.CarProfile [carIndex].Color]);
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

	int getCarPrice (int index)
	{
		switch (index) {
		case 0:
			return 0;

		case 1:
			return 40000;

		case 2:
			return 60000;

		case 3:
			return 70000;

		case 4:
			return 80000;

		case 5:
			return 84000;

		case 6:
			return 90000;

		case 7:
			return 96000;

		case 8:
			return 100000;

		case 9:
			return 104000;

		default:
			return 40000;
		}
	}
}
