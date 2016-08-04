using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NavigationControl : MonoBehaviour {

	public GameObject PauseMenu;
	public Button controlBtn;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	public void BackSelect () {

			PauseMenu.SetActive(true);
			gameObject.SetActive(false);
			controlBtn.Select ();
	}
}
