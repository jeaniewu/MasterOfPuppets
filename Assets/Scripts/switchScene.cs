using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class switchScene : Trigger {

	public string nextScene;
    public bool isOpen;

    public override void switchTrigger() {

        if (isOpen) {
            SceneManager.LoadScene(nextScene);
        }
        isOn = false;
        Debug.Log("IS OPEN: " + isOpen);
    }

	void setNextScene(string s){
		nextScene = s;
	}

    public void setOpen(bool open) {
        isOpen = open;
    }
}
