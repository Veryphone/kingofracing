using UnityEngine;
using System.Collections;

public abstract class BaseMenu
{
		protected MenuRenderer menuRenderer;

		public BaseMenu (MenuRenderer menuRenderer)
		{
				this.menuRenderer = menuRenderer;
		}

		public abstract void render ();
}
