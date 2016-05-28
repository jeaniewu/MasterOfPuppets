using UnityEngine;
using System.Collections;

public class StorySceneManager : MonoBehaviour {

	//Characters
	public GameObject player;
	private Animator playerAnim;
	private Controller2 playerController;

	public GameObject puppetMaster;
	private Animator puppetMasterAnim;

	public GameObject human;
	private Animator humanAnim;
	private Light humanSpotlight;

	private GhostSwitchManager ghostSwitchManager;

	private GameObject maincam;

	public TextBoxManager textBox;
	public TextAsset[] texts;


	//Singleton Instantiation
	public static StorySceneManager instance;

	void Awake() {
		if (instance != null) {
			Debug.LogError("Multiple instances of StorySceneManager!");
		}
		instance = this;
	}

	//Own getInstance method needed to convert types
	public static StorySceneManager getInstance () {
		return (StorySceneManager) instance;
	}


	void Start () {
		ghostSwitchManager = GetComponent<GhostSwitchManager> ();

		maincam = GameObject.FindGameObjectWithTag ("MainCamera");
		textBox = FindObjectOfType < TextBoxManager >();
		initPlayer ();

		puppetMasterAnim = puppetMaster.GetComponent<Animator> ();
		puppetMaster.SetActive (false);

		humanAnim = human.GetComponent<Animator> ();
		humanSpotlight = human.GetComponentInChildren<Light> ();
	}

	void initPlayer(){
		player = GameObject.FindGameObjectWithTag ("Player");
		playerAnim = player.GetComponent<Animator>();
		playerController = player.GetComponent<Controller2> ();
	}
		


	IEnumerator firstTrigger(){
		yield return new WaitForSeconds(0.3f);
		playerAnim.SetFloat("Y", 1);
		disableAll ();
		yield return new WaitForSeconds(0.5f);

		maincam.GetComponent<CameraFollow> ().enabled = false;
		StartCoroutine (moveCam (humanSpotlight.transform));
		yield return new WaitForSeconds(1.2f);
		humanSpotlight.enabled = true;

		yield return new WaitForSeconds(1.5f);
		maincam.GetComponent<CameraFollow> ().enabled = true;
		yield return new WaitForSeconds(1f);
		enableAll ();
		yield return null;

	}

	IEnumerator secondTrigger(){
		while (textBox.isActive) {
			yield return null;
		}
		disableAll ();
		maincam.GetComponent<CameraFollow> ().enabled = false;
		StartCoroutine (moveCam (puppetMaster.transform));
		yield return new WaitForSeconds(0.8f);

		puppetMaster.SetActive (true);
		puppetMasterAnim.SetFloat("Y", -1);
		puppetMasterAnim.SetBool ("hasSoul", true);
		yield return new WaitForSeconds(1.5f);

		textBox.ReloadScript (texts [0]);
		textBox.EnableTextBox();

		while (textBox.isActive) {
			yield return null;
		}

		puppetMasterAnim.SetFloat("X", 1);
		StartCoroutine (moveCam (human.transform));
		StartCoroutine(simulateGhostSwitch (player, human));
		yield return new WaitForSeconds(0.3f);
		playerAnim.enabled = true;
		playerAnim.SetTrigger ("Sliced");

		puppetMasterAnim.SetFloat("Y", -1);
		puppetMasterAnim.SetFloat("X", 0);
		yield return new WaitForSeconds(0.3f);
		textBox.ReloadScript (texts [1]);
		textBox.EnableTextBox();

		//maincam.GetComponent<CameraFollow> ().enabled = true;
		yield return null;
		StartCoroutine (thirdTrigger ());

	}

	IEnumerator thirdTrigger(){

	}

	// ghost switch from 'from' object to 'to' object
	IEnumerator simulateGhostSwitch (GameObject from, GameObject to){
		playerController.ghostMode = true;
		yield return new WaitForSeconds(0.3f);
		ghostSwitchManager.possess (from, to);
		yield return null;
	}


	//Move an object to a new position
	IEnumerator moveCam(Transform target)
	{
		Transform camTransform = maincam.transform;
		//While we are not near to the target
		while((camTransform.position - target.position).sqrMagnitude > 0.1)
		{
			Vector3 targetPosition = new Vector3 (target.position.x, 
				target.position.y,camTransform.position.z);
			//Use smooth damp to move to the new position
			camTransform.position = Vector3.Lerp(camTransform.position, targetPosition, 0.04f);
			//Yield until the next frame
			yield return null;
		}
	}

	void disableAll(){
		playerController.stopWalking ();
		playerController.enabled = false;
		playerAnim.enabled = false;
	}

	void enableAll(){
		playerController.enabled = true;
		playerAnim.enabled = true;
	}
}
