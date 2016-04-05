using UnityEngine;
using System.Collections;

public class Trigger : Interact {

	public GameObject[] toSwitches;

	public bool isOn;
	public string specialDoll;


	void Start () {
		isOn = false;
		specialDoll = "normal";
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void switchTriggerOn(){
		isOn = true;
		Debug.Log ("on");
		foreach (GameObject toSwitch in toSwitches)
			toSwitch.GetComponent<receiveSignal>().activate ();
	}

	public void switchTriggerOff(){
		isOn = false;
		Debug.Log ("off");
		foreach (GameObject toSwitch in toSwitches)
			toSwitch.GetComponent<receiveSignal>().deactivate ();
	}

	public override void interact(){
		switchTrigger ();
	}

	public virtual void switchTrigger(){
		if (isOn) {
			switchTriggerOff ();
		} else if (!isOn) {
			switchTriggerOn ();
		}
	}
}
