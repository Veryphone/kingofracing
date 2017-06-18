using UnityEngine;
using System.Collections;

public class InfoRenderer:BaseMenu
{
		Rect wrongWayPos = new Rect (160, 200, 500, 200);
		Rect orderPos = new Rect (180, 50, 200, 100);
		Rect lapPos = new Rect (575, 50, 200, 50);
		Rect speedPos = new Rect (370, 35, 100, 100);
		Rect timePos = new Rect (385, 80, 100, 100);
		//
		Rect nitroBarPos;
		Rect currentNitroPos;
		//
		Rect topMenuPos;
		Rect speedMeterPos;
		Rect lapTextPos;
		Rect orderTextPos;
		//
		int fontSize;
		int numberRaces;
	
		public InfoRenderer (MenuRenderer menuRenderer):base(menuRenderer)
		{
				this.topMenuPos = new Rect ((800 - menuRenderer.topMenu.width) / 2, 0, 
		                            menuRenderer.topMenu.width, menuRenderer.topMenu.height);
				this.speedMeterPos = new Rect ((800 - menuRenderer.speedMeter.width) / 2, 25,
		                               menuRenderer.speedMeter.width, menuRenderer.speedMeter.height);
		
				this.orderTextPos = new Rect (160, 20, menuRenderer.posImage.width, menuRenderer.posImage.height);
				this.lapTextPos = new Rect (580, 20, menuRenderer.lapImage.width, menuRenderer.lapImage.height);

				this.nitroBarPos = new Rect (270, 0, menuRenderer.nitroBar.width, menuRenderer.nitroBar.height);
				this.currentNitroPos = new Rect (0, 0, menuRenderer.nitroBar.width, menuRenderer.nitroBar.height);
		}
	
		public override void render ()
		{
				GUI.DrawTexture (topMenuPos, menuRenderer.topMenu);
				GUI.DrawTexture (speedMeterPos, menuRenderer.speedMeter);
				GUI.DrawTexture (orderTextPos, menuRenderer.posImage);

				nitroBarPos.width = (menuRenderer.game.carManager.playerCarController.carData.NitroBar / 100f) *
						menuRenderer.nitroBar.width;
				GUI.BeginGroup (nitroBarPos);
				GUI.DrawTexture (this.currentNitroPos, menuRenderer.nitroBar);
				GUI.EndGroup ();

				this.fontSize = GUI.skin.label.fontSize;
				GUI.skin.label.fontSize = 25;

				GUI.skin.label.font = menuRenderer.numberFont;
				if (menuRenderer.game.carManager.playerCarController.carData.CurrentSpeed >= 0) {
						GUI.Label (speedPos, ((int)(menuRenderer.game.carManager.playerCarController.carData.CurrentSpeed * 3)) + string.Empty);
				} else {
						GUI.Label (speedPos, "0");
				}

				GUI.skin.label.fontStyle = FontStyle.Italic;
				GUI.contentColor = Color.yellow;

				GUI.Label (orderPos, menuRenderer.PlayerOrder + "/" + menuRenderer.game.carManager.carDistance.Length);

				switch (GameData.selectedMode) {
				case GameData.GAME_MODE.NORMAL:
						GUI.DrawTexture (lapTextPos, menuRenderer.lapImage);
			
						numberRaces = (GameData.numberRaces - menuRenderer.game.carManager.carInfo [0].NumberRaces);
						if (numberRaces > GameData.numberRaces) {
								numberRaces = GameData.numberRaces;
						}
						GUI.Label (lapPos, numberRaces + "/" + GameData.numberRaces);
						break;

				case GameData.GAME_MODE.TIME_TRIAL:
						GUI.DrawTexture (lapTextPos, menuRenderer.lapImage);
			
						numberRaces = (GameData.numberRaces - menuRenderer.game.carManager.carInfo [0].NumberRaces);
						GUI.Label (lapPos, numberRaces + string.Empty);
						break;

				case GameData.GAME_MODE.CHECK_POINT:
						GUI.DrawTexture (lapTextPos, menuRenderer.checksImage);
						GUI.Label (lapPos, menuRenderer.game.carManager.playerCarController.numberCheckPoint + "/" + GameData.numberCheckpoints);
						break;

				default:
						break;
				}
		
				GUI.contentColor = Color.white;
				GUI.skin.label.fontStyle = FontStyle.Normal;
				GUI.skin.label.fontSize = fontSize;

				switch (Game.GameState) {
				case Game.GAME_STATE.START:
						GUI.skin.label.fontSize = 200;
						if (((3 - (int)menuRenderer.game.DeltaTime)) == 1) {
								GUI.Label (new Rect ((800 - 50) / 2, (480 - 250) / 2, 200, 200),
				           string.Empty + Mathf.Abs (3 - (int)menuRenderer.game.DeltaTime));
						} else {
								GUI.Label (new Rect ((800 - 100) / 2, (480 - 250) / 2, 200, 200),
				           string.Empty + Mathf.Abs (3 - (int)menuRenderer.game.DeltaTime));
						}
						GUI.skin.label.fontSize = fontSize;
						break;
			
				case Game.GAME_STATE.RUNNING:
						if (GameData.selectedMode != GameData.GAME_MODE.NORMAL) {
								float remainTime = GameData.duration - menuRenderer.game.DeltaTime;
								if (remainTime < 0) {
										remainTime = 0;
								}
								GUI.contentColor = Color.red;
								GUI.skin.label.fontSize = 30;
								GUI.Label (timePos, ((int)(remainTime)) / 60 + ":" + ((int)remainTime) % 60);
								GUI.skin.label.fontSize = fontSize;
								GUI.contentColor = Color.white;
						} else {
								GUI.Label (timePos, ((int)menuRenderer.game.DeltaTime) / 60 + ":" + ((int)menuRenderer.game.DeltaTime) % 60);
						}

						if (menuRenderer.IsWrongWay == true) {
								fontSize = GUI.skin.label.fontSize;
								GUI.skin.label.fontSize = 60;
								GUI.Label (wrongWayPos, "Wrong Way");
								GUI.skin.label.fontSize = fontSize;
						}
						break;
			
				default:
						break;
				}
		}
}
