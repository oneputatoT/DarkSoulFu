using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorControl : MonoBehaviour
{
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnAnimatorMove()
    {
        SendMessageUpwards("OnUpdateRM", anim.deltaPosition);
    }
}
