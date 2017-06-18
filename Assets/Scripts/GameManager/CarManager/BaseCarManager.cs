using UnityEngine;
using System;
using System.Collections;

public abstract class BaseCarManager
{
		protected Game game;
		//
		public GameObject[] carList;
		public CarInfo[] carInfo;
		public CarDistance[] carDistance;
		//
		public PlayerCarController playerCarController;
		public EnemyCarController[] enemyCarController;
		public bool isBrake;

		public BaseCarManager (Game game)
		{
				this.game = game;
		}

		public static string getCarPrefab (GameData.CAR_NAME carName)
		{
				switch (carName) {
				case GameData.CAR_NAME.BENTLEY_V8:
						return "Prefabs/Cars/Bentley_V8";
			
				case GameData.CAR_NAME.ASTON_MARTIN_DB9:
						return "Prefabs/Cars/Aston_Martin_DB9";
			
				case GameData.CAR_NAME.MASERATI_GRAN_TURISMO:
						return "Prefabs/Cars/Maserati_GranTurismo";
			
				case GameData.CAR_NAME.MARUSSIA_B2:
						return "Prefabs/Cars/Marussia_B2";
			
				case GameData.CAR_NAME.MERCEDES_BENZ_SLS:
						return "Prefabs/Cars/Mercedes_Benz_SLS";
			
				case GameData.CAR_NAME.AUDI_R8:
						return "Prefabs/Cars/Audi_R8";
			
				case GameData.CAR_NAME.FERRARI_458_ITALIA:
						return "Prefabs/Cars/Ferrari_458";
			
				case GameData.CAR_NAME.PORSCHE_911:
						return "Prefabs/Cars/Porsche_911";
			
				case GameData.CAR_NAME.LAMBORHINI_LP560:
						return "Prefabs/Cars/Lamborghini_LP560";
			
				case GameData.CAR_NAME.LAMBORGHINI_VENENO:
						return "Prefabs/Cars/Lamborghini_Veneno";
            case GameData.CAR_NAME.OTO:
                return "Prefabs/Cars/OTO";

            default:
						return "Prefabs/Cars/Bentley_V8";
				}
		}

