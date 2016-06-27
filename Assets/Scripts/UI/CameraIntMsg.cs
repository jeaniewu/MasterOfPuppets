using UnityEngine;
using System.Collections;

public class CameraIntMsg : MonoBehaviour {

    public GameObject CameraSprite;
    public GameObject ArrowsSide;
    public GameObject ArrowsUp;
    private Controller2 ghostMode;
    private bool onlyOnce;

    // Use this for initialization
    void Start()
    {
        onlyOnce = false;
        ghostMode = GetComponentInChildren<Controller2>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("hi");
            CameraSprite.SetActive(false);
        }
        if (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical"))
        {
            ArrowsSide.SetActive(false);
            ArrowsUp.SetActive(false);
        }
        if (ghostMode.ghostMode && !onlyOnce)
        {
            ArrowsUp.SetActive(false);
            ArrowsSide.SetActive(true);
            onlyOnce = true;
        }
        if (Input.GetButton("Interact"))
        {

            ArrowsSide.SetActive(false);
            ArrowsUp.SetActive(false);
        }
    }
}
