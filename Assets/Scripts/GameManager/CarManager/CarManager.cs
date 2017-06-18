using UnityEngine;
using System.Collections;

public class CarManager:BaseCarManager
{
		int i, j, k;
		CarDistance tempDistance = new CarDistance (0);

		public CarManager (Game game):base(game)
		{
				this.initCar ();
				this.initCarInfo ();
		}

		public void Update ()
		{
				carInfo [0].Update ();

				for (i=1; i<carInfo.Length; i++) {
						carInfo [i].Update ();
				}

				for (i=0; i<carDistance.Length; i++) {
						carDistance [i].Distance = carInfo [carDistance [i].ID].RemainingDistance;
				}

				this.checkGameOverCondition ();

				for (i=1; i<enemyCarController.Length; i++) {
						enemyCarController [i].setOrder (getOrder (i));
				}
				game.menuRenderer.PlayerOrder = getOrder (0);

				game.menuRenderer.IsWrongWay = carInfo [0].isWrongWay ();
				for (i=0; i<carDistance.Length-1; i++) {
						for (j=i+1; j<carDistance.Length; j++) {
								if (carDistance [i].Distance > carDistance [j].Distance) {
										tempDistance.ID = carDistance [i].ID;
										tempDistance.Distance = carDistance [i].Distance;

										carDistance [i].ID = carDistance [j].ID;
										carDistance [i].Distance = carDistance [j].Distance;

										carDistance [j].ID = tempDistance.ID;
										carDistance [j].Distance = tempDistance.Distance;
								}
						}
				}
		}

		public void changePath (bool isSameDirection, PathChangingController pathChangingController)
		{
				if (isSameDirection == true) {
						if (isBrake == false) {
								playerCarController.pathChanger = pathChangingController;
								carInfo [0].NextWaypoint = 1;
//								Debug.Log ("In");
						} else {
								playerCarController.pathChanger = null;
								carInfo [0].NextWaypoint = pathChangingController.inWaypointID;
//								Debug.Log ("Out");
						}
				} else {
						if (isBrake == false) {
								playerCarController.pathChanger = null;
								carInfo [0].NextWaypoint = pathChangingController.inWaypointID;
//								Debug.Log ("Out");
						} else {
								playerCarController.pathChanger = pathChangingController;
								carInfo [0].NextWaypoint = 
					game.map.pathList [pathChangingController.subPathID].waypointList.Length - 1;
//								Debug.Log ("In");
						}
				}
		}

		public int getOrder (int id)
		{
				for (k=0; k<carInfo.Length; k++) {
						if (carDistance [k].ID == id) {
								return k + 1;
						}
				}
				return carInfo.Length;
		}

