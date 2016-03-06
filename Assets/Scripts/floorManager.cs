using UnityEngine;
using System.Collections;

public class floorManager : MonoBehaviour {

	public GameObject[] floors;
	public int curFloorNum;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

	public void setCurrentFloor(int num){
		if (num != curFloorNum) {
			curFloorNum = num;
			manageAccess();
		}
	}


	public void manageAccess(){
		foreach (GameObject floor in floors) {
			if (floor.GetComponent<Floor>().floorNumber != curFloorNum){
				floor.GetComponent<Collider2D>().isTrigger = false;
				floor.GetComponent<Renderer>().enabled = false;
				floor.GetComponent<Floor>().disableAll();
			} else {
				floor.GetComponent<Collider2D>().isTrigger = true;
				floor.GetComponent<Renderer>().enabled = true;
				floor.GetComponent<Floor>().enableAll();
			}
		}
	}


	                 
}
