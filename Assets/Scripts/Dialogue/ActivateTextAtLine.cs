using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ActivateTextAtLine : MonoBehaviour {

	public TextAsset theText;

    public TextBoxManager theTextBox;

	public static bool textShown = false;

	private checkOneTimeTrigger triggeredCheck;

    // Use this for initialization
    void Start () {
		theTextBox = FindObjectOfType < TextBoxManager > ();
		triggeredCheck = GetComponent<checkOneTimeTrigger> ();
	}

    void OnTriggerEnter2D(Collider2D other)
    {
		if (other.CompareTag ("Player") && !triggeredCheck.hasBeenTriggeredOnce) 
        {
			triggeredCheck.setTriggered ();
            theTextBox.ReloadScript(theText);
            theTextBox.EnableTextBox();
        }
    }
}
