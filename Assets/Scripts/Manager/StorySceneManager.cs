using UnityEngine;
using System.Collections;

public class StorySceneManager : MonoBehaviour {

	//Characters
	public GameObject player;
	private Animator playerAnim;
	private Controller2 playerController;

	public GameObject puppetMaster;
	private Animator puppetMasterAnim;
	private string puppetMasterName;
	private bool isNameUpdated;

	public GameObject human;
	private Animator humanAnim;
	private Controller2 humanController;

	public GameObject doorToOpen;
	public GameObject momentaryJail;

	private GhostSwitchManager ghostSwitchManager;

	private GameObject maincam;

	public GameObject inputText;
	public TextBoxManager textBoxManager;


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
		textBoxManager = FindObjectOfType < TextBoxManager >();

		player = GameObject.FindGameObjectWithTag ("Player");
		playerAnim = player.GetComponent<Animator>();
		playerController = player.GetComponent<Controller2> ();

		puppetMasterAnim = puppetMaster.GetComponent<Animator> ();
		//puppetMaster.SetActive (false); TODO
		puppetMasterFace("forward");
		isNameUpdated = false;

		humanAnim = human.GetComponent<Animator> ();
		humanController = human.GetComponent<Controller2> ();
		human.layer = 0;

		//TESTING
		//puppetMasterAnim.SetBool ("hasSoul", true);
		//StartCoroutine (temp ());
		//StartCoroutine (thirdTrigger ());

	}
		

	void Update(){
		if (Input.GetKeyDown(KeyCode.Y)){
			StartCoroutine (fourthTrigger ());
		}
	}
		


	IEnumerator firstTrigger(){
		playerAnim.SetFloat("Y", 1);
		disableAll ();
		yield return new WaitForSeconds(0.5f);

		//Enable spotlight at human body
		maincam.GetComponent<CameraFollow> ().enabled = false;
		Coroutine movecam = StartCoroutine (move (maincam.transform, human.transform, 0.04f));
		yield return new WaitForSeconds(1.2f);
		StopCoroutine (movecam);
		human.GetComponentInChildren<Light> ().enabled = true;

		//camera goes back to player
		yield return new WaitForSeconds(1.5f);
		maincam.GetComponent<CameraFollow> ().enabled = true;
		yield return new WaitForSeconds(1f);
		enableAll ();
		yield return null;

	}

	IEnumerator secondTrigger(){
		while (textBoxManager.isActive) {
			yield return null;
		}
		disableAll ();
		maincam.GetComponent<CameraFollow> ().enabled = false;
		human.layer = LayerMask.NameToLayer ("Doll"); //make human layer to doll so can ghost switch into

		//Puppet master Showdown!
		Coroutine movecam = StartCoroutine (move (maincam.transform, puppetMaster.transform, 0.04f));
		yield return new WaitForSeconds(0.7f);
		StopCoroutine (movecam);
		puppetMaster.SetActive (true);
		puppetMasterFace("forward");
		yield return new WaitForSeconds(1.5f);

		// "Congratulations!..."
		showText(0);
		while (textBoxManager.isActive) {
			yield return null;
		}

		// simulate ghost switch from player to human
		momentaryJail.SetActive(true); // prevent human from moving around
		puppetMasterFace("right");
		puppetMasterAnim.SetBool("raiseHand", true);
		movecam = StartCoroutine (move (maincam.transform, human.transform, 0.04f));
		StartCoroutine(simulateGhostSwitch (player, human));
		yield return new WaitForSeconds(0.8f);
		puppetMasterAnim.SetBool("raiseHand", false);
		//puppetMaster face forward
		puppetMasterFace("forward");

		humanController.enabled = false;
		player.layer = 0;
		playerAnim.enabled = true;
		playerAnim.SetTrigger ("Sliced");
		StopCoroutine (movecam);


		yield return new WaitForSeconds(1f);
		// "Tada! Just as promised.."
		showText(1);
		while (textBoxManager.isActive) {
			yield return null;
		}

		StartCoroutine (thirdTrigger ());
		yield return null;
	}

	IEnumerator thirdTrigger(){

		// open the door with light and stuff
		doorToOpen.SetActive (true);
		humanController.enabled = true;
		humanController.allowSound = false;
		//yield return new WaitForSeconds(3f); // allow human to escape //TODO
		humanController.enabled = false;

		//puppet Master face human and starts moving closer
		puppetMasterFace("right");
		string[] array = textBoxManager.GetComponent<DialogueParser>().textArrays[2].Split (';');
		for (int i = 0; i < 3; i++) {
			//puppetmaster inches closer
			StartCoroutine (simulatePuppetMasterWalking(new Vector3 (5f - 1f*i, 0, 0), 1f));
			textBoxManager.reloadText (array[i]);
			textBoxManager.EnableTextBox();
			while (textBoxManager.isActive) {
				yield return null;
			}
		}

		// “I mean, look at just how adorable I am!”
		showText(3);
		while (textBoxManager.isActive) {
			yield return null;
		}

		StartCoroutine (simulatePuppetMasterWalking(new Vector3 (2, 0, 0), 0.9f));

		// "But I’ve been waiting soooo long…"
		showText(4);
		while (textBoxManager.isActive) {
			yield return null;
		}

		puppetMasterFace("forward");
		StartCoroutine (askPuppetMasterName());
		while (!isNameUpdated) {
			yield return null;
		}
		yield return new WaitForSeconds(0.4f);
		// need to use GameManager.getInstance ().isTrueEnding () instead
		if (puppetMasterName == "annie" || puppetMasterName == "Annie" || puppetMasterName == "ANNIE") {
			StartCoroutine (fifthTrigger ());
		} else {
			StartCoroutine (fourthTrigger ());
		}

		yield return null;
	}

	// BAD ENDING HEREEEE
	IEnumerator fourthTrigger(){
		yield return new WaitForSeconds(1f);
		//puppetmaster inches even closer
		StartCoroutine (simulatePuppetMasterWalking(new Vector3 (1, 0, 0), 1f));

		// "Hm, cute..."
		showText(5);
		while (textBoxManager.isActive) {
			yield return null;
		}

		// "Especially when the only person.."
		showText(6);
		while (textBoxManager.isActive) {
			yield return null;
		}

		StartCoroutine(showRedEyesThenFade (3f));
		yield return new WaitForSeconds(0.8f);

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

	IEnumerator fifthTrigger(){
		//puppetMasterAnim.SetBool ("hasSoul", false);
		yield return new WaitForSeconds(1f);
		yield return null;
	}

	IEnumerator askPuppetMasterName(){
		inputText.SetActive (true);
		inputText.GetComponent<TextField> ().selectTextInput ();
		while (inputText.GetComponent<TextField> ().isActive) {
			yield return null;
		}
		puppetMasterName = inputText.GetComponent<TextField> ().mainInputField.text;
		inputText.SetActive (false);
		Debug.Log (puppetMasterName);
		isNameUpdated = true;
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
		from.GetComponent<Controller2>().ghostMode = true;
		yield return new WaitForSeconds(0.8f);
		ghostSwitchManager.possess (from, to);
		yield return null;
	}


	//Move an object to a new position
	IEnumerator move(Transform objectToMove, Transform target, float speed)
	{
		//While we are not near to the target
		while((objectToMove.position - target.position).sqrMagnitude > 0.2)
		{
			Vector3 targetPosition = new Vector3 (target.position.x, 
				target.position.y,objectToMove.position.z);
			//Use smooth damp to move to the new position
			objectToMove.position = Vector3.Lerp(objectToMove.position, targetPosition, speed);
			//Yield until the next frame
			yield return null;
		}
		yield break;
	}

	IEnumerator dimLight(Light light, float dimSpeed){
		while (light.intensity >= 0) {
			light.intensity -= dimSpeed;
			yield return null;
		}
		yield return null;
	}

	void showText (int index){
		textBoxManager.GetComponent<DialogueParser>().playScript(index);
		textBoxManager.EnableTextBox();
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

	void puppetMasterFace(string dir){
		if (dir == "right") {
			puppetMasterAnim.SetFloat ("X", 1);
			puppetMasterAnim.SetFloat ("Y", 0);
		} else if (dir == "forward") {
			puppetMasterAnim.SetFloat("X", 0);
			puppetMasterAnim.SetFloat("Y", -1);
		}
	}

	IEnumerator showRedEyesThenFade(float waitTime){
		redEye[] eyes = puppetMaster.GetComponentsInChildren<redEye> ();
		foreach (redEye eye in eyes) {
			eye.StartCoroutine ("enlargeObject");
		}

		yield return new WaitForSeconds(waitTime);


		foreach (redEye eye in eyes) {
			eye.StartCoroutine ("fadeBlack");
		}


	}
		
}
