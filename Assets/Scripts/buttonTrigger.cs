using UnityEngine;
using System.Collections;

public class buttonTrigger : MonoBehaviour {

	public GameObject levelManager;

	public string trigger;


	// Use this for initialization
	void Start () {
		levelManager = GameObject.FindGameObjectWithTag ("LevelManager");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay2D(Collider2D other) {
		if (trigger == "jail") {
			levelManager.GetComponent<jailManager> ().disableJail ();
		} else if (trigger == "exit") {
			levelManager.GetComponent<generateExit> ().enableExit ();
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (trigger == "jail") {
			levelManager.GetComponent<jailManager> ().enableJail ();
		} else if (trigger == "exit") {
			levelManager.GetComponent<generateExit> ().disableExit ();
		}
	}


}
