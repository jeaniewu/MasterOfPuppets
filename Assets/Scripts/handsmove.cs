using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class handsmove : MonoBehaviour {

    public Button startText;
    public Button exitText;

    // Use this for initialization
    void Start () {
        startText = startText.GetComponent<Button>();
        exitText = exitText.GetComponent<Button>();
    }
	
	// Update is called once per frame
	bool PlayHighlighted () {
        return startText.IsActive();
	}

    bool ExitHighlighted()
    {
        return exitText.IsActive();
    }
}
