using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseScreen : MonoBehaviour {
    public GameObject pauseScrn_panel;
    public GameObject notebook_panel;
    public GameObject selectedNote_panel;
    public GameObject controls_panel;
    public GameObject controls_panel_joystick;
    public GameObject options_panel;
	private TextBoxManager manager;

    public Button notebookBackButton;
    public Button selectedNoteBackButton;

    public Navigation_Notebook notebookManager;

    public Button resumeButton; //This is the first button that will always be selected

    private GameObject lastSelectedComponent; //The last selected UI component

	//Singleton Instantiation
	public static PauseScreen instance;   

	void Awake()
	{
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
			Debug.Log ("destroy Pause Screen duplicate on Awake");
		}
	}

	void Start () {
		manager = GameObject.FindGameObjectWithTag ("TextBoxManager").GetComponent<TextBoxManager> ();
        pauseScrn_panel.SetActive(false);
        notebookManager.closeEverything();
        options_panel.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Pause"))
        {
            TogglePause();
        }

        //If the pause screen is up, update the selection
        //This is so that the last selected component will be remembered and reselected if the player exits the game while having the pause screen up. 
        if (isPauseScreenUp()) {
            updateSelection();
        }
    }

    private void updateSelection() {
        if (EventSystem.current.currentSelectedGameObject == null) {
            Debug.Log("Reselecting first input");
            EventSystem.current.SetSelectedGameObject(lastSelectedComponent);
        } else {
            lastSelectedComponent = EventSystem.current.currentSelectedGameObject;
        }
    }

    //Checks the different pause screen panels to see if the pause screen is on
    private bool isPauseScreenUp() {
        return (pauseScrn_panel.activeSelf || notebook_panel.activeSelf || options_panel.activeSelf || selectedNote_panel.activeSelf || controls_panel.activeSelf || controls_panel_joystick.activeSelf);
    }

    void TogglePause()
    {
		if (isPauseScreenUp()) {
            resetResumeButton();
            pauseScrn_panel.SetActive (false);
            notebookManager.closeEverything();
			options_panel.SetActive (false);
            controls_panel.SetActive(false);
            controls_panel_joystick.SetActive(false);
			GameManager.getInstance().isPaused = false;
			manager.enablePlayer ();
		} else {
			pauseScrn_panel.SetActive (true);
            resumeButton.Select(); //Select the resume button
			manager.disablePlayer ();
			GameManager.getInstance().isPaused = true;         
		}
    }

    //This will hide the mouse when reselecting the game and also select the appropriate button if you are on the pause screen
    public void hideMouse() {
        //Disabling the cursor, as we have no mouse interaction in the game.
        Cursor.visible = false;

        if (notebook_panel.activeSelf) {
            notebookBackButton.Select();
        } else if (selectedNote_panel.activeSelf) {
            selectedNoteBackButton.Select();
        } 
    }

    //Resets the resume button so that it can be re selected
    private void resetResumeButton() {
        resumeButton.interactable = false;
        resumeButton.interactable = true;
    }

}
