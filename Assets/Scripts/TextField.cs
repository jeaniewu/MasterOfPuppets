using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextField : MonoBehaviour {

	public InputField mainInputField;

	public bool isActive;
	private GameObject player;

	void Start()
	{

		player = GameObject.FindGameObjectWithTag ("Player");   

	}
		
	void Update(){ 
		if (isActive) {
			player.GetComponent<Animator> ().enabled = false;
			player.GetComponent<Controller2> ().enabled = false;
		}

	}

	void OnGUI() {
		if(mainInputField.isFocused && Input.GetKey(KeyCode.Return)) {
			isActive = false;
		}

	}

	public void selectTextInput(){
		isActive = true;
		mainInputField.Select ();
	}



}
