using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Ditch : MonoBehaviour {

	private GameObject doll;
	private GhostSwitchManager manager;

	// Use this for initialization
	void Start () {
		manager = GameObject.FindGameObjectWithTag ("LevelManager").GetComponent<GhostSwitchManager> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag ("Player") || other.gameObject.CompareTag ("Doll")) {
			doll = other.gameObject;
			StartCoroutine ("Fall");
		}
	}

	IEnumerator Fall() {
		while (doll.transform.localScale.x >= 0) {
			doll.GetComponent<Controller2> ().enabled = false;
			doll.transform.Rotate (new Vector3(0,0,20));
			doll.transform.localScale -= new Vector3(0.05F, 0.05F, 0);
			yield return null;
		}
			
		manager.dollsUpdated = false;
		manager.updateDolls ();

		if (doll.CompareTag("Player"))
			SceneManager.LoadScene (SceneManager.GetActiveScene().name);
	}
}
