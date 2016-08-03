using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UITrigger : Interact {

    public GameObject LetterCanvas;
	public Sprite[] replacementSprites;
	public int index;
	public bool found = false;
	public bool destroyOnFound;

	protected TextBoxManager manager;
	private bool isActive = false;

	void Start () {
		found = GameManager.getInstance ().secretItemFound [index];
		checkDestroy ();
		manager = GameObject.FindGameObjectWithTag ("TextBoxManager").GetComponent<TextBoxManager> ();
	}

	private void checkDestroy(){
		if (destroyOnFound && found) {
			DestroyObject (this.gameObject);
		}
	}

    public override void interact()
    {
		StartCoroutine("enableSecretMessage");
    }

	protected IEnumerator enableSecretMessage(){
		manager.disablePlayer ();
		LetterCanvas.SetActive (true);
		isActive = true;
		found = true;
		GameManager.getInstance ().secretItemFound [index] = found;
		GameManager.getInstance ().Save ();

		yield return new WaitForSeconds(0.3f);

		while (isActive) {
			DollAudioManager.getInstance().stopWalkingSound();
			if (Input.GetButtonUp ("Interact")) {
				LetterCanvas.SetActive (false);
				isActive = false;
				manager.enablePlayer ();
				checkDestroy ();
			} else {
				yield return null;
			}
		}
	}
 
}
