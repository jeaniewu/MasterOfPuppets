using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class ControllerNotesInput : MonoBehaviour {

    public InputField lastWordInput;
    public Button[] noteButtons;
    public Button doneInput;

    private Stack<int> registeredIndices = new Stack<int>(); //Keep a record of registered indices, so that we can push the cancel button to delete the text
    private bool isInputDone = false;
    private string[] letters = new string[] { "A", "N", "N", "I", "E" };


    void Update() {
        Debug.Log("Update is running");
        //If B is pressed then we should cancel a letter
        if (Input.GetButtonDown("ghostMode")) {
            Debug.Log("Canceling");
            if (registeredIndices.Count != 0) {
                cancelLetter(registeredIndices.Pop());
            }
        }
    }

    //Getter used to check if the player is done entering input with the notes.
    // Used from the story scene manager. 
    public bool getIsInputDone() { return isInputDone; }

    //Used when the Done? button is clicked
    public void setInputDone () {
        isInputDone = true;
    }

    public void enterLetter(int index) {
        //concat the letter to the input text
        lastWordInput.text += letters[index];
        //disable the input button so that the user cannot use the same input twice 
        noteButtons[index].interactable = false;
        //We should select the next note as the selected buttonk, before disabling the current button
        bool foundSelectableNote = false;
        for (int i=index; i<5 && !foundSelectableNote; i++) {
            if (noteButtons[i].IsActive() && noteButtons[i].interactable) {
                noteButtons[i].Select(); //select the next note
                foundSelectableNote = true;
            }
        }
        if (!foundSelectableNote) {
            doneInput.Select(); //If a button cannot be found, select the done button
        }
        
        //Add this index to the list of registered indices
        registeredIndices.Push(index);
    }

    public void cancelLetter(int index) {
        lastWordInput.text = lastWordInput.text.Remove(lastWordInput.text.Length - 1); //remove the last letter
        noteButtons[index].interactable = true; //set that button you cancelled to be active again
    }
}
