﻿using UnityEngine;
using System.Collections;

public class receiveSignalGhostWall : receiveSignal {

	public AudioSource[] ghostWallAudioSources;
	private Renderer renderer;
   

	void Start(){
		renderer = GetComponent<Renderer> ();

		GameObject parent = transform.parent.gameObject;
		ghostWallAudioSources = parent.GetComponentsInChildren<AudioSource>();
	}

	public override void activate(){
		if (GetComponent<Collider2D> () != null)
			GetComponent<Collider2D> ().enabled = false;

		foreach (AudioSource ghostWallAudio in ghostWallAudioSources){
			ghostWallAudio.enabled = false;
		}

		renderer.enabled = false;
	}

	public override void deactivate(){
		if (GetComponent<Collider2D> () != null)
			GetComponent<Collider2D> ().enabled = true;

		foreach (AudioSource ghostWallAudio in ghostWallAudioSources){
			ghostWallAudio.enabled = true;
		}

		renderer.enabled = true;
	}

}
