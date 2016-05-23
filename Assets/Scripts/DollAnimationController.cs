using UnityEngine;
using System.Collections;

public class DollAnimationController : MonoBehaviour {

	private Animator anim;

	// Use this for initialization
	void Awake () {
		anim = GetComponent<Animator> ();
		if (CompareTag ("Doll")) {
			anim.SetBool ("hasSoul", false);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Slice(){
		anim.SetTrigger ("Sliced");
	}
}
