using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Slice : MonoBehaviour
{

	public Animation animation;
	// Needed to get animation clip's length
    
	private GameObject doll;
    private GhostSwitchManager manager;
	private float clipLength;

    // Use this for initialization
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<GhostSwitchManager>();
		doll = GameObject.FindGameObjectWithTag ("Player");
		Animator anim = doll.GetComponent<Animator>();

		RuntimeAnimatorController ac = anim.runtimeAnimatorController;    //Get Animator controller
		for(int i = 0; i<ac.animationClips.Length; i++)                 //For all animations
		{
			if(ac.animationClips[i].name == "right-soul-sliced")        //If it has the same name as your clip
			{
				clipLength = ac.animationClips[i].length;
			}
		}
    }

    // Update is called once per frame
    void Update()
    {

    }

	void OnTriggerEnter2D(Collider2D other)
    {
		if (other.gameObject.CompareTag("AudioController")){
			return;
		}

		if (other.gameObject.CompareTag("Player"))
        {
			doll = other.gameObject;
			doll.GetComponent<Controller2> ().enabled = false;
			doll.GetComponent<Collider2D> ().enabled = false;
			Debug.Log(doll.name);
            StartCoroutine("SoulSlice");
        }

		else if (other.gameObject.CompareTag("Doll"))
        {
            StartCoroutine("NoSoulSlice");

        }

       
    }
 

    IEnumerator SoulSlice()
    {
	 
		doll.GetComponent<AnimationController> ().Slice ();

		yield return new WaitForSeconds(clipLength);
        manager.dollsUpdated = false;
        manager.updateDolls();
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        
    }

    IEnumerator NoSoulSlice()
    {
		doll.GetComponent<AnimationController> ().Slice ();

		yield return new WaitForSeconds(clipLength);
		manager.dollsUpdated = false;
		manager.updateDolls ();
 
     }
}   


