using UnityEngine;
using System.Collections;

public class receiveSignalLockedDoor : receiveSignal {

    private Animator animator;
    //Currently I am just activating/deactivating a bool that must be on in order to swtich scenes. There is probably
    //a better way to do this though.
	private Collider2D collider;

    public switchScene doorToNextLevel;
    public bool opensUponTrigger = true;

    void Start() {
        animator = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
        if (opensUponTrigger == false) {
            openDoor();
        }
    }


    public override void activate() {
       if (opensUponTrigger == true) {
            openDoor();
        } else {
            closeDoor();
        }
    }

    public override void deactivate() {
        if (opensUponTrigger == true) {
            closeDoor();
        } else {
            openDoor();
        }
    }

    private void openDoor() {
        animator.SetBool("open", true);
        collider.enabled = false;
        if (doorToNextLevel != null) {
            doorToNextLevel.setOpen(true);
        }
    }

    private void closeDoor() {
        animator.SetBool("open", false);
        collider.enabled = true;
        if (doorToNextLevel != null) {
            doorToNextLevel.setOpen(false);
        }
    }
}
