using UnityEngine;
using System.Collections;

public class InteractTrigger : MonoBehaviour {
    public GameObject interactButton;
    // Use this for initialization
    void Start()
    {
        interactButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.layer == 9)
        {
           
            interactButton.SetActive(true);
        }

    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.layer == 9)
        {
            
            interactButton.SetActive(false);
        }
    }
}
