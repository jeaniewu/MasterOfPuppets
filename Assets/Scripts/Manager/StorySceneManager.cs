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
	private Controller2 humanController;

	public GameObject doorToOpen;
	public GameObject momentaryJail;

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

		player = GameObject.FindGameObjectWithTag ("Player");
		playerAnim = player.GetComponent<Animator>();
		playerController = player.GetComponent<Controller2> ();

		puppetMasterAnim = puppetMaster.GetComponent<Animator> ();
		puppetMaster.SetActive (false);

		humanAnim = human.GetComponent<Animator> ();
		humanController = human.GetComponent<Controller2> ();
		human.layer = 0;

		//TESTING
//		puppetMasterAnim.SetBool ("hasSoul", true);
//		StartCoroutine (thirdTrigger ());

	}

	void Update(){
		if (Input.GetKeyDown(KeyCode.Y)){
			StartCoroutine (fourthTrigger ());
		}
	}
		


	IEnumerator firstTrigger(){
		yield return new WaitForSeconds(0.3f);
		playerAnim.SetFloat("Y", 1);
		disableAll ();
		yield return new WaitForSeconds(0.5f);

		maincam.GetComponent<CameraFollow> ().enabled = false;
		StartCoroutine (move (maincam.transform, human.transform, 0.04f));
		yield return new WaitForSeconds(1.2f);
		human.GetComponentInChildren<Light> ().enabled = true;

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
		human.layer = LayerMask.NameToLayer ("Doll"); //make human layer to doll so can ghost switch into

		//Puppet master Showdown!
		StartCoroutine (move (maincam.transform, puppetMaster.transform, 0.04f));
		yield return new WaitForSeconds(0.7f);
		puppetMaster.SetActive (true);
		puppetMasterAnim.SetFloat("Y", -1);
		puppetMasterAnim.SetBool ("hasSoul", true);
		yield return new WaitForSeconds(1.5f);

		// "Congratulations!..."
		showText(0);
		while (textBox.isActive) {
			yield return null;
		}

		// simulate ghost switch from player to human
		momentaryJail.SetActive(true); // prevent human from moving around
		puppetMasterAnim.SetFloat("X", 1); // puppet master anim TODO
		StartCoroutine (move (maincam.transform, human.transform, 0.04f));
		StartCoroutine(simulateGhostSwitch (player, human));
		yield return new WaitForSeconds(0.7f);
		humanController.enabled = false;
		player.layer = 0;
		playerAnim.enabled = true;
		playerAnim.SetTrigger ("Sliced");

		yield return new WaitForSeconds(0.3f);
		//puppetMaster face forward
		puppetMasterAnim.SetFloat("Y", -1);
		puppetMasterAnim.SetFloat("X", 0);

		// "Tada! Just as promised.."
		showText(1);
		while (textBox.isActive) {
			yield return null;
		}

		StartCoroutine (thirdTrigger ());
		yield return null;
	}

	IEnumerator thirdTrigger(){

		// open the door with light and stuff
		doorToOpen.SetActive (true);
		humanController.enabled = true;
		yield return new WaitForSeconds(2f); // allow human to escape //TODO
		humanController.enabled = false;

		// "What's up? dont u want to leave?.."
		showText(2);
		while (textBox.isActive) {
			yield return null;
		}

		//puppetmaster inches closer
		puppetMasterAnim.SetFloat("X", 1);
		StartCoroutine (simulatePuppetMasterWalking(new Vector3 (2, 0, 0), 0.5f));

		// "Aw, have you grown that fond of me, darling?"
		showText(3);
		while (textBox.isActive) {
			yield return null;
		}

		// "But I’ve been waiting soooo long…"
		showText(4);
		while (textBox.isActive) {
			yield return null;
		}

		if (GameManager.getInstance ().isTrueEnding ()) {
			//StartCoroutine (thirdTrigger ());
		} else {
			StartCoroutine (fourthTrigger ());
		}

		yield return null;
	}

	IEnumerator fourthTrigger(){
		yield return new WaitForSeconds(1f);
		//puppetmaster inches even closer
		StartCoroutine (simulatePuppetMasterWalking(new Vector3 (1, 0, 0), 0.5f));

		// "Hm, cute..."
		showText(5);
		while (textBox.isActive) {
			yield return null;
		}

		// "Especially when the only person.."
		showText(6);
		while (textBox.isActive) {
			yield return null;
		}

		Light[] lights = Object.FindObjectsOfType<Light>();
		foreach (Light light in lights) {
			if (light.type == LightType.Spot) {
				StartCoroutine (dimLight(light, 0.03f));
			} else if (light.type == LightType.Directional) {
				StartCoroutine (dimLight(light, 0.005f));
			}
		}

		yield return null;
	}

	IEnumerator simulatePuppetMasterWalking (Vector3 posFromTarget, float waitingTime){
		puppetMasterAnim.SetBool ("isWalking", true);
		GameObject target = new GameObject ();
		target.transform.position = human.transform.position - posFromTarget;
		StartCoroutine(move (puppetMaster.transform, target.transform, 0.02f));
		yield return new WaitForSeconds(waitingTime);
		puppetMasterAnim.SetBool ("isWalking", false);
	}

	// ghost switch from 'from' object to 'to' object
	IEnumerator simulateGhostSwitch (GameObject from, GameObject to){
		playerController.ghostMode = true;
		yield return new WaitForSeconds(0.4f);
		ghostSwitchManager.possess (from, to);
		yield return null;
	}


	//Move an object to a new position
	IEnumerator move(Transform objectToMove, Transform target, float speed)
	{
		//While we are not near to the target
		while((objectToMove.position - target.position).sqrMagnitude > 0.1)
		{
			Vector3 targetPosition = new Vector3 (target.position.x, 
				target.position.y,objectToMove.position.z);
			//Use smooth damp to move to the new position
			objectToMove.position = Vector3.Lerp(objectToMove.position, targetPosition, speed);
			//Yield until the next frame
			yield return null;
		}
	}

	IEnumerator dimLight(Light light, float dimSpeed){
		while (light.intensity >= 0) {
			Debug.Log (light.intensity);
			light.intensity -= dimSpeed;
			yield return null;
		}
		yield return null;
	}

	void showText (int index){
		textBox.ReloadScript (texts [index]);
		textBox.EnableTextBox();
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
