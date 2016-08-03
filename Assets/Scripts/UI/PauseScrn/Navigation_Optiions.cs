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
	public Button ResumeBtn;
	public Text SFXtxt;
	public Text MscText;



	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		Music();
		SFX();


	}

	public void AdjustSFX(float newFillAmnt){
		Debug.Log (newFillAmnt);
		soundFillAmount = newFillAmnt ;
		SFXtxt.color = Color.red;
		MscText.color = Color.white;

	}

	public void AdjustMusic(float newFillAmnt){
		SFXtxt.color = Color.white;
		MscText.color = Color.red;
		musicFillAmount = newFillAmnt;
	}

	public void Back(){
		SFXtxt.color = Color.white;
		MscText.color = Color.white;
		gameObject.SetActive(false);
		PauseMenu.SetActive(true);
		ResumeBtn.Select ();
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