		protected void initCar ()
		{
       
				ProfileManager.userProfile.calculateCarUnlocked ();

				GameData.CAR_NAME[] carNames = (GameData.CAR_NAME[])Enum.GetValues (typeof(GameData.CAR_NAME));
				carList = new GameObject[GameData.numberEnemies + 1];
                //ProfileManager.userProfile.SelectedCar = 10;
        Debug.Log("init Car_________________________________"+ ProfileManager.userProfile.SelectedCar);
        if (GameData.isFirstRacer == true) {
            Debug.Log("init Car_________________________________isFirstRacer");
            carList [0] = (GameObject)GameObject.Instantiate 
				(Resources.Load (getCarPrefab (
//													GameData.CAR_NAME.LAMBORGHINI_VENENO)),
					carNames [ProfileManager.userProfile.SelectedCar])),
				 game.map.spawnPointsList [5].transform.position, game.map.spawnPointsList [0].transform.rotation);
				} else {
            Debug.Log("init Car_________________________________isFirstRacer_false");
            carList [0] = (GameObject)GameObject.Instantiate 
				(Resources.Load (getCarPrefab (
					//								GameData.CAR_NAME.AUDI_R8)),
					carNames [ProfileManager.userProfile.SelectedCar])),
				 game.map.spawnPointsList [0].transform.position, game.map.spawnPointsList [0].transform.rotation);
				}
        Debug.Log(carList[0].name+"_____________________________________");
				carList [0].name = "Player";
				carList [0].layer = 11;
				carList [0].SendMessage ("initCar", true);
				playerCarController = (carList [0].GetComponent<CarData> ()).carController as PlayerCarController;					

				if (GameData.selectedMap == GameData.MAP_NAME.HANOI || GameData.selectedMap == GameData.MAP_NAME.TOKYO || GameData.selectedMap == GameData.MAP_NAME.CHINA) {
						carList [0].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
				} else {
						carList [0].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
				}

				UnityEngine.AI.NavMeshObstacle obstacle = carList [0].AddComponent<UnityEngine.AI.NavMeshObstacle> ();
				obstacle.radius = 2f;
				obstacle.height = 3;

				//

				enemyCarController = new EnemyCarController[GameData.numberEnemies + 1];
				ID_Pool iDPool = new ID_Pool (10);
		
				for (int i=1; i<carList.Length; i++) {
						GameData.CAR_NAME carName = carNames [iDPool.allocateID ()];

						if (GameData.isFirstRacer == true) {
								carList [i] = (GameObject)GameObject.Instantiate (Resources.Load (getCarPrefab (carName)),
				 game.map.spawnPointsList [i - 1].transform.position, game.map.spawnPointsList [i].transform.rotation);
						} else {
								carList [i] = (GameObject)GameObject.Instantiate (Resources.Load (getCarPrefab (carName)),
				                                                  game.map.spawnPointsList [i].transform.position, game.map.spawnPointsList [i].transform.rotation);
						}

						carList [i].name = "Enemy_" + i;
						carList [i].layer = 10;
						carList [i].SendMessage ("initCar", false);

						GameObject.Destroy (carList [i].GetComponent<Rigidbody> ());
						carList [i].GetComponent<CarData> ().wheelFL.enabled = false;
						carList [i].GetComponent<CarData> ().wheelFR.enabled = false;
						carList [i].GetComponent<CarData> ().wheelRL.enabled = false;
						carList [i].GetComponent<CarData> ().wheelRR.enabled = false;
			
						UnityEngine.AI.NavMeshAgent agent = carList [i].AddComponent<UnityEngine.AI.NavMeshAgent> ();
						this.initEnemyDifficulty (agent, i);

						agent.angularSpeed = 720;
						agent.autoBraking = false;
						agent.autoTraverseOffMeshLink = false;
						agent.radius = 2.5f;
						agent.height = 3;

						if (carName == GameData.CAR_NAME.MASERATI_GRAN_TURISMO || carName == GameData.CAR_NAME.PORSCHE_911) {
								agent.baseOffset = 0.6f;
						} 
			
						enemyCarController [i] = (carList [i].GetComponent<CarData> ()).carController as EnemyCarController;
						enemyCarController [i].setAgent (agent);
				}
		}
	
		protected void initCarInfo ()
		{
				carInfo = new CarInfo[carList.Length];
				carDistance = new CarDistance[carList.Length];
		
				for (int i=0; i<carInfo.Length; i++) {
						carInfo [i] = new CarInfo (game, carList [i].transform, i);
						carDistance [i] = new CarDistance (i);
				}
		}
	
		public void startCar ()
		{
				game.carManager.playerCarController.UpdateTorque (false);
				game.carManager.isBrake = false;
		
				for (int i=1; i<carList.Length; i++) {
						enemyCarController [i].startCar ();
				}
		}

		public Transform getCameraPathTarget ()
		{
				return carList [1].transform;
		}

		public void setTarget ()
		{
				Camera.main.SendMessage ("setTarget", carList [0].transform);
		}
	
		public void respawnPlayerCar ()
		{
				playerCarController.stuckTime = 0;
				playerCarController.isStuck = false;

				Vector3 currentPos = carInfo [0].getCurrentWaypointPos ();
				Vector3 nextPos = carInfo [0].getNextWaypointPos ();
		
				carList [0].transform.position = new Vector3 ((currentPos.x + nextPos.x) / 2,
		                                              currentPos.y + 2, (currentPos.z + nextPos.z) / 2);
				carList [0].transform.rotation = Quaternion.LookRotation (nextPos - currentPos);
				carList [0].GetComponent<Rigidbody>().velocity = Vector3.zero;
		}

