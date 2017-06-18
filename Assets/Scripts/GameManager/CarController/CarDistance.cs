using UnityEngine;
using System.Collections;

public class CarDistance
{
		int id;

		public int ID {
				get {
						return id;
				}
				set {
						id = value;
				}
		}

		float distance;

		public float Distance {
				get {
						return distance;
				}
				set {
						distance = value;
				}
		}

		public CarDistance (int id)
		{
				this.id = id;
		}
}
