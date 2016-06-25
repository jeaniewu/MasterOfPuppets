using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ActivateTextAtLine : MonoBehaviour {

    public TextAsset theText;

    public TextBoxManager theTextBox;

    public bool destroyWhenActivated;

   
    public string message;


    // Use this for initialization
    void Start () {

       
   
        theTextBox = FindObjectOfType < TextBoxManager >();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    void OnTriggerEnter2D(Collider2D other)
    {
		if (other.CompareTag ("Player")) 
        {

            theTextBox.ReloadScript(theText);
            theTextBox.EnableTextBox();

            if (destroyWhenActivated)
            {
                Destroy(gameObject);
            }
        }

       
    }
}