		void initEnemyDifficulty (UnityEngine.AI.NavMeshAgent agent, int id)
		{
				switch (GameData.level) {
				case 0:
						agent.speed = 80 - (id - 1) * 7 - UnityEngine.Random.Range (1, 20);
						agent.acceleration = UnityEngine.Random.Range (5, 65);
						break;
				
				case 1:
						agent.speed = 80 - (id - 1) * 7 - UnityEngine.Random.Range (1, 20);
						agent.acceleration = UnityEngine.Random.Range (5, 65);
						break;
				
				case 2:
						agent.speed = 85 - (id - 1) * 7 - UnityEngine.Random.Range (1, 20);
						agent.acceleration = UnityEngine.Random.Range (5, 65);
						break;
				
				case 3:
						agent.speed = 85 - (id - 1) * 7 - UnityEngine.Random.Range (1, 20);
						agent.acceleration = UnityEngine.Random.Range (5, 65);
						break;
				
				case 4:
						agent.speed = 90 - (id - 1) * 7 - UnityEngine.Random.Range (1, 20);
						agent.acceleration = UnityEngine.Random.Range (5, 70);
						break;
				
				case 5:
						agent.speed = 90 - (id - 1) * 7 - UnityEngine.Random.Range (1, 20);
						agent.acceleration = UnityEngine.Random.Range (5, 70);
						break;
				
				case 6:
						agent.speed = 95 - (id - 1) * 7 - UnityEngine.Random.Range (1, 20);
						agent.acceleration = UnityEngine.Random.Range (5, 70);
						break;
				
				case 7:
						agent.speed = 95 - (id - 1) * 7 - UnityEngine.Random.Range (1, 20);
						agent.acceleration = UnityEngine.Random.Range (5, 70);
						break;
				
				case 8:
						agent.speed = 100 - (id - 1) * 7 - UnityEngine.Random.Range (1, 20);
						agent.acceleration = UnityEngine.Random.Range (5, 75);
						break;
				
				case 9:
						agent.speed = 100 - (id - 1) * 7 - UnityEngine.Random.Range (1, 20);
						agent.acceleration = UnityEngine.Random.Range (5, 75);
						break;
				
				case 10:
						agent.speed = 105 - (id - 1) * 7 - UnityEngine.Random.Range (1, 20);
						agent.acceleration = UnityEngine.Random.Range (5, 75);
						break;
				
				case 11:
						agent.speed = 105 - (id - 1) * 7 - UnityEngine.Random.Range (1, 20);
						agent.acceleration = UnityEngine.Random.Range (5, 75);
						break;
				
				case 12:
						agent.speed = 110 - (id - 1) * 7 - UnityEngine.Random.Range (1, 20);
						agent.acceleration = UnityEngine.Random.Range (5, 80);
						break;
				
				case 13:
						agent.speed = 110 - (id - 1) * 7 - UnityEngine.Random.Range (1, 20);
						agent.acceleration = UnityEngine.Random.Range (5, 80);
						break;
				
				case 14:
						agent.speed = 115 - (id - 1) * 7 - UnityEngine.Random.Range (1, 20);
						agent.acceleration = UnityEngine.Random.Range (5, 80);
						break;
				
				case 15:
						agent.speed = 115 - (id - 1) * 7 - UnityEngine.Random.Range (1, 20);
						agent.acceleration = UnityEngine.Random.Range (5, 80);
						break;
				
				case 16:
						agent.speed = 120 - (id - 1) * 7 - UnityEngine.Random.Range (1, 20);
						agent.acceleration = UnityEngine.Random.Range (5, 85);
						break;
				
				case 17:
						agent.speed = 120 - (id - 1) * 7 - UnityEngine.Random.Range (1, 20);
						agent.acceleration = UnityEngine.Random.Range (5, 85);
						break;
				
				case 18:
						agent.speed = 125 - (id - 1) * 7 - UnityEngine.Random.Range (1, 20);
						agent.acceleration = UnityEngine.Random.Range (5, 85);
						break;
				
				case 19:
						agent.speed = 125 - (id - 1) * 7 - UnityEngine.Random.Range (1, 20);
						agent.acceleration = UnityEngine.Random.Range (5, 85);
						break;
				
				case 20:
						agent.speed = 130 - (id - 1) * 7 - UnityEngine.Random.Range (1, 20);
						agent.acceleration = UnityEngine.Random.Range (5, 90);
						break;
				
				case 21:
						agent.speed = 130 - (id - 1) * 7 - UnityEngine.Random.Range (1, 20);
						agent.acceleration = UnityEngine.Random.Range (5, 90);
						break;
				
				case 22:
						agent.speed = 135 - (id - 1) * 7 - UnityEngine.Random.Range (1, 20);
						agent.acceleration = UnityEngine.Random.Range (5, 90);
						break;
				
				case 23:
						agent.speed = 135 - (id - 1) * 7 - UnityEngine.Random.Range (1, 20);
						agent.acceleration = UnityEngine.Random.Range (5, 90);
						break;
				
				case 24:
						agent.speed = 140 - (id - 1) * 7 - UnityEngine.Random.Range (1, 20);
						agent.acceleration = UnityEngine.Random.Range (5, 95);
						break;
				
				case 25:
						agent.speed = 140 - (id - 1) * 7 - UnityEngine.Random.Range (1, 20);
						agent.acceleration = UnityEngine.Random.Range (5, 95);
						break;
				
				case 26:
						agent.speed = 145 - (id - 1) * 7 - UnityEngine.Random.Range (1, 20);
						agent.acceleration = UnityEngine.Random.Range (5, 95);
						break;
				
				case 27:
						agent.speed = 145 - (id - 1) * 7 - UnityEngine.Random.Range (1, 20);
						agent.acceleration = UnityEngine.Random.Range (5, 95);
						break;
				
				case 28:
						agent.speed = 150 - (id - 1) * 7 - UnityEngine.Random.Range (1, 20);
						agent.acceleration = UnityEngine.Random.Range (5, 100);
						break;
				
				case 29:
						agent.speed = 150 - (id - 1) * 7 - UnityEngine.Random.Range (1, 20);
						agent.acceleration = UnityEngine.Random.Range (5, 100);
						break;
				
				case 30:
						agent.speed = 155 - (id - 1) * 7 - UnityEngine.Random.Range (1, 20);
						agent.acceleration = UnityEngine.Random.Range (5, 100);
						break;
				
				case 31:
						agent.speed = 155 - (id - 1) * 7 - UnityEngine.Random.Range (1, 20);
						agent.acceleration = UnityEngine.Random.Range (5, 100);
						break;
				
				case 32:
						agent.speed = 160 - (id - 1) * 7 - UnityEngine.Random.Range (1, 20);
						agent.acceleration = UnityEngine.Random.Range (5, 105);
						break;
				
				case 33:
						agent.speed = 160 - (id - 1) * 7 - UnityEngine.Random.Range (1, 20);
						agent.acceleration = UnityEngine.Random.Range (5, 105);
						break;
				
				case 34:
						agent.speed = 165 - (id - 1) * 7 - UnityEngine.Random.Range (1, 20);
						agent.acceleration = UnityEngine.Random.Range (5, 105);
						break;
				
				case 35:
						agent.speed = 170 - (id - 1) * 7 - UnityEngine.Random.Range (1, 20);
						agent.acceleration = UnityEngine.Random.Range (5, 105);
						break;
				
				default:
						int maxAgentSpeed = 70;
						switch (ProfileManager.userProfile.SelectedCar) {
						case 0:
								maxAgentSpeed = 70;
								break;

						case 1:
								maxAgentSpeed = 80;
								break;

						case 2:
								maxAgentSpeed = 90;
								break;

						case 3:
								maxAgentSpeed = 100;
								break;

						case 4:
								maxAgentSpeed = 110;
								break;

						case 5:
								maxAgentSpeed = 120;
								break;

						case 6:
								maxAgentSpeed = 130;
								break;

						case 7:
								maxAgentSpeed = 140;
								break;

						case 8:
								maxAgentSpeed = 150;
								break;

						case 9:
								maxAgentSpeed = 160;
								break;

						default:
								maxAgentSpeed = 70;
								break;
						}
						agent.speed = maxAgentSpeed - (id - 1) * 7 - UnityEngine.Random.Range (1, 20);
						agent.acceleration = UnityEngine.Random.Range (5, 105);
						break;
				}
		}
}
