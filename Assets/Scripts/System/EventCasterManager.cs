using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCasterManager : IActorManager
{
    public string eventName;
    public bool active = true;
    public float offset;

    public GameObject Parent => transform.parent.gameObject;
    public Vector3 Offset => Parent.transform.position+Parent.transform.forward *offset;
    public Animator anim;

    [Header("===Setting===")]
    public bool isItem;

    private void Start()
    {
        if (!isItem)
        {
            am = GetComponentInParent<ActorManager>();
        }
        else
        {
            anim = Parent.GetComponentInChildren<Animator>();
        }
        
    }
}
