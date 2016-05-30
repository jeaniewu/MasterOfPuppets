using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	//Singleton Instantiation
	public static GameManager instance;   

	public bool trueEnding;

	//Awake is always called before any Start functions
	void Awake()
	{
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);    
		}
			
		DontDestroyOnLoad(gameObject);
		trueEnding = false;
		//InitGame();
	}
		
	public static GameManager getInstance () {
		return (GameManager) instance;
	}

	public bool isTrueEnding(){
		return trueEnding;
	}


	//Initializes the game for each level.
	void InitGame()
	{
		

	}



	//Update is called every frame.
	void Update()
	{

	}
}
