using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This script is for making audio player louder/quieter depending on the distance to the audio
/// source. 
/// 
/// Make sure to look at the object's parent and assaign the correct localScale.
/// </summary>


public class AudioDistanceController : MonoBehaviour {

    public float maxVolume;

    //The radius of the circle collider is determined by the local scale (For example if 
    // the parent container was scaled to 0.5 and the radius was 15, the actuall global radius 
    // would only be 7.5. 
    public float localScale;
    private BoxCollider2D audioRangeCollider;
    private AudioSource audioSource;
    private List<GameObject> dolls = new List<GameObject>();

    // Use this for initialization
    void Start() {
        audioRangeCollider = GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update() {
        GameObject player = dolls.Find(x => x.CompareTag("Player"));
        if (player != null) {
            changeVolumeByDistance(player);
        } else {
            audioSource.volume = 0;
        }
    }

    //Add the possible player to the list of dolls
    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            dolls.Add(other.gameObject);
        }
    }

    //Remove the doll from the list of dolls in range
    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            dolls.Remove(other.gameObject);
        }
    }

    //Change the volume of the audioSource relative to the distance of the player to the source
    private void changeVolumeByDistance(GameObject player) {
        float distanceFromPlayer = Vector2.Distance(player.transform.position, transform.position);
		float maxDistance = Mathf.Sqrt (audioRangeCollider.size.x * localScale * audioRangeCollider.size.y * localScale);
		float volumePercentage = 1 - distanceFromPlayer / maxDistance; 

		Debug.Log (volumePercentage);
        audioSource.volume = maxVolume * volumePercentage;
    }   
}
