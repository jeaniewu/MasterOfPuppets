using UnityEngine;
using System.Collections;

public class Controller2 : MonoBehaviour {

	public float maxSpeed = 10f;
	public bool ghostMode = false;
	public bool alive = false;
//	public bool facingRight = true;
//	
//	Animator anim;
	
	// Use this for initialization
	void Start () {
		
		//anim = GetComponent<Animator>();
		
	}
	
	void Update(){

		setGhostMode ();

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		if (!ghostMode) {
			move (moveHorizontal, moveVertical);
		} else {

		}

	}

	void move(float x,float y)
	{
		
		//anim.SetFloat ("Speed", Mathf.Abs(moveHorizontal));
		
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (x * maxSpeed, y * maxSpeed);
//		if (moveHorizontal  > 0 && ! facingRight)
//			Flip ();
//		else if (moveHorizontal < 0 && facingRight)
//			Flip ();
		
	}

	void setGhostMode() {
		if (Input.GetKeyDown (KeyCode.X)){
			ghostMode = !ghostMode;
			Debug.Log(ghostMode);
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

