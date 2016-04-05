using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class switchScene : Interact {

	public string nextScene;
    public bool isOpen;

    public override void interact() {
		if (isOpen) {
			SceneManager.LoadScene (nextScene);
		}
    }

	void setNextScene(string s){
		nextScene = s;
	}

    public void setOpen(bool open) {
        isOpen = open;
    }
}
