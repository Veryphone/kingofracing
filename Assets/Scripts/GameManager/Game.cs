using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour
{
    public static int allCarNumber = 10;
		public enum GAME_STATE
		{
				START,
				RUNNING,
				PAUSE,
				WIN,
				LOSE
		}

		static GAME_STATE gameState;

		public static GAME_STATE GameState {
				get {
						return gameState;
				}
		}

		//
		public MenuRenderer menuRenderer;
		public Map map;
		public CarManager carManager;
		public Material[] colorMaterial;
		public SoundManager soundManager;
		//public SunnetPlugin sunnetPlugin;
		//
		public GameObject leftTrail;
		public GameObject rightTrail;
		//
		public AudioSource backgroundMusic;
		//
		float deltaTime;

		public float DeltaTime {
				get {
						return deltaTime;
				}
		}

		int reward;
	
		public int Reward {
				get {
						return reward;
				}
				set {
						reward = value;
				}
		}

		void Start ()
		{
        
				ProfileManager.init ();
				backgroundMusic.volume = (ProfileManager.setttings.MusicVolume / 100f) * 0.8f;
				backgroundMusic.Play ();

				gameState = GAME_STATE.START;

				carManager = new CarManager (this);
				menuRenderer.initMiniMap ();

				string[] names = QualitySettings.names;
				for (int i=0; i<names.Length; i++) {
						switch (ProfileManager.setttings.GraphicsQualitiy) {
						case 0:
								if (names [i].Trim () == "Fastest") {
										QualitySettings.SetQualityLevel (i, true);
										QualitySettings.antiAliasing = 0;
								}
								break;
				
						case 1:
								if (names [i].Trim () == "Simple") {
										QualitySettings.SetQualityLevel (i, true);
										QualitySettings.antiAliasing = 2;
								}
								break;
				
						case 2:
								if (names [i].Trim () == "Fantastic") {
										QualitySettings.SetQualityLevel (i, true);
										QualitySettings.antiAliasing = 0;
								}
								break;
				
						default:
								if (names [i].Trim () == "Simple") {
										QualitySettings.SetQualityLevel (i, true);
										QualitySettings.antiAliasing = 2;
								}
								break;
						}
				}

				switch (ProfileManager.setttings.GraphicsQualitiy) {
				case 0:
						((GlowEffect)Camera.main.GetComponent<GlowEffect> ()).enabled = false;
						break;

				case 1:
						((GlowEffect)Camera.main.GetComponent<GlowEffect> ()).enabled = false;
						break;

				case 2:
						((GlowEffect)Camera.main.GetComponent<GlowEffect> ()).enabled = true;
						((GlowEffect)Camera.main.GetComponent<GlowEffect> ()).blurIterations = 3;
						break;

				default:
						((GlowEffect)Camera.main.GetComponent<GlowEffect> ()).enabled = false;
						break;
				}

				colorMaterial [ProfileManager.userProfile.SelectedCar].SetColor ("_Color",
			GameData.GetCarColor (ProfileManager.userProfile.CarProfile [ProfileManager.userProfile.SelectedCar].Color));
        print(ProfileManager.userProfile.CarProfile[ProfileManager.userProfile.SelectedCar].Color);
        //				sunnetPlugin.hideAd ();
        print("Start_____________________");
    }

		void OnEnable ()
		{
        print("OnEnable_____________________"+ GameData.selectedMode);
				string temp = "Play Game Screen";
				if (GameData.level == -1) {
						temp += ",quickrace";
				} else {
						temp += ",level" + GameData.level;
				}
		
				switch (GameData.selectedMode) {
				case GameData.GAME_MODE.CHECK_POINT:
						temp += ",checkpoint";
						break;
			
				case GameData.GAME_MODE.NORMAL:
						temp += ",normal";
						break;
			
				case GameData.GAME_MODE.TIME_TRIAL:
						temp += ",timetrial";
						break;
			
				default:
						break;
				}
		if(ProfileManager.userProfile!=null)
			temp += ",car" + ProfileManager.userProfile.SelectedCar;
		
				GoogleAnalytics.LogScreen (temp);
		}

		void Update ()
		{
				switch (gameState) {
				case GAME_STATE.START:
						deltaTime += Time.deltaTime / 2.5f;

						if (deltaTime > 2) {
								soundManager.playCountDown ();
						} else if (deltaTime > 1) {
								soundManager.playCountDown ();
						} else if (deltaTime > 0) {
								soundManager.playCountDown ();
						}

						if (deltaTime > 3) {
								gameState = GAME_STATE.RUNNING;
								carManager.setTarget ();
								carManager.startCar ();
								deltaTime = 0;
								soundManager.playEngine ();
						}
						break;

				case GAME_STATE.RUNNING:
						deltaTime += Time.deltaTime;
						carManager.Update ();
						break;

				case GAME_STATE.PAUSE:
						break;

				case GAME_STATE.WIN:
						break;

				case GAME_STATE.LOSE:
						break;

				default:
						break;
				}
		}
	
		void OnApplicationPause (bool pauseStatus)
		{
				this.pauseGame ();
		}

		public void pauseGame ()
		{
				if (Game.gameState == Game.GAME_STATE.RUNNING) {
						gameState = GAME_STATE.PAUSE;
						Time.timeScale = 0;
						soundManager.stopEngine ();
            //GoogleMobileAdControll.AdmobControll.ShowBanner();
        }
		}

		public void resumeGame ()
		{
				Game.gameState = Game.GAME_STATE.RUNNING;
				Time.timeScale = 1;
				soundManager.playEngine ();
		}

		public void winGame ()
		{
        //				sunnetPlugin.loadLargeAd (SunnetPlugin.PAYMENT_ALIGN_CENTER_HORIZONTAL, SunnetPlugin.PAYMENT_ALIGN_CENTER_VERTICAL);
        
        soundManager.stopEngine ();
				Time.timeScale = 0;
				Game.gameState = Game.GAME_STATE.WIN;

				ProfileManager.userProfile.Cash += carManager.playerCarController.dollar;

				if (GameData.isDoubleReward == true) {
						reward = reward * 2;
				} 

				if (GameData.level != -1) {
						if (ProfileManager.userProfile.MapProfile [GameData.level].LastReward > 0) {
								ProfileManager.userProfile.Cash += (int)(reward * 0.4f);
						}

						if (carManager.playerCarController.isNitroUsed == false) {
								ProfileManager.achievementProfile.IsNitroUsed = false;
						}			
						ProfileManager.userProfile.calculateStar ();
				} else {
						ProfileManager.userProfile.Cash += reward;
						ProfileManager.achievementProfile.NumberQuickRaceWon += 1;
				}

				PlayerPrefs.Save ();
       // ChartboostController.chartBoost.Show();
    }

		public void loseGame ()
		{		
//				sunnetPlugin.loadLargeAd (SunnetPlugin.PAYMENT_ALIGN_CENTER_HORIZONTAL, SunnetPlugin.PAYMENT_ALIGN_CENTER_VERTICAL);
				soundManager.stopEngine ();
				ProfileManager.userProfile.Cash += carManager.playerCarController.dollar;
				PlayerPrefs.Save ();

				Time.timeScale = 0;
				Game.gameState = Game.GAME_STATE.LOSE;
		}
}
