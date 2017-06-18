using UnityEngine;
using System.Collections;

[System.Serializable]
public class Waypoint
{
		public bool useDefault = true;
		public float radius = 1;
		public Vector3 waypointPos = Vector3.zero;

		public bool isReachWaypoint (Vector3 currentPos)
		{
				return this.isReachWaypoint (currentPos, radius);
		}

		public bool isReachWaypoint (Vector3 currentPos, float r)
		{
				if (Vector3.Distance (currentPos, waypointPos) <= r) {
						return true;
				} else {
						return false;
				}
		}
}
