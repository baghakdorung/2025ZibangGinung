using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : Singleton<Fade>
{
    private Animator animator;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

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
