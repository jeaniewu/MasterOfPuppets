using UnityEngine;
using System.Collections;

public class SwitchSelectMsg : MonoBehaviour {
    public GameObject textBox;
    public GameObject switchMessage;
    public GameObject acceptMessage;
    private bool onlyOnce;

    private Controller2 ghostMode;
    // Use this for initialization
    //this is a tutotial script for level 1a
    void Start()
    {
        ghostMode = GetComponent<Controller2>();
        //switchMessage = GameObject.FindGameObjectWithTag("SwitchMessage");
        switchMessage.SetActive(false);
        acceptMessage.SetActive(false);

        onlyOnce = false;
        

    }

    // Update is called once per frame
    void Update()
    {

        if (!textBox.gameObject.activeSelf && !ghostMode.ghostMode && !onlyOnce)
        {
            switchMessage.SetActive(true);
            acceptMessage.SetActive(false);
           
        }
        if (!textBox.gameObject.activeSelf &&  ghostMode.ghostMode )
        {
            onlyOnce = true;

            acceptMessage.SetActive(true);
            switchMessage.SetActive(false);
           

        }

        if(!textBox.gameObject.activeSelf && !ghostMode.ghostMode && onlyOnce){
            Debug.Log("pressed");
            switchMessage.SetActive(false);
            acceptMessage.SetActive(false);
        }


    }
}
