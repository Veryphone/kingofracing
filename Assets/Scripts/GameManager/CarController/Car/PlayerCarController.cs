using UnityEngine;
using System.Collections;

public class PlayerCarController:BaseCarController
{
		public static float SENTIVITY = 0.5f;
		//
		public Game game;
		public PathChangingController pathChanger;
		public int dollar;
		public int numberCheckPoint;
		//
		public bool isNitroUsed;
		//
		public float stuckTime;
		public bool isStuck;
		public bool isCrash = false;
	
		public PlayerCarController (CarData carData):base(carData)
		{
				this.dollar = 0;
				this.numberCheckPoint = 0;

				this.game = GameObject.Find ("GameManager").GetComponent<Game> ();
				SENTIVITY = 0.5f - 0.5f * ProfileManager.setttings.Sensitivity / 100f;
		}
	
		public override void FixedUpdate ()
		{
				// Angle left and right Fwheel
				carData.wheelFLtrans.localEulerAngles = new Vector3 (carData.wheelFLtrans.localEulerAngles.x, 
		                                                     carData.wheelFL.steerAngle - carData.wheelFLtrans.localEulerAngles.z, carData.wheelFLtrans.localEulerAngles.z);
				carData.wheelFRtrans.localEulerAngles = new Vector3 (carData.wheelFRtrans.localEulerAngles.x, 
		                                                     carData.wheelFR.steerAngle - carData.wheelFRtrans.localEulerAngles.z, carData.wheelFRtrans.localEulerAngles.z);
		
				carData.controlCar ();
		}
	
		public override void Update ()
		{
				if (Application.isEditor == true) {
						if (Input.GetAxis ("Horizontal") >= SENTIVITY || Input.GetAxis ("Horizontal") <= -SENTIVITY) {
								carData.setAxis (Input.GetAxis ("Horizontal"));
						} else { 
								carData.setAxis (0);
						}
				} else {
						if (Input.acceleration.x >= SENTIVITY || Input.acceleration.x <= -SENTIVITY) {
								carData.setAxis (Input.acceleration.x);
						} else { 
								carData.setAxis (0);
						}
				}

				carData.NitroBar = carData.NitroBar + Time.deltaTime;
		
				if (carData.IsNitroUsing == true) {
						carData.NitroBar = carData.NitroBar - Time.deltaTime * carData.NitroRate;
			
						if (carData.NitroBar <= 0) {
								carData.activateNitro (false);
						}
				}

				if ((this.carData.CurrentSpeed * 3) >= 300) {
						ProfileManager.achievementProfile.IsReachTopSpeed = true;
				}

				if (Mathf.Abs (carData.GetComponent<Rigidbody>().velocity.x) < 10f && Mathf.Abs (carData.GetComponent<Rigidbody>().velocity.y) < 10f 
						&& Mathf.Abs (carData.GetComponent<Rigidbody>().velocity.z) < 10f) {

						stuckTime += Time.deltaTime;

				} else {
						stuckTime = 0;
						this.isStuck = false;
				}

				if (stuckTime > 2) {
						this.isStuck = true;
				}
		}
	
		public override bool isPlayer ()
		{
				return true;
		}
	
		public void UpdateTorque (bool isBrake)
		{
				if (isBrake == true) {
						game.soundManager.playBrake ();
						carData.runningBackward ();
				} else {
						carData.runningForward ();			
				}
		}
	
		public void processItem (Collider item)
		{
				if (item.name == "BlueNitro") {
						game.soundManager.playItem ();
						carData.NitroBar += 10;
						GameObject.Destroy (item.transform.gameObject);

				} else if (item.name == "RedNitro") {
						game.soundManager.playItem ();
						carData.NitroBar += 30;
						GameObject.Destroy (item.transform.gameObject);

				} else if (item.name == "Dollar") {
						game.soundManager.playItem ();
						if (GameData.isDoubleReward == true) {
								dollar += 50;
						} else {
								dollar += 100;
						}

						GameObject.Destroy (item.transform.gameObject);
				}
		}

		public void reachCheckPoint (Collider checkPoint)
		{
				game.soundManager.playItem ();
				checkPoint.transform.gameObject.SetActive (false);
				numberCheckPoint++;
		}

		public void collisionOther (Collision collision)
		{
				if (ProfileManager.setttings.IsVibrate == true) {
						Handheld.Vibrate ();
				}
		
				this.isCrash = true;
		}

		public void changeCarDirection ()
		{
				this.carData.transform.rotation = Quaternion.Lerp (this.carData.transform.rotation,
			Quaternion.LookRotation (game.carManager.carInfo [0].Direction), carData.handling);
		}
}
