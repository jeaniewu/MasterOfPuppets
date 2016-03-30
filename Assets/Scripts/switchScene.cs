using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class switchScene : Trigger {

	public string nextScene;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void switchTrigger(){
		SceneManager.LoadScene (nextScene);
	}

	void setNextScene(string s){
		nextScene = s;
	}
}
