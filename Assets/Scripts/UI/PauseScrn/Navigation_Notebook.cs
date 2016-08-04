using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


public class Navigation_Notebook : MonoBehaviour
{
	public GameObject PauseMenu;
    public GameObject noteBookPanel;
    public GameObject selectedNotePanel;

    public Button noteBookPanelBack;
    public Button selecteNotePanelBack;
	public Button pausePanelNoteBookButton;



    public GameObject[] smallNotes;
    public GameObject[] bigNotes;

    private int selectedNoteIndex;

    // Use this for initialization
    void Start()
    {
		foreach(GameObject smallNote in smallNotes) {
            smallNote.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
		AddToInverntory ();
    }

    //As we can escape the notebook at any state. We need to make sure when we enter the notebook from the pause menu,
    // it enters at the default state.
    public void startNotebook() {
        noteBookPanel.SetActive(true);
        selectedNotePanel.SetActive(false);
        deactivateAllBigNotes();
        //re-enable the buttons
        noteBookPanelBack.interactable = true;
        selecteNotePanelBack.interactable = true;

        noteBookPanelBack.Select();
    }



    //this is how you would find a button and select, but there is the same bug with the same button not being unselected
    //private void chooseButtonToSelect() {
    //    bool buttonFound = false;
    //    int i = 0;
    //    while(!buttonFound && i<5) {
    //        if (GameManager.instance.secretItemFound[i]) {
    //            smallNotes[i].GetComponent<Button>().Select(); // if we find a note, then select it
    //            buttonFound = true;
    //        }
    //        i++;
    //    }
    //    if (!buttonFound) {
    //        noteBookPanelBack.Select();
    //    }
    //}

    //when we push escape we should close both the notebookpanel and the selectednote panel.
    public void closeEverything() {
        noteBookPanel.SetActive(false);
        selectedNotePanel.SetActive(false);
        deactivateAllBigNotes();
        noteBookPanelBack.interactable = false;
        selecteNotePanelBack.interactable = false;
    }

	public void returnToPause(){
        selectedNotePanel.SetActive(false);
        noteBookPanel.SetActive(false);
        noteBookPanelBack.interactable = false;
        PauseMenu.SetActive(true);
        pausePanelNoteBookButton.Select();
    }

    void AddToInverntory() {
        for (int i = 0; i < 5; i++) {
            if (GameManager.instance.secretItemFound[i]) {
                smallNotes[i].SetActive(true);
            }
        }
    }

    //deactivate all the big notes
    public void deactivateAllBigNotes() {
        foreach (GameObject note in bigNotes) {
            note.SetActive(false);
        }
    }

    //Deactivate the selectedNOtebook panel, and activate the notebook panel
    public void returnToNotebookPanel() {
        deactivateAllBigNotes();
        noteBookPanel.SetActive(true);
        smallNotes[selectedNoteIndex].GetComponent<Button>().Select(); // select the note that you were on
        selectedNotePanel.SetActive(false);
    }

    //Turns on the selected notebook panel and turn on the select note
    public void selectNote(int index) {
        selectedNoteIndex = index;

        selectedNotePanel.SetActive(true);
        noteBookPanel.SetActive(false);
        selecteNotePanelBack.interactable = true;
        selecteNotePanelBack.Select(); 
        bigNotes[index].SetActive(true);
    }
}
