using UnityEngine;
using System.Collections;

public class generateExit : MonoBehaviour {

	public GameObject[] triggers;
	public GameObject checkpoint;

	// Use this for initialization
	void Start () {
		triggers = GameObject.FindGameObjectsWithTag ("Trigger");
		checkpoint = GameObject.FindGameObjectWithTag ("checkpoint");
		checkpoint.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void disableExit(){
		checkpoint.SetActive(false);
	}
	
	public void enableExit(){
		checkpoint.SetActive(true);
	}
}
