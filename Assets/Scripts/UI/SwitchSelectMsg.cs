using UnityEngine;
using System.Collections;

public class SwitchSelectMsg : MonoBehaviour {
    public GameObject textBox;
    public GameObject switchMessage;
    public GameObject acceptMessage;
    private bool onlyOnce;
    private bool onlyOnce2;
    public Controller2 isghostMode;
    // Use this for initialization
    void Start()
    {
        //switchMessage = GameObject.FindGameObjectWithTag("SwitchMessage");
        switchMessage.SetActive(false);
        acceptMessage.SetActive(false);

        onlyOnce = false;
        onlyOnce2 = false;

    }

    // Update is called once per frame
    void Update()
    {

        if (!textBox.gameObject.activeSelf && /*Input.GetKeyDown(KeyCode.X)*/ !isghostMode.ghostMode && !onlyOnce)
        {
            switchMessage.SetActive(true);
            acceptMessage.SetActive(false);
            onlyOnce = true;

        }
        if (!textBox.gameObject.activeSelf &&  isghostMode.ghostMode && !onlyOnce2)
        {

            acceptMessage.SetActive(true);
            switchMessage.SetActive(false);
            onlyOnce2 = true;

        }

        
    }
}
