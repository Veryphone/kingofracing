using UnityEngine;
using System.Collections;

public class PauseMenu:BaseMenu
{
		Rect settingsPos;
		//
		ImageButton continueButton;
		ImageButton restartButton;
		ImageButton mainMenuButton;
		//
		ToggleButton vibrateButton;
		ToggleButton lowButton;
		ToggleButton mediumButton;
		ToggleButton highButton;
		//
		Rect soundPos;
		Rect musicPos;
		Rect sensitivityPos;
		//
		GUIStyle slider;
		GUIStyle thumb;
		//
		Rect background;
		Color color;

		public PauseMenu (MenuRenderer menuRenderer):base(menuRenderer)
		{
				this.settingsPos = new Rect ((800 - menuRenderer.settingBackground.width) / 2, (480 - menuRenderer.settingBackground.height) / 2,
		                             menuRenderer.settingBackground.width, menuRenderer.settingBackground.height);

				this.continueButton = new ImageButton (menuRenderer.continueUp, menuRenderer.continueDown,
		                                       new Rect (50, 410, 200, 50));
				this.continueButton.onClick = continueOnClick;

				this.restartButton = new ImageButton (menuRenderer.restartUp, menuRenderer.restartDown,
		                                      new Rect (300, 410, 200, 50));
				this.restartButton.onClick = restartOnClick;

				this.mainMenuButton = new ImageButton (menuRenderer.mainMenuUp, menuRenderer.mainMenuDown,
		                                      new Rect (550, 410, 200, 50));
				this.mainMenuButton.onClick = mainMenuOnClick;

				this.vibrateButton = new ToggleButton (menuRenderer.onButton, menuRenderer.offButton,
		                                       new Rect (360, 290, 94, 31));
				this.vibrateButton.setChecked (ProfileManager.setttings.IsVibrate);
				this.vibrateButton.toggleOn = vibrateOn;
				this.vibrateButton.toggleOff = vibrateOff;

				this.lowButton = new ToggleButton (menuRenderer.lowUp, menuRenderer.lowDown,
		                                  new Rect (360, 337, 72, 31));
				this.lowButton.toggleOn = lowOn;
				this.lowButton.toggleOff = lowOff;

				this.mediumButton = new ToggleButton (menuRenderer.mediumUp, menuRenderer.mediumDown,
		                                   new Rect (450, 337, 72, 31));
				this.mediumButton.toggleOn = mediumOn;
				this.mediumButton.toggleOff = mediumOff;

				this.highButton = new ToggleButton (menuRenderer.highUp, menuRenderer.highDown,
		                                   new Rect (540, 337, 72, 31));
				this.highButton.toggleOn = highOn;
				this.highButton.toggleOff = highOff;

				switch (ProfileManager.setttings.GraphicsQualitiy) {
				case 0:
						this.lowButton.setChecked (true);
						break;

				case 1:
						this.mediumButton.setChecked (true);
						break;

				case 2:
						this.highButton.setChecked (true);
						break;

				default:
						this.mediumButton.setChecked (true);
						break;
				}

				this.soundPos = new Rect (360, 160, 249, 20);
				this.musicPos = new Rect (360, 205, 249, 20);
				this.sensitivityPos = new Rect (360, 250, 249, 20);

				this.slider = menuRenderer.skin.GetStyle ("horizontalslider");
				this.thumb = menuRenderer.skin.GetStyle ("horizontalsliderthumb");

				this.background = new Rect (0, 0, 800, 480);
				this.color = new Color32 (255, 255, 255, 255);
		}

