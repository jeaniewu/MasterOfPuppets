using UnityEngine;
using System.Collections;

public class Lever : MonoBehaviour {

	public bool leverSwitch;


	void Start () {
		leverSwitch = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void switchLever(){
		leverSwitch = !leverSwitch;
	}
}
