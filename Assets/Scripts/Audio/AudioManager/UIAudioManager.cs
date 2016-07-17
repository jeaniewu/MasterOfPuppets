using UnityEngine;
using System.Collections;

public class UIAudioManager : AbstractAudioManager {
    //AudioSources
    public AudioSource selectSoundSource;
    public AudioSource cancelSoundSource;
    public AudioSource enterSoundSource;
    public AudioSource giggleEnterSource;
    //AudioClips
    public AudioClip selectSoundClip;
    public AudioClip cancelSoundClip;
    public AudioClip enterSoundClip;
    public AudioClip giggleEnterClip;
    //AudioClip Volumes
    public float selectSoundVolume;
    public float cancelSoundVolume;
    public float enterSoundVolume;
    public float giggleEnterVolume;

    //Singleton Instantiation
    public static UIAudioManager instance;

    void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }
    }

    //Own getInstance method needed to convert types
    public static UIAudioManager getInstance() {
        return (UIAudioManager)instance;
    }
    
    //Public play methods
    public void playSelectSound() {
        playOneShotSound(selectSoundClip, selectSoundSource, selectSoundVolume);
    }
    public void playCancelSound() {
        playOneShotSound(cancelSoundClip, cancelSoundSource, cancelSoundVolume);
    }
    public void playEnterSound() {
        playOneShotSound(enterSoundClip, enterSoundSource, enterSoundVolume);
    }
    public void playGiggleEnterSound() {
        playOneShotSound(giggleEnterClip, giggleEnterSource, giggleEnterVolume);
        playEnterSound();
    }
}
