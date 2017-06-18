using UnityEditor;
using UnityEngine;

public class PathfindingMenu : MonoBehaviour
{
		[MenuItem("AI Extension/Pathfinding/Waypoint/Create Path")]
		static void Waypoint ()
		{
				GameObject path = GameObject.CreatePrimitive (PrimitiveType.Cube);
				path.name = "Path created at " + System.DateTime.Now;
				path.isStatic = true;
		
				Component[] comp = path.GetComponents (typeof(Component));
				for (int i=1; i<comp.Length; i++) {
						GameObject.DestroyImmediate (comp [i]);
				}
				path.AddComponent<Path> ();
		}
	
		[MenuItem("AI Extension/Pathfinding/Waypoint/Add Waypoint Agent Component",true)]
		static bool ValidateWaypointAgentSelected ()
		{
				return Selection.activeTransform != null;
		}
	
		[MenuItem("AI Extension/Pathfinding/Waypoint/Add Waypoint Agent Component")]
		static void AddAgent ()
		{
				if (Selection.activeGameObject != null) {
						if (Selection.activeGameObject.GetComponent<PathfindingAgent> () != null) {
								if (EditorUtility.DisplayDialog ("Multiple Pathfinding Agents!", 
				                                 "This Game object already contains a Pathfinding Agent component. " +
										"Multiple Pathfinding Agents inside 1 Game object can cause strange behaviours. " +
										"Do you still want to add another Pathfinding Agent?",
				                                 "Add", "Do not add")) {
										Selection.activeGameObject.AddComponent<WaypointAgent> ();
								}
						} else {
								Selection.activeGameObject.AddComponent<WaypointAgent> ();
						}
				}
		}
}