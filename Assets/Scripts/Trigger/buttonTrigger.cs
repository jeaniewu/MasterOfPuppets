using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class buttonTrigger : Trigger {


	public List<GameObject> objects = new List<GameObject> ();

	public Sprite buttonPressed;
	public Sprite buttonUnpressed;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.CompareTag ("Player") || other.CompareTag ("Doll")) {
			if (!objects.Contains (other.gameObject)) 
				objects.Add (other.gameObject);
			switchTriggerOn ();
            MechanicAudioManager.getInstance().playButtonSound();

            GetComponent<SpriteRenderer>().sprite = buttonPressed;
		}
	}
		

	void OnTriggerExit2D(Collider2D other) {
		if (other.CompareTag ("Player") || other.CompareTag ("Doll")) {
			objects.Remove (other.gameObject);
			if (objects.Count == 0) {
				switchTriggerOff ();
				GetComponent<SpriteRenderer>().sprite = buttonUnpressed;
			}
		}
	}


}
