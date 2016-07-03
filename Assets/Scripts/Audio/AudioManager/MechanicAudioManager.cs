using UnityEngine;
using System.Collections;

public class MechanicAudioManager : AbstractAudioManager {

    //Audioclips
    public AudioClip buttonClip;
	public AudioClip unlockDoorClip;
	public AudioClip deathBySawbladeClip;
	public AudioClip fallingDownDitchClip;

    //AudioSources
    public AudioSource button;
	public AudioSource unlockDoor;
	public AudioSource deathBySawblade;
	public AudioSource fallingDownDitch;

    //Volume
    public float buttonVolume;
	public float unlockDoorVolume;
	public float maxConveyorBeltVolume;
	public float maxGhostWallVolume;
	public float maxSawBladeVolume;
	public float deathBySawbladeVolume;
	public float fallingDownDitchVolume;

	//fadeTime
	public float fadeTime;

	public GameObject[] audioControllers;


    public static MechanicAudioManager instance;

    void Awake() {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}
    }

	void Start(){
		audioControllers = GameObject.FindGameObjectsWithTag ("AudioController");
	}

    public static MechanicAudioManager getInstance() {
        return instance;
    }

    public void playButtonSound() {
		playOneShotSound(instance.buttonClip, instance.button, buttonVolume);
    }
		
	public void playUnlockDoorSound() {
		playOneShotSound(instance.unlockDoorClip, instance.unlockDoor, unlockDoorVolume);
	}

	public void playDeathBySawBlade() {
		playOneShotSound(instance.deathBySawbladeClip, instance.deathBySawblade, deathBySawbladeVolume);
	}

	public void playFallingDownDitchSound() {
		playOneShotSound(instance.fallingDownDitchClip, instance.fallingDownDitch, fallingDownDitchVolume);
	}

}
