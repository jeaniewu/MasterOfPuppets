using UnityEngine;
using System.Collections;

public class jailManager : MonoBehaviour {

	public GameObject[] jails;

	// Use this for initialization
	void Start () {
		jails = GameObject.FindGameObjectsWithTag ("Jail");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void disableJail(){
		foreach(GameObject jail in jails){
			jail.GetComponent<BoxCollider2D>().enabled = false;
		}
	}

	public void enableJail(){
		foreach(GameObject jail in jails){
			jail.GetComponent<BoxCollider2D>().enabled = true;
		}
	}
}
