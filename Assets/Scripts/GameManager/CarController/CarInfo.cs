using UnityEngine;
using System.Collections;

public class CarInfo
{
	Game game;
	Transform car;
	//
	int id;
	int numberRaces;

	public int NumberRaces {
		get {
			return numberRaces;
		}
	}

	//
	Vector3 direction;

	public Vector3 Direction {
		get {
			return direction;
		}
	}

	float remainingDistance;

	public float RemainingDistance {
		get {
			return remainingDistance;
		}
	}

	int nextWaypoint;

	public int NextWaypoint {
		get {
			return nextWaypoint;
		}
		set {
			nextWaypoint = value;
		}
	}

	//
	Path[] pathList;
	PathChangingController pathChanger;

	public CarInfo (Game game, Transform car, int ID)
	{
		this.game = game;
		this.car = car;
		
		this.id = ID;
		this.remainingDistance = 0;
		this.nextWaypoint = 0;
		this.numberRaces = GameData.numberRaces;

		this.pathList = game.map.pathList;
	}

	public void Update ()
	{
		if (id == 0) {
			if (game.carManager.playerCarController.pathChanger != null) {

				pathChanger = game.carManager.playerCarController.pathChanger;

				if (pathList [pathChanger.subPathID].isReachWaypoint (car.transform.position, nextWaypoint)) {
					nextWaypoint = pathList [pathChanger.subPathID].getNextWaypoint (nextWaypoint);
				}

				remainingDistance = Vector3.Distance (car.transform.position,
				                                      pathList [pathChanger.subPathID].waypointList [nextWaypoint].waypointPos)
					+ pathList [pathChanger.subPathID].getDistance (nextWaypoint, 
					                                                pathList [pathChanger.subPathID].waypointList.Length - 1, 0)
					+ pathList [0].getDistance (pathChanger.outWaypointID, 0, numberRaces);
				
				direction = pathList [pathChanger.subPathID].waypointList [nextWaypoint].waypointPos - 
					pathList [pathChanger.subPathID].waypointList [pathList [pathChanger.subPathID].getPreviousWaypoint (nextWaypoint)].waypointPos;

			} else {
				if (pathList [0].isReachWaypoint (car.transform.position, nextWaypoint)) {
					if (nextWaypoint == 0) {
						numberRaces--;
						game.map.resetCheckPoint ();
					}
				
					nextWaypoint = pathList [0].getNextWaypoint (nextWaypoint);
				}
						
				remainingDistance = Vector3.Distance (car.transform.position,
				                                      pathList [0].waypointList [nextWaypoint].waypointPos)
					+ pathList [0].getDistance (nextWaypoint, 0, numberRaces);

				direction = pathList [0].waypointList [nextWaypoint].waypointPos - 
					pathList [0].waypointList [pathList [0].getPreviousWaypoint (nextWaypoint)].waypointPos;
			}
		} else {
			if (pathList [0].isReachWaypoint (car.transform.position, nextWaypoint)) {
				if (nextWaypoint == 0) {
					numberRaces--;
				}
				
				nextWaypoint = pathList [0].getNextWaypoint (nextWaypoint);
			}
			
			remainingDistance = Vector3.Distance (car.transform.position,
			                                      pathList [0].waypointList [nextWaypoint].waypointPos)
				+ pathList [0].getDistance (nextWaypoint, 0, numberRaces);
		}
	}

	public Vector3 getNextWaypointPos ()
	{
		return pathList [0].waypointList [nextWaypoint].waypointPos;
	}
	
	public Vector3 getCurrentWaypointPos ()
	{
		return pathList [0].waypointList [pathList [0].getPreviousWaypoint (nextWaypoint)].waypointPos;
	}

	public bool isWrongWay ()
	{
		if (Vector3.Dot (direction.normalized, car.transform.forward.normalized) < 0) {
			return true;
		} else {
			return false;
		}
	}
}
