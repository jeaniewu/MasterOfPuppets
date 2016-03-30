using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class switchSceneCondition : MonoBehaviour {

	public string nextScene;
	public int indexToSwitchScene;

	private int currindex;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		currindex = GetComponent<dialogueTrigger> ().pointer;
		if (currindex == indexToSwitchScene)
			SceneManager.LoadScene (nextScene);
	}
}
