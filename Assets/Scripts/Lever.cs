using UnityEngine;
using System.Collections;

public class Lever : MonoBehaviour {

	private bool isOn;
	private Animator anim;

	// Use this for initialization
	void Start () {
		isOn = false;
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		isOn = GetComponent<Trigger> ().isOn;
		anim.SetBool ("isOn", isOn);
	}
}
