using UnityEngine;
using System.Collections;

public class buttonTrigger : Trigger {


	// Use this for initialization
	void Start () {
		//levelManager = GameObject.FindGameObjectWithTag ("LevelManager");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay2D(Collider2D other) {
		switchTriggerOn ();
	}

	void OnTriggerExit2D(Collider2D other) {
		switchTriggerOff ();
//		if (trigger == "jail") {
//			levelManager.GetComponent<jailManager> ().enableJail ();
//		} else if (trigger == "exit") {
//			levelManager.GetComponent<generateExit> ().disableExit ();
//		}
	}


}
