using UnityEngine;
using System.Collections;

public class UITriggerForOneB : UITrigger {

    public SpriteRenderer[] interactButtons;

    public override void interact() {
        //First show the text to show that you have picked up the letter
        disableInteractUI();
        manager.EnableTextBox();
        StartCoroutine(WaitForTextBoxInput());
    }

    //Helper for the interaction to explain the secret item
    IEnumerator WaitForTextBoxInput() {
        //Wait half a second so the current interact press doesn't cause the secret item to show.
        yield return new WaitForSeconds(0.5f);
        gameObject.layer = 0;
        //We wait until the next interact press
        while (!Input.GetButtonDown("Interact")) {
            yield return null;
            Debug.Log("Still waiting");
        }
        // Disable the textbox while we show the secret item
        manager.DisableTextBox();
        yield return enableSecretMessage();

        //After we show the secret item, resume the textbox to explain the notebook feature
        manager.EnableTextBox();
        manager.currentLine = 2; //we are setting the current line to 3 because the pressed interact causes the text box manager to proceed to line 4, which is the line we want.
        Debug.Log("Finished Secret MEssage()");
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
