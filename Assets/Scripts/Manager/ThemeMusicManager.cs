using UnityEngine;
using System.Collections;

public class ThemeMusicManager : MusicManager {

	//Singleton Instantiation
	public static ThemeMusicManager instance;

	//Songs
	public AudioSource mainTheme;
	public AudioSource mechanicalTheme;

	//Song Volume
	public float mainThemeVolume;
	public float mechanicalThemeVolume;

	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
		if (instance != null) {
			Debug.LogError("Multiple instances of ThemeMusicManager!");
		}
		instance = this;
	}

	public static ThemeMusicManager getInstance() {
		return (ThemeMusicManager) instance;
	}

	public void startMainTheme() {
		setTrackToFadeIn(mainTheme, mainThemeVolume);
	}

	public void startMechanicalTheme() {
		setTrackToFadeIn(mechanicalTheme, mechanicalThemeVolume);
	}
}
