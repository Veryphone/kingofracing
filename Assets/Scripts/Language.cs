using UnityEngine;
using System.Collections;

public class Language : MonoBehaviour
{
	public static string BACK = "BACK";
	public static string QUICKRACE = "QUICK RACE";
	public static string CAREERMODE = "CAREER MODE";
	public static string SETTING = "SETTING";
	public static string ACHIEVEMENT = "ACHIEVEMENT";
	public static string SHOP = "SHOP";
	public static string FREECOIN = "";

	public static string MOREGAME = "MORE GAME";
	public static string FACEBOOK = "FACEBOOK";
	public static string CREDIT = "CREDITS";

	public static void ChangeLangToEn ()
	{
		BACK = "BACK";
		QUICKRACE = "QUICK RACE";
		CAREERMODE = "CAREER MODE";
		SETTING = "SETTING";
		ACHIEVEMENT = "ACHIEVEMENT";
		SHOP = "SHOP";
		FREECOIN = "FREE";

		MOREGAME = "MORE GAME";
		FACEBOOK = "FACEBOOK";
		CREDIT = "CREDITS";
	}

	public static void ChangeLangToVn ()
	{
		BACK = "TRƯỚC";
		QUICKRACE = "QUICK RACE";
		CAREERMODE = "CAREER MODE";
		SETTING = "CÀI ĐẶT";
		ACHIEVEMENT = "DANH HIỆU";
		SHOP = "HÀNG";
		FREECOIN = "FREE";

		MOREGAME = "MORE GAME";
		FACEBOOK = "FACEBOOK";
		CREDIT = "CREDITS";
	}
}
