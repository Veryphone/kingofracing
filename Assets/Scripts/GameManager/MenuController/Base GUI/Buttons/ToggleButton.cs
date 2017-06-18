using UnityEngine;
using System.Collections;

public class ToggleButton
{
		public delegate void ChangeListener ();

		private Texture on;
		private Texture off;
		//
		private Rect bound;
		private GUIStyle style = new GUIStyle ();
		//
		public ChangeListener toggleOn;
		public ChangeListener toggleOff;
		private bool isOn = false;

		public ToggleButton (Texture on, Rect bound)
		{
				this.on = on;
				this.bound = bound;
				this.toggleOn = defaultToggleOn;
				this.toggleOff = defaultToggleOff;
		}
	
		public ToggleButton (Texture on, Texture off, Rect bound)
		{
				this.on = on;
				this.off = off;
				this.bound = bound;
				this.toggleOn = defaultToggleOn;
				this.toggleOff = defaultToggleOff;
		}

		public void setChecked (bool isOn)
		{
				this.isOn = isOn;
		}

		public void draw ()
		{
				if (this.isOn == false) {
						if (off != null) {
								if (GUI.Button (bound, off, style)) {
										this.isOn = true;
					
										if (toggleOn != null) {
												toggleOn ();
										}
								}
						} else {
								if (GUI.Button (bound, on, style)) {
										this.isOn = true;
					
										if (toggleOn != null) {
												toggleOn ();
										}
								}
						}
				} else {
						if (GUI.Button (bound, on, style)) {
								this.isOn = false;
				
								if (toggleOff != null) {
										toggleOff ();
								}
						}
				}
		}

		public void defaultToggleOn ()
		{
				Debug.Log ("Toggle On");
		}
	
		public void defaultToggleOff ()
		{
				Debug.Log ("Toggle Off");
		}
}
