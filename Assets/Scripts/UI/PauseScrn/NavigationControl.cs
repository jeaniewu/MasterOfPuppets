using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NavigationControl : MonoBehaviour {

	public GameObject PauseMenu;
	public Button controlBtn;
	public GameObject joystickControl;
	public GameObject keyboardControl;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	public void BackSelect () {

			PauseMenu.SetActive(true);
			keyboardControl.SetActive(false);
			joystickControl.SetActive (false);
			controlBtn.Select ();
	}
}
