using UnityEngine;
using System.Collections;

public class ImageButton
{
		public delegate void ClickListener ();

		public static float CLICK_TIME = 0.3f;
		//
		private Texture up;
		private Texture down;
		private Rect bound;
		private GUIStyle style = new GUIStyle ();
		//
		public ClickListener onClick;
		private bool isClicked = false;
		//
		private float stateTime;
		private float lastClickTime;

		public ImageButton (Texture up, Rect bound)
		{
				this.up = up;
				this.bound = bound;
				this.onClick = defaultOnClick;
		}

		public ImageButton (Texture up, Texture down, Rect bound)
		{
				this.up = up;
				this.down = down;
				this.bound = bound;
				this.onClick = defaultOnClick;
		}

		public void draw ()
		{
				this.stateTime += Time.fixedDeltaTime;
				if (this.isClicked == false) {
						if (GUI.Button (bound, up, style)) {
								this.isClicked = true;
								this.lastClickTime = this.stateTime;
						}
				} else {
						if (down != null) {
								GUI.Button (bound, down, style);
						} else {
								GUI.Button (bound, up, style);
						}

						if (this.stateTime - this.lastClickTime > CLICK_TIME) {
								if (onClick != null) {
										onClick ();
								}

								this.isClicked = false;
						}
				}
		}

		public void defaultOnClick ()
		{
				Debug.Log ("On Click");
		}
}
