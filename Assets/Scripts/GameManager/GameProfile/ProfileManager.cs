using UnityEngine;
using System.Collections;

public class ProfileManager
{
	public static Settings setttings;
	public static UserProfile userProfile;
	public static AchievementProfile achievementProfile;

	public static void init ()
	{
        Debug.Log("delete all key data game_________________");
        //PlayerPrefs.DeleteAll();
        if (setttings == null || userProfile == null || achievementProfile == null) {
            Debug.Log("null______________________________ value");
            setttings = new Settings ();
			userProfile = new UserProfile ();
			achievementProfile = new AchievementProfile ();

			if (Profile.isFirstTime () == true) {
                Debug.Log("default______________________________ value");
				setttings.saveDefaultValue ();
				userProfile.saveDefaultValue ();
				achievementProfile.saveDefaultValue ();

				Profile.saveFirstTime (false);
			} else {
                Debug.Log("load______________________________ value");
                setttings.load ();
				userProfile.load ();
				achievementProfile.load ();
			}

			string[] names = QualitySettings.names;
			for (int i=0; i<names.Length; i++) {
				switch (setttings.GraphicsQualitiy) {
				case 0:
					if (names [i].Trim () == "Fastest") {
						QualitySettings.SetQualityLevel (i, true);
					}
					break;
				
				case 1:
					if (names [i].Trim () == "Simple") {
						QualitySettings.SetQualityLevel (i, true);
					}
					break;
				
				case 2:
					if (names [i].Trim () == "Fantastic") {
						QualitySettings.SetQualityLevel (i, true);
					}
					break;
				
				default:
					if (names [i].Trim () == "Simple") {
						QualitySettings.SetQualityLevel (i, true);
					}
					break;
				}
			}
		}
	}
}
