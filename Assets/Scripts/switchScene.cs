using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class switchScene : MonoBehaviour {

	public string nextScene;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player"))
			SceneManager.LoadScene (nextScene);
	}

	void setNextScene(string s){
		nextScene = s;
	}
}