		public void checkGameOverCondition ()
		{
				if (GameData.level != -1) {
						switch (GameData.selectedMode) {
						case GameData.GAME_MODE.NORMAL:
								if (carInfo [0].NumberRaces <= -1) {
										game.Reward = 0;

										switch (GameData.level) {
										case 0:
												switch (getOrder (0)) {
												case 1:
														game.Reward = calculateReward (3);
														ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 3;
														break;
							
												case 2:
														game.Reward = calculateReward (2);
														ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 2;
														break;
							
												case 3:
														game.Reward = calculateReward (1);
														ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 1;
														break;

												default:
														break;
												}
												break;

										case 1:
												switch (getOrder (0)) {
												case 1:
														game.Reward = calculateReward (3);
														ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 3;
														break;
							
												case 2:
														game.Reward = calculateReward (2);
														ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 2;
														break;
							
												case 3:
														game.Reward = calculateReward (1);
														ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 1;
														break;
							
												default:
														break;
												}
												break;

										case 4:
												switch (getOrder (0)) {
												case 1:
														game.Reward = calculateReward (3);
														ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 3;
														break;
							
												case 2:
														game.Reward = calculateReward (2);
														ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 2;
														break;
							
												case 3:
														game.Reward = calculateReward (1);
														ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 1;
														break;
							
												default:
														break;
												}
												break;

										case 6:
												switch (getOrder (0)) {
												case 1:
														game.Reward = calculateReward (3);
														ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 3;
														break;
							
												case 2:
														game.Reward = calculateReward (2);
														ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 2;
														break;
							
												case 3:
														game.Reward = calculateReward (1);
														ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 1;
														break;
							
												default:
														break;
												}
												break;

										case 8:
												switch (getOrder (0)) {
												case 1:
														game.Reward = calculateReward (3);
														ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 3;
														break;
							
												case 2:
														game.Reward = calculateReward (2);
														ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 2;
														break;
							
												case 3:
														game.Reward = calculateReward (1);
														ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 1;
														break;
							
												default:
														break;
												}
												break;

										case 15:
												switch (getOrder (0)) {
												case 1:
														game.Reward = calculateReward (3);
														ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 3;
														break;
							
												case 2:
														game.Reward = calculateReward (2);
														ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 2;
														break;
							
												case 3:
														game.Reward = calculateReward (1);
														ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 1;
														break;
							
												default:
														break;
												}
												break;

										case 18:
												switch (getOrder (0)) {
												case 1:
														game.Reward = calculateReward (3);
														ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 3;
														break;
							
												case 2:
														game.Reward = calculateReward (2);
														ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 2;
														break;
							
												case 3:
														game.Reward = calculateReward (1);
														ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 1;
														break;
							
												default:
														break;
												}
												break;

										case 19:
												switch (getOrder (0)) {
												case 1:
														game.Reward = calculateReward (3);
														ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 3;
														break;
							
												case 2:
														game.Reward = calculateReward (2);
														ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 2;
														break;
							
												case 3:
														game.Reward = calculateReward (1);
														ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 1;
														break;
							
												default:
														break;
												}
												break;

										case 22:
												switch (getOrder (0)) {
												case 1:
														game.Reward = calculateReward (3);
														ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 3;
														break;
							
												case 2:
														game.Reward = calculateReward (2);
														ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 2;
														break;
							
												case 3:
														game.Reward = calculateReward (1);
														ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 1;
														break;
							
												default:
														break;
												}
												break;

										case 24:
												switch (getOrder (0)) {
												case 1:
														game.Reward = calculateReward (3);
														ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 3;
														break;
							
												case 2:
														game.Reward = calculateReward (2);
														ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 2;
														break;
							
												case 3:
														game.Reward = calculateReward (1);
														ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 1;
														break;
							
												default:
														break;
												}
												break;

										case 26:
												switch (getOrder (0)) {
												case 1:
														game.Reward = calculateReward (3);
														ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 3;
														break;
							
												case 2:
														game.Reward = calculateReward (2);
														ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 2;
														break;
							
												case 3:
														game.Reward = calculateReward (1);
														ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 1;
														break;
							
												default:
														break;
												}
												break;

										case 33:
												switch (getOrder (0)) {
												case 1:
														game.Reward = calculateReward (3);
														ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 3;
														break;
							
												case 2:
														game.Reward = calculateReward (2);
														ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 2;
														break;
							
												case 3:
														game.Reward = calculateReward (1);
														ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 1;
														break;
							
												default:
														break;
												}
												break;

										default:
												break;
										}

										if (getOrder (0) > 3) {
												game.loseGame ();
										} else {
												game.winGame ();
										}
								}
								break;

						case GameData.GAME_MODE.CHECK_POINT:
								if (game.DeltaTime >= GameData.duration) {
										game.Reward = 0;

										switch (GameData.level) {
										case 2:
												if (playerCarController.numberCheckPoint >= 3) {
														switch (getOrder (0)) {
														case 1:
																game.Reward = calculateReward (3);
																ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 3;
																game.winGame ();
																break;
								
														case 2:
																game.Reward = calculateReward (2);
																ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 2;
																game.winGame ();
																break;
								
														case 3:
																game.Reward = calculateReward (1);
																ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 1;
																game.winGame ();
																break;
								
														default:
																game.loseGame ();
																break;
														}														
												} else {
														game.loseGame ();
												}
												break;
						
										case 7:
												if (playerCarController.numberCheckPoint >= 4) {
														switch (getOrder (0)) {
														case 1:
																game.Reward = calculateReward (3);
																ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 3;
																game.winGame ();
																break;
								
														case 2:
																game.Reward = calculateReward (2);
																ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 2;
																game.winGame ();
																break;
								
														case 3:
																game.Reward = calculateReward (1);
																ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 1;
																game.winGame ();
																break;
								
														default:
																game.loseGame ();
																break;
														}
												} else {
														game.loseGame ();
												}
												break;
						
										case 11:
												if (playerCarController.numberCheckPoint >= 5) {
														switch (getOrder (0)) {
														case 1:
																game.Reward = calculateReward (3);
																ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 3;
																game.winGame ();
																break;
								
														case 2:
																game.Reward = calculateReward (2);
																ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 2;
																game.winGame ();
																break;
								
														case 3:
																game.Reward = calculateReward (1);
																ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 1;
																game.winGame ();
																break;
								
														default:
																game.loseGame ();
																break;
														}
												} else {
														game.loseGame ();
												}
												break;
						
										case 12:
												if (playerCarController.numberCheckPoint >= 5) {
														switch (getOrder (0)) {
														case 1:
																game.Reward = calculateReward (3);
																ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 3;
																game.winGame ();
																break;
								
														case 2:
																game.Reward = calculateReward (2);
																ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 2;
																game.winGame ();
																break;
								
														case 3:
																game.Reward = calculateReward (1);
																ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 1;
																game.winGame ();
																break;
								
														default:
																game.loseGame ();
																break;
														}
												} else {
														game.loseGame ();
												}
												break;
						
										case 14:
												if (playerCarController.numberCheckPoint >= 6) {
														switch (getOrder (0)) {
														case 1:
																game.Reward = calculateReward (3);
																ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 3;
																game.winGame ();
																break;
								
														case 2:
																game.Reward = calculateReward (2);
																ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 2;
																game.winGame ();
																break;
								
														case 3:
																game.Reward = calculateReward (1);
																ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 1;
																game.winGame ();
																break;
								
														default:
																game.loseGame ();
																break;
														}
												} else {
														game.loseGame ();
												}
												break;
						
										case 16:
												if (playerCarController.numberCheckPoint >= 6) {
														switch (getOrder (0)) {
														case 1:
																game.Reward = calculateReward (3);
																ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 3;
																game.winGame ();
																break;
								
														case 2:
																game.Reward = calculateReward (2);
																ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 2;
																game.winGame ();
																break;
								
														case 3:
																game.Reward = calculateReward (1);
																ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 1;
																game.winGame ();
																break;
								
														default:
																game.loseGame ();
																break;
														}
												} else {
														game.loseGame ();
												}
												break;
						
										case 20:
												if (playerCarController.numberCheckPoint >= 6) {
														switch (getOrder (0)) {
														case 1:
																game.Reward = calculateReward (3);
																ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 3;
																game.winGame ();
																break;
								
														case 2:
																game.Reward = calculateReward (2);
																ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 2;
																game.winGame ();
																break;
								
														case 3:
																game.Reward = calculateReward (1);
																ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 1;
																game.winGame ();
																break;
								
														default:
																game.loseGame ();
																break;
														}
												} else {
														game.loseGame ();
												}
												break;
						
										case 25:
												if (playerCarController.numberCheckPoint >= 6) {
														switch (getOrder (0)) {
														case 1:
																game.Reward = calculateReward (3);
																ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 3;
																game.winGame ();
																break;
								
														case 2:
																game.Reward = calculateReward (2);
																ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 2;
																game.winGame ();
																break;
								
														case 3:
																game.Reward = calculateReward (1);
																ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 1;
																game.winGame ();
																break;
								
														default:
																game.loseGame ();
																break;
														}
												} else {
														game.loseGame ();
												}
												break;
						
										case 29:
												if (playerCarController.numberCheckPoint >= 7) {
														switch (getOrder (0)) {
														case 1:
																game.Reward = calculateReward (3);
																ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 3;
																game.winGame ();
																break;
								
														case 2:
																game.Reward = calculateReward (2);
																ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 2;
																game.winGame ();
																break;
								
														case 3:
																game.Reward = calculateReward (1);
																ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 1;
																game.winGame ();
																break;
								
														default:
																game.loseGame ();
																break;
														}
												} else {
														game.loseGame ();
												}
												break;
						
										case 30:
												if (playerCarController.numberCheckPoint >= 7) {
														switch (getOrder (0)) {
														case 1:
																game.Reward = calculateReward (3);
																ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 3;
																game.winGame ();
																break;
								
														case 2:
																game.Reward = calculateReward (2);
																ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 2;
																game.winGame ();
																break;
								
														case 3:
																game.Reward = calculateReward (1);
																ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 1;
																game.winGame ();
																break;
								
														default:
																game.loseGame ();
																break;
														}
												} else {
														game.loseGame ();
												}
												break;
						
										case 32:
												if (playerCarController.numberCheckPoint >= 8) {
														switch (getOrder (0)) {
														case 1:
																game.Reward = calculateReward (3);
																ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 3;
																game.winGame ();
																break;
								
														case 2:
																game.Reward = calculateReward (2);
																ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 2;
																game.winGame ();
																break;
								
														case 3:
																game.Reward = calculateReward (1);
																ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 1;
																game.winGame ();
																break;
								
														default:
																game.loseGame ();
																break;
														}
												} else {
														game.loseGame ();
												}
												break;
						
										case 34:
												if (playerCarController.numberCheckPoint >= 9) {
														switch (getOrder (0)) {
														case 1:
																game.Reward = calculateReward (3);
																ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 3;
																game.winGame ();
																break;
								
														case 2:
																game.Reward = calculateReward (2);
																ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 2;
																game.winGame ();
																break;
								
														case 3:
																game.Reward = calculateReward (1);
																ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 1;
																game.winGame ();
																break;
								
														default:
																game.loseGame ();
																break;
														}
												} else {
														game.loseGame ();
												}
												break;
						
										default:
												break;
										}
								}
								break;

						case GameData.GAME_MODE.TIME_TRIAL:
								if (game.DeltaTime >= GameData.duration) {
										game.Reward = 0;

										switch (GameData.level) {
										case 3:
												switch (getOrder (0)) {
												case 1:
														game.Reward = calculateReward (3);
														ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 3;
														break;
							
												case 2:
														game.Reward = calculateReward (2);
														ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 2;
														break;
							
												case 3:
														game.Reward = calculateReward (1);
														ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 1;
														break;
							
												default:
														break;
												}
												break;
						
										case 5:
												switch (getOrder (0)) {
												case 1:
														game.Reward = calculateReward (3);
														ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 3;
														break;
							
												case 2:
														game.Reward = calculateReward (2);
														ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 2;
														break;
							
												case 3:
														game.Reward = calculateReward (1);
														ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 1;
														break;
							
												default:
														break;
												}
												break;
						
										case 9:
												switch (getOrder (0)) {
												case 1:
														game.Reward = calculateReward (3);
														ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 3;
														break;
							
												case 2:
														game.Reward = calculateReward (2);
														ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 2;
														break;
							
												case 3:
														game.Reward = calculateReward (1);
														ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 1;
														break;
							
												default:
														break;
												}
												break;
						
										case 10:
												switch (getOrder (0)) {
												case 1:
														game.Reward = calculateReward (3);
														ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 3;
														break;
							
												case 2:
														game.Reward = calculateReward (2);
														ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 2;
														break;
							
												case 3:
														game.Reward = calculateReward (1);
														ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 1;
														break;
							
												default:
														break;
												}
												break;
						
										case 13:
												switch (getOrder (0)) {
												case 1:
														game.Reward = calculateReward (3);
														ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 3;
														break;
							
												case 2:
														game.Reward = calculateReward (2);
														ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 2;
														break;
							
												case 3:
														game.Reward = calculateReward (1);
														ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 1;
														break;
							
												default:
														break;
												}
												break;
						
										case 17:
												switch (getOrder (0)) {
												case 1:
														game.Reward = calculateReward (3);
														ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 3;
														break;
							
												case 2:
														game.Reward = calculateReward (2);
														ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 2;
														break;
							
												case 3:
														game.Reward = calculateReward (1);
														ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 1;
														break;
							
												default:
														break;
												}
												break;
						
										case 21:
												switch (getOrder (0)) {
												case 1:
														game.Reward = calculateReward (3);
														ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 3;
														break;
							
												case 2:
														game.Reward = calculateReward (2);
														ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 2;
														break;
							
												case 3:
														game.Reward = calculateReward (1);
														ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 1;
														break;
							
												default:
														break;
												}
												break;
						
										case 23:
												switch (getOrder (0)) {
												case 1:
														game.Reward = calculateReward (3);
														ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 3;
														break;
							
												case 2:
														game.Reward = calculateReward (2);
														ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 2;
														break;
							
												case 3:
														game.Reward = calculateReward (1);
														ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 1;
														break;
							
												default:
														break;
												}
												break;
						
										case 27:
												switch (getOrder (0)) {
												case 1:
														game.Reward = calculateReward (3);
														ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 3;
														break;
							
												case 2:
														game.Reward = calculateReward (2);
														ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 2;
														break;
							
												case 3:
														game.Reward = calculateReward (1);
														ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 1;
														break;
							
												default:
														break;
												}
												break;
						
										case 28:
												switch (getOrder (0)) {
												case 1:
														game.Reward = calculateReward (3);
														ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 3;
														break;
							
												case 2:
														game.Reward = calculateReward (2);
														ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 2;
														break;
							
												case 3:
														game.Reward = calculateReward (1);
														ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 1;
														break;
							
												default:
														break;
												}
												break;
						
										case 31:
												switch (getOrder (0)) {
												case 1:
														game.Reward = calculateReward (3);
														ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 3;
														break;
							
												case 2:
														game.Reward = calculateReward (2);
														ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 2;
														break;
							
												case 3:
														game.Reward = calculateReward (1);
														ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 1;
														break;
							
												default:
														break;
												}
												break;
						
										case 35:
												switch (getOrder (0)) {
												case 1:
														game.Reward = calculateReward (3);
														ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 3;
														break;
							
												case 2:
														game.Reward = calculateReward (2);
														ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 2;
														break;
							
												case 3:
														game.Reward = calculateReward (1);
														ProfileManager.userProfile.MapProfile [GameData.level].LastReward = 1;
														break;
							
												default:
														break;
												}
												break;
						
										default:
												break;
										}

										if (getOrder (0) > 3) {
												game.loseGame ();
										} else {
												game.winGame ();
										}
								}
								break;

						default:
								break;
						}
				} else {
						switch (GameData.selectedMode) {
						case GameData.GAME_MODE.NORMAL:
								if (carInfo [0].NumberRaces <= -1) {
										game.Reward = 0;

										switch (getOrder (0)) {
										case 1:
												game.Reward = 800;
												game.winGame ();
												break;

										case 2:
												game.Reward = 700;
												game.winGame ();
												break;

										case 3:
												game.Reward = 600;
												game.winGame ();
												break;

										default:
												game.loseGame ();
												break;
										}
								}
								break;
				
						case GameData.GAME_MODE.CHECK_POINT:
								if (game.DeltaTime >= GameData.duration) {
										game.Reward = 0;					

										if (playerCarController.numberCheckPoint >= GameData.numberCheckpoints) {
												switch (getOrder (0)) {
												case 1:
														game.Reward = 800;
														game.winGame ();
														break;
						
												case 2:
														game.Reward = 700;
														game.winGame ();
														break;
						
												case 3:
														game.Reward = 600;
														game.winGame ();
														break;
						
												default:
														game.loseGame ();
														break;
												}
										} else {
												game.loseGame ();
										}
								}
								break;
				
						case GameData.GAME_MODE.TIME_TRIAL:
								if (game.DeltaTime >= GameData.duration) {
										game.Reward = 0;					

										switch (getOrder (0)) {
										case 1:
												game.Reward = 800;
												game.winGame ();
												break;
						
										case 2:
												game.Reward = 700;
												game.winGame ();
												break;
						
										case 3:
												game.Reward = 600;
												game.winGame ();
												break;
						
										default:
												game.loseGame ();
												break;
										}
								}
								break;
				
						default:
								break;
						}
				}
		}

