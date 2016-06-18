using UnityEngine;
using System.Collections;

public class ConveyorUpdate : MonoBehaviour {
	
	public bool conveyorMovedL = false;
	public bool conveyorMovedU = false;
	public bool conveyorMovedD = false;
	public bool conveyorMovedR = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		conveyorMovedR = false;
		conveyorMovedL = false;
		conveyorMovedU = false;
		conveyorMovedD = false;
	}
}