		public override void render ()
		{
				GUI.color = color;
				GUI.Box (background, string.Empty);
				GUI.color = Color.white;

				GUI.DrawTexture (this.settingsPos, menuRenderer.settingBackground);

				this.continueButton.draw ();
				this.restartButton.draw ();
				this.mainMenuButton.draw ();
				this.vibrateButton.draw ();

				switch (ProfileManager.setttings.GraphicsQualitiy) {
				case 0:
						this.lowButton.draw ();
						break;

				case 1:
						this.mediumButton.draw ();
						break;

				case 2:
						this.highButton.draw ();
						break;

				default:
						this.mediumButton.draw ();
						break;
				}

				ProfileManager.setttings.SoundVolume = GUI.HorizontalSlider (soundPos,
		                                                             ProfileManager.setttings.SoundVolume, 0, 100, slider, thumb);
				
				ProfileManager.setttings.MusicVolume = GUI.HorizontalSlider (musicPos,
		                                                             ProfileManager.setttings.MusicVolume, 0, 100, slider, thumb);
				menuRenderer.game.backgroundMusic.volume = (ProfileManager.setttings.MusicVolume / 100f) * 0.8f;
				
				ProfileManager.setttings.Sensitivity = GUI.HorizontalSlider (sensitivityPos, 
		                                                             ProfileManager.setttings.Sensitivity, 0, 100, slider, thumb);

				PlayerCarController.SENTIVITY = 0.5f - 0.5f * ProfileManager.setttings.Sensitivity / 100f;
		}

		public void continueOnClick ()
		{
				PlayerPrefs.Save ();
				menuRenderer.game.resumeGame ();
        //GoogleMobileAdControll.AdmobControll.HideBanner();
		}

		public void restartOnClick ()
		{
				//Hide ad
				Time.timeScale = 1;
				PlayerPrefs.Save ();
        //GoogleMobileAdControll.AdmobControll.HideBanner();

        SceneLoader.scene = "SLevel_" + GameData.getMapID (GameData.selectedMap);
				Application.LoadLevel ("Loading");
		}

		public void mainMenuOnClick ()
		{
				//Hide ad
				Time.timeScale = 1;
				PlayerPrefs.Save ();
       // GoogleMobileAdControll.AdmobControll.HideBanner();
        AutoFade.LoadLevel ("MainMenu", 0.5f, 0.5f, Color.black);
		}

		public void vibrateOn ()
		{
				ProfileManager.setttings.IsVibrate = true;
		}

		public void vibrateOff ()
		{
				ProfileManager.setttings.IsVibrate = false;
		}

		public void lowOn ()
		{
				ProfileManager.setttings.GraphicsQualitiy = 0;
				((GlowEffect)Camera.main.GetComponent<GlowEffect> ()).enabled = false;

				this.mediumButton.setChecked (false);
				this.highButton.setChecked (false);

				string[] names = QualitySettings.names;
				for (int i=0; i<names.Length; i++) {
						if (names [i].Trim () == "Fastest") {
								QualitySettings.SetQualityLevel (i, true);
						}
				}
		}

		public void lowOff ()
		{
				this.lowButton.setChecked (true);
		}

		public void mediumOn ()
		{
				ProfileManager.setttings.GraphicsQualitiy = 1;
				((GlowEffect)Camera.main.GetComponent<GlowEffect> ()).enabled = false;

				this.lowButton.setChecked (false);
				this.highButton.setChecked (false);

				string[] names = QualitySettings.names;
				for (int i=0; i<names.Length; i++) {
						if (names [i].Trim () == "Simple") {
								QualitySettings.SetQualityLevel (i, true);
						}
				}
		}

		public void mediumOff ()
		{
				this.mediumButton.setChecked (true);
		}

		public void highOn ()
		{
				ProfileManager.setttings.GraphicsQualitiy = 2;

				((GlowEffect)Camera.main.GetComponent<GlowEffect> ()).enabled = true;		
				((GlowEffect)Camera.main.GetComponent<GlowEffect> ()).blurIterations = 3;

				this.lowButton.setChecked (false);
				this.mediumButton.setChecked (false);

				string[] names = QualitySettings.names;
				for (int i=0; i<names.Length; i++) {
						if (names [i].Trim () == "Fantastic") {
								QualitySettings.SetQualityLevel (i, true);
						}
				}
		}

		public void highOff ()
		{
				this.highButton.setChecked (true);
		}
}
