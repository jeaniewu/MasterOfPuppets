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
	
	void Update(){

		checkGhostMode ();

		if (!ghostMode) {
			if (Input.GetButtonDown ("Interact")) {
				startInteraction ();


			}

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
		
		transform.position += new Vector3 (moveHorizontal, moveVertical, 0).normalized * Time.deltaTime * dollSpeed;
		

		rigidbody2D2.position = new Vector3 
			(
				Mathf.Clamp(rigidbody2D2.position.x, boundary.xMin, boundary.xMax), 
				Mathf.Clamp(rigidbody2D2.position.y, boundary.yMin, boundary.yMax),
				0.0f
				);
		
	}

	public void stopWalking(){
		DollAudioManager.getInstance().stopWalkingSound();
		anim.SetBool ("isWalking", false);
	}

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

