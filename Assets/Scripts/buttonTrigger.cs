using UnityEngine;
using System.Collections;

public class buttonTrigger : MonoBehaviour {

	public GameObject levelManager;

	private bool triggered;

	// Use this for initialization
	void Start () {
		levelManager = GameObject.FindGameObjectWithTag ("LevelManager");
		triggered = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay2D(Collider2D other) {
		levelManager.GetComponent<jailManager> ().disableJail ();
		triggered = true;
	}

	void OnTriggerExit2D(Collider2D other) {
		levelManager.GetComponent<jailManager> ().enableJail ();
		triggered = false;
	}

	public bool isTriggered(){
		return triggered;
	}
}
