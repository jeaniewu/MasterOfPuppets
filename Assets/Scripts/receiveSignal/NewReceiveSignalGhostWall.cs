using UnityEngine;
using System.Collections;

public class NewReceiveSignalGhostWall : receiveSignal {
    private AudioSource ghostWallAudioSource;
    private SpriteRenderer[] spriteRenderers;

    void Start() {
        ghostWallAudioSource = GetComponentInChildren<AudioSource>();
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
    }

    public override void activate() {
        if (GetComponent<Collider2D>() != null)
            GetComponent<Collider2D>().enabled = false;
        foreach(SpriteRenderer renderer in spriteRenderers) {
            renderer.enabled = false;
        }
        ghostWallAudioSource.enabled = false;
    }

    public override void deactivate() {
        if (GetComponent<Collider2D>() != null)
            GetComponent<Collider2D>().enabled = true;
        foreach (SpriteRenderer renderer in spriteRenderers) {
            renderer.enabled = true;
        }
        ghostWallAudioSource.enabled = true;
    }
}
