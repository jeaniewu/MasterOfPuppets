using UnityEngine;
using System.Collections;

public class receiveSignalExit : receiveSignal {

	// Use this for initialization
	void Start () {
		GetComponent<BoxCollider2D>().enabled = false;
		GetComponent<SpriteRenderer>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void activate(){
		GetComponent<Collider2D>().enabled = true;
		GetComponent<Renderer>().enabled = true;
	}

	public override void deactivate(){
		GetComponent<Collider2D>().enabled = false;
		GetComponent<Renderer>().enabled = false;
	}
}
