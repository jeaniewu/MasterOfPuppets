using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class menuScript : MonoBehaviour {

    public Canvas QuitMenu;
    public Button startText;
    public Button exitText;
    
	// Use this for initialization
	void Start () {
        QuitMenu = QuitMenu.GetComponent<Canvas>();
        startText = startText.GetComponent<Button>();
        exitText = exitText.GetComponent<Button>();
        QuitMenu.enabled = false;
    }

    public void ExitPress() 
    {
        QuitMenu.enabled = true; //enable the Quit menu when we click the Exit button
        startText.enabled = false; //then disable the Play and Exit buttons so they cannot be clicked
        exitText.enabled = false;
    }

    public void NoPress() 
    {
        QuitMenu.enabled = false; //disable the Quit menu when we click the No button
        startText.enabled = true; //then enable the Play and Exit buttons so they can be clicked
        exitText.enabled = true;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("OpenScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
