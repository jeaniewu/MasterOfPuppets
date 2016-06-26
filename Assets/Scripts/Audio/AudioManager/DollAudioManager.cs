using UnityEngine;
using System.Collections;

public class DollAudioManager : AbstractAudioManager {
    //AudioSources
    public AudioSource walking;
    public AudioSource ghostSwitch;

    //AudioClips for oneshot sounds
    public AudioClip ghostSwitchClip;

    //Singleton Instantiation
    public static DollAudioManager instance;

    void Awake() {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}
    }

    //Own getInstance method needed to convert types
    public static DollAudioManager getInstance () {
        return (DollAudioManager) instance;
    }

    //Plays the walking sound
    public void playWalkingSound () {
		playLoopingSound(instance.walking);
    }

    //stops the walking sound
    public void stopWalkingSound() {
        stopLoopingSound(instance.walking);
    }

    public void playGhostSwitchSound() {
		playOneShotSound(instance.ghostSwitchClip, instance.ghostSwitch, 0.5f);
    }

}
