using UnityEngine;
using System.Collections;

public class Controller2 : MonoBehaviour {

	public float maxSpeed = 10f;
//	public bool facingRight = true;
//	
//	Animator anim;
	
	// Use this for initialization
	void Start () {
		
		//anim = GetComponent<Animator>();
		
	}
	
	void Update(){
//		bool shoot = Input.GetButton ("Fire1");
//		shoot |= Input.GetButtonDown("Fire2");
//		if (shoot)
//		{
//			Weapon weapon = GetComponent<Weapon>();
//			if (weapon != null)
//			{
//				// false because the player is not an enemy
//				weapon.Attack(false);
//			}
//		}
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		Move ();

	}

	void Move()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		
		//anim.SetFloat ("Speed", Mathf.Abs(moveHorizontal));
		
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (moveHorizontal * maxSpeed, moveVertical * maxSpeed);
//		if (moveHorizontal  > 0 && ! facingRight)
//			Flip ();
//		else if (moveHorizontal < 0 && facingRight)
//			Flip ();
		
	}

	
//	void Flip()
//	{
//		facingRight = ! facingRight;
//		Vector3 theScale = transform.localScale;
//		theScale.x *= -1;
//		transform.localScale = theScale;
//	}
}

