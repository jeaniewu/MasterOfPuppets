using UnityEngine;
using System.Collections;

public class UITriggerForOneB : UITrigger {

    public SpriteRenderer[] interactButtons;

    public override void interact() {
        disableInteractUI();
        manager.EnableTextBox();
        StartCoroutine(WaitForTextBoxInput());
    }

    IEnumerator WaitForTextBoxInput() {
        yield return new WaitForSeconds(0.5f);
        gameObject.layer = 0;
        bool textBoxClosed = false;
        while (!Input.GetButtonDown("Interact")) {
            yield return null;
            Debug.Log("Still waiting");
        }
        yield return enableSecretMessage();
    }

    //Get rid of the tutorial interact UI
    private void disableInteractUI() {
        gameObject.layer = 0;
        //disable all the interact sprites
        foreach(SpriteRenderer interactButton in interactButtons) {
            interactButton.enabled = false;
        }
    }
}
