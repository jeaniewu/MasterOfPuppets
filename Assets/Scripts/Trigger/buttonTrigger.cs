using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class buttonTrigger : Trigger {


	public List<GameObject> objects = new List<GameObject> ();

	public Sprite buttonPressed;
	public Sprite buttonUnpressed;

	private UITrigger secretItem;

	// Use this for initialization
	void Start () {
		secretItem = GetComponent<UITrigger> ();
		if (secretItem != null && secretItem.found) {
			switchSprite ();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.CompareTag ("Player") || other.CompareTag ("Doll")) {
            bool canPushButtons = other.GetComponent<Controller2>().canPushButtons;
            if (canPushButtons) {
                if (!objects.Contains(other.gameObject))
                    objects.Add(other.gameObject);
                if (!isOn) {
                    switchTriggerOn();
                    MechanicAudioManager.getInstance().playButtonSound();
                }
                GetComponent<SpriteRenderer>().sprite = buttonPressed;
            }
		}
	}
		

	void OnTriggerExit2D(Collider2D other) {
		if (other.CompareTag ("Player") || other.CompareTag ("Doll")) {
			objects.Remove (other.gameObject);
			if (objects.Count == 0) {
				switchTriggerOff ();
				GetComponent<SpriteRenderer>().sprite = buttonUnpressed;
			}
		}
	}

	public override void interact()
	{
		if (secretItem != null && !secretItem.found) {
			secretItem.interact ();
			switchSprite ();
		}
	}

	void switchSprite ()
	{
		Debug.Log ("switch Sprite as secret item is found");
		buttonPressed = secretItem.replacementSprites [0];
		buttonUnpressed = secretItem.replacementSprites [1];
		if (isOn) {
			GetComponent<SpriteRenderer> ().sprite = buttonPressed;
		}
		else {
			GetComponent<SpriteRenderer> ().sprite = buttonUnpressed;
		}
	}
}
