using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InteractTrigger : MonoBehaviour {
    public GameObject interactButton;

	public List<GameObject> objects = new List<GameObject> ();

	private LayerMask mask;

    // Use this for initialization
    void Start()
    {
        interactButton.SetActive(false);
		mask = 1 << LayerMask.NameToLayer ("Interactive") | 1 << LayerMask.NameToLayer ("Wall");
    }

    // Update is called once per frame
    void Update()
	{

	}

	void OnTriggerEnter2D (Collider2D col){
		if (!objects.Contains(col.gameObject) && collideWithLayer(col))
			objects.Add(col.gameObject);
	}

    void OnTriggerStay2D(Collider2D col)
	{
		// check if there is wall between player and interactive objects
		if (objects.Exists (o => o.layer == LayerMask.NameToLayer ("Wall"))) {
			interactButton.SetActive(false);
			return;
		}

		if (col.gameObject.layer == LayerMask.NameToLayer ("Interactive"))
        {
            interactButton.SetActive(true);
        }

    }

    void OnTriggerExit2D(Collider2D col)
    {
		if (collideWithLayer(col))
        {
            interactButton.SetActive(false);
			objects.Remove (col.gameObject);
        }
    }

	private bool collideWithLayer(Collider2D col){
		// check if collider collides with layer mask specified
		return (mask.value & 1 << col.gameObject.layer) != 0;
	}
}
