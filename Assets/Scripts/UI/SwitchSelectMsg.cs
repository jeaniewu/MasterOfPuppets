using UnityEngine;
using System.Collections;

public class SwitchSelectMsg : MonoBehaviour {
   // public GameObject textBox;
    public GameObject switchMessage;
    public GameObject acceptMessage;

	private Controller2 controller2;
    public TextBoxManager textBoxManager;

	private checkOneTimeTrigger switchTriggeredCheck;
	private checkOneTimeTrigger acceptTriggeredCheck;

	private Renderer switchMessageRenderer;
	private Renderer acceptMessageRenderer;

    //this is a tutotial script for level 1a
    void Start()
    {
        controller2 = GetComponent<Controller2>();
		textBoxManager = GameObject.FindObjectOfType<TextBoxManager> ();

		switchTriggeredCheck = switchMessage.GetComponent<checkOneTimeTrigger> ();
		acceptTriggeredCheck = acceptMessage.GetComponent<checkOneTimeTrigger> ();
		switchMessageRenderer = switchMessage.GetComponent<Renderer> ();
		acceptMessageRenderer = acceptMessage.GetComponent<Renderer> ();

		disableMessages ();
		StartCoroutine ("ghostSwitchTutorial");
    }

	IEnumerator ghostSwitchTutorial(){
		yield return new WaitForEndOfFrame();
		while (textBoxManager.isActive) {
			yield return null;
		}

		if(!switchTriggeredCheck.hasBeenTriggeredOnce){
			switchMessageRenderer.enabled = true; //Press X
			switchTriggeredCheck.setTriggered();
		}

		while (!controller2.ghostMode) {
			yield return null;
		}

		switchMessageRenderer.enabled = false;

		if(!acceptTriggeredCheck.hasBeenTriggeredOnce){
			acceptMessageRenderer.enabled = true; //Press Z
			acceptTriggeredCheck.setTriggered();
		}

		while (controller2.ghostMode) {
			yield return null;
		}
		disableMessages ();	
	}

	private void disableMessages ()
	{
		// Disable renderer so I can do triggered check on the game object
		switchMessageRenderer.enabled = false;
		acceptMessageRenderer.enabled = false;
	}
}
