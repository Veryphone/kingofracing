using UnityEngine;
using System.Collections;

public abstract class BaseCarController
{
		public CarData carData;

		public CarData CarData {
				get {
						return carData;
				}
		}

		public BaseCarController (CarData carData)
		{
				this.carData = carData;
		}

		public abstract void FixedUpdate ();
	
		public abstract void Update ();

		public abstract bool isPlayer ();
}
