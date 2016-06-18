using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UITrigger : Interact {

    public GameObject LetterCanvas;
	public Sprite[] replacementSprites;
	public bool found = false;
	public bool destroyOnFound;

	private TextBoxManager manager;
	private bool isActive = false;

	// Use this for initialization
	void Start () {
		manager = GameObject.FindGameObjectWithTag("TextBoxManager").GetComponent<TextBoxManager>();
    }

    public void Update()
    {
		if (isActive){
			DollAudioManager.getInstance().stopWalkingSound();
			if (Input.GetButtonDown ("Interact")) {
				LetterCanvas.SetActive (false);
				isActive = false;
				manager.enablePlayer ();
				if (destroyOnFound) {
					DestroyObject (this);
				}
			}
		}
    }



    public override void interact()
    {
		manager.disablePlayer ();
		LetterCanvas.SetActive (true);
		isActive = true;
		found = true;
    }


 
}
