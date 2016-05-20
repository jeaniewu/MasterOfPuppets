using UnityEngine;
using System.Collections;

public class MechanicAudioManager : AbstractAudioManager {

    //Audioclips
    public AudioClip buttonClip;
	public AudioClip unlockDoorClip;

    //AudioSources
    public AudioSource button;
	public AudioSource unlockDoor;

    //Volume
    public float buttonVolume;
	public float unlockDoorVolume;
	public float maxConveyorBeltVolume;
	public float maxGhostWallVolume;
	public float maxSawBladeVolume;

	//fadeTime
	public float fadeTime;

	public GameObject[] audioControllers;


    public static MechanicAudioManager instance;

    void Awake() {
        if (instance != null) {
            Debug.LogError("Multiple instances of SoundEffectsHelper!");
        }
        instance = this;
    }

	void Start(){
		audioControllers = GameObject.FindGameObjectsWithTag ("AudioContoller");
	}

    public static MechanicAudioManager getInstance() {
        return instance;
    }

    public void playButtonSound() {
        playOneShotSound(buttonClip, button, buttonVolume);
    }
		
	public void playUnlockDoorSound() {
		playOneShotSound(unlockDoorClip, unlockDoor, unlockDoorVolume);
	}

}
