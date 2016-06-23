using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneSkip : MonoBehaviour {
   

    public void sceneSwitch(string scene) {
		if (scene == "continue") {
			GameManager.getInstance ().Load ();
			SceneManager.LoadScene (GameManager.getInstance ().currentLevel);
		} else {
			SceneManager.LoadScene (scene);
		}
    }

}
