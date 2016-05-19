using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Slice : MonoBehaviour
{


    private GameObject doll;
    private GhostSwitchManager manager;
    private Animator anim;

    // Use this for initialization
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<GhostSwitchManager>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        doll = other.gameObject;
        if (doll.tag == "Player")
        {
            doll.GetComponent<Controller2>().enabled = false;
            StartCoroutine("SoulSlice");
        }

		else if (doll.tag == "Doll")
        {

            StartCoroutine("NoSoulSlice");

        }

       
    }
 

    IEnumerator SoulSlice()
    {
        AnimatorClipInfo[] infos = this.anim.GetCurrentAnimatorClipInfo(0);
        AnimatorClipInfo currentClipInfo = infos[0];
        AnimationClip currentClip = currentClipInfo.clip;
        float currentCliplength = currentClip.length;

        


            doll.GetComponent<Animator>().SetBool("isSoulSliced", true);


            yield return new WaitForSeconds(currentCliplength-2);
            manager.dollsUpdated = false;
            manager.updateDolls();
            SceneManager.LoadScene(Application.loadedLevel);


        
    }

    IEnumerator NoSoulSlice()
    {
        AnimatorClipInfo[] infos = this.anim.GetCurrentAnimatorClipInfo(0);
        AnimatorClipInfo currentClipInfo = infos[0];
        AnimationClip currentClip = currentClipInfo.clip;
        float currentCliplength = currentClip.length;

  
            doll.GetComponent<Animator>().Play("soulless-sliced");

            yield return new WaitForSeconds(currentCliplength - 2);

            if (doll.tag == "Doll")
            {
                doll.SetActive(false);
            }


 
        }
    }   


