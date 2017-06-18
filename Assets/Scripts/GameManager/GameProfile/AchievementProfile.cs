using UnityEngine;
using System.Collections;

public class AchievementProfile : Profile
{
	public static string REACH_MAX_SPEED = "REACH_MAX_SPEED";
	public static string WIN_CAREER_WITHOUT_NITRO = "WIN_CAREER_WITHOUT_NITRO";
	public static string NUMBER_QUICK_RACE_WON = "NUMBER_QUICK_RACE_WON";
	public static string SHARE_FACEBOOK_TIME = "SHARE_FACEBOOK_TIME";
	public static string CONNECT_FACEBOOK = "CONNECT_FACEBOOK";
	//
	bool isReachTopSpeed;

	public bool IsReachTopSpeed {
		get {
			return isReachTopSpeed;
		}
		set {
			this.isReachTopSpeed = value;
			this.setBool (REACH_MAX_SPEED, isReachTopSpeed);
		}
	}

	bool isNitroUsed;

	public bool IsNitroUsed {
		get {
			return isNitroUsed;
		}
		set {
			this.isNitroUsed = value;
			this.setBool (WIN_CAREER_WITHOUT_NITRO, isNitroUsed);
		}
	}

	int numberQuickRaceWon;

	public int NumberQuickRaceWon {
		get {
			return numberQuickRaceWon;
		}
		set {
			this.numberQuickRaceWon = value;
			this.setInt (NUMBER_QUICK_RACE_WON, numberQuickRaceWon);
		}
	}
	
	int shareFacebookTime;

	public int ShareFacebookTime {
		get {
			return shareFacebookTime;
		}
		set {
			this.shareFacebookTime = value;
			this.setInt (SHARE_FACEBOOK_TIME, shareFacebookTime);
		}
	}

	bool isConnectFacebook;

	public bool IsConnectFacebook {
		get {
			return isConnectFacebook;
		}
		set {
			this.isConnectFacebook = value;
			this.setBool (CONNECT_FACEBOOK, isConnectFacebook);
		}
	}

	public override void saveDefaultValue ()
	{
		this.isReachTopSpeed = false;
		this.isNitroUsed = true;
		this.numberQuickRaceWon = 0;
		this.shareFacebookTime = 0;
		this.isConnectFacebook = false;

		this.setBool (REACH_MAX_SPEED, isReachTopSpeed);
		this.setBool (WIN_CAREER_WITHOUT_NITRO, isNitroUsed);
		this.setInt (NUMBER_QUICK_RACE_WON, numberQuickRaceWon);
		this.setInt (SHARE_FACEBOOK_TIME, shareFacebookTime);
		this.setBool (CONNECT_FACEBOOK, isConnectFacebook);

		PlayerPrefs.Save ();
	}

	public override void load ()
	{
		this.isReachTopSpeed = this.getBool (REACH_MAX_SPEED);
		this.isNitroUsed = this.getBool (WIN_CAREER_WITHOUT_NITRO);
		this.numberQuickRaceWon = this.getInt (NUMBER_QUICK_RACE_WON);
		this.shareFacebookTime = this.getInt (SHARE_FACEBOOK_TIME);
		this.isConnectFacebook = this.getBool (CONNECT_FACEBOOK);
	}

