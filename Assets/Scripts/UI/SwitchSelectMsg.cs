using UnityEngine;
using System.Collections;

public class SwitchSelectMsg : MonoBehaviour {

	public GameObject[] tutorialMessages;

	public GameObject[] joyStickMessages;

	public GameObject[] currentMessages;

	protected Controller2 controller2;
    public TextBoxManager textBoxManager;

    //this is a tutotial script for level 1a
    void Start()
    {
        controller2 = GetComponent<Controller2>();
		textBoxManager = GameObject.FindObjectOfType<TextBoxManager> ();

		currentMessages = (Input.GetJoystickNames ().Length == 0) ? tutorialMessages : joyStickMessages;

		disableMessages (tutorialMessages);
		disableMessages (joyStickMessages);
		StartCoroutine ("ghostSwitchTutorial");
    }

	IEnumerator ghostSwitchTutorial(){
		yield return new WaitForEndOfFrame();
		while (!controller2.enabled) {
			yield return null;
		}
		while (textBoxManager.isActive) {
			yield return null;
		}

		enableMessage (currentMessages[0]); // Press X - Switch

		while (!controller2.ghostMode) {
			yield return null;
		}
			
		// has entered ghostMode, disable switch tutorial
		currentMessages[0].GetComponent<Renderer> ().enabled = false;

		enableMessage (currentMessages[1]);
		enableMessage (currentMessages[2]);

		while (controller2.ghostMode) {
			yield return null;
		}

		disableMessages (currentMessages);	

		yield break;
	}

	protected void enableMessage (GameObject message){
		message.GetComponent<Renderer> ().enabled = true;
	}

	protected void disableMessages (GameObject[] messages)
	{
		foreach(GameObject message in messages){
			message.GetComponent<Renderer> ().enabled = false;
		}
	}
}
