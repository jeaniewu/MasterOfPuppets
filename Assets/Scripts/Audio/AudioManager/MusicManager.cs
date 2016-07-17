using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

public class MusicManager : AbstractAudioManager {

	public float defaultFadeInSpeed;
    public float defaultFadeOutSpeed;

	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
	}  

	protected void setTrackToFadeIn(AudioSource source, float volume, float fadeInSpeed = 0f) {
		if (fadeInSpeed == 0)
			fadeInSpeed = defaultFadeInSpeed;
		
		source.volume = 0;
		playLoopingSound(source);
		StartCoroutine(fadeIn(source, volume, fadeInSpeed));
    }

	IEnumerator fadeIn (AudioSource source, float volume, float fadeInSpeed){
		while (source.volume < volume) {
			source.volume += fadeInSpeed * Time.deltaTime;
			yield return null;
		}
		yield break;
	}

	protected void setTrackToFadeOut(AudioSource source, float fadeOutSpeed = 0f) {
		if (fadeOutSpeed == 0)
			fadeOutSpeed = defaultFadeOutSpeed;
		
		StartCoroutine(fadeOut(source, fadeOutSpeed));
    }

	IEnumerator fadeOut (AudioSource source, float fadeOutSpeed){
		while (source.volume > 0) {
			source.volume -= fadeOutSpeed * Time.deltaTime;
			yield return null;
		}
		source.Stop ();
		yield break;
	}

    public void setSongSwitch(AudioSource sourceFrom, AudioSource sourceTo, float sourceToVolume) {
		setTrackToFadeOut (sourceFrom);
		setTrackToFadeIn (sourceTo, sourceToVolume);
    }

}
