using UnityEngine;
using System.Collections;

public class CameraIntMsg : SwitchSelectMsg {


    // Use this for initialization
    void Start()
    {
		controller2 = GetComponent<Controller2>();
		textBoxManager = GameObject.FindObjectOfType<TextBoxManager> ();

		disableMessages ();
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

		enableMessage (messages[0]); // Press C - Switch

		while (!Input.GetKeyDown (KeyCode.C)) {
			yield return null;
		}
			
		messages[0].GetComponent<Renderer> ().enabled = false;

		while (!controller2.ghostMode) {
			yield return null;
		}

		enableMessage (messages[1]);
		enableMessage (messages[2]);

		while (controller2.ghostMode) {
			yield return null;
		}

		disableMessages ();	

		yield break;
	}
}
