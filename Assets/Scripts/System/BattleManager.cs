using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class BattleManager : IActorManager
{
    CapsuleCollider coll;

    private void Start()
    {
        coll = GetComponent<CapsuleCollider>();
        coll.center = Vector3.up * 1.0f;
        coll.radius = 0.5f;
        coll.height = 2.0f;
        coll.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.CompareTag("Weapon"))
        { 
            am.DoDamage(); 
        } 
    }
}
