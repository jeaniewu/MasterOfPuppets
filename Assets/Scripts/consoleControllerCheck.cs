using UnityEngine;
using System.Collections;

public class consoleControllerCheck : MonoBehaviour {

	public bool forConsole;

	void Start () {
		Debug.Log (Input.GetJoystickNames ().Length);
		if (!hasJoyStick() && forConsole) {
			this.gameObject.SetActive(false);
		}

		if (hasJoyStick() && !forConsole) {
			this.gameObject.SetActive(false);
		}
	}

	bool hasJoyStick(){
		return Input.GetJoystickNames ().Length != 0;
	}

}
