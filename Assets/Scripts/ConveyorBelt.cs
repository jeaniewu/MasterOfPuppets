using UnityEngine;
using System.Collections;

public class ConveyorBelt : MonoBehaviour {

	public string direction;
	public float speed;

	private bool isSwitched;
	private ConveyorBeltPiece[] pieces;
    private DollManager.Boundary boundary;

    void Start() {
		pieces = GetComponentsInChildren<ConveyorBeltPiece> ();
        boundary = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<DollManager>().boundary;
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
            Rigidbody2D rigidBody = other.GetComponent<Rigidbody2D>();
            rigidBody.position = new Vector3
            (
                Mathf.Clamp(rigidBody.position.x, boundary.xMin, boundary.xMax),
                Mathf.Clamp(rigidBody.position.y, boundary.yMin, boundary.yMax),
                0.0f
                );
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
