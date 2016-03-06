using UnityEngine;
using System.Collections;

public class ActivateTextAtLine2 : MonoBehaviour
{

    public TextAsset theText;

    public int startLine;
    public int endLine;

    public TextBoxManager2 theTextBox;

    public bool destroyWhenActivated;

    // Use this for initialization
    void Start()
    {

        theTextBox = FindObjectOfType<TextBoxManager2>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Doll")
        {

            theTextBox.ReloadScript(theText);
            theTextBox.currentLine = startLine;
            theTextBox.endAtLine = endLine;
            theTextBox.EnableTextBox();

            if (destroyWhenActivated)
            {
                Destroy(gameObject);
            }
        }


    }
}
