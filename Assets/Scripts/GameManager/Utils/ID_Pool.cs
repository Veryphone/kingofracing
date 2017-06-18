using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ID_Pool
{
		public const int EMPTY = -1;
		private List<int> idList;

		public ID_Pool (int size)
		{
				idList = new List<int> (size);

				for (int i=0; i<idList.Capacity; i++) {
						idList.Add (i);
				}
		}

		public void addID (int id)
		{
				if (idList.Contains (id) == false) {
						idList.Add (id);
				}
		}

		public void removeID (int id)
		{
				idList.Remove (id);
		}

		public int allocateID ()
		{
				if (idList.Count > 0) {
						int id = idList [Random.Range (0, idList.Count)];
						this.removeID (id);

						return id;
				} else {
						return -1;
				}
		}
}
