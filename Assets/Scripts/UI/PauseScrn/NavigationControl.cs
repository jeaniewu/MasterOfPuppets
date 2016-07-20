using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NavigationControl : MonoBehaviour {

	public Text BACK;
	public GameObject PauseMenu;

	// Use this for initialization
	void Start () {
		BACK.color = Color.red;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Interact"))
		{
			PauseMenu.SetActive(true);
			gameObject.SetActive(false);
		}
	}
}
