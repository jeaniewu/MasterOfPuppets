using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InteractTrigger : MonoBehaviour {

	public string customObjectName = ""; // Used for 1b so that UI only show up for buttons

	public GameObject interactButton;

	public GameObject joyStickInteractButton;

    private GameObject currentInteractButton;

	public List<GameObject> objects = new List<GameObject> ();

	private LayerMask mask;

    // Use this for initialization
    void Start()
    {
		currentInteractButton =  (Input.GetJoystickNames ().Length == 0) ? interactButton : joyStickInteractButton;
        interactButton.SetActive(false);
		joyStickInteractButton.SetActive (false);

		mask = 1 << LayerMask.NameToLayer ("Interactive") | 1 << LayerMask.NameToLayer ("Wall");
    }

	void OnTriggerEnter2D (Collider2D col){
		if (!objects.Contains(col.gameObject) && collideWithLayer(col))
			objects.Add(col.gameObject);
	}

    void OnTriggerStay2D(Collider2D col)
	{
		// check if there is wall between player and interactive objects
		if (objects.Exists (o => o.layer == LayerMask.NameToLayer ("Wall"))) {
			currentInteractButton.SetActive(false);
			return;
		}

		if (col.gameObject.layer == LayerMask.NameToLayer ("Interactive") && col.gameObject.name.Contains(customObjectName))
        {
            currentInteractButton.SetActive(true);
        }

    }

    void OnTriggerExit2D(Collider2D col)
    {
		if (collideWithLayer(col))
        {
            currentInteractButton.SetActive(false);
			objects.Remove (col.gameObject);
        }
    }

	private bool collideWithLayer(Collider2D col){
		// check if collider collides with layer mask specified
		return (mask.value & 1 << col.gameObject.layer) != 0;
	}
}
