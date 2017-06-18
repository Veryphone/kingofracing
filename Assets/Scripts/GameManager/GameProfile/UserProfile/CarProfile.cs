using UnityEngine;
using System.Collections;

public class CarProfile : Profile
{
		public static string BOUGHT_TAG = "isBought";
		public static string ACCELERATION_TAG = "acceleration";
		public static string SPEED_TAG = "speed";
		public static string HANDLING_TAG = "handling";
		public static string NITRO_TAG = "nitro";
		public static string COLOR_TAG = "color";
		//
		private int ID;
		private bool isBought;

		public bool IsBought {
				get {
						return isBought;
				}
				set {
						this.isBought = value;
						this.setBool (BOUGHT_TAG + "_" + ID, isBought);
				}
		}

		private int acceleration;

		public int Acceleration {
				get {
						return acceleration;
				}
				set {
						this.acceleration = value;
						this.setInt (ACCELERATION_TAG + "_" + ID, acceleration);
				}
		}

		private int speed;

		public int Speed {
				get {
						return speed;
				}
				set {
						this.speed = value;
						this.setInt (SPEED_TAG + "_" + ID, speed);
				}
		}

		private int handling;

		public int Handling {
				get {
						return handling;
				}
				set {
						this.handling = value;
						this.setInt (HANDLING_TAG + "_" + ID, handling);
				}
		}

		private int nitro;

		public int Nitro {
				get {
						return nitro;
				}
				set {
						this.nitro = value;
						this.setInt (NITRO_TAG + "_" + ID, nitro);
				}
		}

		private int color;

		public int Color {
				get {
						return color;
				}
				set {
						this.color = value;
						this.setInt (COLOR_TAG + "_" + ID, color);
				}
		}

		public CarProfile (int id)
		{
				this.ID = id;
		}

		public override void saveDefaultValue ()
		{
        if (ID < 10)
        {
            if (ID == 0)
            {
                this.isBought = true;
                this.color = 9;
            }
            else {
                this.isBought = false;
                this.color = ID;
            }
        }
        else
        {
            Debug.Log("set color ID for new car____________________"+ID);
            switch (ID)
            {
                case 10:
                    color = 4;
                    break;
                case 11:
                    break;
            }
        }

        this.acceleration = 0;
				this.speed = 0;
				this.handling = 0;
				this.nitro = 0;

				this.setBool (BOUGHT_TAG + "_" + ID, isBought);
				this.setInt (ACCELERATION_TAG + "_" + ID, acceleration);
				this.setInt (SPEED_TAG + "_" + ID, speed);
				this.setInt (HANDLING_TAG + "_" + ID, handling);
				this.setInt (NITRO_TAG + "_" + ID, nitro);
				this.setInt (COLOR_TAG + "_" + ID, color);
		}
	
		public override void load ()
		{
				this.isBought = this.getBool (BOUGHT_TAG + "_" + ID);
				this.acceleration = this.getInt (ACCELERATION_TAG + "_" + ID);
				this.speed = this.getInt (SPEED_TAG + "_" + ID);
				this.handling = this.getInt (HANDLING_TAG + "_" + ID);
				this.nitro = this.getInt (NITRO_TAG + "_" + ID);
				this.color = this.getInt (COLOR_TAG + "_" + ID);
		}
}
