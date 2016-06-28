using UnityEngine;
using System.Collections;


/**
* Abstract Class that all audioManagers should implement
**/

public class AbstractAudioManager : MonoBehaviour {

    protected void playOneShotSound(AudioClip clip, AudioSource source, float volume) {
		source.PlayOneShot(clip, volume);
    }

    //Use this when you want the previous sound to stop when you retrigger the sound
    protected void playOneShotNoOVerlay(AudioSource source, float volume) {
        source.Play();
    }

    protected void playLoopingSound(AudioSource source) {
        if (!source.isPlaying) {
            source.Play();
        }
    }

    protected void stopLoopingSound(AudioSource source) {
        if (source.isPlaying) {
            source.Stop();
        }
    }
}
