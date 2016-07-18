using UnityEngine;
using System.Collections;
using UnityEngine.UI;
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
    public float soundFillAmount = 10f;
    public float musicFillAmount = 10f;
    public Slider soundSlider;
    public Slider musicSlider;
    public MechanicAudioManager soundAudio;
    public DollAudioManager dollAudio;
    public ThemeMusicManager mainMusic;

   // public AudioListener soundAudio;



    // Use this for initialization
    void Start()
    {
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
                 
                    soundFillAmount--;
                   
                   
                     
                }
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    sound_Slider_selected.color = Color.red;
                    Debug.Log("Pressed sound");
                    soundFillAmount++;

                }
                soundSlider.value = soundFillAmount / 10;
                soundAudio.buttonVolume = soundFillAmount / 10;
                soundAudio.unlockDoorVolume = soundFillAmount / 10;
                soundAudio.maxConveyorBeltVolume = soundFillAmount / 10;
                soundAudio.maxGhostWallVolume = soundFillAmount / 10;
                soundAudio.maxSawBladeVolume = soundFillAmount / 10;
                soundAudio.deathBySawbladeVolume = soundFillAmount / 10;
                dollAudio.ghostSwitchVolume = soundFillAmount / 10;
                
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
                    musicFillAmount--;



                }
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    music_Slider_selected.color = Color.red;   
                    musicFillAmount++;

                }
                musicSlider.value = musicFillAmount / 10;

				mainMusic.mainthemevolumeAdjuster(musicFillAmount/10);
				mainMusic.mechthemevolumeAdjuster(musicFillAmount/10);	


            }
            if (selectedOption == 2)
            {
                sound_Slider_selected.color = Color.white;
                music_Slider_selected.color = Color.white;
                SoundDeSelected();
                MusicDeSelected();
                BackSelected();
                if (Input.GetKeyDown(KeyCode.Return))
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
   
}