		public int calculateReward (int star)
		{
				int reward = 0;

				switch (GameData.selectedMap) {
				case GameData.MAP_NAME.TOKYO:
						switch (star) {
						case 3:
								reward += 1500;
								break;
						case 2:
								reward += 1000;
								break;
						case 1:
								reward += 500;
								break;
						default:
								break;
						}
						break;

				case GameData.MAP_NAME.HANOI:
						switch (star) {
						case 3:
								reward += 1000;
								break;				
						case 2:
								reward += 500;
								break;				
						case 1:
								reward += 200;
								break;				
						default:
								break;
						}
						break;

				case GameData.MAP_NAME.NEWYORK:
						switch (star) {
						case 3:
								reward += 1500;
								break;				
						case 2:
								reward += 1000;
								break;				
						case 1:
								reward += 500;
								break;				
						default:
								break;
						}
						break;

				case GameData.MAP_NAME.NEVADA:
						switch (star) {
						case 3:
								reward += 2500;
								break;				
						case 2:
								reward += 1000;
								break;				
						case 1:
								reward += 500;
								break;				
						default:
								break;
						}
						break;

				case GameData.MAP_NAME.CHINA:
						switch (star) {
						case 3:
								reward += 2000;
								break;				
						case 2:
								reward += 1000;
								break;				
						case 1:
								reward += 500;
								break;				
						default:
								break;
						}
						break;

				case GameData.MAP_NAME.PARIS:
						switch (star) {
						case 3:
								reward += 2500;
								break;				
						case 2:
								reward += 1000;
								break;				
						case 1:
								reward += 500;
								break;				
						default:
								break;
						}
						break;

				default:
						break;
				}

				switch (GameData.selectedMode) {
				case GameData.GAME_MODE.NORMAL:
						switch (star) {
						case 3:
								reward += 200;
								break;				
						case 2:
								reward += 100;
								break;				
						case 1:
								reward += 50;
								break;				
						default:
								break;
						}
						break;

				case GameData.GAME_MODE.TIME_TRIAL:
						switch (star) {
						case 3:
								reward += 1000;
								break;				
						case 2:
								reward += 500;
								break;				
						case 1:
								reward += 200;
								break;				
						default:
								break;
						}
						break;

				case GameData.GAME_MODE.CHECK_POINT:
						switch (star) {
						case 3:
								reward += 500;
								break;				
						case 2:
								reward += 200;
								break;				
						case 1:
								reward += 20;
								break;				
						default:
								break;
						}
						break;
				default:
						break;
				}

				return reward;
		}
}
