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
		Debug.Log ("activate");
		if (GetComponent<Floor> ().floorNumber== 1) {
			GetComponent<Floor> ().floorNumber ++;
		} else if (GetComponent<Floor> ().floorNumber == 2) {
			GetComponent<Floor> ().floorNumber --;
		}
	}
}
