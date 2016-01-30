using UnityEngine;
using System.Collections;

public class buttonTrigger : MonoBehaviour {

	public GameObject levelManager;
	// Use this for initialization
	void Start () {
		levelManager = GameObject.FindGameObjectWithTag ("LevelManager");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay2D(Collider2D other) {
		levelManager.GetComponent<jail> ().disableJail ();
	}

	void OnTriggerExit2D(Collider2D other) {
		levelManager.GetComponent<jail> ().enableJail ();
	}
}
