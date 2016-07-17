using UnityEngine;
using System.Collections;

public class FinalSceneMusicManager : MusicManager{

	//Singleton Instantiation
	public static FinalSceneMusicManager instance;

	void Awake() {
		if (instance != null) {
			FinalSceneMusicManager temp = instance;
			Destroy (temp);
			//Debug.LogError("Multiple instances of FinalSceneMusicManager!");
		}
		instance = this;
	}

	public AudioSource[] tracks;
	public float trackVolume;

	public static FinalSceneMusicManager getInstance() {
		return (FinalSceneMusicManager) instance;
	}

	public void playTrack(int index){
		setTrackToFadeIn (tracks [index], trackVolume);
	}

	public void playTrack(int index, float customVolume){
		setTrackToFadeIn (tracks [index], customVolume);
	}

	public void stopTrack(int index){
		setTrackToFadeOut (tracks [index]);
	}

	public void switchTrack(int fromIndex, int toIndex){
		setSongSwitch (tracks [fromIndex], tracks [toIndex], trackVolume);
	}
}
