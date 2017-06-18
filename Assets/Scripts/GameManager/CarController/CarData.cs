using UnityEngine;
using System;
using System.Collections;

public class CarData : MonoBehaviour
{	
		static float ROTATION_SPEED = 1000 * Time.deltaTime;

		// Wheel Colliders, no transform
		public WheelCollider wheelFL;
		public WheelCollider wheelFR;
		public WheelCollider wheelRL;
		public WheelCollider wheelRR;
	
		// Wheel transform
		public Transform wheelFLtrans;
		public Transform wheelFRtrans;
		public Transform wheelRLtrans;
		public Transform wheelRRtrans;
		//
		public GameObject nitro;
		public TextMesh carInfo;
		public Vector3 centerOfMass = new Vector3 (0, -0.9f, 0);
		//
		public LightBlastController lightBlast;
		public GameObject sparkLeft;
		public GameObject sparkRight;
	
		// private info
		float lowSteer = 10;
		float highSteer = 3;
	
		//
		float torque;
		float axis;
		bool isNitroUsing;
		public float handling;
	
		public bool IsNitroUsing {
				get {
						return isNitroUsing;
				}
		}
	
		private float nitroBar;
	
		public float NitroBar {
				get {
						return nitroBar;
				}
				set {
						if (value > 100) {
								nitroBar = 100;
						} else if (value < 0) {
								nitroBar = 0;
						} else {
								nitroBar = value;
						}
				}
		}
	
		private float nitroRate;
	
		public float NitroRate {
				get {
						return nitroRate;
				}
		}
	
		//
		float currentSteerAngle;
		float currentSpeed;
	
		public float CurrentSpeed {
				get {
						return currentSpeed;
				}
		}
	
		//car info
		float initialTopSpeed;
		float initialMaxTorque;
		//
		float topSpeed;
		float maxTorque;
		bool isBrake;
		bool isRunningBackward;
		//
		public BaseCarController carController;
		//
		float nitroTime;
		float speedRate;

    
	
		public void initCar (bool isPlayer)
		{
				GetComponent<Rigidbody>().centerOfMass = this.centerOfMass;
		
				if (isPlayer == true) {
						this.carController = new PlayerCarController (this);
						this.initPlayerCarData ();
				} else {
						this.carController = new EnemyCarController (this);
						this.nitroTime = UnityEngine.Random.Range (0, 9);
				}

				if (GameData.isNitroFull == true) {
						this.nitroBar = 100;
				}

				topSpeed = initialTopSpeed;
				maxTorque = initialMaxTorque;
		}
	
		// Control speed
		public void controlCar ()
		{
				speedRate = GetComponent<Rigidbody>().velocity.magnitude / topSpeed;
				currentSteerAngle = Mathf.Lerp (lowSteer, highSteer, speedRate);		
				currentSpeed = GetComponent<Rigidbody>().velocity.magnitude;

				wheelFL.steerAngle = currentSteerAngle * axis;
				wheelFR.steerAngle = currentSteerAngle * axis;

				if (axis < -0.2f || axis > 0.2f) {
						((PlayerCarController)carController).changeCarDirection ();
				}

				if (this.isBrake == false) {
						((PlayerCarController)carController).game.leftTrail.SetActive (false);
						((PlayerCarController)carController).game.rightTrail.SetActive (false);

						if (currentSpeed < topSpeed) {
								wheelRR.motorTorque = torque;
								wheelRL.motorTorque = torque;
						} else {
								wheelRR.motorTorque = 0;
								wheelRL.motorTorque = 0;	

								GetComponent<Rigidbody>().velocity = Vector3.Lerp (GetComponent<Rigidbody>().velocity, 
				                                   GetComponent<Rigidbody>().velocity.normalized * topSpeed, 0.05f);
						}
				} else {
						if (this.isRunningBackward == false) {
								this.makeTrail ();

								if (this.currentSpeed > 10) {
										wheelRR.brakeTorque = maxTorque * 2f;
										wheelRL.brakeTorque = maxTorque * 2f;
								} else {
										this.isRunningBackward = true;
										wheelRR.brakeTorque = 0;
										wheelRL.brakeTorque = 0;
								}
						} else {
								wheelRR.motorTorque = -maxTorque;
								wheelRL.motorTorque = -maxTorque;
						}
				}
		}

		void makeTrail ()
		{		
				WheelHit hit = new WheelHit ();
				wheelRL.GetGroundHit (out hit);

				((PlayerCarController)carController).game.rightTrail.SetActive (true);
				((PlayerCarController)carController).game.rightTrail.transform.position = hit.point + this.transform.forward * 2;

				wheelRR.GetGroundHit (out hit);
				((PlayerCarController)carController).game.leftTrail.SetActive (true);
				((PlayerCarController)carController).game.leftTrail.transform.position = hit.point + this.transform.forward * 2;
		}
	
		void FixedUpdate ()
		{
				if (Game.GameState == Game.GAME_STATE.RUNNING) {
						carController.FixedUpdate ();
				} else {
						wheelRR.motorTorque = 0;
						wheelRL.motorTorque = 0;
				}
		}
	
