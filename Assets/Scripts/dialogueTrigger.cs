using UnityEngine;
using System.Collections;

public class dialogueTrigger : Trigger {

	public TextAsset theText;

	public int startLine;
	public int endLine;

	public TextBoxManager theTextBox;

	// Use this for initialization
	void Start () {
		theTextBox = FindObjectOfType < TextBoxManager >();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void switchTrigger(){
		theTextBox.ReloadScript(theText);
		theTextBox.currentLine = startLine;
		theTextBox.endAtLine = endLine;
		Invoke("enable", 0.1f);
	}

	public void enable(){
		theTextBox.EnableTextBox();
	}

}
