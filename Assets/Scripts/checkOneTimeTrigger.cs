using UnityEngine;
using System.Collections;

public class checkOneTimeTrigger : MonoBehaviour {

	public int index;
	public bool hasBeenTriggeredOnce;


	// Use this for initialization
	void Start () {
		UpdateTrigger ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void UpdateTrigger () {
		hasBeenTriggeredOnce = GameManager.getInstance ().objectTriggered[index];
	}

	public void setTriggered(){
		GameManager.getInstance ().objectTriggered [index] = true;
		UpdateTrigger ();
	}
}
