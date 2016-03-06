using UnityEngine;
using System.Collections;

public class ConveyorBelt : MonoBehaviour {

	public string direction;
	public bool isDirChange;
	public int speed;

	// Use this for initialization
	void Start () {
		isDirChange = false;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerStay2D(Collider2D other) {
		if (other.CompareTag("Doll") || other.CompareTag("Player")){
			if (direction == "up"){
				other.gameObject.transform.position += new Vector3 (0, 1,0) * Time.deltaTime * speed;
			}else if (direction == "down") {
				other.gameObject.transform.position += new Vector3 (0, -1,0)* Time.deltaTime * speed;
			}else if (direction == "right") {
				other.gameObject.transform.position += new Vector3 (1, 0,0)* Time.deltaTime * speed;
			}else if (direction == "left") {
				other.gameObject.transform.position += new Vector3 (-1, 0,0)* Time.deltaTime * speed;
			}
		}

	}
}
