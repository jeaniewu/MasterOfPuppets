using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

public class MusicManager : AbstractAudioManager {

	public float fadeInSpeed;
    public float fadeOutSpeed;

    private bool isFadingIn = false;
    private bool isFadingOut = false;
    private AudioSource currentFadeInTrack = null;
    private AudioSource currentFadeOutTrack = null;
    private float currentFadeInVolume = 0f;

    private bool isSongSwitching = false;
    private AudioSource switchingFrom;
    private AudioSource switchingTo;
    private int switchingState = 0; //representing start, fadingOut, fadingIN

	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
	}

    void Update () {
        if (isSongSwitching) {
            updateSongSwitching();
        }
        if (isFadingIn) {
            fadeIn();
        }
        if (isFadingOut) {
            fadeOut();
        }
    }

    //Fades in a song by the speed
    private void fadeIn() {
        isFadingIn = true;

        if (currentFadeInTrack.volume < currentFadeInVolume) {
            currentFadeInTrack.volume += Time.deltaTime * fadeInSpeed ;
        } else {
            isFadingIn = false;
        }
    }

    //Fades out a song by the speed
    private void fadeOut() {
        isFadingOut = true;

        if (currentFadeOutTrack.volume > 0) {
            currentFadeOutTrack.volume -= Time.deltaTime * fadeOutSpeed;
        } else {
            isFadingOut = false;
        }
    }

    protected void setTrackToFadeIn(AudioSource source, float volume) {
        currentFadeInTrack = source;
        isFadingIn = true;
        currentFadeInVolume = volume;
        source.volume = 0;
        playLoopingSound(source);
    }

	protected void setTrackToFadeOut(AudioSource source) {
        currentFadeOutTrack = source;
        isFadingOut = true;
    }

    public void setSongSwitch(AudioSource sourceFrom, AudioSource sourceTo, float sourceToVolume) {
        isSongSwitching = true;
        currentFadeInVolume = sourceToVolume;
        switchingFrom = sourceFrom;
        switchingTo = sourceTo;
    }

    private void updateSongSwitching() {
        if (switchingState == 0 ) {
            switchingState = 1;
            setTrackToFadeOut(switchingFrom);
        } else if (switchingState == 1 && !isFadingOut) {
            switchingState = 2;
            setTrackToFadeIn(switchingTo, currentFadeInVolume);
        } else if (switchingState == 2 && !isFadingIn) {
            switchingState = 0;
            isSongSwitching = false;
        }
    }

}
