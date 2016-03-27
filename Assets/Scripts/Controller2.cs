using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class Controller2 : MonoBehaviour {

	public GameObject levelManager;

	private float maxSpeed = 10f;
	public bool ghostMode = false;
	private int radius;
	private DollManager.Boundary boundary;

	public RaycastHit2D hit;

	public float Horizontal;
	public float Vertical;

    Animator anim;


	
	//	public bool facingRight = true;
//	
//	Animator anim;
	
	// Use this for initialization
	void Start () {

		levelManager = GameObject.FindGameObjectWithTag ("LevelManager");
		boundary = levelManager.GetComponent<DollManager>().boundary;
		maxSpeed = levelManager.GetComponent<DollManager> ().maxSpeed;
		anim = GetComponent<Animator>();
		
	}
	
	void Update(){

		checkGhostMode ();
		if (Input.GetKeyDown (KeyCode.R))
			SceneManager.LoadScene (Application.loadedLevel);

	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (!ghostMode) {
			move ();
			if (Input.GetKeyDown (KeyCode.Z))
				interact ();
		}

        /*ANIMATION*/
        float input_x = Input.GetAxisRaw("Horizontal");
        float input_y = Input.GetAxisRaw("Vertical");
        bool isWalking = (Mathf.Abs(input_x) + Mathf.Abs(input_y)) > 0;
        anim.SetBool("isWalking", isWalking);
        /*--------*/

        if (isWalking)
        {
            anim.SetFloat("X", input_x);
            anim.SetFloat("Y", input_y);
            if (Input.GetAxis("Horizontal") != 0)
            {
                
                Horizontal = Input.GetAxis("Horizontal");
                Vertical = 0;
            }
            else if (Input.GetAxis("Vertical") != 0)
            {
                
                Vertical = Input.GetAxis("Vertical");
                Horizontal = 0;
            }
        }

	}

	void move()
	{


		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		//GetComponent<Rigidbody2D>().velocity = new Vector2(moveHorizontal * maxSpeed, moveVertical * maxSpeed);
		
		transform.position += new Vector3 (moveHorizontal, moveVertical, 0).normalized * Time.deltaTime * maxSpeed;
		
		GetComponent<Rigidbody2D>().position = new Vector3 
			(
				Mathf.Clamp(GetComponent<Rigidbody2D>().position.x, boundary.xMin, boundary.xMax), 
				Mathf.Clamp(GetComponent<Rigidbody2D>().position.y, boundary.yMin, boundary.yMax),
				0.0f
				);

        updateMovingSound(moveHorizontal, moveVertical);

//		if (moveHorizontal  > 0 && ! facingRight)
//			Flip ();
//		else if (moveHorizontal < 0 && facingRight)
//			Flip ();
		
	}

	void checkGhostMode() {
		if (Input.GetKeyDown (KeyCode.X)){
			ghostMode = !ghostMode;
		}
	}

	void interact(){


		var direction = new Vector3 (0, 0, 0);

		if (Horizontal > 0) {
			direction = new Vector3 (1, 0, 0);
		} else if (Horizontal < 0) {
			direction = new Vector3 (-1, 0, 0);
		} else if (Vertical > 0) {
			direction = new Vector3 (0, 1, 0);
		} else if (Vertical < 0) {
			direction = new Vector3 (0, -1, 0);
		}

		hit = 
			Physics2D.Raycast(this.transform.position + new Vector3(0.5f,0.5f,0), direction,1, 1 << LayerMask.NameToLayer ("Interactive"));
		Debug.DrawRay (this.transform.position + new Vector3(0.5f,0.5f,0),direction, Color.green,0.2f);

		if (hit.collider != null) {
			hit.collider.gameObject.GetComponent<Trigger> ().switchTrigger ();
		}


	}


    private void updateMovingSound(float horizontal, float vertical) {
        if (horizontal == 0 && vertical == 0) {
            DollAudioManager.getInstance().stopWalkingSound();
        } else {
            DollAudioManager.getInstance().playWalkingSound();
        }
    }

//	void Flip()
//	{
//		facingRight = ! facingRight;
//		Vector3 theScale = transform.localScale;
//		theScale.x *= -1;
//		transform.localScale = theScale;
//	}
}

