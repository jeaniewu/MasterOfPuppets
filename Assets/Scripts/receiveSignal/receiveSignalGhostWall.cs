using UnityEngine;
using System.Collections;

public class receiveSignalGhostWall : receiveSignal {

	private ParticleSystem particleSystem;
	private ParticleSystemRenderer renderer;

	void Start(){
		particleSystem = GetComponentInChildren<ParticleSystem> ();
		renderer = particleSystem.GetComponent<ParticleSystemRenderer> ();

	}

	public override void activate(){
		if (GetComponent<Collider2D> () != null)
			GetComponent<Collider2D> ().enabled = false;
		
		renderer.enabled = false;
	}

	public override void deactivate(){
		if (GetComponent<Collider2D> () != null)
			GetComponent<Collider2D> ().enabled = true;
		
		renderer.enabled = true;
	}

}
