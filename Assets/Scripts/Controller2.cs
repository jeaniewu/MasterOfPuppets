using UnityEngine;
using System.Collections;

public class Controller2 : MonoBehaviour {

	public float maxSpeed = 10f;
	public bool ghostMode = false;


//	public bool facingRight = true;
//	
//	Animator anim;
	
	// Use this for initialization
	void Start () {
		
		//anim = GetComponent<Animator>();
		
	}
	
	void Update(){

		checkGhostMode ();

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		if (!ghostMode) {
			move (moveHorizontal, moveVertical);
		} 

	}

	void move(float x,float y)
	{
		
		//anim.SetFloat ("Speed", Mathf.Abs(moveHorizontal));
		
		transform.position += new Vector3(x,y,0).normalized * Time.deltaTime * maxSpeed;
//		if (moveHorizontal  > 0 && ! facingRight)
//			Flip ();
//		else if (moveHorizontal < 0 && facingRight)
//			Flip ();
		
	}

	void checkGhostMode() {
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

