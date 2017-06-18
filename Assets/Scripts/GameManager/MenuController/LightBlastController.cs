using UnityEngine;
using System.Collections;

public class LightBlastController : MonoBehaviour
{
	public ParticleSystem holyBlast;
	public ParticleSystem glow;
	public ParticleSystem ray;
	public ParticleSystem sparkles;
	public ParticleSystem ring;

	void Start ()
	{
		holyBlast.playbackSpeed = 3;
		glow.playbackSpeed = 3;
		ray.playbackSpeed = 3;
		sparkles.playbackSpeed = 3;
		ring.playbackSpeed = 3;
		
		holyBlast.Stop ();
		glow.Stop ();
		ray.Stop ();
		sparkles.Stop ();
		ring.Stop ();
	}

	public void activate ()
	{
		if (holyBlast.isStopped) {
			holyBlast.time = 0;
			glow.time = 0;
			ray.time = 0;
			sparkles.time = 0;
			ring.time = 0;

			holyBlast.Play ();
			glow.Play ();
			ray.Stop ();
			sparkles.Play ();
			ring.Play ();
		}
	}
}
