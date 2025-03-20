using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : Singleton<Fade>
{
    Animator animator;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void FadeOut()
    {
        animator.SetTrigger("FadeOut");
    }

    public void FadeIn()
    {
        animator.SetTrigger("FadeIn");
    }
}
