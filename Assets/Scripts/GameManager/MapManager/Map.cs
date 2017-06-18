using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour
{
	public Transform[] spawnPointsList;
	//
	public Transform origin;
	public Transform worldEnd;
	//
	public Path[] pathList;
	//
	public Transform[] destinationList;
	//
	public Transform[] checkPointList;

	void Start ()
	{
		if (GameData.selectedMode != GameData.GAME_MODE.CHECK_POINT) {
			for (int i=0; i<checkPointList.Length; i++) {
				checkPointList [i].gameObject.SetActive (false);
			}
		}
	}

	public void resetCheckPoint ()
	{
		if (GameData.selectedMode == GameData.GAME_MODE.CHECK_POINT) {
			for (int i=0; i<checkPointList.Length; i++) {
				checkPointList [i].gameObject.SetActive (true);
			}
		}
	}
}
