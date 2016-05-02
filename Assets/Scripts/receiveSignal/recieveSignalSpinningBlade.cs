using UnityEngine;
using System.Collections;

public class recieveSignalSpinningBlade : receiveSignal 
{

    //private Collider2D collider2D2;
    // private Renderer renderer2;
   public  SpinningBlade spinningBlade;
   public GameObject spinningSawblade;
   public float rotationSpeed;
   public bool isRight;



    // Use this for initialization
    void Start()
    {


        

    }

    // Update is called once per frame
    void Update()
    {
        if (isRight)
        {
            Spingright();
        }
        if (!isRight)
        {
            Spinleft();
        }
    }

    public void Spingright()
    {
        transform.Rotate(Vector3.forward * 90, Time.deltaTime * rotationSpeed);

    }

    public void Spinleft()
    {
        transform.Rotate(Vector3.forward * -90, Time.deltaTime * rotationSpeed);

    }

    public void rotationSide(bool right)
    {
        isRight = right;
    }

    public override void activate()
    {

        rotationSide(false);
        

    }

    public override void deactivate()
    {
        rotationSide(true);
    }

  
}
