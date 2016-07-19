using UnityEngine;
using System.Collections;

public class PauseScreen : MonoBehaviour {
    public GameObject pauseScrn_panel;
    public GameObject notebook_panel;
    public GameObject selectedNote_panel;
    public GameObject options_panel;
	private TextBoxManager manager;

    public Navigation_Notebook notebookManager;
	// Use this for initialization

	void Start () {
		manager = GameObject.FindGameObjectWithTag ("TextBoxManager").GetComponent<TextBoxManager> ();
        pauseScrn_panel.SetActive(false);
        notebookManager.closeEverything();
        options_panel.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	if (Input.GetKeyDown(KeyCode.Escape))
        {
			
            TogglePause();
        }
	}

    void TogglePause()
    {
		if (pauseScrn_panel.activeSelf||notebook_panel.activeSelf||options_panel.activeSelf || selectedNote_panel.activeSelf) {
			pauseScrn_panel.SetActive (false);
            notebookManager.closeEverything();
			options_panel.SetActive (false);
			manager.enablePlayer ();
		} else {
			pauseScrn_panel.SetActive (true);
			manager.disablePlayer ();
		}
    }
}
