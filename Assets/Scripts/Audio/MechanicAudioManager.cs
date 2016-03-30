using UnityEngine;
using System.Collections;

public class MechanicAudioManager : AbstractAudioManager {

    //Audioclips
    public AudioClip buttonClip;

    //AudioSources
    public AudioSource button;

    //Volume
    public float buttonVolume;

    public static MechanicAudioManager instance;

    void Awake() {
        if (instance != null) {
            Debug.LogError("Multiple instances of SoundEffectsHelper!");
        }
        instance = this;
    }

    public static MechanicAudioManager getInstance() {
        return instance;
    }

    public void playButtonSound() {
        playOneShotSound(buttonClip, button, buttonVolume);
    }
}
