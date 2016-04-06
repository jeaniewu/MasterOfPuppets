using UnityEngine;
using System.Collections;

public class adjustSize : MonoBehaviour {


	// Use this for initialization
	void Start () {
		GameObject levelManager = GameObject.FindGameObjectWithTag ("LevelManager");
		int radius = levelManager.GetComponent<DollManager> ().radius;
		transform.localScale = new Vector3(radius, radius, radius);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
