using UnityEngine;
using System.Collections;

public class UserProfile : Profile
{
		public static string STAR_TAG = "star";
		public static string CASH_TAG = "cash";
		public static string SELECTED_CAR_TAG = "selectedCar";
		public static string SELECTED_MAP = "selectedMap";
		//
		private int star;

		public int Star {
				get {
						return star;
				}
				set {
						star = value;
						this.setInt (STAR_TAG, this.star);
				}
		}

		private int cash;

		public int Cash {
				get {
						return cash;
				}
				set {
						cash = value;
						this.setInt (CASH_TAG, this.cash);
				}
		}

		private int selectedCar;

		public int SelectedCar {
				get {
						return selectedCar;
				}
				set {
						selectedCar = value;
						this.setInt (SELECTED_CAR_TAG, this.selectedCar);
				}
		}

		private int selectedMap;
	
		public int SelectedMap {
				get {
						return selectedMap;
				}
				set {
						selectedMap = value;
						this.setInt (SELECTED_MAP, this.selectedMap);
				}
		}

		private CarProfile[] carProfile;

		public CarProfile[] CarProfile {
				get {
						return carProfile;
				}
				set {
						carProfile = value;
				}
		}

		private MapProfile[] mapProfile;
	
		public MapProfile[] MapProfile {
				get {
						return mapProfile;
				}
				set {
						mapProfile = value;
				}
		}

		public UserProfile ()
		{
				carProfile = new CarProfile[Game.allCarNumber];
				for (int i=0; i<carProfile.Length; i++) {
						carProfile [i] = new CarProfile (i);
				}

				mapProfile = new MapProfile[36];
				for (int i=0; i<MapProfile.Length; i++) {
						mapProfile [i] = new MapProfile (i);
				}
		}

		public override void saveDefaultValue ()
		{	
				this.star = 0;
				this.cash = 0;
				this.selectedCar = 0;
				this.selectedMap = 0;
		
				this.setInt (STAR_TAG, this.star);
				this.setInt (CASH_TAG, this.cash);
				this.setInt (SELECTED_CAR_TAG, this.selectedCar);
				this.setInt (SELECTED_MAP, this.selectedMap);

				for (int i=0; i<carProfile.Length; i++) {
						carProfile [i].saveDefaultValue ();
				}

				for (int i=0; i<MapProfile.Length; i++) {
						mapProfile [i].saveDefaultValue ();
				}

				PlayerPrefs.Save ();
		}
	
		public override void load ()
		{
				this.star = this.getInt (STAR_TAG);
				this.cash = this.getInt (CASH_TAG);
				this.selectedCar = this.getInt (SELECTED_CAR_TAG);
				this.selectedMap = this.getInt (SELECTED_MAP);
        Debug.Log(selectedCar.ToString() + "______________________________selected car");
				for (int i=0; i<carProfile.Length; i++) {
						carProfile [i].load ();
				}

				for (int i=0; i<MapProfile.Length; i++) {
						mapProfile [i].load ();
				}
		}

		public void calculateStar ()
		{
				int tempStar = 0;
				for (int i=0; i<MapProfile.Length; i++) {
						tempStar += mapProfile [i].LastReward;
				}
				Star = tempStar;
		}

		public void calculateCarUnlocked ()
		{		
				if (ProfileManager.userProfile.carProfile [ProfileManager.userProfile.SelectedCar].IsBought == false) {
						ProfileManager.userProfile.SelectedCar = 0;
						PlayerPrefs.Save ();
				}
		}
}
