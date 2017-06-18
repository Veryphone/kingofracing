using UnityEngine;
using System.Collections;

public class GameOverMenu : BaseMenu
{
		Rect gameOverPos;
		//
		ImageButton shareButton;
		ImageButton continueButton;
		ImageButton restartButton;
		//
		Rect rankPos;
		Rect lapPos;
		Rect durationPos;
		Rect rewardPos;
		//
		int fontSize;
		TextAnchor anchor;
		//
		Rect background;
		Color color;

		public GameOverMenu (MenuRenderer menuRenderer):base(menuRenderer)
		{
				this.gameOverPos = new Rect ((800 - menuRenderer.gameOverBackground.width) / 2, (480 - menuRenderer.gameOverBackground.height) / 2,
		                             menuRenderer.gameOverBackground.width, menuRenderer.gameOverBackground.height);
		
				this.continueButton = new ImageButton (menuRenderer.continueUp, menuRenderer.continueDown,
		                                       new Rect (50, 410, 200, 50));
				this.continueButton.onClick = continueOnClick;
		
				this.restartButton = new ImageButton (menuRenderer.restartUp, menuRenderer.restartDown,
		                                      new Rect (300, 410, 200, 50));
				this.restartButton.onClick = restartOnClick;
		
				//this.shareButton = new ImageButton (menuRenderer.shareUp, menuRenderer.shareDown,
		  //                                     new Rect (550, 410, 200, 50));
				//this.shareButton.onClick = shareOnClick;

				this.rankPos = new Rect (400, 175, 200, 50);
				this.lapPos = new Rect (400, 220, 200, 50);
				this.durationPos = new Rect (400, 265, 200, 50);
				this.rewardPos = new Rect (400, 315, 200, 50);

				this.background = new Rect (0, 0, 800, 480);
				this.color = new Color32 (255, 255, 255, 255);
		}

		public override void render ()
		{
				GUI.color = color;
				GUI.Box (background, string.Empty);
				GUI.color = Color.white;

				GUI.DrawTexture (this.gameOverPos, menuRenderer.gameOverBackground);

				if (Game.GameState == Game.GAME_STATE.WIN) {
						GUI.DrawTexture (new Rect (250, 100, 308, 62), menuRenderer.winImage);
				} else if (Game.GameState == Game.GAME_STATE.LOSE) {
						GUI.DrawTexture (new Rect (270, 100, 262, 52), menuRenderer.loseImage);
				}

				this.continueButton.draw ();
				this.restartButton.draw ();
				//this.shareButton.draw ();

				GUI.skin.label.font = menuRenderer.numberFont;

				anchor = GUI.skin.label.alignment;
				GUI.skin.label.alignment = TextAnchor.MiddleRight;
				this.fontSize = GUI.skin.label.fontSize;

				GUI.skin.label.fontSize = 30;

				GUI.Label (rankPos, string.Empty + menuRenderer.game.carManager.getOrder (0));

				if (GameData.selectedMode == GameData.GAME_MODE.NORMAL) {
						int numberRaces = (GameData.numberRaces - menuRenderer.game.carManager.carInfo [0].NumberRaces);
						if (numberRaces > GameData.numberRaces) {
								numberRaces = GameData.numberRaces;
						}
						GUI.Label (lapPos, string.Empty + numberRaces);
				} else {
						int numberRaces = (GameData.numberRaces - menuRenderer.game.carManager.carInfo [0].NumberRaces);
						GUI.Label (lapPos, string.Empty + numberRaces);
				}

				GUI.Label (durationPos, ((int)menuRenderer.game.DeltaTime) / 60 + ":" +
						((int)menuRenderer.game.DeltaTime) % 60);

				int reward = menuRenderer.game.Reward;

				if (GameData.level != -1) {
						if (ProfileManager.userProfile.MapProfile [GameData.level].LastReward > 0) {
								reward = (int)(reward * 0.4f);
						} 
				}

				GUI.Label (rewardPos, (reward + menuRenderer.game.carManager.playerCarController.dollar) + "$");

				GUI.skin.label.fontSize = fontSize;
				GUI.skin.label.alignment = anchor;
		}

		public void continueOnClick ()
		{
				Time.timeScale = 1;
				PlayerPrefs.Save ();
				AutoFade.LoadLevel ("MainMenu", 0.5f, 0.5f, Color.black);
		}
	
		public void restartOnClick ()
		{
				Time.timeScale = 1;
				PlayerPrefs.Save ();
		
				SceneLoader.scene = "SLevel_" + GameData.getMapID (GameData.selectedMap);
				Application.LoadLevel ("Loading");
		}
	
		public void shareOnClick ()
		{
//				Time.timeScale = 1;
				//menuRenderer.game.sunnetPlugin.postScoreToFacebook (ProfileManager.userProfile.Star);
				ProfileManager.achievementProfile.IsConnectFacebook = true;
				PlayerPrefs.Save ();
		}
}
