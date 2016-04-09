using UnityEngine;
using System.Collections;

public class ConveyorBelt : MonoBehaviour {

	public string direction;
	public int speed;
    private bool isSwitched;

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

    public void setConveyorBeltDirection(bool switched) {
        if (isSwitched != switched) {
            isSwitched = switched;
            direction = swap(direction);
        }
    }

    private string swap(string dir) {
        if (dir.Equals("right")) {
            return "left";
        } else if (dir.Equals("left")) {
            return "right";
        } else if (dir.Equals("up")) {
            return "down";
        } else {
            return "up";
        }
    }
}
