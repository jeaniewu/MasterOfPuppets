using UnityEngine;
using System.Collections;

public class TestSongSwitch : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player") || other.CompareTag("Doll")) {
            MusicManager musicManager = MusicManager.getInstance();
            musicManager.setSongSwitch(musicManager.mainTheme, musicManager.mechanicalTheme, musicManager.mechanicalThemeVolume);
        }
    }
}
