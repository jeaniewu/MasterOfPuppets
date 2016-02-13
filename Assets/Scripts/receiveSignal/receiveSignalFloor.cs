using UnityEngine;
using System.Collections;

public class receiveSignalFloor : receiveSignal {
	

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	}
	
	public override void activate(){
		if (GetComponent<Floor> ().floorNumber== 1) {
			Invoke ("ascend", 2f);
		} else if (GetComponent<Floor> ().floorNumber == 2) {
			Invoke ("descend", 2f);
		}
	}

	void ascend(){
		Debug.Log ("goo");
		GetComponent<Floor> ().ascend();
	}

	void descend(){
		GetComponent<Floor> ().descend();
	}
}
