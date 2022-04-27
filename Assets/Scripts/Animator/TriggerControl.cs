using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerControl : MonoBehaviour
{
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }


    public void RestTrigger(string stateName)
    {
        anim.ResetTrigger(stateName);
    }

}
