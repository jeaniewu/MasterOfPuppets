using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ActivateTextAtLine : MonoBehaviour {

	public TextAsset theText;

    public int startLine;
    public int endLine;

    public TextBoxManager theTextBox;
   
    public string message;

	public int index;

	public static bool textShown = false;
	public bool textTriggered;

    // Use this for initialization
    void Start () {
		theTextBox = FindObjectOfType < TextBoxManager > ();
		UpdateTrigger ();
	}

    void OnTriggerEnter2D(Collider2D other)
    {
		if (other.CompareTag ("Player") && !textTriggered) 
        {
			GameManager.getInstance ().textTriggered [index] = true;
			UpdateTrigger ();
            theTextBox.ReloadScript(theText);
            theTextBox.currentLine = startLine;
            theTextBox.endAtLine = endLine;
            theTextBox.EnableTextBox();
        }
    }

	void UpdateTrigger () {
		textTriggered = GameManager.getInstance ().textTriggered[index];
	}
}
