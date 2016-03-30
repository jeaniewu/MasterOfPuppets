using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIanim : MonoBehaviour
{

    public Animator anim;

	public void onPlay()
    {
        anim.SetBool("NearPlay", true);
    }

    public void offPlay()
    {
        anim.SetBool("NearPlay", false);
    }

    public void onExit()
    {
        anim.SetBool("NearExit", true);
    }

    public void offExit()
    {
        anim.SetBool("NearExit", false);
    }


}
