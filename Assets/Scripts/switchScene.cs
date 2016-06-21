using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class switchScene : Interact {

	public string nextScene;
    public bool isOpen;

    public override void interact() {
		if (isOpen) {
			StartCoroutine ("changeNextScene");
		}
    }

	IEnumerator changeNextScene(){
		MechanicAudioManager.getInstance ().playUnlockDoorSound ();

		yield return new WaitForSeconds(MechanicAudioManager.getInstance().unlockDoorClip.length*0.5f);

		SceneManager.LoadScene (nextScene);
	}

	void setNextScene(string s){
		nextScene = s;
	}

    public void setOpen(bool open) {
        isOpen = open;
    }
}
	