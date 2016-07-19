using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Navigation : MonoBehaviour
{

    public Text RESUME;
    public Text NOTEBOOK;
    public Text OPTION;
    public Text QUIT;

    public GameObject NotebookOption;
    public Button BackButton;
    public GameObject OptionsOption;
	private TextBoxManager manager;
    //private Text[] menuOptions;
    private int selectedOption;



    // Use this for initialization
    void Start()
    {
        ResumeSelected();
		manager = GameObject.FindGameObjectWithTag ("TextBoxManager").GetComponent<TextBoxManager> ();
        NotebookOption.SetActive(false);
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


        if (selectedOption <= 3 && selectedOption >= 0)
        {
            if (selectedOption == 0)
            {
                ResumeSelected();
                OptionsDeSelected();
                NotebookDeSelected();
                if (Input.GetButtonDown("Interact"))
                {
                    gameObject.SetActive(false);
					manager.enablePlayer ();
                    Debug.Log("pressed");
                }
            }
            if (selectedOption == 1)
            {
                ResumeDeSelected();
                OptionsDeSelected();
                NotebookSelected();
                if (Input.GetButtonDown("Interact"))
                {
                    NotebookOption.SetActive(true);
                    BackButton.Select();
                    Debug.Log("pressed");
                    gameObject.SetActive(false);
                }
            }
            if (selectedOption == 2)
            {
                NotebookDeSelected();
                QuitDeSelected();
                OptionsSelected();
                if (Input.GetButtonDown("Interact"))
                {
                    OptionsOption.SetActive(true);
                    Debug.Log("pressed");
                    gameObject.SetActive(false);
                }
            }
            if (selectedOption == 3)
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
        else if (selectedOption > 2)
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
            selectedOption = 3;
        }

    }



    void ResumeSelected()
    {
        RESUME.color = Color.red;

    }

    void NotebookSelected()
    {
        NOTEBOOK.color = Color.red;
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

    void OptionsDeSelected()
    {
        OPTION.color = Color.white;
    }

    void QuitDeSelected()
    {
        QUIT.color = Color.white;
    }
}
