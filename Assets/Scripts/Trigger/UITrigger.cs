using UnityEngine;
using System.Collections;

public class UITrigger : Interact {

    public bool found;
    public GameObject LetterCanvas;
    public GameObject[] toSwitches;
    
  

	// Use this for initialization
	void Start () {
        
        found = false;
        LetterCanvas = GameObject.Find("Panel");
    }

    public void Update()
    {
        interact();
    }

    public void switchTriggerOn()
    {
        found = true;
        foreach (GameObject toSwitch in toSwitches)
            toSwitch.GetComponent<receiveSignal>().activate();
    }

    public void switchTriggerOff()
    {
        found = false;
        foreach (GameObject toSwitch in toSwitches)
            toSwitch.GetComponent<receiveSignal>().deactivate();
    }

    public override void interact()
    {
        switchTrigger();
    }

    public virtual void switchTrigger()
    {
        if (found)
        {
            switchTriggerOn();

        }
       else if (!found)
        {

            switchTriggerOff();
        }

    }


 
}
