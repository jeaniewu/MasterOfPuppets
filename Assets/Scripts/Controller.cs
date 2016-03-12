using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour
{
    public float maxSpeed = 10f;
    public bool facingRight = true;
    public bool canMove;

    Animator anim;

    // Use this for initialization
    void Start()
    {

        anim = GetComponent<Animator>();

    }

    void Update()
    {


    }

    // Update is called once per frame
    void FixedUpdate()
    {


        if (!canMove)
        {
            return;
        }



        Horizontal();
        Vertical();


    }
    void Horizontal()
    {
        float moveHorizontal = Input.GetAxis("Vertical");

        GetComponent<Rigidbody2D>().velocity = new Vector2(moveHorizontal * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
  

    }

    void Vertical()
    {
        float moveVertical = Input.GetAxis("Horizontal");
       
        GetComponent<Rigidbody2D>().velocity = new Vector2(moveVertical * maxSpeed, GetComponent<Rigidbody2D>().velocity.x);

    
    }


}