	public bool isReachAchievement (int id)
	{
		switch (id) {
		case 0:
			for (int i=0; i<ProfileManager.userProfile.MapProfile.Length; i++) {
				if (ProfileManager.userProfile.MapProfile [i].LastReward != 0) {
					return true;
				}
			}
			return false;

		case 1:
			for (int i=1; i<ProfileManager.userProfile.CarProfile.Length; i++) {
				if (ProfileManager.userProfile.CarProfile [i].IsBought == true) {
					return true;
				}
			}
			return false;

		case 2:
			{
				int numberCars = 0;
				for (int i=0; i<ProfileManager.userProfile.CarProfile.Length; i++) {
					if (ProfileManager.userProfile.CarProfile [i].IsBought == true) {
						numberCars++;
					}
				}

				if (numberCars >= 5) {
					return true;
				} else {
					return false;
				}
			}

		case 3:
			{
				int numberCars = 0;
				for (int i=0; i<ProfileManager.userProfile.CarProfile.Length; i++) {
					if (ProfileManager.userProfile.CarProfile [i].IsBought == true) {
						numberCars++;
					}
				}
			
				if (numberCars >= 10) {
					return true;
				} else {
					return false;
				}
			}

		case 4:
			for (int i=0; i<ProfileManager.userProfile.MapProfile.Length; i++) {
				if (ProfileManager.userProfile.MapProfile [i].LastReward != 3) {
					return false;
				}
			}
			return true;

		case 5:
			return isReachTopSpeed;

		case 6:
			for (int i=0; i<ProfileManager.userProfile.CarProfile.Length; i++) {
				if (ProfileManager.userProfile.CarProfile [i].Acceleration > 0 ||
					ProfileManager.userProfile.CarProfile [i].Speed > 0 ||
					ProfileManager.userProfile.CarProfile [i].Handling > 0 ||
					ProfileManager.userProfile.CarProfile [i].Nitro > 0) {

					return true;
				}
			}
			return false;

		case 7:
			{
				int numberFullyUpgradedCars = 0;
			
				for (int i=0; i<ProfileManager.userProfile.CarProfile.Length; i++) {
					if (ProfileManager.userProfile.CarProfile [i].Acceleration == 5 &&
						ProfileManager.userProfile.CarProfile [i].Speed == 5 &&
						ProfileManager.userProfile.CarProfile [i].Handling == 5 &&
						ProfileManager.userProfile.CarProfile [i].Nitro == 5) {
					
						numberFullyUpgradedCars++;
					}
				}
			
				if (numberFullyUpgradedCars >= 1) {
					return true;
				} else {
					return false;
				}
			}

		case 8:
			if (numberQuickRaceWon >= 10) {
				return true;
			} else {
				return false;
			}

		case 9:
			if (numberQuickRaceWon >= 50) {
				return true;
			} else {
				return false;
			}

		case 10:
			if (numberQuickRaceWon >= 100) {
				return true;
			} else {
				return false;
			}

		case 11:
			if (shareFacebookTime >= 3) {
				return true;
			} else {
				return false;
			}

		case 12:
			return !isNitroUsed;

		case 13:
			return isConnectFacebook;

		case 14:
			{
				int numberFullyUpgradedCars = 0;

				for (int i=0; i<ProfileManager.userProfile.CarProfile.Length; i++) {
					if (ProfileManager.userProfile.CarProfile [i].Acceleration == 5 &&
						ProfileManager.userProfile.CarProfile [i].Speed == 5 &&
						ProfileManager.userProfile.CarProfile [i].Handling == 5 &&
						ProfileManager.userProfile.CarProfile [i].Nitro == 5) {
					
						numberFullyUpgradedCars++;
					}
				}

				if (numberFullyUpgradedCars >= 2) {
					return true;
				} else {
					return false;
				}
			}

		case 15:
			{
				int numberFullyUpgradedCars = 0;
			
				for (int i=0; i<ProfileManager.userProfile.CarProfile.Length; i++) {
					if (ProfileManager.userProfile.CarProfile [i].Acceleration == 5 &&
						ProfileManager.userProfile.CarProfile [i].Speed == 5 &&
						ProfileManager.userProfile.CarProfile [i].Handling == 5 &&
						ProfileManager.userProfile.CarProfile [i].Nitro == 5) {
					
						numberFullyUpgradedCars++;
					}
				}
			
				if (numberFullyUpgradedCars >= 5) {
					return true;
				} else {
					return false;
				}
			}

		case 16:
			{
				int numberFullyUpgradedCars = 0;
			
				for (int i=0; i<ProfileManager.userProfile.CarProfile.Length; i++) {
					if (ProfileManager.userProfile.CarProfile [i].Acceleration == 5 &&
						ProfileManager.userProfile.CarProfile [i].Speed == 5 &&
						ProfileManager.userProfile.CarProfile [i].Handling == 5 &&
						ProfileManager.userProfile.CarProfile [i].Nitro == 5) {
					
						numberFullyUpgradedCars++;
					}
				}
			
				if (numberFullyUpgradedCars >= 10) {
					return true;
				} else {
					return false;
				}
			}

		default:
			return false;
		}
	}
}
