using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextBoxManager : MonoBehaviour
{

    public GameObject textBox;

    public Text theText;

    public TextAsset textFile;
    public string[] textLines;

    public int currentLine;
    public int endAtLine;

	public GameObject player;
    // Use this for initialization

    public bool isActive;
    public bool stopPlayerMovement;
    void Start()
    {

		player = GameObject.FindGameObjectWithTag ("Player");   

        if (textFile != null)
        {
            textLines = (textFile.text.Split('\n'));
        }

        if (endAtLine == 0)
        {
            endAtLine = textLines.Length - 1;
        }
        if (isActive)
        {
            EnableTextBox();
            
        }
        else
        {
			textBox.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {

			player.GetComponent<Animator> ().enabled = false;
			player.GetComponent<Controller2> ().enabled = false;
			//Debug.Log ("currentLine: " + currentLine);
			//Debug.Log ("Length: " + textLines.Length);
			if (currentLine == textLines.Length) { //find better fix later?
				DisableTextBox ();
			}
			if (currentLine < textLines.Length) {
				//Debug.Log ("rendering: " + currentLine);
				theText.text = textLines [currentLine];
			}
			if (Input.GetKeyDown(KeyCode.Z))
			{
				currentLine += 1;
				//Debug.Log ("currentLine increased! now: " + currentLine);
			}

//			if (currentLine >= endAtLine)
//			{
//				DisableTextBox();
//			}

        }
			
    }

    public void EnableTextBox()
    {
		currentLine = 0;
		endAtLine = textLines.Length - 1;
		textBox.SetActive(true);
        isActive = true;
		theText.text = textLines[currentLine];
		DollAudioManager.getInstance().stopWalkingSound();
    }

    public void DisableTextBox()
    {
        textBox.SetActive(false);
        isActive = false;
		player.GetComponent<Animator> ().enabled = true;
		player.GetComponent<Controller2> ().enabled = true; 

    }

    public void ReloadScript(TextAsset textInput)
    {
        if (textInput != null)
        {
            textLines = new string[1];
            textLines = (textInput.text.Split('\n'));
        }
    }

	public void reloadText(string text)
	{
		if (text != null)
		{
			textLines = new string[1];
			textLines [0] = text;
		}
	}
}
