using UnityEngine;
using System.Collections;

public class RetryScript : MonoBehaviour {

    public GameObject Retry;
    // Use this for initialization
    void Start()
    {
        Retry.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Retry.SetActive(true);
        }
    }
}
