using UnityEngine;
using System.Collections;

public class receiveSignalGhostWall : receiveSignal {

    public GameObject ghostWallAudio;
    private ParticleSystem particleSystem;
	private ParticleSystemRenderer renderer;
   

	void Start(){
		particleSystem = GetComponentInChildren<ParticleSystem> ();
		renderer = particleSystem.GetComponent<ParticleSystemRenderer> ();
	}

	public override void activate(){
		if (GetComponent<Collider2D> () != null)
			GetComponent<Collider2D> ().enabled = false;

        ghostWallAudio.SetActive(false);
		renderer.enabled = false;
	}

	public override void deactivate(){
		if (GetComponent<Collider2D> () != null)
			GetComponent<Collider2D> ().enabled = true;

        ghostWallAudio.SetActive(true);
		renderer.enabled = true;
	}

}
