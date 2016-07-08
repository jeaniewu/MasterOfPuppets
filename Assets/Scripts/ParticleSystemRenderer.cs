using UnityEngine;
using System.Collections;

public class ParticleSystemRenderer : MonoBehaviour {

    public string renderingLayer;

	// Use this for initialization
	void Start () {
        Renderer particleSystem = GetComponent<Renderer>();
        //Change Foreground to the layer you want it to display on 
        //You could prob. make a public variable for this
        particleSystem.sortingLayerName = renderingLayer;
        particleSystem.sortingOrder = 5;
    }
}
