using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Linq;


public class Controller2 : MonoBehaviour {

	public GameObject levelManager;
	
	public bool ghostMode = false;
	private int radius;
    private float dollSpeed = 10f;

    //Doll Prototype Version Properties
    public float maxSpeedSlowerBy = 0;
    public bool canPushButtons = true;
    public bool canOnlyMoveUpAndDown = false;

    public Animator anim;
	private Rigidbody2D rigidbody2D2;

	public bool allowSound;
	
	// Use this for initialization
	void Start () {
		levelManager = GameObject.FindGameObjectWithTag ("LevelManager");
		dollSpeed = levelManager.GetComponent<DollManager> ().maxSpeed - maxSpeedSlowerBy;
		anim = GetComponent<Animator>();
		rigidbody2D2 = GetComponent<Rigidbody2D> ();
		anim.SetFloat("Y", -1); // face the front
		allowSound = true;
    }
	
	void FixedUpdate(){

		manageInput ();

		if (!ghostMode) {

			/*ANIMATION*/
			float input_x = canOnlyMoveUpAndDown ? 0 : Input.GetAxisRaw ("Horizontal");
			float input_y = Input.GetAxisRaw ("Vertical");
			move (input_x,input_y);
			bool isWalking = (Mathf.Abs (input_x) + Mathf.Abs (input_y)) > 0;
			anim.SetBool ("isWalking", isWalking);
			/*--------*/

			if (isWalking) {
				anim.SetFloat ("X", input_x);
				anim.SetFloat ("Y", input_y);
				if(allowSound)
					DollAudioManager.getInstance ().playWalkingSound ();
			} else {
				DollAudioManager.getInstance().stopWalkingSound();
			}
		}
      
        else {
			stopWalking ();

		}

	}
		
	void move(float moveHorizontal, float moveVertical)
	{
		rigidbody2D2.MovePosition (new Vector2 (rigidbody2D2.position.x + (Time.deltaTime * dollSpeed * moveHorizontal), rigidbody2D2.position.y + (Time.deltaTime * dollSpeed * moveVertical)));
	}
		
	public void stopWalking(){
		DollAudioManager.getInstance().stopWalkingSound();
		anim.SetBool ("isWalking", false);
	}

	void manageInput(){
		if (!ghostMode) {
			if (Input.GetButtonDown("ghostMode") && !SceneManager.GetActiveScene().Equals(SceneManager.GetSceneByName("OpenScene"))){
				ghostMode = true;
				Input.ResetInputAxes();
			}
			if (Input.GetButtonDown ("Interact")) {
				startInteraction ();
			}
		} else {
			if (Input.GetButtonDown("cancelGhostMode")){
				ghostMode = false;
				Input.ResetInputAxes();
			}
			if (Input.GetButtonDown ("Possess")) {
				levelManager.GetComponent<GhostSwitchManager> ().possessCurrentSelection ();
			}
		}
	}

	//whenever player press Z
	void startInteraction(){

		float Horizontal = anim.GetFloat ("X");
		float Vertical = anim.GetFloat ("Y");

		var direction = new Vector3 (0, 0, 0);

		if (Horizontal> 0) {
			direction = new Vector3 (0.8f, 0, 0);
		} else if (Horizontal < 0) {
			direction = new Vector3 (-0.8f, 0, 0);
		} else if (Vertical > 0) {
			direction = new Vector3 (0, 1, 0);
		} else if (Vertical < 0) {
			direction = new Vector3 (0, -1, 0);
		}

		GameObject toInteract = objectToInteract (direction);
		if (toInteract != null) {
			toInteract.GetComponent<Interact> ().interact ();
		}
			
	}

	private GameObject objectToInteract(Vector3 direction){
		LayerMask layer = 1 << LayerMask.NameToLayer ("Interactive") | 1 << LayerMask.NameToLayer ("Wall");
		List<GameObject> interactables = new List<GameObject> ();

		// create raycast from -45 degrees to 45 degrees
		for (int delta_degree = 1; delta_degree < 90; delta_degree++) {
			Quaternion quartenion = Quaternion.AngleAxis (delta_degree-45, Vector3.forward);
			RaycastHit2D[] hits = 
				Physics2D.RaycastAll(this.transform.position, quartenion * direction,1.5f, layer);
			Debug.DrawRay (this.transform.position ,quartenion * direction*1.5f, Color.green,0.1f);

			// prevent wall to come between player and interactive objects
			foreach (RaycastHit2D hit in hits) {
				if (hit.collider != null) {
					if (hit.collider != null) {
						if (hit.collider.CompareTag ("impenetrable"))
							break;
						// Record interatables item
						interactables.Add (hit.collider.gameObject);
					}
				}
			}

		}

		if (interactables.Count == 0)
			return null;

		GameObject most = interactables.GroupBy(i=>i).OrderByDescending(grp=>grp.Count())
			.Select(grp=>grp.Key).First();

		// most common interactable in the list is the closest one
		return most;
	}

    //Helper to get DollSpeed
    public float getDollSpeed() {
        return dollSpeed;
    }

		
}

