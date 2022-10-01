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

        other.transform.parent.TryGetComponent<WeaponControl>(out WeaponControl attack);
      
        

        Vector3 attacktDirection = am.transform.position - attack.wm.am.gameObject.transform.position;
        Vector3 counterDireaction = attack.wm.am.gameObject.transform.position - am.transform.position;


        float attackerAngle = Vector3.Angle(attack.wm.am.transform.forward, attacktDirection);
        float counterAngle = Vector3.Angle(am.transform.forward, counterDireaction);
        float faceAngle = Vector3.Angle(am.transform.forward, attack.wm.am.transform.forward);
        Debug.Log(attackerAngle);

        bool attatckEnable = attackerAngle < 45f;
        bool counterEnable = counterAngle < 30f &&Mathf.Abs(faceAngle-180f)<30f;

        
        if(other.CompareTag("Weapon"))
        { 
            am.TryDoDamage(attack, attatckEnable, counterEnable); 
        } 
    }
}


