using UnityEngine;
using System.Collections;

public class receiveSignalJail : receiveSignal {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void activate(){
		if(GetComponent<Collider2D>() != null){
			GetComponent<Collider2D>().enabled = false;
		}
		GetComponent<Renderer>().enabled = false;
	}

	public override void deactivate(){
		if (GetComponent<Collider2D> () != null) {
			GetComponent<Collider2D> ().enabled = true;
		}
		GetComponent<Renderer>().enabled = true;
	}
}
