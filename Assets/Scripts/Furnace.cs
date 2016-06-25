using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Furnace : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag ("Doll")) {
			other.gameObject.SetActive (false);
			StorySceneManager.getInstance ().StartCoroutine ("sixthTrigger");
		} else if (other.gameObject.CompareTag ("Player")) {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
		}
	}
}
