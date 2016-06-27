using UnityEngine;
using System.Collections;

public class checkOneTimeTrigger : MonoBehaviour {

	public bool hasBeenTriggeredOnce;

	// Use this for initialization
	void Start () {
		UpdateTrigger ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void UpdateTrigger () {
		GameManager.getInstance ().objectsTriggered.TryGetValue (gameObject.name, out hasBeenTriggeredOnce);
	}

	public void setTriggered(){
		GameManager.getInstance ().objectsTriggered.Add (gameObject.name, true);
		UpdateTrigger ();
	}
}
