using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GhostSwitchManager : MonoBehaviour
{

	public GameObject[] dolls;
	public GameObject player;

	public int dollIndex = 0;
	public bool dollsUpdated = false;

	private List<GameObject> container = new List<GameObject> ();

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
			} else {
				setGhostMode (false);
			}


		} else {
			dollsUpdated = false;
			if (dolls.Length > 0 && dolls [dollIndex] != null)
				notHighlight (dolls [dollIndex]);
			particleSystem.gameObject.SetActive (false);
		}

	}


	bool isGhostMode ()
	{
		return player.GetComponent<Controller2> ().ghostMode;
	}

	void setGhostMode (bool ghostMode)
	{
		player.GetComponent<Controller2> ().ghostMode = ghostMode;
	}

	// find which game object is the player, update the environment
	void initPlayer ()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		player.GetComponent<Controller2> ().enabled = true;
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
	public void updateDolls ()
	{

		if (!dollsUpdated) {

			container.Clear ();

			float minDistance = 100;
			int tempIndex = 0;

			for (int delta_degree = 1; delta_degree < 360; delta_degree++) {
				Quaternion quartenion = Quaternion.AngleAxis (delta_degree, Vector3.forward);
				Vector3 distance = player.transform.right * GetComponent<DollManager> ().radius;
				RaycastHit2D[] hits =
					Physics2D.RaycastAll (player.transform.position, quartenion * distance, GetComponent<DollManager> ().radius, 1 << LayerMask.NameToLayer ("Doll"));
				Debug.DrawRay (player.transform.position, quartenion * distance, Color.green, 0.2f);

				foreach (RaycastHit2D hit in hits) {
					if (hit.collider != null) {
						if (!container.Contains (hit.collider.gameObject) && hit.transform != player.transform 
							&& (hit.collider.CompareTag ("Doll") || hit.collider.CompareTag ("Player"))){
							container.Add (hit.collider.gameObject); //add doll to the list

							float dist = hit.distance;
							if (dist < minDistance) {
								dollIndex = tempIndex;
								minDistance = dist;
							}
							tempIndex++;
						}
					}
				}
			}

			dolls = container.ToArray ();
			dollsUpdated = true;
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
		highlight (dolls [dollIndex]);
		target (dolls [dollIndex]);
		moveCamera (dolls [dollIndex].transform.position);

		if (dolls.Length > 1) {
			if (Input.GetKeyDown (KeyCode.RightArrow)) {
				notHighlight (dolls [dollIndex]);
				if (dollIndex - 1 < 0)
					dollIndex = dolls.Length;
				choose (dollIndex - 1);
			} else if (Input.GetKeyDown (KeyCode.LeftArrow)) {
				notHighlight (dolls [dollIndex]);
				choose (dollIndex + 1);
			}
		}

		if (Input.GetButtonDown ("Possess")) {
			if (!hasGhostWall (dolls [dollIndex])) {
				possess (player, dolls [dollIndex]);
				particleSystem.gameObject.SetActive (false);
			}
		}

	}

	void choose (int index){
		dollIndex = (index) % dolls.Length;
		highlight(dolls[dollIndex]);
		particleSystem.Clear ();
		particleSystem.gameObject.SetActive (false);
		target (dolls [dollIndex]);
		moveCamera (dolls [dollIndex].transform.position);
	}

	bool hasGhostWall (GameObject doll)
	{
		Vector2 direction = doll.transform.position - player.transform.position;

		LayerMask layer = 1 << LayerMask.NameToLayer ("Doll") | 1 << LayerMask.NameToLayer ("ghostCollider") | 1 << LayerMask.NameToLayer ("Wall");
		RaycastHit2D[] hits = Physics2D.RaycastAll (player.transform.position, direction,
			GetComponent<DollManager> ().radius, layer);
		Debug.DrawRay (player.transform.position, direction, Color.green, 0.3f);
		foreach (RaycastHit2D hit in hits) {
			if (hit.collider != null) {
				if (hit.collider.CompareTag ("Doll") || hit.collider.CompareTag ("Player") || hit.collider.CompareTag ("GhostModeCollider")) {
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
	public void possess (GameObject player, GameObject doll)
	{
		dollsUpdated = false;

		player.GetComponent<Animator> ().SetBool ("hasSoul", false);
		setGhostMode (false);

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
