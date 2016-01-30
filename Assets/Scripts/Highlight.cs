using UnityEngine;
using System.Collections;

public class Highlight : MonoBehaviour {

	public bool higlighted = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		checkHighlighted ();
	}

	void checkHighlighted(){

		if (higlighted) {
			this.GetComponent<SpriteRenderer> ().color = Color.black;
		} else {
			this.GetComponent<SpriteRenderer> ().color = Color.white;
		}
	}
}
