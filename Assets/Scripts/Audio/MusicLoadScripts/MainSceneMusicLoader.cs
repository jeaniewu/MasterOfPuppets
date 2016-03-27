using UnityEngine;
using System.Collections;

public class MainSceneMusicLoader : MonoBehaviour {

	// Music Loader scripts exist to start the initial soundtrack
    //If we want to have a scene that starts with a different song we would make a new script
	void Start () {
        MusicManager.getInstance().startMainTheme();
	}
}
