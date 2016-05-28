using UnityEngine;

public class recieveSignalSpinningBlade : receiveSignal 
{

   //public GameObject spinningSawblade;
   public float rotationSpeed;
   public float ROTATIONSIDE = 90;

    // Use this for initialization
    void Start()
    {
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