using UnityEngine;
using System.Collections;

public class EffectController : MonoBehaviour
{
	public CameraController cameraController;

	void Update ()
	{
		if(Game.GameState==Game.GAME_STATE.RUNNING){
		this.transform.position = new Vector3 (cameraController.target.transform.position.x,
		                                       this.transform.position.y, cameraController.target.transform.position.z);
		}
	}
}
