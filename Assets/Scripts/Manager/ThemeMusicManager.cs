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

	//Song Volume
	public float mainThemeVolume;
	public float mechanicalThemeVolume;
	public float choirThemeVolume;
    public float titleThemeVolume;

	private static String[] mainThemeScenes = {"OpenScene"};
	private static String[] mechanicalThemeScenes = {"1a", "1b", "2a", "2b", "3a", "3b"};
	private static String[] choirThemeScenes = {"4a-i", "4a-ii", "4b", "5a", "5b"};
    private static String[] titleThemeScenes = { "TitleScreen"};

	void Awake() {
		if (instance == null) {
			DontDestroyOnLoad (transform.root.gameObject);
			instance = this;
		} else if (instance != this) {
			// Final Scene has it's own Music Manager
			if (SceneManager.GetActiveScene ().name.Equals ("finalScene")) {
                //fade out the current playing song (title theme if loading the final scene from the title screen)
                if (instance.choirTheme.isPlaying) {
                    setTrackToFadeOut(instance.choirTheme);
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
		if (mainThemeScenes.ToList ().Contains (currentLevel)) {
            if (instance.titleTheme.isPlaying) {
                setSongSwitch(instance.titleTheme, instance.mainTheme, mainThemeVolume);
            } else {
                startMainTheme();
            }
		} else if (mechanicalThemeScenes.ToList ().Contains (currentLevel)) {
            if (currentLevel.Equals("1a") && instance.mainTheme.isPlaying) {
                setSongSwitch(instance.mainTheme, instance.mechanicalTheme, mechanicalThemeVolume);
            } else if (instance.titleTheme.isPlaying) {
                setSongSwitch(instance.titleTheme, instance.mechanicalTheme, mechanicalThemeVolume);
            } else if (!instance.mechanicalTheme.isPlaying) {
                startMechanicalTheme();
            }
		} else if (choirThemeScenes.ToList ().Contains (currentLevel)) {
			if (currentLevel.Equals ("4a-i") && instance.mechanicalTheme.isPlaying) {
				setSongSwitch (instance.mechanicalTheme, instance.choirTheme, choirThemeVolume);
			} else if (instance.titleTheme.isPlaying) {
                setSongSwitch(instance.titleTheme, instance.choirTheme, choirThemeVolume);
            } else if (!instance.choirTheme.isPlaying) {
                startChoirTheme();
            }
        } else if (titleThemeScenes.ToList().Contains(currentLevel)) {
            startTitleTheme();
        }
	}

	public static ThemeMusicManager getInstance() {
		return (ThemeMusicManager) instance;
	}

    public void startTitleTheme() {
        setTrackToFadeIn(instance.titleTheme, titleThemeVolume);
    }

	public void startMainTheme() {
		setTrackToFadeIn(instance.mainTheme, mainThemeVolume);
	}

	public void startMechanicalTheme() {
		setTrackToFadeIn(instance.mechanicalTheme, mechanicalThemeVolume);
	}

	public void startChoirTheme() {
		setTrackToFadeIn(instance.choirTheme, choirThemeVolume);
	}
}
