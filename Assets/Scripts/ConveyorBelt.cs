using UnityEngine;
using System.Collections;

public class ConveyorBelt : MonoBehaviour {

	public string direction;
	public int speed;

	private bool isSwitched;
	private ConveyorBeltPiece[] pieces;

    void Start() {
		pieces = GetComponentsInChildren<ConveyorBeltPiece> ();
    }

    void OnTriggerStay2D(Collider2D other) {
		if (other.CompareTag("Doll") || other.CompareTag("Player")){
			ConveyorUpdate convUpdate = other.GetComponent<ConveyorUpdate> ();
			if (direction == "up" && !convUpdate.conveyorMovedU) {
				other.gameObject.transform.position += new Vector3 (0, 1, 0) * Time.deltaTime * speed;
				convUpdate.conveyorMovedU = true;
			} else if (direction == "down" && !convUpdate.conveyorMovedD) {
				other.gameObject.transform.position += new Vector3 (0, -1, 0) * Time.deltaTime * speed;
				convUpdate.conveyorMovedD = true;
			} else if (direction == "right" && !convUpdate.conveyorMovedR) {
				other.gameObject.transform.position += new Vector3 (1, 0, 0) * Time.deltaTime * speed;
				convUpdate.conveyorMovedR = true;
			} else if (direction == "left" && !convUpdate.conveyorMovedL) {
				other.gameObject.transform.position += new Vector3 (-1, 0, 0) * Time.deltaTime * speed;
				convUpdate.conveyorMovedL = true;
			}
		}
	}

    public void setConveyorBeltDirection(bool switched) {
        if (isSwitched != switched) {
            isSwitched = switched;
            switchAllAnimations();
            swapDirection();
        }
    }

	private void switchAllAnimations(){
		foreach (ConveyorBeltPiece piece in pieces) {
			piece.switchAnimation();
		}
	}

    private void swapDirection() {
        if (direction.Equals("right")) {
            direction =  "left";
        } else if (direction.Equals("left")) {
            direction =  "right";
        } else if (direction.Equals("up")) {
            direction =  "down";
        } else {
            direction =  "up";
        }
    }
		
}
