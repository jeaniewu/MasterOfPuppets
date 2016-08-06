using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;
using System;


public class Navigation_Optiions : MonoBehaviour {
	public AudioMixer masterMixer;
	private static float MAXVAL = 10f;
	public float soundFillAmount = 10f;
	public float musicFillAmount = 10f;
	public GameObject PauseMenu;
	public Button OptionsBtn;
	public GameObject soundSlider;
	public GameObject musicSlider;


	// Use this for initialization
	void Start () {
		musicFillAmount = GameManager.getInstance ().MusicSliderValue;
		setMusicLvl (musicFillAmount);
		musicSlider.GetComponent<Slider> ().value = musicFillAmount;

		soundFillAmount = GameManager.getInstance ().SFXSliderValue;
		setSfxLvl (soundFillAmount);
		soundSlider.GetComponent<Slider> ().value = soundFillAmount;
	}

	// Update is called once per frame
	void Update () {
		Music();
		SFX();


	}

	public void AdjustSFX(float newFillAmnt){
		Debug.Log (newFillAmnt);
		soundFillAmount = newFillAmnt ;


	}

	public void AdjustMusic(float newFillAmnt){
		musicFillAmount = newFillAmnt;
	}

	public void Back(){
		gameObject.SetActive(false);
		PauseMenu.SetActive(true);
		OptionsBtn.Select ();
	}
		

	void Music(){
		setMusicLvl (0 - (60/MAXVAL) * (MAXVAL -  musicFillAmount));
		GameManager.getInstance ().MusicSliderValue = musicFillAmount;
	}

	void SFX(){
		setSfxLvl (0 - (70/MAXVAL) * (MAXVAL - soundFillAmount));
		GameManager.getInstance ().SFXSliderValue = soundFillAmount;
	}
 

	public void setSfxLvl (float sfxLvl){
		masterMixer.SetFloat ("SFXVol", sfxLvl);
	}

	public void setMusicLvl (float musicLvl){
		masterMixer.SetFloat ("ThemeSongVol", musicLvl);
	}
}
