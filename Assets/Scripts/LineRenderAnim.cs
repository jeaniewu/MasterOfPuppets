using UnityEngine;
using System.Collections;
using System;

public class LineRenderAnim : MonoBehaviour {

	public Transform startTransform;
	public Transform endTransform;
	public float oscillationPeriod;
	public float pointSpacing = 0.8F;

	private int lengthOfLineRenderer;

	private LineRenderer currentLine;


	// Use this for initialization
	void Start () {
		// Set Line Renderer to the correct layer and order
		currentLine = GetComponent<LineRenderer> ();
		currentLine.sortingLayerName = "Collider";
		currentLine.sortingOrder = 1;

		// Set the length of the line renderer relative to the location of ghsot wall projector
		lengthOfLineRenderer = (int) Math.Floor(computeLength (startTransform.localPosition, endTransform.localPosition));
		currentLine.SetVertexCount (lengthOfLineRenderer);
	}


	// Update is called once per frame
	void Update () {
		float t = Time.time * oscillationPeriod;
		for (int i = 0; i < lengthOfLineRenderer; i++){
			Vector3 position = new Vector3 (i* pointSpacing,
				(Mathf.Sin ((i*0.5F) + t))/4, 0);
			currentLine.SetPosition (i, position);
		}
	}

	float computeLength (Vector3 start, Vector3 end){
		// figure out to use horizontal or vertical length diff
		float xPositionDiff = Math.Abs(start.x-end.x);
		float yPositionDiff = Math.Abs(start.y-end.y);
		float lengthDiff = (xPositionDiff > yPositionDiff) ? xPositionDiff : yPositionDiff;

		return lengthDiff / pointSpacing;
	}
}