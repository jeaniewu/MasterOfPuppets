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

	private static String[] mainThemeScenes = {"OpenScene"};
	private static String[] mechanicalThemeScenes = {"1a", "1b", "2a", "2b", "3a", "3b"};

	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
		if (instance != null) {
			Debug.LogError("Multiple instances of ThemeMusicManager!");
		}
		instance = this;
	}

	void Start(){
		String currentLevel = SceneManager.GetActiveScene ().name;
		if (mainThemeScenes.ToList ().Contains (currentLevel)) {
			startMainTheme ();
		} else if (mechanicalThemeScenes.ToList ().Contains (currentLevel)) {
			startMechanicalTheme ();
		}
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
