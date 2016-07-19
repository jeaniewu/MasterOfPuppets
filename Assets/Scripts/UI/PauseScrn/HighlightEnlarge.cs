using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HighlightEnlarge : MonoBehaviour {
	public GameObject bigNote0;
	public GameObject bigNote1;
	public GameObject bigNote2;
	public GameObject bigNote3;
	public GameObject bigNote4;
	public GameObject SelectedNote_Panel;
	public Button back;

	// Use this for initialization
	void Start () {
		SelectedNote_Panel.gameObject.SetActive (false);
		deactivateAll ();


	}

	// Update is called once per frame
	void Update () {
		
	}

	void deactivateAll(){
		bigNote0.SetActive (false);
		bigNote1.SetActive (false);
		bigNote2.SetActive (false);
		bigNote3.SetActive (false);
		bigNote4.SetActive (false);
	}
	public void Return(){

			SelectedNote_Panel.SetActive (false);
			deactivateAll ();
			back.interactable = true;
			back.Select ();
		//SelectedNote_Panel.gameObject.SetActive (false);
		//deactivateAll ();
	}

	public void NotezeroSelected(){
		SelectedNote_Panel.gameObject.SetActive (true);
		bigNote0.SetActive (true);
		back.interactable = false;
	}
	public void NoteoneSelected(){
		SelectedNote_Panel.gameObject.SetActive (true);
		bigNote1.SetActive (true);
		back.interactable = false;
	}
	public void NotetwoSelected(){
		SelectedNote_Panel.gameObject.SetActive (true);
		bigNote2.SetActive (true);
		back.interactable = false;
	}
	public void NotethreeSelected(){
		SelectedNote_Panel.gameObject.SetActive (true);
		bigNote3.SetActive (true);
		back.interactable = false;
	}
	public void NotefourSelected(){
		SelectedNote_Panel.gameObject.SetActive (true);
		bigNote4.SetActive (true);
		back.interactable = false;
	}
}