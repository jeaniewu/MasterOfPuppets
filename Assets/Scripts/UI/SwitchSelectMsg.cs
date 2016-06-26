using UnityEngine;
using System.Collections;

public class SwitchSelectMsg : MonoBehaviour {
   // public GameObject textBox;
    public GameObject switchMessage;
    public GameObject acceptMessage;

	private Controller2 controller2;
    public TextBoxManager textBoxManager;
    // Use this for initialization
    //this is a tutotial script for level 1a
    void Start()
    {
        controller2 = GetComponent<Controller2>();
		textBoxManager = GameObject.FindObjectOfType<TextBoxManager> ();
		disableMessages ();
		StartCoroutine ("ghostSwitchTutorial");
    }

	IEnumerator ghostSwitchTutorial(){
		yield return new WaitForEndOfFrame();
		while (textBoxManager.isActive) {
			yield return null;
		}
		switchMessage.SetActive (true); //Press X

		while (!controller2.ghostMode) {
			yield return null;
		}

		switchMessage.SetActive (false);
		acceptMessage.SetActive (true); //Press Z

		while (controller2.ghostMode) {
			yield return null;
		}
		disableMessages ();	
	}

	private void disableMessages ()
	{
		switchMessage.SetActive (false);
		acceptMessage.SetActive (false);
	}
}
