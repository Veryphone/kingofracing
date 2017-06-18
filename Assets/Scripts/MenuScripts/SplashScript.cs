using UnityEngine;
using System.Collections;

public class SplashScript : MonoBehaviour
{
	void Awake ()
	{
		Screen.SetResolution (800, 480, true);
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
	}

	void Start ()
	{
		StartCoroutine (PlayMovie ());
	}

	protected IEnumerator PlayMovie ()
	{
		//Handheld.PlayFullScreenMovie ("Movies/Splay.mp4", Color.clear, FullScreenMovieControlMode.CancelOnInput);
		yield return new WaitForSeconds (0.01f);

		SceneLoader.scene = "MainMenu";
		Application.LoadLevel ("Loading");
	}
}
