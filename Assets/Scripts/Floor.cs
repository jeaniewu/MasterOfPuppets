using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Floor : MonoBehaviour {

	public int floorNumber;
	public string floorType;

	public GameObject levelManager;
	public List<GameObject> objects = new List<GameObject> ();

	public bool hasPlayer;
	public bool isHeavy;
	// Use this for initialization
	void Start () {
		GetComponent<Renderer>().sortingLayerName = "Background";
		GetComponent<Renderer>().sortingOrder = -1;
		levelManager = GameObject.FindGameObjectWithTag ("LevelManager");
		hasPlayer = false;
		isHeavy = false;
	}
	
	// Update is called once per frame
	void Update () {
		hasPlayer = false;
		foreach (GameObject o in objects){
			if (o.CompareTag("Player")){
				levelManager.GetComponent<floorManager> ().setCurrentFloor (floorNumber);
				hasPlayer = true;
			} 
		}

		
	}

	void OnTriggerEnter2D(Collider2D other){
		if (!objects.Contains (other.gameObject))
			objects.Add (other.gameObject);
	}

	void OnTriggerStay2D(Collider2D other) {
		if ((other.CompareTag("Doll") || other.CompareTag("Player")) 
		    && other.GetComponent<dollType>().type.Equals("heavy") 
		    && !floorType.Equals("mechanical")){

			if (other.GetComponent<dollType>().type.Equals("heavy"))
				isHeavy = true;
			Invoke ("descend", 2f);
		}

	}

	void OnTriggerExit2D(Collider2D other) {
		if ((other.CompareTag("Doll") || other.CompareTag("Player")) 
		    && other.GetComponent<dollType>().type.Equals("heavy")
		    && !floorType.Equals("mechanical")){
			if (other.GetComponent<dollType>().type.Equals("heavy"))
				isHeavy = false;
			Invoke ("ascend", 2f);
		}
		if (other.CompareTag("Player")){
			hasPlayer = false;
		}
		objects.Remove (other.gameObject);
	}

	public void descend(){
		if ((isHeavy && !floorType.Equals ("mechanical")) || floorType.Equals ("mechanical")) {
			floorNumber = 1;
			manageFloor ();
		}
	}

	public void ascend(){
		if ((!isHeavy && !floorType.Equals ("mechanical")) || floorType.Equals ("mechanical")) {
			floorNumber = 2;
			manageFloor ();
		}
	}

	void manageFloor(){
		if (hasPlayer) {
			levelManager.GetComponent<floorManager> ().setCurrentFloor (floorNumber);
		} else {
			levelManager.GetComponent<floorManager> ().manageAccess ();
		}
	}

	public void disableAll(){
		foreach (GameObject o in objects){
			o.SetActive(false);
		}
	}

	public void enableAll(){
		foreach (GameObject o in objects){
			o.SetActive(true);
		}
	}

}
