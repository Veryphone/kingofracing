using UnityEngine;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
	public static string scene = "MainMenu";
	public Texture loading;

	void Start ()
	{		
		Application.LoadLevel (scene);
		Application.backgroundLoadingPriority = ThreadPriority.High;
		useGUILayout = false;

		ProfileManager.init();
	}

	void OnGUI ()
	{		
		if (Application.isLoadingLevel) {
			GUI.matrix = Matrix4x4.TRS (Vector3.zero, Quaternion.identity, new Vector3 (Screen.width / 800f, Screen.height / 480f, 1f));
			GUI.DrawTexture (new Rect (0, 0, 800, 480), loading);
		}
	}
}
