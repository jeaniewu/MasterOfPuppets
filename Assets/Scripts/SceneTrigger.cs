using UnityEngine;
using System.Collections;

public class SceneTrigger : MonoBehaviour {

	public string triggerName;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag ("Player")) 
		{
			StorySceneManager.getInstance ().StartCoroutine (triggerName);
			Destroy(gameObject);
		}
	}


}
