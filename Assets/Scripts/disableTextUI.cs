using UnityEngine;
using System.Collections;

public class disableTextUI : MonoBehaviour {

	public float disableDelay;

	// Use this for initialization
	void Start () {
		disableText();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void disableText(){
		Invoke ("disableTextInvoke", disableDelay);
	}

	void disableTextInvoke(){
		gameObject.SetActive (false);
	}
}
