using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class exploration1 : MonoBehaviour {


    public GameObject textBox;

    public Text theText;

    public TextAsset textFile;
    public string[] textLines;

    public int currentLine;
    public int endAtLine;

    public Controller player;
    // Use this for initialization

    public bool isActive;
    public bool stopPlayerMovement;
    void Start()
    {

        player = FindObjectOfType<Controller>();

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
            DisableTextBox();
        }

    }

    // Update is called once per frame
    void Update()
    {
        theText.text = textLines[currentLine];
        if (!isActive)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentLine += 1;
        }

        if (currentLine > endAtLine)
        {
            DisableTextBox();
        }
    }

    public void EnableTextBox()
    {
        textBox.SetActive(true);
        isActive = true;

        if (stopPlayerMovement)
        {
            player.canMove = false;


        }

    }

    public void DisableTextBox()
    {
        textBox.SetActive(false);
        isActive = false;

        player.canMove = true;

    }

    public void ReloadScript(TextAsset theText)
    {
        if (theText != null)
        {
            textLines = new string[1];
            textLines = (theText.text.Split('\n'));


        }
    }
}

