using UnityEngine;
using System.Collections;

public class PlayMenu:BaseMenu
{
		CarManager carManager;
		//
		Rect brakePos;
		Rect nitroPos;
		Rect pausePos;
		Rect miniMapSize;
		//
		Rect brakeEventPos = new Rect (0, 200, 300, 240);
		Rect nitroEventPos = new Rect (560, 200, 300, 240);
		//
		ImageButton pauseButton;
		ImageButton respawnButton;
		//
		Rect enemyPos;
		Rect playerPos;
		float tempWidth;
		float tempHeight;
		int i;

		public PlayMenu (MenuRenderer menuRenderer):base(menuRenderer)
		{
				this.carManager = menuRenderer.game.carManager;
		
				miniMapSize = new Rect (530, 100, menuRenderer.miniMap.width, menuRenderer.miniMap.height);
		
				float realWidth = menuRenderer.game.map.worldEnd.transform.position.x;
				float realHeight = menuRenderer.game.map.worldEnd.transform.position.z;
		
				tempWidth = miniMapSize.width / realWidth;
				tempHeight = miniMapSize.height / realHeight;
		
				playerPos = new Rect (0, 0, 4, 4);
				enemyPos = new Rect (0, 0, 4, 4);

				this.brakePos = new Rect (10, 330, menuRenderer.brakeUp.width, menuRenderer.brakeUp.height);
				this.nitroPos = new Rect (650, 330, menuRenderer.nitroUp.width, menuRenderer.nitroUp.height);
				this.pausePos = new Rect (50, 10, menuRenderer.pauseUp.width, menuRenderer.pauseDown.height);

				this.respawnButton = new ImageButton (menuRenderer.respawnUp, menuRenderer.respawnDown,
		                                   new Rect ((800 - menuRenderer.respawnUp.width) / 2, 350, 
		         menuRenderer.respawnUp.width, menuRenderer.respawnUp.height));
				this.respawnButton.onClick = onRespawnClick;

				this.pauseButton = new ImageButton (menuRenderer.pauseUp, menuRenderer.pauseDown, pausePos);
				this.pauseButton.onClick = onPauseClick;
		}

		public override void render ()
		{
				this.drawMinimap ();
				this.drawControlButton ();
		}

		public void drawControlButton ()
		{
				this.pauseButton.draw ();

				if (carManager.isBrake == true) {
						GUI.DrawTexture (brakePos, menuRenderer.brakeDown);
				} else {
						GUI.DrawTexture (brakePos, menuRenderer.brakeUp);
				}

				if (carManager.playerCarController.carData.IsNitroUsing == true) {
						GUI.DrawTexture (nitroPos, menuRenderer.nitroDown);
				} else {
						GUI.DrawTexture (nitroPos, menuRenderer.nitroUp);
				}

				if (carManager.carList [0].transform.position.y < -50) {
						menuRenderer.game.carManager.respawnPlayerCar ();
				}

				if (carManager.playerCarController.isStuck == true || menuRenderer.IsWrongWay == true) {
						this.respawnButton.draw ();
				} else if (Vector3.Dot (menuRenderer.game.carManager.carList [0].transform.up, Vector3.up) < 0) {
						menuRenderer.game.carManager.carList [0].GetComponent<Rigidbody>().velocity = Vector3.zero;
						this.respawnButton.draw ();
				}
			
				if (Game.GameState == Game.GAME_STATE.RUNNING) {
						if (Application.isEditor == true) {
								if (Input.GetMouseButtonDown (0)) {
										Vector2 mousePos = Input.mousePosition;
										mousePos.y = Screen.height - mousePos.y;
										mousePos.Scale (MenuRenderer.SCALE);
					
										if (brakeEventPos.Contains (mousePos)) {
												carManager.playerCarController.UpdateTorque (true);
												carManager.isBrake = true;
												carManager.playerCarController.carData.activateNitro (false);
										} else if (nitroEventPos.Contains (mousePos)) {
												carManager.playerCarController.carData.activateNitro (true);
										}
								}
				
								if (Input.GetMouseButtonUp (0)) {
										Vector2 mousePos = Input.mousePosition;
										mousePos.y = Screen.height - mousePos.y;
										mousePos.Scale (MenuRenderer.SCALE);

										carManager.playerCarController.UpdateTorque (false);
										carManager.isBrake = false;
								}
						} else {
								if (Input.touchCount > 0) {
										foreach (Touch touch in Input.touches) {
												Vector2 touchPos = touch.position;
												touchPos.y = Screen.height - touchPos.y;								
												touchPos.Scale (MenuRenderer.SCALE);
						
												if (touch.phase == TouchPhase.Began) {
														if (brakeEventPos.Contains (touchPos)) {
																carManager.playerCarController.UpdateTorque (true);
																carManager.isBrake = true;
																carManager.playerCarController.carData.activateNitro (false);
														} else	if (nitroEventPos.Contains (touchPos)) {
																carManager.playerCarController.carData.activateNitro (true);
														}

												} else if (touch.phase == TouchPhase.Ended) {
														carManager.playerCarController.UpdateTorque (false);
														carManager.isBrake = false;
												}
										}				
								}
						}
				}
		}

		void drawMinimap ()
		{
				GUI.DrawTexture (miniMapSize, menuRenderer.miniMap);
		
				playerPos.x = carManager.carList [0].transform.position.x * tempWidth + miniMapSize.x;
				playerPos.y = miniMapSize.height - carManager.carList [0].transform.position.z * tempHeight + miniMapSize.y;
		
				GUI.DrawTexture (playerPos, menuRenderer.playerIndicator);
		
				for (i=1; i<carManager.carList.Length; i++) {
						enemyPos.x = carManager.carList [i].transform.position.x * tempWidth + miniMapSize.x;
						enemyPos.y = miniMapSize.height - carManager.carList [i].transform.position.z * tempHeight + miniMapSize.y;
			
						GUI.DrawTexture (enemyPos, menuRenderer.enemyIndicator);
				}
		}

		void onPauseClick ()
		{
				this.menuRenderer.game.pauseGame ();
		}

		void onRespawnClick ()
		{
				if (Game.GameState == Game.GAME_STATE.RUNNING) {
						menuRenderer.game.carManager.respawnPlayerCar ();
				}
		}
}
