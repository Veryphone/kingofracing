using UnityEngine;
using System.Collections;

public class Settings:Profile
{
	public static string SOUND_TAG = "sound";
	public static string MUSIC_TAG = "music";
	public static string SENSITIVITY_TAG = "sensitivity";
	public static string VIBRATE_TAG = "vibrate";
	public static string GRAPHICS_TAG = "graphics";
	//
	private float soundVolume;

	public float SoundVolume {
		get {
			return soundVolume;
		}
		set {
			this.soundVolume = value;
			this.setFloat (SOUND_TAG, soundVolume);
		}
	}

	private float musicVolume;

	public float MusicVolume {
		get {
			return musicVolume;
		}
		set {
			this.musicVolume = value;
			this.setFloat (MUSIC_TAG, musicVolume);
		}
	}

	private float sensitivity;

	public float Sensitivity {
		get {
			return sensitivity;
		}
		set {
			this.sensitivity = value;
			this.setFloat (SENSITIVITY_TAG, sensitivity);
		}
	}

	private int graphicsQualitiy;

	public int GraphicsQualitiy {
		get {
			return graphicsQualitiy;
		}
		set {
			this.graphicsQualitiy = value;
			this.setInt (GRAPHICS_TAG, graphicsQualitiy);
		}
	}

	private bool isVibrate;

	public bool IsVibrate {
		get {
			return isVibrate;
		}
		set {
			this.isVibrate = value;
			this.setBool (VIBRATE_TAG, isVibrate);
		}
	}

	public override void saveDefaultValue ()
	{
		this.soundVolume = 100f;
		this.musicVolume = 100f;
		this.sensitivity = 100f;
		this.graphicsQualitiy = 1;
		this.isVibrate = true;

		this.setFloat (SOUND_TAG, soundVolume);
		this.setFloat (MUSIC_TAG, musicVolume);
		this.setFloat (SENSITIVITY_TAG, sensitivity);
		this.setInt (GRAPHICS_TAG, graphicsQualitiy);
		this.setBool (VIBRATE_TAG, isVibrate);
		
		PlayerPrefs.Save ();
	}
	
	public override void load ()
	{
		this.soundVolume = this.getFloat (SOUND_TAG);
		this.musicVolume = this.getFloat (MUSIC_TAG);
		this.sensitivity = this.getFloat (SENSITIVITY_TAG);
		this.graphicsQualitiy = this.getInt (GRAPHICS_TAG);
		this.isVibrate = this.getBool (VIBRATE_TAG);
	}
}
