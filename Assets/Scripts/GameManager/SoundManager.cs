using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
		public AudioSource brake;
		public AudioSource countDown;
		public AudioSource engine;
		public AudioSource getItem;

		public void playBrake ()
		{
				if (brake.isPlaying == false) {
						brake.volume = ProfileManager.setttings.SoundVolume / 100f;
						brake.Play ();
				}
		}

		public void playCountDown ()
		{
				if (countDown.isPlaying == false) {
						countDown.volume = (ProfileManager.setttings.SoundVolume / 100f) * 0.5f;
						countDown.Play ();
				}
		}

		public void playEngine ()
		{	
				if (engine.isPlaying == false) {
						engine.volume = ProfileManager.setttings.SoundVolume / 100f;
						engine.Play ();
				}
		}

		public void stopEngine ()
		{
				engine.Stop ();
		}

		public void playItem ()
		{
				if (getItem.isPlaying == false) {
						getItem.volume = ProfileManager.setttings.SoundVolume / 100f;
						getItem.Play ();
				}
		}
}
