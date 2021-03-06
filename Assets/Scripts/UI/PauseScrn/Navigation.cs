﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Navigation : MonoBehaviour
{
	private TextBoxManager manager;
	public GameObject Options_Panel;
	public GameObject Control_Panel_KB;
	public GameObject Control_Panel_JS;
	public GameObject Notebook_Option;
	public Navigation_Notebook notebookManager;
	public Slider SFXSlider;
	public Button controlBtnKB;
	public Button controlBtnJS;
    public Button[] allButtons; //All buttons to reselect and deselect in order for the selecting glitch to not happen. 


    // Use this for initialization
    void Start () {
		manager = GameObject.FindGameObjectWithTag ("TextBoxManager").GetComponent<TextBoxManager> ();
		Debug.Log (Input.GetJoystickNames().Length);
	}

	public void selectResume(){
        resetSelection();
        gameObject.SetActive(false);
		manager.enablePlayer ();
		GameManager.getInstance().isPaused = false;
	}

	public void selectNotebook(){
        resetSelection();
        notebookManager.startNotebook();
		gameObject.SetActive(false);
	}
	public void selectControls(){
			resetSelection ();
			Control_Panel_KB.SetActive (true);
			controlBtnKB.Select ();
			gameObject.SetActive (false);
		
	}

	public void selectOptions(){
        resetSelection();
		Options_Panel.SetActive (true);
		SFXSlider.Select ();
        resetSelection();
        Options_Panel.SetActive (true);
		gameObject.SetActive(false);
	}


	public void selectQuit(){
        resetSelection();
        GameManager.getInstance().isPaused = false; //Set paused to false as you exit the pause screen when exiting the game. 
        SceneManager.LoadScene("NewTitleScene");
	}

    //Go through all the buttons to "unselect them"
    //This is so we don't select an "already selected" button. (This can happen when you disable the parent, and you
    //reanable it. The button will not have the selected color, but the event system will think that it is selected). 
    public void resetSelection() {
        foreach (Button button in allButtons) {
            button.interactable = false;
            button.interactable = true;
        }
    }

	public void selectJSControl(){
		resetSelection ();
		gameObject.SetActive (false);
		Control_Panel_JS.SetActive (true);
		controlBtnJS.Select ();

	}

	public void SelectKBControl(){
		if (Control_Panel_JS.activeSelf) {
			Control_Panel_JS.SetActive (false);
			Control_Panel_KB.SetActive (true);
			controlBtnKB.Select ();

		}
			
	}


}
