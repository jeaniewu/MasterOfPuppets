using UnityEngine;
using System.Collections;

public class receiveSignalConveyorBelt : receiveSignal {

	private string direction;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void activate(){
		direction = GetComponent<ConveyorBelt> ().direction;
		if (!GetComponent<ConveyorBelt> ().isDirChange) {
			GetComponent<ConveyorBelt> ().direction = swap (direction);
			GetComponent<ConveyorBelt> ().isDirChange = true;
		}
	}

	public override void deactivate(){
		direction = GetComponent<ConveyorBelt> ().direction;
		if (GetComponent<ConveyorBelt> ().isDirChange) {
			GetComponent<ConveyorBelt> ().direction = swap (direction);
			GetComponent<ConveyorBelt> ().isDirChange = false;
		}
	}

	private string swap(string dir){
		if (dir.Equals ("right")) {
			return "left";
		} else if (dir.Equals ("left")) {
			return "right";
		} else if (dir.Equals ("up")) {
			return "down";
		} else {
			return "up";
		}
	}
}
