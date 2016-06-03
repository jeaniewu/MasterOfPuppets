using UnityEngine;
using System.Collections;

public class DialogueParser : MonoBehaviour {

	public TextAsset textFile;
	public string[] textArrays;

	// Use this for initialization
	void Start () {
		if (textFile != null)
		{
			textArrays = new string[1];
			textArrays = textFile.text.Split('\n');
		}
	}

	// Update is called once per frame
	void Update () {
	
	}

	public void playScript(int index){
		GetComponent<TextBoxManager> ().textLines = new string[1];
		GetComponent<TextBoxManager> ().textLines = textArrays [index].Split (';');
	}
}
