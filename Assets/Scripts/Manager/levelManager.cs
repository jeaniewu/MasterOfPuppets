using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class levelManager : MonoBehaviour
{

	public GameObject[] dolls;
	public GameObject player;

	public int dollIndex = 0;

	private List<GameObject> container = new List<GameObject> ();
	private bool dollsUpdated = false;

	public GameObject TextBoxManager;

	public ParticleSystem particleSystem;
	public int warmupTime = 2;


	void Start ()
	{
		initPlayer ();
		particleSystem = GetComponentInChildren<ParticleSystem> ();
		TextBoxManager = GameObject.FindGameObjectWithTag("TextBoxManager");
	}
		

	// Update is called once per frame
	void Update ()
	{
		if (isGhostMode ()) {
			DollAudioManager.getInstance ().stopWalkingSound ();
			updateDolls (); //find the dolls you can possess
			if (dolls.Length > 0) {
				selectPlayer ();
				initParticleSystem ();
			}


		} else {
			dollsUpdated = false;
			if (dolls.Length > 0)
				notHighlight (dolls [Mathf.Abs(dollIndex)]);
			particleSystem.gameObject.SetActive (false);
		}

	}


	bool isGhostMode ()
	{
		return player.GetComponent<Controller2> ().ghostMode;
	}

	// find which game object is the player, update the environment
	void initPlayer ()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		player.GetComponent<Controller2> ().enabled = true;
		player.GetComponent<Animator> ().enabled = true;
		player.GetComponent<Animator> ().SetBool ("hasSoul", true);
		player.GetComponent<Rigidbody2D> ().isKinematic = false;
		player.layer = 0;


		if (TextBoxManager != null) {
			TextBoxManager.GetComponent<TextBoxManager> ().player = player;
		}
	}

	void initParticleSystem (){
		particleSystem.transform.position = player.transform.position;
		particleSystem.enableEmission = true;
		particleSystem.Play ();
	}

	// update dolls array with dolls that you can possess
	void updateDolls ()
	{

		if (!dollsUpdated) {

			container.Clear ();

			for (int delta_degree = 1; delta_degree < 360; delta_degree++) {
				Quaternion q = Quaternion.AngleAxis (delta_degree, Vector3.forward);
				Vector3 d = player.transform.right * GetComponent<DollManager> ().radius;
				RaycastHit2D[] hits =
					Physics2D.RaycastAll (player.transform.position, q * d, GetComponent<DollManager> ().radius, 1 << LayerMask.NameToLayer ("Doll"));
				Debug.DrawRay (player.transform.position, q * d, Color.green, 0.2f);

				foreach (RaycastHit2D hit in hits) {
					if (hit.collider != null) {
						if (!container.Contains (hit.collider.gameObject) && hit.transform != player.transform 
							&& (hit.collider.CompareTag ("Doll") || hit.collider.CompareTag ("Player")))
							container.Add (hit.collider.gameObject); //add doll to the list

					}
				}
			}

			dolls = container.ToArray ();
			Debug.Log ("updating");
			dollsUpdated = true;
			dollIndex = 0;
		}
	}

	// make the camera follow the current player
	void updateCamera ()
	{
		Camera.main.GetComponent<CameraFollow> ().findPlayer ();
	}

	void moveCamera (Vector3 position)
	{
		Camera.main.GetComponent<CameraFollow> ().ghostModeSelect (position);
	}

	// selecting player from the array 'dolls'
	void selectPlayer ()
	{
		highlight(dolls[Mathf.Abs(dollIndex)]);
		target (dolls [Mathf.Abs (dollIndex)]);
		moveCamera (dolls [Mathf.Abs (dollIndex)].transform.position);
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			notHighlight(dolls[Mathf.Abs(dollIndex)]);
			choose (dollIndex - 1);
		} else if (Input.GetKeyDown (KeyCode.RightArrow)) {
			notHighlight(dolls[Mathf.Abs(dollIndex)]);
			choose (dollIndex + 1);
		}

		if (Input.GetButtonDown ("Possess")) {
			if (!hasGhostWall (dolls [Mathf.Abs (dollIndex)])) {
				possess (player, dolls [Mathf.Abs (dollIndex)]);
				particleSystem.gameObject.SetActive (false);
			}
		}

	}

	void choose (int index){
		dollIndex = (index) % dolls.Length;
		highlight(dolls[Mathf.Abs(dollIndex)]);
		particleSystem.Clear ();
		particleSystem.gameObject.SetActive (false);
		target (dolls [Mathf.Abs (dollIndex)]);
		moveCamera (dolls [Mathf.Abs (dollIndex)].transform.position);
	}

	bool hasGhostWall (GameObject doll)
	{
		Vector2 direction = doll.transform.position - player.transform.position;

		LayerMask layer = 1 << LayerMask.NameToLayer ("Doll") | 1 << LayerMask.NameToLayer ("ghostCollider");
		RaycastHit2D[] hits = Physics2D.RaycastAll (player.transform.position, direction,
			GetComponent<DollManager> ().radius, layer);
		Debug.DrawRay (player.transform.position, direction, Color.green, 0.3f);
		foreach (RaycastHit2D hit in hits) {
			if (hit.collider != null) {
				if (hit.collider.CompareTag ("Doll") || hit.collider.CompareTag ("Player")) {
					return false;
				} else if (hit.collider.CompareTag ("impenetrable")) {
					Debug.Log (hit.collider.name);
					return true;
				}
			}
		}

		return false;
	}

	// switch control of the player to the doll, as well as update the environment
	void possess (GameObject player, GameObject doll)
	{
		dollsUpdated = false;

		player.GetComponent<Animator> ().SetBool ("hasSoul", false);
		player.GetComponent<Controller2> ().ghostMode = false;

		player.GetComponent<Controller2> ().enabled = false;
		player.GetComponent<Rigidbody2D> ().isKinematic = true;
		player.layer = LayerMask.NameToLayer ("Doll");

		player.tag = "Doll";
		doll.tag = "Player";

		notHighlight(doll);

		initPlayer ();

		DollAudioManager.getInstance ().playGhostSwitchSound ();
		updateCamera ();
	}

	void turnOffAnimator ()
	{
		player.GetComponent<Animator> ().enabled = false;
	}


	void highlight (GameObject doll)
	{
		doll.GetComponentInChildren<BoxCollider2D> ().enabled = true;
		doll.GetComponent<Highlight> ().higlighted = true;
	}

	void notHighlight (GameObject doll)
	{
		doll.GetComponentInChildren<BoxCollider2D> ().enabled = false;
		doll.GetComponent<Highlight> ().higlighted = false;
	}

	void target (GameObject doll)
	{
		particleSystem.gameObject.SetActive (true);
		
		Vector3 vectorToTarget = doll.transform.position - player.transform.position;
		float angle = (float)Mathf.Atan2 (vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
		Quaternion direction = Quaternion.AngleAxis (angle, Vector3.forward);

		particleSystem.transform.rotation = direction;
		particleSystem.transform.rotation *= Quaternion.Euler (0, 90, 0);
	}

}
