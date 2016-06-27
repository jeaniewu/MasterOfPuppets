using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

public class ThemeMusicManager : MusicManager {

	//Singleton Instantiation
	public static ThemeMusicManager instance;

	//Songs
	public AudioSource mainTheme;
	public AudioSource mechanicalTheme;

	//Song Volume
	public float mainThemeVolume;
	public float mechanicalThemeVolume;

	private static String[] mainThemeScenes = {"OpenScene"};
	private static String[] mechanicalThemeScenes = {"1a", "1b", "2a", "2b", "3a", "3b"};

	void Awake() {
		if (instance == null) {
			DontDestroyOnLoad (transform.root.gameObject);
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}
	}

	public void playThemeSong (string currentLevel)
	{
		if (mainThemeScenes.ToList ().Contains (currentLevel)) {
			startMainTheme ();
		}
		else if (mechanicalThemeScenes.ToList ().Contains (currentLevel)) {
			if (currentLevel.Equals ("1a")&& instance.mainTheme.isPlaying) {
				setSongSwitch(instance.mainTheme, instance.mechanicalTheme, mechanicalThemeVolume);
			} else {
				startMechanicalTheme ();
			}
		}
	}

	public static ThemeMusicManager getInstance() {
		return (ThemeMusicManager) instance;
	}

	public void startMainTheme() {
		setTrackToFadeIn(instance.mainTheme, mainThemeVolume);
	}

	public void startMechanicalTheme() {
		setTrackToFadeIn(instance.mechanicalTheme, mechanicalThemeVolume);
	}
}
