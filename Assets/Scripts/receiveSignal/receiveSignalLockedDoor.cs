using UnityEngine;
using System.Collections;

public class receiveSignalLockedDoor : receiveSignal {

    private Animator animator;
    //Currently I am just activating/deactivating a bool that must be on in order to swtich scenes. There is probably
    //a better way to do this though.
    public switchScene doorToNextLevel;

    void Start() {
        animator = GetComponent<Animator>();
    }


    public override void activate() {
        animator.SetBool("open", true);
        doorToNextLevel.setOpen(true);
        
    }

    public override void deactivate() {
        animator.SetBool("open", false);
        doorToNextLevel.setOpen(false);
    }
}
