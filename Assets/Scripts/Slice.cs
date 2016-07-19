using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Slice : MonoBehaviour
{
	private GameObject doll;
    private GhostSwitchManager manager;
	private DollManager dollManager;
	private float clipLength;

    // Use this for initialization
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<GhostSwitchManager>();
		dollManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<DollManager>();
		doll = GameObject.FindGameObjectWithTag ("Player");

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
			MechanicAudioManager.getInstance ().playDeathBySawBlade ();
            StartCoroutine("SoulSlice");
        }

		else if (other.gameObject.CompareTag("Doll"))
        {
			doll = other.gameObject;
            StartCoroutine("NoSoulSlice");

        }

       
    }
 

    IEnumerator SoulSlice()
    {
		Animator anim = doll.GetComponent<Animator>();
		clipLength = calculateClipLength(anim, "soulSliceReference");
		doll.GetComponent<DollAnimationController> ().Slice ();
		Debug.Log (clipLength);
		yield return new WaitForSeconds(clipLength);
        manager.dollsUpdated = false;
        manager.updateDolls();
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        
    }

    IEnumerator NoSoulSlice()
    {
		Animator anim = doll.GetComponent<Animator>();
		clipLength = calculateClipLength(anim, "soullessSliceReference");
		doll.GetComponent<DollAnimationController> ().Slice ();

		doll.layer = 0;
		manager.dollsUpdated = false;
		manager.updateDolls ();

		Debug.Log (clipLength);
		yield return new WaitForSeconds(clipLength);
		dollManager.Death (doll);
     }

	public float calculateClipLength(Animator anim, string clipName){
		RuntimeAnimatorController ac = anim.runtimeAnimatorController;    //Get Animator controller
		for(int i = 0; i<ac.animationClips.Length; i++)                 //For all animations
		{
			if(ac.animationClips[i].name == clipName)        //If it has the same name as your clip
			{
				return ac.animationClips[i].length;
			}
		}
		return 0;
	}
}   


