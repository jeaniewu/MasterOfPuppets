using UnityEngine;
using System.Collections;

public class SwitchSelectMsg : MonoBehaviour {

	public GameObject[] messages;

	protected Controller2 controller2;
    public TextBoxManager textBoxManager;

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
		while (!controller2.enabled) {
			yield return null;
		}
		while (textBoxManager.isActive) {
			yield return null;
		}

		enableMessage (messages[0]); // Press X - Switch

		while (!controller2.ghostMode) {
			yield return null;
		}
			
		// has entered ghostMode, disable switch tutorial
		messages[0].GetComponent<Renderer> ().enabled = false;

		enableMessage (messages[1]);

		while (controller2.ghostMode) {
			yield return null;
		}

		disableMessages ();	

		yield break;
	}

	protected void enableMessage (GameObject message){
		checkOneTimeTrigger triggeredCheck = message.GetComponent<checkOneTimeTrigger> ();
			
		if(!triggeredCheck.hasBeenTriggeredOnce){
			message.GetComponent<Renderer> ().enabled = true;
			triggeredCheck.setTriggered();
		}
	}

	protected void disableMessages ()
	{
		foreach(GameObject message in messages){
			message.GetComponent<Renderer> ().enabled = false;
		}
	}
}
