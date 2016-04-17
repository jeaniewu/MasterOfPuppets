using UnityEngine;
using System.Collections;

public class receiveSignalGhostWall : receiveSignal {
	private GhostWall[] ghostWalls;

	void Start(){
		ghostWalls = GetComponentsInChildren<GhostWall>();
	}

	public override void activate(){
		foreach (GhostWall wall in ghostWalls) {
			activateChild (wall.GetComponent<Collider2D> (), wall.GetComponent<Renderer> ());
		}
	}

	public override void deactivate(){
		foreach (GhostWall wall in ghostWalls) {
			deactivateChild (wall.GetComponent<Collider2D> (), wall.GetComponent<Renderer> ());
		}
	}

	public void activateChild(Collider2D collider2D2, Renderer renderer2){

		if(collider2D2 != null){
			collider2D2.enabled = false;
		}

		if (renderer2 != null) {
			renderer2.enabled = false;
		}
	}

	public void deactivateChild(Collider2D collider2D2, Renderer renderer2){
		if (collider2D2 != null) {
			collider2D2.enabled = true;
		}
		if (renderer2!= null) {
			renderer2.enabled = true;
		}
	}
}
