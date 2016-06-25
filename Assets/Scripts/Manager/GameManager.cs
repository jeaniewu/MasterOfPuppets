using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameManager : MonoBehaviour {

	//Singleton Instantiation
	public static GameManager instance;   

	public bool trueEnding;

	public string currentLevel;
	public bool[] secretItemFound;

	public bool firstRun;
	public bool[] textTriggered;

	public GameObject saveText;

	private static String[] canSaveScenes = {"OpenScene", "1a", "1b", "2a", "2b", "3a", "3b", "4a-i", "4a-ii", "4b"};

	private static String[] cannotRestartScenes = {"OpenScene"};

	//Awake is always called before any Start functions
	void Awake()
	{
		if (instance == null) {
			//Game just start
			DontDestroyOnLoad (transform.root.gameObject);
			instance = this;
			instance.secretItemFound = new bool[5];
			trueEnding = false;
			initSceneFirstRun();
		} else if (instance != this) {
			Destroy (gameObject);
			Debug.Log ("destroy Game Manager duplicate on Awake");

			if (!instance.currentLevel.Equals (SceneManager.GetActiveScene ().name)) {
				// Just enter a new scene
				initSceneFirstRun();
			} else {
				initScene ();
				instance.firstRun = false;
			}
		}
	}

	void Start(){
	}

	void Update(){
		if (Input.GetKeyDown(KeyCode.S)){
			Save ();
		} 
		if (Input.GetKeyDown(KeyCode.L)){
			Load ();
			//SceneManager.LoadScene (currentLevel);
		}

		if (Input.GetButton ("Restart") && !cannotRestartScenes.ToList().Contains(instance.currentLevel)) {
			SceneManager.LoadScene (instance.currentLevel);
		}
	}

	//Initialize Variables here that won't change even if you retry the scene
	private void initSceneFirstRun(){
		Debug.Log("first run");
		instance.firstRun = true;
		instance.currentLevel = SceneManager.GetActiveScene ().name;
		int numTextTriggers = GameObject.FindGameObjectsWithTag ("TextTrigger").Length;
		instance.textTriggered = new bool[numTextTriggers];

		initScene ();

		if (canSaveScenes.ToList().Contains(instance.currentLevel)) {
			Save ();
		}

	}

	// Initialize variables needed every scenes regardless of how many retries
	private void initScene(){
		instance.saveText = GameObject.Find("MainCam/Canvas/Save");
	}
		
	public static GameManager getInstance () {
		return (GameManager) instance;
	}

	public bool isTrueEnding(){
		return trueEnding;
	}

	public void setTrueEnding(){
		trueEnding = true;
	}

	public void Save(){
		SaveData ();

		instance.saveText.SetActive (true);
		instance.saveText.GetComponent<disableTextUI> ().disableText ();
	}

	void SaveData ()
	{
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Open (Application.persistentDataPath + "/MasterOfPuppets.dat", FileMode.OpenOrCreate);
		GameState data = new GameState ();
		data.currentLevel = instance.currentLevel;
		data.secretItemFound = instance.secretItemFound;
		Debug.Log ("saving data at " + data.currentLevel);
		bf.Serialize (file, data);
		file.Close ();
	}

	public void Load(){

		if (File.Exists (Application.persistentDataPath + "/MasterOfPuppets.dat")) {
			LoadData ();
			SceneManager.LoadScene (GameManager.getInstance ().currentLevel);
			initSceneFirstRun();
		}


	}

	void LoadData ()
	{
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Open (Application.persistentDataPath + "/MasterOfPuppets.dat", FileMode.Open);
		GameState data = (GameState)bf.Deserialize (file);
		file.Close ();
		Debug.Log ("loading data at" + data.currentLevel);

		instance.currentLevel = data.currentLevel;
		instance.secretItemFound = data.secretItemFound;
	}
}

[Serializable]
class GameState
{
	public string currentLevel;
	public bool[] secretItemFound;

	public GameState(){
		currentLevel = "";
		secretItemFound = new bool[5];
	}
}