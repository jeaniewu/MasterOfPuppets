using UnityEngine;
using System.Collections;

public class ConveyorBeltPiece : MonoBehaviour {

	public string direction;

	public int conveyorBeltPart;

	private Animator animator;

	void Awake() {
		animator = GetComponent<Animator>();
	}
		
	void Start () {
		direction = GetComponentInParent<ConveyorBelt> ().direction;
		animator.SetInteger("BeltPart", conveyorBeltPart);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void switchAnimation() {
		direction = GetComponentInParent<ConveyorBelt> ().direction;
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
