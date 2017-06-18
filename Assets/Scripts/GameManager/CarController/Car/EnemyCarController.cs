using UnityEngine;
using System.Collections;

public class EnemyCarController : BaseCarController
{
		public Game game;
		public int currentWaypoint;
		public int currentID;
		//
		public UnityEngine.AI.NavMeshAgent agent;
		int destinationIndex;

		public EnemyCarController (CarData carData):base(carData)
		{
				game = GameObject.Find ("GameManager").GetComponent<Game> ();
				destinationIndex = 1;
		}

		public override void Update ()
		{
		}

		public override void FixedUpdate ()
		{
				switch (destinationIndex) {
				case 1:
						if (Vector3.Distance (carData.transform.position, game.map.destinationList [0].position) < 30) {
								agent.SetDestination (game.map.destinationList [1].position);
								destinationIndex = 2;
						}
						break;
			
				case 2:
						if (Vector3.Distance (carData.transform.position, game.map.destinationList [1].position) < 30) {
								agent.SetDestination (game.map.destinationList [2].position);
								destinationIndex = 3;
						}
						break;
			
				case 3:
						if (Vector3.Distance (carData.transform.position, game.map.destinationList [2].position) < 30) {
								agent.SetDestination (game.map.destinationList [0].position);
								destinationIndex = 1;
						}
						break;
				}
		}

		public override bool isPlayer ()
		{
				return false;
		}

		public void setOrder (int order)
		{
				carData.carInfo.text = order + string.Empty;
		}

		public void setAgent (UnityEngine.AI.NavMeshAgent agent)
		{
				this.agent = agent;				
		}

		public void startCar ()
		{
				this.agent.SetDestination (game.map.destinationList [0].position);
		}
}
