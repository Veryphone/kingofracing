using UnityEngine;
using System;
using System.Collections;

public abstract class Profile
{	
	public static string FIRST_TIME_TAG = "isFirstTime";

	public abstract void saveDefaultValue ();

	public abstract void load ();

	protected bool getBool (string tag)
	{
		int result = 0;
		try {
			result = int.Parse (DataEncryption.Decrypt (PlayerPrefs.GetString (DataEncryption.Encrypt (tag))));
		} catch (Exception e) {
			Debug.Log(e);
			result = 0;
		}

		if (result == 0) {
			return false;
		} else {
			return true;
		}
	}

	protected void setBool (string tag, bool value)
	{
		int val = 0;
		if (value == true) {
			val = 1;
		}

		PlayerPrefs.SetString (DataEncryption.Encrypt (tag), DataEncryption.Encrypt (val + string.Empty));
	}

	protected int getInt (string tag)
	{
		int result = 0;
		try {
			result = int.Parse (DataEncryption.Decrypt (PlayerPrefs.GetString (DataEncryption.Encrypt (tag))));
		} catch (Exception e) {
			Debug.Log(e);
			result = 0;
		}

		return result;
	}
	
	protected void setInt (string tag, int value)
	{
		PlayerPrefs.SetString (DataEncryption.Encrypt (tag), DataEncryption.Encrypt (value + string.Empty));
	}

	protected float getFloat (string tag)
	{
		float result = 0;
		try {
			result = float.Parse (DataEncryption.Decrypt (PlayerPrefs.GetString (DataEncryption.Encrypt (tag))));
		} catch (Exception e) {
			Debug.Log(e);
			result = 0;
		}

		return result;
	}
	
	protected void setFloat (string tag, float value)
	{
		PlayerPrefs.SetString (DataEncryption.Encrypt (tag), DataEncryption.Encrypt (value + string.Empty));
	}

	protected string getString (string tag)
	{
		string result = string.Empty;
		try {
			result = DataEncryption.Decrypt (PlayerPrefs.GetString (DataEncryption.Encrypt (tag)));
		} catch (Exception e) {
			Debug.Log(e);
			result = string.Empty;
		}

		return result;
	}
	
	protected void setString (string tag, string value)
	{
		PlayerPrefs.SetString (DataEncryption.Encrypt (tag), DataEncryption.Encrypt (value));
	}
	
	public static bool isFirstTime ()
	{
		int value = PlayerPrefs.GetInt (FIRST_TIME_TAG, 1);
		if (value == 1) {
			return true;
		} else {
			return false;
		}
	}
	
	public static void saveFirstTime (bool isFirstTime)
	{
		if (isFirstTime == true) {
			PlayerPrefs.SetInt (FIRST_TIME_TAG, 1);
			PlayerPrefs.Save ();
		} else {
			PlayerPrefs.SetInt (FIRST_TIME_TAG, 0);
			PlayerPrefs.Save ();
		}
	}
}
