using UnityEngine;
using System.Collections;

public class MapProfile : Profile
{
		public static string LAST_REWARD_TAG = "LastReward";
		//
		private int ID;
		private int lastReward;

		public int LastReward {
				get {
						return lastReward;
				}
				set {
						if (this.lastReward < value) {
								this.lastReward = value;
								this.setInt (LAST_REWARD_TAG + "_" + ID, this.lastReward);
						}
				}
		}
	
		public MapProfile (int id)
		{
				this.ID = id;
		}
	
		public override void saveDefaultValue ()
		{
				this.lastReward = 0;
				this.setInt (LAST_REWARD_TAG + "_" + ID, this.lastReward);
		}
	
		public override void load ()
		{
				this.lastReward = this.getInt (LAST_REWARD_TAG + "_" + ID);
		}
}
