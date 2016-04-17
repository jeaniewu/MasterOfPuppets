using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Ditch : MonoBehaviour {

	private GameObject player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag ("Player")) {
			player = other.gameObject;
			StartCoroutine ("Fall");
		}
	}

	IEnumerator Fall() {
		while (player.transform.localScale.x >= 0) {
			player.GetComponent<Controller2> ().enabled = false;
			player.transform.Rotate (new Vector3(0,0,20));
			player.transform.localScale -= new Vector3(0.05F, 0.05F, 0);
			yield return null;
		}
		SceneManager.LoadScene (Application.loadedLevel);
	}
}
