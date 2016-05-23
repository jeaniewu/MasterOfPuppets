using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class SoundEffectFadeController : MonoBehaviour {

    private float maxVolume;
	private float fadeTime;

	public bool hasPlayer;

    private AudioSource audioSource;
    public List<GameObject> dolls = new List<GameObject>();

    // Use this for initialization
    void Start() {
        audioSource = GetComponent<AudioSource>();
		fadeTime = MechanicAudioManager.getInstance ().fadeTime;

	
		if (audioSource.clip.name == "Conveyor belt") {
			maxVolume = MechanicAudioManager.getInstance ().maxConveyorBeltVolume;
		} else if (audioSource.clip.name == "GhostWallSound") {
			maxVolume = MechanicAudioManager.getInstance ().maxGhostWallVolume;
		} else if (audioSource.clip.name == "Sawblade Sound") {
			maxVolume = MechanicAudioManager.getInstance ().maxSawBladeVolume;
		}
    }
		

	void Update(){
		GameObject player = dolls.Find(x => x.CompareTag("Player"));
		if (player == null && hasPlayer) {
			fadeOut ();
			hasPlayer = false;
		} else if (player != null && !hasPlayer){
			fadeIn ();
			hasPlayer = true;
		}
	}

    //Add the possible player to the list of dolls
    void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Doll")) {
			if (!dolls.Contains (other.gameObject)) 
				dolls.Add (other.gameObject);
        }
    }

    //Remove the doll from the list of dolls in range
    void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Doll")) {
            dolls.Remove(other.gameObject);
        }
    }
		
	public void fadeIn() { 
		if(fadeTime == 0) { 
			audioSource.volume = maxVolume;
			return;
		}
		StartCoroutine("_FadeIn"); 
	}

	IEnumerator _FadeIn() {
		float t = 0;
		while (t < fadeTime) {
			yield return null;
			t+= Time.deltaTime;
			audioSource.volume = t/fadeTime * maxVolume;
		}
		yield break;
	}

	public void fadeOut() { 
		if(fadeTime == 0) { 
			audioSource.volume = 0;
			return;
		}
		StartCoroutine("_FadeOut"); 
	}

	IEnumerator _FadeOut() {
		float t = fadeTime;
		while (t > 0) {
			yield return null;
			t-= Time.deltaTime;
			audioSource.volume = t/fadeTime * maxVolume;
		}
		yield break;
	}
}
