using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class Controller2 : MonoBehaviour {

	public GameObject levelManager;

	
	public bool ghostMode = false;
	private int radius;
	private DollManager.Boundary boundary;
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
		boundary = levelManager.GetComponent<DollManager>().boundary;
		dollSpeed = levelManager.GetComponent<DollManager> ().maxSpeed - maxSpeedSlowerBy;
		anim = GetComponent<Animator>();
		rigidbody2D2 = GetComponent<Rigidbody2D> ();
		anim.SetFloat("Y", -1); // face the front
		allowSound = true;
    }
	
	void FixedUpdate(){

		//manageInput ();
		//checkGhostMode ();

		if (!ghostMode) {
//			if (Input.GetButtonDown ("Interact")) {
//				startInteraction ();
//
//
//			}

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

	void OnGUI(){
		if (!ghostMode) {
			if (Event.current.keyCode == KeyCode.X && !SceneManager.GetActiveScene().Equals(SceneManager.GetSceneByName("OpenScene"))){
				ghostMode = true;
			}
			if (Event.current.keyCode == KeyCode.Z) {
				startInteraction ();
			}
		} else {
			if (Event.current.keyCode == KeyCode.Z){
				ghostMode = false;
			}
			if (Event.current.keyCode == KeyCode.X) {
				levelManager.GetComponent<GhostSwitchManager> ().possessCurrentSelection ();
			}
		}
		Event.current.Use ();
	}
		
	void move(float moveHorizontal, float moveVertical)
	{
		rigidbody2D2.MovePosition (new Vector2 (rigidbody2D2.position.x + (Time.deltaTime * dollSpeed * moveHorizontal), rigidbody2D2.position.y + (Time.deltaTime * dollSpeed * moveVertical)));
	}



	public void stopWalking(){
		DollAudioManager.getInstance().stopWalkingSound();
		anim.SetBool ("isWalking", false);
	}

//	void manageInput(){
//		if (!ghostMode) {
//			if (Input.GetButtonDown("ghostMode") && !SceneManager.GetActiveScene().Equals(SceneManager.GetSceneByName("OpenScene"))){
//				ghostMode = true;
//			}
//			if (Input.GetButtonDown ("Interact")) {
//				startInteraction ();
//			}
//		} else {
//			if (Input.GetButtonDown("ghostMode")){
//				ghostMode = false;
//			}
//			if (Input.GetButtonDown ("Possess")) {
//				levelManager.GetComponent<GhostSwitchManager> ().possessCurrentSelection ();
//			}
//		}
//	}

	void checkGhostMode() {

		if (Input.GetButtonDown("ghostMode") && !SceneManager.GetActiveScene().Equals(SceneManager.GetSceneByName("OpenScene"))){
			ghostMode = !ghostMode;
         
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

		LayerMask layer = 1 << LayerMask.NameToLayer ("Interactive") | 1 << LayerMask.NameToLayer ("Wall");

		RaycastHit2D[] hits = 
			Physics2D.RaycastAll(this.transform.position, direction,1.5f, layer);
		Debug.DrawRay (this.transform.position ,direction*1.5f, Color.green,0.2f);

		// prevent wall to come between player and interactive objects
		foreach (RaycastHit2D hit in hits) {
			if (hit.collider != null) {
				if (hit.collider.CompareTag ("impenetrable"))
					return;
				hit.collider.gameObject.GetComponent<Interact> ().interact ();
			}
		}
	}

    //Helper to get DollSpeed
    public float getDollSpeed() {
        return dollSpeed;
    }

		
}

