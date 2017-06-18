using UnityEngine;
using System.Collections;

public class WaypointAgent : PathfindingAgent
{
		public Path path;
		//
		public float speed = 1;
		public int destinationWaypoint = 0;
		public int currentWaypoint = 0;
		public bool isLooping = true;
		//
		Vector3 velocity;

		void Start ()
		{
				if (path != null && path.isValidWaypointIndex (currentWaypoint)) {
						this.transform.position = path.waypointList [currentWaypoint].waypointPos;
				} else {
						if (path == null) {
								Debug.Log ("The WaypointAgent of \"" + this.name + "\" doesn't have any Path.\n" +
										"- You need to create a new Path, then drag and drop into WaypointAgent Path field.\n" +
										"- To create Path, you have to select this GameObject and find: " +
										"AI Extension -> Pathfinding -> Waypoint -> Create Path");
						} else {
								Debug.Log ("The Path in WaypointAgent of \"" + this.name + "\" don't have any Waypoint. " +
										"You can add more waypoints by select Path, and edit components of that Path");
						}
				}
		}

		void Update ()
		{
				if (path != null) {
						if (isLooping) {
								if (path.isReachWaypoint (transform.position, currentWaypoint)) {

										currentWaypoint = (currentWaypoint + 1) % path.waypointList.Length;								
								} else {
										if (path.isValidWaypointIndex (currentWaypoint)) {
												velocity = (path.waypointList [currentWaypoint].waypointPos -
														transform.position).normalized * speed * Time.deltaTime;

												transform.position += velocity; 
												transform.rotation = Quaternion.LookRotation (velocity);
										}
								}
						} else {
								if (currentWaypoint != destinationWaypoint) { 
										if (path.isReachWaypoint (transform.position, currentWaypoint)) {

												currentWaypoint = (currentWaypoint + 1) % path.waypointList.Length;
										} else {
												if (path.isValidWaypointIndex (currentWaypoint)) {
														velocity = (path.waypointList [currentWaypoint].waypointPos -
																transform.position).normalized * speed * Time.deltaTime;

														transform.position += velocity; 
														transform.rotation = Quaternion.LookRotation (velocity);
												}
										}
								}
						}
				}
		}	
}
