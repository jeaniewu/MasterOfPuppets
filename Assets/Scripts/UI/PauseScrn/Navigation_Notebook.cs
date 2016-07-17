using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


public class Navigation_Notebook : MonoBehaviour
{
	private UITrigger foundLetters;
	private int index; 
	public Text BACK;
	public GameObject PauseMenu;
	public GameObject note0;
	public GameObject note1;
	public GameObject note2;
	public GameObject note3;
	public GameObject note4;


    // Use this for initialization
    void Start()
    {
		note0.SetActive (false);
		note1.SetActive (false);
		note2.SetActive (false);
		note3.SetActive (false);
		note4.SetActive (false);

		if (note0.activeSelf == false && note1.activeSelf == false && note2.activeSelf == false && note3.activeSelf == false && note4.activeSelf == false) {
			BACK.color = Color.red;
		}

    }

    // Update is called once per frame
    void Update()
    {
		AddToInverntory ();
    }

	public void Return(){
		
			gameObject.SetActive(false);
			PauseMenu.SetActive(true);


	}

	void AddToInverntory(){
		if (GameManager.instance.secretItemFound [0] == true) {
			Debug.Log ("found!");
			note0.SetActive (true);

		} 
		if (GameManager.instance.secretItemFound [1] == true) {
			Debug.Log ("found!");
			note1.SetActive (true);

		} 
		if (GameManager.instance.secretItemFound [2] == true) {
			Debug.Log ("found!");
			note2.SetActive (true);

		} 
		if (GameManager.instance.secretItemFound [3] == true) {
			Debug.Log ("found!");
			note3.SetActive (true);

		} 
		if (GameManager.instance.secretItemFound [4] == true) {
			Debug.Log ("found!");
			note4.SetActive (true);

		} 
		



	}




}
