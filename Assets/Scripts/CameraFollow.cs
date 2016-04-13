using System;
using UnityEngine;



public class CameraFollow : MonoBehaviour
{
	public float maxSpeed = 10f;
	public float xMargin = 1f; // Distance in the x axis the player can move before the camera follows.
    public float yMargin = 1f; // Distance in the y axis the player can move before the camera follows.
    public float xSmooth = 8f; // How smoothly the camera catches up with it's target movement in the x axis.
    public float ySmooth = 8f; // How smoothly the camera catches up with it's target movement in the y axis.
    public Vector2 maxXAndY; // The maximum x and y coordinates the camera can have.
    public Vector2 minXAndY; // The minimum x and y coordinates the camera can have.
	public float xMovementBound;
	public float yMovementBound;

    private Transform m_Player; // Reference to the player's transform.
	public GameObject player;


    private void Awake()
    {
        // Setting up the reference.
		player = GameObject.FindGameObjectWithTag("Player");
        m_Player = player.transform;
    }


    private bool CheckXMargin()
    {
        // Returns true if the distance between the camera and the player in the x axis is greater than the x margin.
        return Mathf.Abs(transform.position.x - m_Player.position.x) > xMargin;
    }


    private bool CheckYMargin()
    {
        // Returns true if the distance between the camera and the player in the y axis is greater than the y margin.
        return Mathf.Abs(transform.position.y - m_Player.position.y) > yMargin;
    }


    private void Update()
    {
		var controller2 = player.GetComponent<Controller2> ();
		var animator = player.GetComponent<Animator> ();

		if (!controller2.ghostMode) {
			if (Input.GetKey (KeyCode.C)) {
				controller2.enabled = false;
				animator.enabled = false;
				DollAudioManager.getInstance().stopWalkingSound();
				moveCamera ();
			} else {
				controller2.enabled = true;
				animator.enabled = true;
				TrackPlayer ();
			}
		} 

    }


    private void TrackPlayer()
    {
        // By default the target x and y coordinates of the camera are it's current x and y coordinates.
        float targetX = transform.position.x;
        float targetY = transform.position.y;

        // If the player has moved beyond the x margin...
        if (CheckXMargin())
        {
            // ... the target x coordinate should be a Lerp between the camera's current x position and the player's current x position.
            targetX = Mathf.Lerp(transform.position.x, m_Player.position.x, xSmooth*Time.deltaTime);
        }

        // If the player has moved beyond the y margin...
        if (CheckYMargin())
        {
            // ... the target y coordinate should be a Lerp between the camera's current y position and the player's current y position.
            targetY = Mathf.Lerp(transform.position.y, m_Player.position.y, ySmooth*Time.deltaTime);
        }

        // The target x and y coordinates should not be larger than the maximum or smaller than the minimum.
        targetX = Mathf.Clamp(targetX, minXAndY.x, maxXAndY.x);
        targetY = Mathf.Clamp(targetY, minXAndY.y, maxXAndY.y);

        // Set the camera's position to the target position with the same z component.
        transform.position = new Vector3(targetX, targetY, transform.position.z);
    }

	public void findPlayer(){
		player = GameObject.FindGameObjectWithTag("Player");
		m_Player = player.transform;
	}

	private void moveCamera(){
		Debug.Log ("move");
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		transform.position += new Vector3 (moveHorizontal, moveVertical, 0).normalized * Time.deltaTime * maxSpeed;

		transform.position = new Vector3 
			(
				Mathf.Clamp(transform.position.x, m_Player.position.x-xMovementBound, m_Player.position.x+xMovementBound), 
				Mathf.Clamp(transform.position.y, m_Player.position.y-yMovementBound, m_Player.position.y+yMovementBound),
				transform.position.z
			);

		Debug.Log (moveHorizontal);
		Debug.Log (moveVertical);

	}

	public void ghostModeSelect(Vector3 position){
		float targetX = Mathf.Lerp(transform.position.x, position.x, xSmooth*Time.deltaTime);
		float targetY = Mathf.Lerp(transform.position.y, position.y, ySmooth*Time.deltaTime);
		transform.position = new Vector3(targetX, targetY, transform.position.z);
	}
}

