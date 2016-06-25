using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

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
	public GameObject cage;

	private GhostSwitchManager ghostSwitchManager;

	private GameObject maincam;

	public GameObject inputText;
	public TextBoxManager textBoxManager;
	public GameObject panel;
	public GameObject whitePanel;

	private FinalSceneMusicManager musicManager;

	public List<Light> spotLights;
	private Light dirLight;


	//Singleton Instantiation
	public static StorySceneManager instance;

	void Awake() {
		if (instance != null) {
			Debug.LogError("Multiple instances of StorySceneManager!");
		}
		instance = this;

		// Instantiate Lights
		spotLights = new List<Light> ();
		Light[] lights = Object.FindObjectsOfType<Light>();
		foreach (Light light in lights) {
			if (light.type == LightType.Spot) {
				spotLights.Add (light);
			} else if (light.type == LightType.Directional) {
				dirLight = light;
			}
		}
	}

	//Own getInstance method needed to convert types
	public static StorySceneManager getInstance () {
		return (StorySceneManager) instance;
	}


	void Start () {
		musicManager = FinalSceneMusicManager.getInstance ();
		musicManager.playTrack (0); //E1
		ghostSwitchManager = GetComponent<GhostSwitchManager> ();

		maincam = GameObject.FindGameObjectWithTag ("MainCamera");
		textBoxManager = FindObjectOfType < TextBoxManager >();

		player = GameObject.FindGameObjectWithTag ("Player");
		playerAnim = player.GetComponent<Animator>();
		playerController = player.GetComponent<Controller2> ();

		puppetMasterAnim = puppetMaster.GetComponent<Animator> ();
		puppetMaster.SetActive (false);
		puppetMasterFace("forward");
		isNameUpdated = false;

		humanAnim = human.GetComponent<Animator> ();
		humanController = human.GetComponent<Controller2> ();
		human.layer = 0;

		doorToOpen.SetActive (false);

		//TESTING
		//puppetMasterAnim.SetBool ("hasSoul", true);
		//StartCoroutine (temp ());
		//StartCoroutine (thirdTrigger ());
	}

	void Update(){
//		if (Input.GetKeyDown(KeyCode.Y)){
//			StartCoroutine (fourthTrigger ());
//		}
//		if (Input.GetKeyDown(KeyCode.K)){
//			StartCoroutine (instantiateDoll ());
//		}
//		if (Input.GetKeyDown(KeyCode.F)){
//			StartCoroutine (fifthTrigger ());
//		}
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
		musicManager.stopTrack(0); //E1
		musicManager.playTrack(1); //E2
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

		musicManager.playTrack (2); //E3
		yield return new WaitForSeconds(0.5f);
		musicManager.stopTrack (1); //E2
		// open the door with light and stuff
		doorToOpen.SetActive (true);
		humanController.enabled = true;
		humanController.allowSound = false;
		yield return new WaitForSeconds(5f); // allow human to escape //TODO
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
		musicManager.stopTrack(2); //E3
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
		momentaryJail.SetActive(false); 
		yield return new WaitForSeconds(1f);
		musicManager.playTrack (5); //E6
		//puppetmaster inches even closer
		puppetMasterFace("right");
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
		musicManager.playTrack (6); //E6 (Scream)
		StartCoroutine(showRedEyesThenFade (3f));
		yield return new WaitForSeconds(0.8f);
		foreach (Light light in spotLights) {
			StartCoroutine (dimLight(light, 0.03f));
		}
		StartCoroutine (dimLight(dirLight, 0.005f));

		enlargeTextBoxpanel (panel,1.5f);
		changePanelFontColor (Color.red);

		yield return new WaitForSeconds(3.6f);
		showText(7);
		while (textBoxManager.isActive) {
			yield return null;
		}

		enlargeTextBoxpanel (panel,2f);
		showText(8);
		while (textBoxManager.isActive) {
			yield return null;
		}

		yield return null;
	}

	// TRUE ENDING HEREE
	IEnumerator fifthTrigger(){
		musicManager.stopTrack(2); //E3
		//puppetMasterAnim.SetBool ("hasSoul", false);
		puppetMasterAnim.SetTrigger("tiltHead");
		yield return new WaitForSeconds(0.5f);

		//"How do yoU knoW tHaT nAmE?"
		changePanelFontColor (Color.red);
		showText(9);
		while (textBoxManager.isActive) {
			yield return null;
		}
			
		musicManager.playOnce(4); //E4 (beginning)
		yield return new WaitForSeconds(2.3f);
		musicManager.playTrack(3,0.75f); //E4

		foreach (Light light in spotLights) {
			light.intensity = 1f;
		}
		dirLight.intensity = 0.8f;

		puppetMasterAnim.SetTrigger("spazz");
		StartCoroutine (instantiateDoll ());

		//“hUrrY, aNd DEstRoy tHe dOll!”
		changePanelFontColor (Color.blue);
		showText(10);
		while (textBoxManager.isActive) {
			yield return null;
		}

		//“nO! yOu Can’T!”; “HuRry!”
		StartCoroutine(showTextAlternateColor (11));

		maincam.GetComponent<CameraFollow> ().enabled = true;

		yield return null;
	}

	IEnumerator sixthTrigger(){
		musicManager.stopTrack(3); //E4
		//“ArghaaehHAHhahahaHAAaaaHAHAhA!”;“Just shut up and diE already!” 
		StartCoroutine(showTextAlternateColor (12));
		puppetMasterAnim.SetBool ("lowerHead", true);
		while (textBoxManager.isActive) {
			yield return null;
		}

		maincam.GetComponent<CameraFollow> ().enabled = false;
		Coroutine movecam = StartCoroutine (move (maincam.transform, human.transform, 0.04f));
		yield return new WaitForSeconds(1.2f);
		StopCoroutine (movecam);

		yield return new WaitForSeconds(0.5f);
		puppetMasterAnim.SetBool ("lowerHead", false);

		musicManager.playTrack(8); //E6 pinao only
		changePanelFontColor (Color.white);
		//“She’s gone?”
		showText(13);
		while (textBoxManager.isActive) {
			yield return null;
		}

		puppetMasterFace ("right");
		//“Yes, she’s finally gone!” She goes up to the cage. “Thank you! Thank you so, so much!” 
		showText(14);
		while (textBoxManager.isActive) {
			yield return null;
		}

		//“I’ve been trapped for so… so long, I almost gave up hope… But you saved me. I, I can’t thank you enough!”
		showText(15);
		while (textBoxManager.isActive) {
			yield return null;
		}

		puppetMasterAnim.SetBool ("raiseHand", true);

		//“I know where the real exit is, let’s go!”
		showText(16);
		while (textBoxManager.isActive) {
			yield return null;
		}

		foreach (Light light in spotLights) {
			StartCoroutine (dimLight(light, 0.03f));
		}
		Coroutine dim = StartCoroutine (dimLight(dirLight, 0.005f));
		yield return new WaitForSeconds(3f);
		enlargeTextBoxpanel (panel,1f);

		//rest of dialogue
		for (int i = 17; i < 21; i++) {
			showText(i);
			while (textBoxManager.isActive) {
				yield return null;
			}
		}
		StopCoroutine (dim);

		whitePanel.SetActive (true);
		textBoxManager.textBox = whitePanel;
		Coroutine brighten = StartCoroutine (brightenLight(dirLight, 0.05f, 2f));
		yield return new WaitForSeconds(1.8f);

		Text[] texts = whitePanel.GetComponentsInChildren<Text> ();
		textBoxManager.theText = texts [0];
		showText(21);
		StopCoroutine (brighten);
		yield return new WaitForSeconds(0.8f);
		musicManager.playTrack (7,1); //E9
		textBoxManager.theText = texts [1];
		showText(22);
		while (textBoxManager.isActive) {
			yield return null;
		}
		texts [0].gameObject.SetActive (false);
		texts [1].gameObject.SetActive (false);
		textBoxManager.theText = texts [2];
		showText(8);
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

	IEnumerator dimLight(Light light, float dimSpeed){
		while (light.intensity >= 0) {
			light.intensity -= dimSpeed;
			yield return null;
		}
		yield return null;
	}

	IEnumerator brightenLight(Light light, float brightSpeed, float intensity){
		Debug.Log ("brighten");
		while (light.intensity <= intensity) {
			light.intensity += brightSpeed;
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

	void enlargeTextBoxpanel(GameObject panel, float size){
		panel.transform.localScale = new Vector3 (size, size, -10);
		panel.transform.position = maincam.transform.position;
	}

	void changePanelFontColor(Color color) {
		panel.GetComponentInChildren<Text> ().color = color;
	}

	IEnumerator instantiateDoll(){
		GameObject doll = Instantiate(puppetMaster.GetComponentInChildren<DollAnimationController> ().gameObject);
		doll.tag = "Doll";
		doll.layer = LayerMask.NameToLayer ("Doll");
		doll.transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
		doll.transform.position = puppetMaster.transform.position;

		Transform fallPosition = GameObject.Find ("FallPosition").transform;
		Coroutine simulateThrow = StartCoroutine (simulateThrowDoll (doll, doll.transform, fallPosition));
		yield return new WaitForSeconds(0.8f);
		StopCoroutine (simulateThrow);
		doll.GetComponent<Collider2D> ().enabled = true;
		doll.GetComponent<Rigidbody2D> ().isKinematic = false;
	}

	IEnumerator simulateThrowDoll(GameObject doll, Transform origin, Transform target) {
		//While we are not near to the target
		while((origin.position - target.position).sqrMagnitude > 0.5)
		{
			doll.transform.Rotate (new Vector3(0,0,-10));
			Vector3 targetPosition = new Vector3 (target.position.x, 
				target.position.y,origin.position.z);
			origin.position = Vector3.Lerp(origin.position, targetPosition, 0.03f);
			yield return null;
		}
		yield break;
	}

	public void initFallenDoll(GameObject doll){
		doll.GetComponent<Animator> ().enabled = true;
		doll.transform.localScale = new Vector3 (0.75f, 0.75f, 0.75f);
		doll.transform.rotation = Quaternion.identity;
	}

	IEnumerator showTextAlternateColor(int index){
		string[] array = textBoxManager.GetComponent<DialogueParser>().textArrays[index].Split (';');
		changePanelFontColor (Color.red);
		textBoxManager.reloadText (array[0]);
		textBoxManager.EnableTextBox();
		while (textBoxManager.isActive) {
			yield return null;
		}
		changePanelFontColor (Color.blue);
		textBoxManager.reloadText (array[1]);
		textBoxManager.EnableTextBox();
		while (textBoxManager.isActive) {
			yield return null;
		}
	}
		
		
}
