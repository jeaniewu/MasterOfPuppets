﻿using UnityEngine;
using System.Collections;

public class SpinningBlade : MonoBehaviour {
    public GameObject spinningSawblade;
    public float rotationSpeed;
    public bool isRight;

	// Use this for initialization
	void Start () {
        isRight = true;
	
	}
	
	// Update is called once per frame
	void Update () {
        
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
}
