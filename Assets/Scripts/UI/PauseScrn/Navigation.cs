using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Navigation : MonoBehaviour
{

    public Text RESUME;
    public Text NOTEBOOK;
	public Text CONTROL;
    public Text OPTION;
    public Text QUIT;
    public Button BackButton;
    public GameObject OptionsOption;
	public GameObject controls;
	private TextBoxManager manager;
    //private Text[] menuOptions;
    public int selectedOption;

    public Navigation_Notebook notebookManager;


    // Use this for initialization
    void Start()
    {
        ResumeSelected();
		manager = GameObject.FindGameObjectWithTag ("TextBoxManager").GetComponent<TextBoxManager> ();
        OptionsOption.SetActive(false);
        //menuOptions = GetComponentsInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Length of options:" + menuOptions.Length);

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectedOption++;
            Debug.Log("Option is" + selectedOption);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            selectedOption--;
            Debug.Log("Option is" + selectedOption);
        }


        if (selectedOption <= 4 && selectedOption >= 0)
        {
            if (selectedOption == 0) // RESUME
            {
                ResumeSelected();
                ControlDeSelected();
                NotebookDeSelected();
                if (Input.GetButtonDown("Interact"))
                {
                    gameObject.SetActive(false);
					manager.enablePlayer ();
                    Debug.Log("pressed");
                }
            }
            if (selectedOption == 1) // NOTEBOOK
            {
                ResumeDeSelected();
                ControlDeSelected();
                NotebookSelected();
                if (Input.GetButtonDown("Interact"))
                {
                    notebookManager.startNotebook();
                    gameObject.SetActive(false);
                }
            }
			if (selectedOption == 2) // CONTROLS
			{
				ControlSelected ();
				NotebookDeSelected();
				OptionsDeSelected ();
				if (Input.GetButtonDown("Interact"))
				{
					controls.SetActive(true);
					gameObject.SetActive(false);
				}
			}
			if (selectedOption == 3) // OPTIONS
            {
                ControlDeSelected();
                QuitDeSelected();
                OptionsSelected();
                if (Input.GetButtonDown("Interact"))
                {
                    OptionsOption.SetActive(true);
                    Debug.Log("pressed");
                    gameObject.SetActive(false);
                }
            }
			if (selectedOption == 4)  // QUIT
            {
                OptionsDeSelected();
                ResumeDeSelected();
                QuitSelected();
                if (Input.GetButtonDown("Interact"))
                {
					SceneManager.LoadScene("NewTitleScene");
                }
            }
        }
        else if (selectedOption > 4)
        {
            QuitDeSelected();
            ResumeSelected();
            NotebookDeSelected();
            selectedOption = 0;
        }
        else if (selectedOption < 0)
        {
            ResumeDeSelected();
            QuitSelected();
            selectedOption = 4;
        }

    }



    void ResumeSelected()
    {
        RESUME.color = Color.red;

    }

    public void NotebookSelected()
    {
        NOTEBOOK.color = Color.red;
    }

	public void ControlSelected()
	{
		CONTROL.color = Color.red;
	}

    void OptionsSelected()
    {
        OPTION.color = Color.red;
    }

    void QuitSelected()
    {
        QUIT.color = Color.red;
    }
    void ResumeDeSelected()
    {
        RESUME.color = Color.white;
    }

    void NotebookDeSelected()
    {
        NOTEBOOK.color = Color.white;
    }

	public void ControlDeSelected()
	{
		CONTROL.color = Color.white;
	}

    void OptionsDeSelected()
    {
        OPTION.color = Color.white;
    }

    void QuitDeSelected()
    {
        QUIT.color = Color.white;
    }
}
