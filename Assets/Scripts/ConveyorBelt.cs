using UnityEngine;
using System.Collections;

public class ConveyorBelt : MonoBehaviour {

	public string direction;
	public int speed;
    public int conveyorBeltPart;

    private bool isSwitched;
    private Animator animator;

    void Awake() {
        animator = GetComponent<Animator>();
    }

    void Start() {
        animator.SetInteger("BeltPart", conveyorBeltPart);
        if (direction == "right" || direction == "down") {
            transform.Rotate(0, 0, 180f);
        }
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

    public void setConveyorBeltDirection(bool switched) {
        if (isSwitched != switched) {
            isSwitched = switched;
            switchAnimation();
            swapDirection();
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

    private void switchAnimation() {
        switch(conveyorBeltPart) {
            case 1:
                conveyorBeltPart = 3;
                break;
            case 2:
                conveyorBeltPart = 2;
                //Need to restart animation for 2 as it actually does not switch animations
                animator.Play("ConveyorBeltAnimation2", -1, 0f);
                break;
            case 3:
                conveyorBeltPart = 1;
                break;
        }
        animator.SetInteger("BeltPart", conveyorBeltPart);
        transform.Rotate(0, 0, 180f);
    }
}
