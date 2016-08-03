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
	public AudioSource choirTheme;
    public AudioSource titleTheme;
	public AudioSource iceTheme;

	//Song Volume
	public float mainThemeVolume;
	public float mechanicalThemeVolume;
	public float choirThemeVolume;
    public float titleThemeVolume;
	public float iceThemeVolume;

	private static String[] mainThemeScenes = {"OpenScene", "1a", "1b", "EndCredits" };
	private static String[] mechanicalThemeScenes = {"2a", "2b", "3a", "3b"};
	private static String[] choirThemeScenes = {"4a-i", "4a-ii", "4b"};
	private static String[] iceThemeScenes = {"5a", "5b"};

	public AudioSource currentSong;

	void Awake() {
		if (instance == null) {
			DontDestroyOnLoad (transform.root.gameObject);
			instance = this;
		} else if (instance != this) {
			// Final Scene has it's own Music Manager
			if (SceneManager.GetActiveScene ().name.Equals ("finalScene")) {
                //fade out the current playing song (title theme if loading the final scene from the title screen)
                if (instance.iceTheme.isPlaying) {
					setTrackToFadeOut(instance.iceTheme);
                } else if (instance.titleTheme.isPlaying) {
                    setTrackToFadeOut(instance.titleTheme);
                }
				
				ThemeMusicManager temp = instance;
				instance = this;
				Destroy (temp);
			} else {
				Destroy (gameObject);
			}
		}
	}

	public void playThemeSong (string currentLevel)
	{
		if (!currentLevel.Equals("NewTitleScene") && instance.titleTheme.isPlaying) {
            setTrackToFadeOut(instance.titleTheme);
        } 

		if (mainThemeScenes.ToList ().Contains (currentLevel)) { 
			if (!instance.mainTheme.isPlaying) {
				startMainTheme ();
				instance.currentSong = instance.mainTheme;
			}
		} else if (mechanicalThemeScenes.ToList ().Contains (currentLevel)) {
			if (currentLevel.Equals ("2a") && instance.mainTheme.isPlaying) {
				setSongSwitch (instance.mainTheme, instance.mechanicalTheme, mechanicalThemeVolume);
			} else if (!instance.mechanicalTheme.isPlaying) {
				startMechanicalTheme ();
			}
			instance.currentSong = instance.mechanicalTheme;

		} else if (choirThemeScenes.ToList ().Contains (currentLevel)) {
			if (currentLevel.Equals ("4a-i") && instance.mechanicalTheme.isPlaying) {
				setSongSwitch (instance.mechanicalTheme, instance.choirTheme, choirThemeVolume);
			} else if (!instance.choirTheme.isPlaying) {
				startChoirTheme ();
			}
			instance.currentSong = instance.choirTheme;
		} else if (iceThemeScenes.ToList ().Contains (currentLevel)) {
			if (currentLevel.Equals ("5a") && instance.choirTheme.isPlaying) {
				setSongSwitch (instance.choirTheme, instance.iceTheme, iceThemeVolume);
			} else if (!instance.iceTheme.isPlaying) {
				startIceTheme ();
			}
			instance.currentSong = instance.iceTheme;
		} else if (currentLevel.Equals ("NewTitleScene")) {
			if (instance.currentSong != null) {
				setTrackToFadeOut(instance.currentSong);
			} 
		}
	}

	public static ThemeMusicManager getInstance() {
		return (ThemeMusicManager) instance;
	}

	public void mainthemevolumeAdjuster(float totalVolume){
		mainTheme.volume = totalVolume;
	}

	public void mechthemevolumeAdjuster(float totalVolume){
		mechanicalTheme.volume = totalVolume;
	}


    public void startTitleTheme() {
        setTrackToFadeIn(instance.titleTheme, titleThemeVolume);
    }

	public void startMainTheme() {
		setTrackToFadeIn(instance.mainTheme, mainThemeVolume);
	}

    public void fadeOutMainTheme() {
        setTrackToFadeOut(instance.mainTheme);
    }

	public void startMechanicalTheme() {
		setTrackToFadeIn(instance.mechanicalTheme, mechanicalThemeVolume);
	}

	public void startChoirTheme() {
		setTrackToFadeIn(instance.choirTheme, choirThemeVolume);
	}

	public void startIceTheme() {
		setTrackToFadeIn(instance.iceTheme, iceThemeVolume);
	}
}
