using UnityEngine;
using System.Collections;

public class RetryScript : MonoBehaviour {

    public GameObject restart;
	public GameObject restartJoyStick;
	private GameObject currentRestart;
    // Use this for initialization
    void Start()
    {
		currentRestart = (Input.GetJoystickNames ().Length == 0) ? restart : restartJoyStick;

        restart.SetActive(false);
		restartJoyStick.SetActive (false);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
			currentRestart.SetActive(true);
			Invoke ("deactivate", 1f);
        }
    }

	void deactivate(){
		currentRestart.SetActive(false);
		Destroy(this.gameObject);
	}
}
