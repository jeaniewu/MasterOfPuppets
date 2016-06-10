using UnityEngine;
using System.Collections;

public class dollType : MonoBehaviour {

	public string type;
    public int dollColor = 0;
    private Animator animator;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        animator.SetInteger("Color", dollColor);
	}
	
	// Update is called once per frame
	void Update () {
	
	}


}
