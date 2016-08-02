using UnityEngine;
using System.Collections;

public class CameraIntMsg : SwitchSelectMsg {


    // Use this for initialization
    void Start()
    {
		controller2 = GetComponent<Controller2>();
		textBoxManager = GameObject.FindObjectOfType<TextBoxManager> ();

		currentMessages = (Input.GetJoystickNames ().Length == 0) ? tutorialMessages : joyStickMessages;

		disableMessages (tutorialMessages);
		disableMessages (joyStickMessages);
		StartCoroutine ("cameraTutorial");
    }

	IEnumerator cameraTutorial(){
		yield return new WaitForEndOfFrame();
		while (!controller2.enabled) {
			yield return null;
		}
		while (textBoxManager.isActive) {
			yield return null;
		}

		enableMessage (currentMessages[0]); // Press C - Switch

		while (!Input.GetButtonDown("Camera")) {
			yield return null;
		}
			
		currentMessages[0].GetComponent<Renderer> ().enabled = false;

		while (!controller2.ghostMode) {
			yield return null;
		}

		enableMessage (currentMessages[1]);
		enableMessage (currentMessages[2]);

		while (controller2.ghostMode) {
			yield return null;
		}

		disableMessages (currentMessages);	

		yield break;
	}
}
