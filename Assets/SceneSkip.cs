using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneSkip : MonoBehaviour {
   

    public void sceneSwitch(string scene) {
        SceneManager.LoadScene(scene);
    }

}
