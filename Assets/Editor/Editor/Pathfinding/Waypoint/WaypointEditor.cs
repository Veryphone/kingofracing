using UnityEngine;
using UnityEditor;
using System;
using System.Collections;

[CustomEditor(typeof(Path))]
public class WaypointEditor : Editor
{
		Rect size = new Rect (0, 0, 600, 30);
		Rect distanceSize = new Rect (200, 30, 400, 30);
		//
		Rect waypointIndex = new Rect (10, 5, 190, 20);
		Rect insert = new Rect (200, 0, 100, 30);
		Rect delete = new Rect (300, 0, 100, 30);
		Rect calculateDistance = new Rect (400, 0, 100, 30);
		Rect mode = new Rect (500, 0, 100, 30);
		//
		bool isCalculateDistance = false;
		string startIndex = "0";
		string endIndex = "0";
		string race = "0";
		Rect startField = new Rect (250, 35, 40, 20);
		Rect endField = new Rect (350, 35, 40, 20);
		Rect raceField = new Rect (450, 35, 40, 20);
		Rect startLabel = new Rect (200, 35, 50, 30);
		Rect endLabel = new Rect (300, 35, 50, 30);
		Rect raceLabel = new Rect (400, 35, 50, 30);
		Rect getDistance = new Rect (500, 30, 100, 30);
		//
		int i;
		int fontSize;
		bool isMoveMode = true;
		bool isEventUsedBeforeHandle, isEventUsedByHandle;
		int controlIDBeforeHandle, controlIDAfterHandle;
		//
		GUIStyle currentStyle = null;

		public void OnSceneGUI ()
		{
				Path path = target as Path;
				if (path.isDebug == true) {
						for (i=0; i<path.waypointList.Length; i++) {

								// Get the needed data before the handle
								controlIDBeforeHandle = GUIUtility.GetControlID (GetHashCode (), FocusType.Passive);
								isEventUsedBeforeHandle = (Event.current.type == EventType.used);

								if (isMoveMode) {
										path.waypointList [i].waypointPos = 
				Handles.PositionHandle (path.waypointList [i].waypointPos, Quaternion.identity);
								} else {
										if (path.waypointList [i].useDefault == false) {
												path.waypointList [i].radius = Handles.ScaleValueHandle (path.waypointList [i].radius,
			                                                      path.waypointList [i].waypointPos, Quaternion.identity,
			                                                         path.waypointList [i].radius * 13, Handles.SphereCap, 1);
										} else {
												path.waypointList [i].radius = Handles.ScaleValueHandle (path.defaultRadius,
						                                                         path.waypointList [i].waypointPos, Quaternion.identity,
						                                                         path.waypointList [i].radius * 13, Handles.SphereCap, 1);
										}
								}

								// Get the needed data after the handle
								controlIDAfterHandle = GUIUtility.GetControlID (GetHashCode (), FocusType.Passive);
								isEventUsedByHandle = !isEventUsedBeforeHandle && (Event.current.type == EventType.used);
			
								if ((controlIDBeforeHandle < GUIUtility.hotControl &&
										GUIUtility.hotControl < controlIDAfterHandle) || isEventUsedByHandle) {
										path.SelectedWaypoint = i;
								}
						}

						Handles.BeginGUI ();

						this.initStyles ();
						GUI.Box (size, string.Empty, currentStyle);

						fontSize = GUI.skin.label.fontSize;
						GUI.skin.label.fontSize = 15;
						GUI.Label (waypointIndex, "Selected Waypoint: " + path.SelectedWaypoint);

						if (GUI.Button (insert, "Insert")) {
								path.insert ();
						}
		
						if (GUI.Button (delete, "Delete")) {
								path.delete ();
						}
			
						if (GUI.Button (calculateDistance, "Distance")) {
								isCalculateDistance = !isCalculateDistance;
						}
			
						if (isMoveMode == true) {
								if (GUI.Button (mode, "Mode: move")) {
										isMoveMode = false;
								}
						} else {
								if (GUI.Button (mode, "Mode: scale")) {
										isMoveMode = true;
								}
						}
			
						if (isCalculateDistance == true) {
								GUI.Box (distanceSize, string.Empty, currentStyle);

								GUI.Label (startLabel, "Start");
								startIndex = GUI.TextField (startField, startIndex);

								GUI.Label (endLabel, "End");
								endIndex = GUI.TextField (endField, endIndex);

								GUI.Label (raceLabel, "Round");
								race = GUI.TextField (raceField, race);
				
								if (GUI.Button (getDistance, "Calculate")) {
										try {
												int sIndex = Int32.Parse (startIndex);
												int dIndex = Int32.Parse (endIndex);
												int raceIndex = Int32.Parse (race);

												EditorUtility.DisplayDialog ("Result!", "Distance between waypoint " + sIndex +
														" and waypoint " + dIndex + " (" + raceIndex + " rounds) is " +
														path.getDistance (sIndex, dIndex, raceIndex), "OK");
										} catch (Exception exception) {
												EditorUtility.DisplayDialog ("An error has occured!", 
						                           "Exception: " + exception.Message +
														"\nOpen Unity console window for more details.", "OK");
										}
								}
						}
			
						GUI.skin.label.fontSize = fontSize;
			
						Handles.EndGUI ();
				}
		}
	
		void initStyles ()
		{
				if (currentStyle == null) {
						currentStyle = new GUIStyle (GUI.skin.box);
						currentStyle.normal.background = makeTex (2, 2, new Color (0.5f, 0.5f, 0.5f, 0.9f));
						currentStyle.normal.background.hideFlags = HideFlags.HideAndDontSave;
				}
		}

		Texture2D makeTex (int width, int height, Color col)
		{
				Color[] pix = new Color[width * height];
				for (int i = 0; i < pix.Length; ++i) {
						pix [i] = col;
				}

				Texture2D result = new Texture2D (width, height);
				result.SetPixels (pix);
				result.hideFlags = HideFlags.HideAndDontSave;
				result.Apply ();
				return result;
		}
}
