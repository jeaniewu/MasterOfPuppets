using UnityEngine;
using System.Collections;

public class levelManager : MonoBehaviour {

	public GameObject[] dolls;
	public GameObject player;

	public int dollIndex = 0;

	void Start () {
		updateDolls ();
		initPlayer ();

	}
	
	// Update is called once per frame
	void Update () {
		if (isGhostMode ()) {
			highlight(dolls[dollIndex]);
			selectPlayer ();
		} else {
			notHighlight (dolls [dollIndex]);
		}

	}

	bool isGhostMode ()
	{
		return player.GetComponent<Controller2>().ghostMode;
	}

	void initPlayer(){
		player = GameObject.FindGameObjectWithTag ("Player");
		player.GetComponent<Controller2> ().enabled = true;
	}

	void updateDolls(){
		dolls = GameObject.FindGameObjectsWithTag ("Doll");
	}

	void selectPlayer(){
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			notHighlight(dolls[dollIndex]);
			dollIndex= Mathf.Abs((dollIndex - 1) % dolls.Length);
			highlight(dolls[dollIndex]);
		} else if (Input.GetKeyDown (KeyCode.RightArrow)) {
			notHighlight(dolls[dollIndex]);
			dollIndex= (dollIndex + 1) % dolls.Length;
			highlight(dolls[dollIndex]);
		} else if (Input.GetKeyDown (KeyCode.Z)){
			possess(player,dolls[dollIndex]);
			dollIndex = 0;
		}
	}

	void possess(GameObject player, GameObject doll){
		player.GetComponent<Controller2> ().ghostMode = false;
		player.GetComponent<Controller2> ().enabled = false;
		player.tag = "Doll";
		doll.tag = "Player";
		notHighlight(doll);
		initPlayer();
		updateDolls();
	}

	void highlight (GameObject doll){
		doll.GetComponent<Highlight> ().higlighted = true;	
	}

	void notHighlight (GameObject doll){
		doll.GetComponent<Highlight> ().higlighted = false;	
	}
}
