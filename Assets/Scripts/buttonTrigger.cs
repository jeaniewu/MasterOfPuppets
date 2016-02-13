using UnityEngine;
using System.Collections;

public class buttonTrigger : Trigger {


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay2D(Collider2D other) {
		if (other.CompareTag ("Player") || other.CompareTag ("Doll")) {
			if (other.gameObject.GetComponent<dollType>().type.Equals(specialDoll))
				switchTriggerOn ();
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.CompareTag ("Player") || other.CompareTag ("Doll")) {
			switchTriggerOff ();
		}
	}


}
