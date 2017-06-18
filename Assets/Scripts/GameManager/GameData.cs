using UnityEngine;
using System.Collections;

public class GameData : MonoBehaviour
{
		public enum MAP_NAME
		{
				TOKYO,
				HANOI,
				NEWYORK,
				NEVADA,
				CHINA,
				PARIS
		}

		public enum CAR_NAME
		{
				ASTON_MARTIN_DB9,
				AUDI_R8,
				BENTLEY_V8,
				FERRARI_458_ITALIA,
				LAMBORHINI_LP560,
				LAMBORGHINI_VENENO,
				MARUSSIA_B2,
				MASERATI_GRAN_TURISMO,
				MERCEDES_BENZ_SLS,
				PORSCHE_911,
                OTO,
		}

		public enum GAME_MODE
		{
				NORMAL,
				TIME_TRIAL,
				CHECK_POINT
		}

		public static MAP_NAME selectedMap = MAP_NAME.TOKYO;
		public static GAME_MODE selectedMode = GAME_MODE.NORMAL;
		public static int level = -1;
		//
		public static bool isFirstRacer = false;
		public static bool isNitroFull = false;
		public static bool isDoubleReward = false;
		//
		public static int numberRaces = 10;
		public static float duration = 120;
		public static int numberCheckpoints = 4;
		//
		public static int numberEnemies = 5;

		public static int getMapID (MAP_NAME mapName)
		{
				switch (mapName) {
				case MAP_NAME.TOKYO:
						return 0;

				case MAP_NAME.HANOI:
						return 1;

				case MAP_NAME.NEWYORK:
						return 2;

				case MAP_NAME.NEVADA:
						return 3;

				case MAP_NAME.CHINA:
						return 4;

				case MAP_NAME.PARIS:
						return 5;

				default:
						return 0;
				}
		}

		public static Color GetCarColor (int colorId)
		{
				switch (colorId) {
				case 0:
						return new Color (1.000f, 1.000f, 1.000f, 1.000f);

				case 1:
						return new Color (0.318f, 0.318f, 0.318f, 1.000f);

				case 2:
						return new Color (0.000f, 0.502f, 0.486f, 1.000f);

				case 3:
						return new Color (0.271f, 0.529f, 0.000f, 1.000f);

				case 4:
						return new Color (1.000f, 0.973f, 0.000f, 1.000f);

				case 5:
						return new Color (1.000f, 0.471f, 0.000f, 1.000f);

				case 6:
						return new Color (0.847f, 0.059f, 0.059f, 1.000f);

				case 7:
						return new Color (0.361f, 0.000f, 0.302f, 1.000f);

				case 8:
						return new Color (0.000f, 0.510f, 0.898f, 1.000f);

				case 9:
						return new Color (0.000f, 0.000f, 0.000f, 1.000f);
            case 10:
                return new Color(1.000f, 0.973f, 0.000f, 1.000f);
            default:
						return new Color (1.000f, 1.000f, 1.000f, 1.000f);
				}
		}

		public static string getCarName (int index)
		{
				switch (index) {
				case 0:
						return "ASTON MARTIN DB9";

				case 1:
						return "AUDI R8";

				case 2:
						return "BENTLEY V8";

				case 3:
						return "FERRARI 458 ITALIA";

				case 4:
						return "LAMBORHINI LP560";

				case 5:
						return "LAMBORGHINI VENENO";

				case 6:
						return "MARUSSIA B2";

				case 7:
						return "MASERATI GRAN TURISMO";

				case 8:
						return "MERCEDES BENZ SLS";

				case 9:
						return "PORSCHE 911";
            case 10:
                return "OTO";
				default:
						return "ASTON MARTIN DB9";
				}
		}

		public static void resetItem ()
		{
				isFirstRacer = false;
				isNitroFull = false;
				isDoubleReward = false;
		}
}
