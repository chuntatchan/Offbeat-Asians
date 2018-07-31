using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour {

    public string[] aniName;
    public Animator animator;
    public int ani;
	// Use this for initialization
	void Start () {
        animator.Play(aniName[ani]);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayAni(bool flag)
    {
        if (flag)
        {
            if (ani < aniName.Length-1)
            {
               
                ani++;
                animator.Play(aniName[ani]);
            }
        }
        else
        {
            if (ani > 0)
            {
                ani--;
                animator.Play(aniName[ani]);
            }
        }
    }
}