		void Update ()
		{
				if (Game.GameState == Game.GAME_STATE.RUNNING) {
						carController.Update ();
						if (carController.isPlayer () == true) {
								if (this.isRunningBackward == false) {
										wheelFLtrans.Rotate (ROTATION_SPEED * speedRate, 0, 0);
										wheelFRtrans.Rotate (ROTATION_SPEED * speedRate, 0, 0);
										wheelRLtrans.Rotate (ROTATION_SPEED * speedRate, 0, 0);
										wheelRRtrans.Rotate (ROTATION_SPEED * speedRate, 0, 0);
								} else {
										wheelFLtrans.Rotate (-ROTATION_SPEED * speedRate, 0, 0);
										wheelFRtrans.Rotate (-ROTATION_SPEED * speedRate, 0, 0);
										wheelRLtrans.Rotate (-ROTATION_SPEED * speedRate, 0, 0);
										wheelRRtrans.Rotate (-ROTATION_SPEED * speedRate, 0, 0);
								}						
						} else {
								nitroTime += Time.deltaTime;
				
								if (nitroTime > 10) {
										nitroTime = 0;
										if (UnityEngine.Random.Range (0, 100) >= 30) {
												this.nitro.SetActive (true);
										} else {
												this.nitro.SetActive (false);
										}
								}

								wheelFLtrans.Rotate (ROTATION_SPEED, 0, 0);
								wheelFRtrans.Rotate (ROTATION_SPEED, 0, 0);
								wheelRLtrans.Rotate (ROTATION_SPEED, 0, 0);
								wheelRRtrans.Rotate (ROTATION_SPEED, 0, 0);
						}
				} else {
						wheelRR.motorTorque = 0;
						wheelRL.motorTorque = 0;
				}
		}
	
		public void runningForward ()
		{
				this.isBrake = false;
				this.isRunningBackward = false;
		
				wheelRR.brakeTorque = 0;
				wheelRL.brakeTorque = 0;

				this.torque = maxTorque;
		}
	
		public void runningBackward ()
		{
				this.isBrake = true;
		}
	
		public void setAxis (float value)
		{
				this.axis = value;
		}
	
		public void activateNitro (bool value)
		{
				if (value == true) {
						if (nitroBar >= 10) {
								maxTorque = initialMaxTorque * 1.5f;
								topSpeed = initialTopSpeed * 1.5f;
				
								this.nitro.SetActive (value);
								this.isNitroUsing = value;

//								if (nitroBar >= 50) {
//										RenderSettings.ambientLight = new Color32 (159, 35, 132, 255);
//								}

								((PlayerCarController)carController).isNitroUsed = true;
						}
				} else {
						this.nitro.SetActive (value);
						this.isNitroUsing = value;

//						RenderSettings.ambientLight = Color.white;
			
						maxTorque = initialMaxTorque;
						topSpeed = initialTopSpeed;
				}
		}
	
		void OnTriggerEnter (Collider other)
		{
				if (carController.isPlayer () == true) {
						if (other.transform.gameObject.layer == 12) {
								((PlayerCarController)carController).processItem (other);
								lightBlast.activate ();

						} else if (other.transform.gameObject.layer == 15) {
								((PlayerCarController)carController).reachCheckPoint (other);
								lightBlast.activate ();
						}
				}
		}
	
		void OnCollisionEnter (Collision collision)
		{
				if (carController.isPlayer () == true) {
						if (collision.collider.gameObject.layer != 0 && collision.collider.gameObject.layer != 9) {
								((PlayerCarController)carController).collisionOther (collision);
						}
				}
		}

		void OnCollisionStay (Collision collision)
		{
				if (carController.isPlayer () == true) {
						if (collision.collider.gameObject.layer != 0 && collision.collider.gameObject.layer != 9) {
								if (collision.collider.gameObject.layer == 8) {
										this.GetComponent<Rigidbody>().velocity = this.GetComponent<Rigidbody>().velocity * 0.985f;
					
										if (collision.contacts.Length > 0) {
												Vector3 relativePosition = this.transform.InverseTransformPoint (collision.contacts [0].point);
							
												if (relativePosition.x > 0) {								
														sparkRight.SetActive (true);								
												} else {
														sparkLeft.SetActive (true);
												}
										}
								}
						}
				}
		}

		void OnCollisionExit (Collision collision)
		{
				if (carController.isPlayer () == true) {
						if (collision.collider.gameObject.layer != 0 && collision.collider.gameObject.layer != 9) {
								sparkLeft.SetActive (false);
								sparkRight.SetActive (false);
						}
				}
		}

