using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UITrigger : Interact {

    public GameObject LetterCanvas;
	public Sprite[] replacementSprites;
	public int index;
	public bool found = false;
	public bool destroyOnFound;

	private TextBoxManager manager;
	private bool isActive = false;

	void Awake(){
		
	}

	void Start () {
		found = GameManager.getInstance ().secretItemFound [index];
		checkDestroy ();
		manager = GameObject.FindGameObjectWithTag ("TextBoxManager").GetComponent<TextBoxManager> ();
	}

    public void Update()
    {
		if (isActive){
			DollAudioManager.getInstance().stopWalkingSound();
			if (Input.GetButtonDown ("Interact")) {
				LetterCanvas.SetActive (false);
				isActive = false;
				manager.enablePlayer ();
				checkDestroy ();
			}
		}
    }

	private void checkDestroy(){
		if (destroyOnFound && found) {
			DestroyObject (this.gameObject);
		}
	}



    public override void interact()
    {
		manager.disablePlayer ();
		LetterCanvas.SetActive (true);
		isActive = true;
		found = true;
		GameManager.getInstance ().secretItemFound [index] = true;
		GameManager.getInstance ().Save ();
    }


 
}
