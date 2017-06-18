using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
		/*
	This camera smoothes out rotation around the y-axis and height.
	Horizontal Distance to the target is always fixed.
	
	There are many different ways to smooth the rotation but doing it this way gives you a lot of control over how the camera behaves.
	
	For every of those smoothed values we calculate the wanted value and the current value.
	Then we smooth it using the Lerp function.
	Then we apply the smoothed values to the transform's position.
	*/

		public Game game;
		public Path path;
		public float speed = 1;
		int currentWaypoint = 0;
		Vector3 velocity;

		// The target we are following
		public Transform target;
		// The distance in the x-z plane to the target
		public float distance = 5.0f;
		// the height we want the camera to be above the target
		public float height = 1f;
		// How much we 
		private float heightDamping = 2.0f;
		private float rotationDamping = 3.0f;
		//
		private float wantedRotationAngle;
		private	float wantedHeight;
		private float currentRotationAngle;
		private	float currentHeight;

		void Start ()
		{
				if (path != null && path.isValidWaypointIndex (currentWaypoint)) {
						this.transform.position = path.waypointList [currentWaypoint].waypointPos;
				} 		
		}

		void LateUpdate ()
		{
				if (Game.GameState == Game.GAME_STATE.START) {
						if (path != null) {
								if (path.isReachWaypoint (transform.position, currentWaypoint)) {						
										currentWaypoint = (currentWaypoint + 1) % path.waypointList.Length;								
								} else {
										if (path.isValidWaypointIndex (currentWaypoint)) {
												velocity = (path.waypointList [currentWaypoint].waypointPos -
														transform.position).normalized * speed * Time.deltaTime;
						
												transform.position += velocity; 
												transform.LookAt (game.carManager.getCameraPathTarget ());
										}
								}
						}
				} else {
						// Early out if we don't have a target
						if (!target)
								return;
		
						// Calculate the current rotation angles
						wantedRotationAngle = target.eulerAngles.y;
						wantedHeight = target.position.y + height;
			
						currentRotationAngle = transform.eulerAngles.y;
						currentHeight = transform.position.y;
		
						// Damp the rotation around the y-axis
						currentRotationAngle = Mathf.LerpAngle (currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);//Time.GetMyDeltaTime());
		
						// Damp the height
						currentHeight = Mathf.Lerp (currentHeight, wantedHeight, heightDamping * Time.deltaTime);//Time.GetMyDeltaTime());
						//System.Console.WriteLine("dt: {0}", dt);//Time.GetMyDeltaTime());
						//Debug.Log("dt: " + Time.deltaTime);
		
						// Convert the angle into a rotation
						Quaternion currentRotation = Quaternion.Euler (0, currentRotationAngle, 0);
		
						// Set the position of the camera on the x-z plane to:
						// distance meters behind the target
						Vector3 pos = target.position - currentRotation * Vector3.forward * distance;
						pos.y += 1;
		
						// Set the height of the camera
						transform.position = pos;
		
						// Always look at the target
						transform.LookAt (target);

						if (Time.timeScale == 1) {
								if (target.GetComponent<Rigidbody>().velocity.magnitude > 20) {
										transform.position += Random.insideUnitSphere * 0.0003f * target.GetComponent<Rigidbody>().velocity.magnitude;
								}	
						}
				}
		}

		public void setTarget (Transform target)
		{
				this.target = target;
				this.transform.position = target.transform.position;
		}
}
