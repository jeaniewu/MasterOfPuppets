using UnityEngine;

public class recieveSignalSpinningBlade : receiveSignal 
{

   //public GameObject spinningSawblade;
   public float rotationSpeed;
   public float ROTATIONSIDE;

    // Use this for initialization
    void Start()
    {
        ROTATIONSIDE = 90;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward* ROTATIONSIDE, Time.deltaTime* rotationSpeed);
    
    }

    public override void activate()
    {
        ROTATIONSIDE = -90;
    }

    public override void deactivate()
    {
        ROTATIONSIDE = 90;
    }

  
}