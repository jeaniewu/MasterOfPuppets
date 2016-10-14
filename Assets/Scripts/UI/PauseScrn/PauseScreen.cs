using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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
	}

    void TogglePause()
    {
		if (pauseScrn_panel.activeSelf||notebook_panel.activeSelf||options_panel.activeSelf || selectedNote_panel.activeSelf || controls_panel.activeSelf || controls_panel_joystick.activeSelf) {
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
