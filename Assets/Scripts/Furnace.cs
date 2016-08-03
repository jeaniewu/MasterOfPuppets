using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

// THIS SHOULD ONLY BE USED FOR FINAL SCENE
public class Furnace : MonoBehaviour {

	private Vector3 respawnPosition; 

	void Start () {
		respawnPosition = StorySceneManager.getInstance ().player.transform.position;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag ("Doll")) {
			other.gameObject.SetActive (false);
			StorySceneManager.getInstance ().StartCoroutine ("sixthTrigger");
		} else if (other.gameObject.CompareTag ("Player")) {
			other.gameObject.transform.position = respawnPosition;
			//SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
		}
	}
		
}
