using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;
using System;


public class Navigation_Optiions : MonoBehaviour {

    public Text SOUND;
    public Text MUSIC;
    public Text BACK;

    public Image sound_Slider_selected;
    public Image music_Slider_selected;


    public GameObject PauseMenu;
    //public GameObject MusicOption;
    //private Text[] menuOptions;
    private int selectedOption;

	public AudioMixer masterMixer;

	public float soundFillAmount;
	public float musicFillAmount;
    public Slider soundSlider;
    public Slider musicSlider;

	private static float MAXVAL = 10f;


    // Use this for initialization
    void Start()
    {
		soundFillAmount = GameManager.getInstance ().SFXSliderValue;
		soundSlider.value = soundFillAmount;
		musicFillAmount = GameManager.getInstance ().MusicSliderValue;
		musicSlider.value = musicFillAmount;
        SoundSelected();
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectedOption++;
          
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            selectedOption--;
           
        }


        if (selectedOption <= 2 && selectedOption >= 0)
        {
            if (selectedOption == 0)
            {
                music_Slider_selected.color = Color.white;
                SoundSelected();
                MusicDeSelected();
                BackDeSelected();

                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    sound_Slider_selected.color = Color.red;
					if (soundFillAmount > 0)
						soundFillAmount--; 
                     
                }
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    sound_Slider_selected.color = Color.red;
					if (soundFillAmount < MAXVAL)
                    	soundFillAmount++;

                }
				soundSlider.value = soundFillAmount;
				setSfxLvl (0 - (70/MAXVAL) * (MAXVAL - soundFillAmount));
				GameManager.getInstance ().SFXSliderValue = soundFillAmount;
            }
            if (selectedOption == 1)
            {
                sound_Slider_selected.color = Color.white;
                SoundDeSelected();
                MusicSelected();
                BackDeSelected();

                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    music_Slider_selected.color = Color.red;
					if (musicFillAmount > 0)
						musicFillAmount--;

                }
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    music_Slider_selected.color = Color.red;
					if (musicFillAmount < MAXVAL)
                    	musicFillAmount++;

                }
				musicSlider.value = musicFillAmount;
				setMusicLvl (0 - (70/MAXVAL) * (MAXVAL -  musicFillAmount));
				GameManager.getInstance ().MusicSliderValue = musicFillAmount;
            }
            if (selectedOption == 2)
            {
                sound_Slider_selected.color = Color.white;
                music_Slider_selected.color = Color.white;
                SoundDeSelected();
                MusicDeSelected();
                BackSelected();
                if (Input.GetButtonDown("Interact"))
                {
                    gameObject.SetActive(false);
                    PauseMenu.SetActive(true);
                }

            }

        }
        else if (selectedOption > 1)
        {
            BackDeSelected();
            SoundSelected();
            MusicDeSelected();
            selectedOption = 0;
        }
        else if (selectedOption < 0)
        {
            SoundDeSelected();
            BackSelected();
            selectedOption = 2;
        }

    }



    void SoundSelected()
    {
        SOUND.color = Color.red;

    }

    void MusicSelected()
    {
        MUSIC.color = Color.red;
    }

    void BackSelected()
    {
        BACK.color = Color.red;
    }


    void SoundDeSelected()
    {
        SOUND.color = Color.white;

    }

    void MusicDeSelected()
    {
        MUSIC.color = Color.white;
    }

    void BackDeSelected()
    {
        BACK.color = Color.white;
    }

	public void setSfxLvl (float sfxLvl){
		masterMixer.SetFloat ("SFXVol", sfxLvl);
	}

	public void setMusicLvl (float musicLvl){
		masterMixer.SetFloat ("ThemeSongVol", musicLvl);
	}
   
}
