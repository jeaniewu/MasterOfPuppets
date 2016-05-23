using UnityEngine;
using System.Collections;

public class DollManager : MonoBehaviour {

	[System.Serializable]
	public class Boundary {
		//create a separate class to make the code useable
		public float xMin, xMax, yMin, yMax;
	}

	public float maxSpeed = 10f;
	public bool ghostMode = false;
	public int radius;
	public Boundary boundary;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        
	
	}

	public void Death(GameObject doll){
		doll.transform.position = new Vector3 (1000, 1000, 1000);
		Object.Destroy (doll, 1);
	}
}
