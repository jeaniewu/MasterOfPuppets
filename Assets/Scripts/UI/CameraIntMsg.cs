using UnityEngine;
using System.Collections;

public class CameraIntMsg : MonoBehaviour {

    public GameObject Camera;
    public GameObject ArrowsSide;
    public GameObject ArrowsUp;
    public Controller2 isghostMode;
    private bool onlyOnce;

    // Use this for initialization
    void Start()
    {
        onlyOnce = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {

            Debug.Log("hi");
            Camera.SetActive(false);


        }
        if (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical"))
        {
            ArrowsSide.SetActive(false);
            ArrowsUp.SetActive(false);
        }
        if (isghostMode.ghostMode && !onlyOnce)
        {
            ArrowsUp.SetActive(false);
            ArrowsSide.SetActive(true);
            onlyOnce = true;
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {

            ArrowsSide.SetActive(false);
            ArrowsUp.SetActive(false);
        }
    }
}
