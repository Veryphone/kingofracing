using UnityEngine;
using System.Collections;

public class PathChangingController : MonoBehaviour
{
		public int mainPathID;
		public int subPathID;
		public int inWaypointID;
		public int outWaypointID;
		//
		Game game;
		Vector3 direction;

		void Start ()
		{
				game = GameObject.Find ("GameManager").GetComponent<Game> ();

				Path subPath = game.map.pathList [subPathID];

				if (inWaypointID < outWaypointID) {
						direction = subPath.waypointList [0].waypointPos - subPath.waypointList [1].waypointPos;
				} else {
						direction = subPath.waypointList [subPath.waypointList.Length - 1].waypointPos 
								- subPath.waypointList [subPath.waypointList.Length - 2].waypointPos;
				}
		}
	
		void OnTriggerExit (Collider other)
		{
				if (other.transform.root.gameObject.layer == 11) {
						if (Vector3.Dot (other.transform.root.forward, direction) < 0)
								game.carManager.changePath (true, this);
						else {
								game.carManager.changePath (false, this);
						}
				} 
		}
}
