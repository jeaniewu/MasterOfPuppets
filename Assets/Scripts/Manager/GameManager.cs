using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;                                 

	public bool trueEnding;
	public string currentLevel;
	public bool[] secretItemFound;

	public bool firstRun;
	public bool[] textTriggered;

	private static String[] saveScene = {"OpenScene", "1a", "1b", "2a", "2b", "3a", "3b"};

	//Awake is always called before any Start functions
	void Awake()
	{
		if (instance == null) {
			DontDestroyOnLoad(transform.root.gameObject);
			instance = this;
			instance.secretItemFound = new bool[5];
			instance.firstRun = true;
		} else if (instance != this) {
			Destroy (gameObject);
			instance.firstRun = false;
			Debug.Log ("destroy");
		}
		updateLevel ();
	}

	void Start(){
		trueEnding = false;
	}

	void Update(){
		if (Input.GetKeyDown(KeyCode.S)){
			Save ();
		} 
		if (Input.GetKeyDown(KeyCode.L)){
			Load ();
			//SceneManager.LoadScene (currentLevel);
		}
	}

	private void updateLevel(){
		if (!instance.currentLevel.Equals(SceneManager.GetActiveScene ().name)) {
			instance.currentLevel = SceneManager.GetActiveScene ().name;
			Debug.Log ("updating level.. " + SceneManager.GetActiveScene ().name);
			if (saveScene.ToList().Contains(instance.currentLevel)) {
				Save ();
			}
			instance.firstRun = true;
		}
	}
		
	public static GameManager getInstance () {
		return (GameManager) instance;
	}

	public bool isTrueEnding(){
		return trueEnding;
	}

	public void Save(){
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

			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/MasterOfPuppets.dat", FileMode.Open);
			GameState data = (GameState) bf.Deserialize (file);
			file.Close ();
			Debug.Log ("loading data at" + data.currentLevel);

			instance.currentLevel = data.currentLevel;
			instance.secretItemFound = data.secretItemFound;
		}
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