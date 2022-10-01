using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorControl : MonoBehaviour
{
    

    [Header("Root Motion")]
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    //¹¥»÷Ê±ºòµÄRootMotion

    private void OnAnimatorMove()
    {
        SendMessageUpwards("OnUpdateRM", anim.deltaPosition);
    }
}