		void initPlayerCarData ()
		{
				GameData.CAR_NAME[] carNames = (GameData.CAR_NAME[])Enum.GetValues (typeof(GameData.CAR_NAME));

				switch (carNames [ProfileManager.userProfile.SelectedCar]) {
				case GameData.CAR_NAME.ASTON_MARTIN_DB9:
						initialTopSpeed = 60 + 2 * ProfileManager.userProfile.CarProfile [0].Speed;
						initialMaxTorque = 30 + ProfileManager.userProfile.CarProfile [0].Acceleration;
						nitroRate = 18 - 1f * ProfileManager.userProfile.CarProfile [0].Nitro;
						handling = 0.04f + ProfileManager.userProfile.CarProfile [0].Handling * 0.001f;
						break;
			
				case GameData.CAR_NAME.AUDI_R8:
						initialTopSpeed = 63 + 2 * ProfileManager.userProfile.CarProfile [1].Speed;
						initialMaxTorque = 31 + ProfileManager.userProfile.CarProfile [1].Acceleration;
						nitroRate = 17 - 1f * ProfileManager.userProfile.CarProfile [1].Nitro;
						handling = 0.045f + ProfileManager.userProfile.CarProfile [0].Handling * 0.001f;
						break;
			
				case GameData.CAR_NAME.BENTLEY_V8:
						initialTopSpeed = 66 + 2 * ProfileManager.userProfile.CarProfile [2].Speed;
						initialMaxTorque = 32 + ProfileManager.userProfile.CarProfile [2].Acceleration;
						nitroRate = 16 - 1f * ProfileManager.userProfile.CarProfile [2].Nitro;
						handling = 0.05f + ProfileManager.userProfile.CarProfile [0].Handling * 0.001f;
						break;
			
				case GameData.CAR_NAME.FERRARI_458_ITALIA:
						initialTopSpeed = 69 + 2 * ProfileManager.userProfile.CarProfile [3].Speed;
						initialMaxTorque = 33 + ProfileManager.userProfile.CarProfile [3].Acceleration;
						nitroRate = 15 - 1f * ProfileManager.userProfile.CarProfile [3].Nitro;
						handling = 0.055f + ProfileManager.userProfile.CarProfile [0].Handling * 0.001f;
						break;
			
				case GameData.CAR_NAME.LAMBORHINI_LP560:
						initialTopSpeed = 72 + 2 * ProfileManager.userProfile.CarProfile [4].Speed;
						initialMaxTorque = 34 + ProfileManager.userProfile.CarProfile [4].Acceleration;
						nitroRate = 14 - 1f * ProfileManager.userProfile.CarProfile [4].Nitro;
						handling = 0.06f + ProfileManager.userProfile.CarProfile [0].Handling * 0.001f;
						break;
			
				case GameData.CAR_NAME.LAMBORGHINI_VENENO:
						initialTopSpeed = 75 + 2 * ProfileManager.userProfile.CarProfile [5].Speed;
						initialMaxTorque = 45 + ProfileManager.userProfile.CarProfile [5].Acceleration;
						nitroRate = 13 - 1f * ProfileManager.userProfile.CarProfile [5].Nitro;
						handling = 0.065f + ProfileManager.userProfile.CarProfile [0].Handling * 0.001f;
						break;
			
				case GameData.CAR_NAME.MARUSSIA_B2:
						initialTopSpeed = 78 + 2 * ProfileManager.userProfile.CarProfile [6].Speed;
						initialMaxTorque = 36 + ProfileManager.userProfile.CarProfile [6].Acceleration;
						nitroRate = 12 - 1f * ProfileManager.userProfile.CarProfile [6].Nitro;
						handling = 0.07f + ProfileManager.userProfile.CarProfile [0].Handling * 0.001f;
						break;
			
				case GameData.CAR_NAME.MASERATI_GRAN_TURISMO:
						initialTopSpeed = 81 + 2 * ProfileManager.userProfile.CarProfile [7].Speed;
						initialMaxTorque = 37 + ProfileManager.userProfile.CarProfile [7].Acceleration;
						nitroRate = 11 - 1f * ProfileManager.userProfile.CarProfile [7].Nitro;
						handling = 0.075f + ProfileManager.userProfile.CarProfile [0].Handling * 0.001f;
						break;
			
				case GameData.CAR_NAME.MERCEDES_BENZ_SLS:
						initialTopSpeed = 84 + 2 * ProfileManager.userProfile.CarProfile [8].Speed;
						initialMaxTorque = 38 + ProfileManager.userProfile.CarProfile [8].Acceleration;
						nitroRate = 10 - 1f * ProfileManager.userProfile.CarProfile [8].Nitro;
						handling = 0.08f + ProfileManager.userProfile.CarProfile [0].Handling * 0.001f;
						break;
			
				case GameData.CAR_NAME.PORSCHE_911:
						initialTopSpeed = 87 + 2 * ProfileManager.userProfile.CarProfile [9].Speed;
						initialMaxTorque = 39 + ProfileManager.userProfile.CarProfile [9].Acceleration;
						nitroRate = 9 - 1f * ProfileManager.userProfile.CarProfile [9].Nitro;
						handling = 0.085f + ProfileManager.userProfile.CarProfile [0].Handling * 0.001f;
						break;
			
				default:
						initialTopSpeed = 60;
						initialMaxTorque = 30;
						nitroRate = 15;
						handling = 0.05f;
						break;
				}
		}
}
