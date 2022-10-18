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
        if (!other.CompareTag("Weapon")) return;

        other.transform.parent.TryGetComponent<WeaponControl>(out WeaponControl attack);
      
        

        //Debug.Log(attackerAngle);

        bool attatckEnable = CheckAttackable(attack,45f);
        bool counterEnable = CheckCounterable(attack, 30f);



        if (other.CompareTag("Weapon"))
        { 
            am.TryDoDamage(attack, attatckEnable, counterEnable); 
        } 
    }

    //ÅÐ¶Ï¹¥»÷ÊÇ·ñÉúÐ§
    public bool CheckAttackable(WeaponControl attack,float radius)
    {

        Vector3 attacktDirection = am.transform.position - attack.wm.am.gameObject.transform.position;
        float attackerAngle = Vector3.Angle(attack.wm.am.transform.forward, attacktDirection);

        bool attatckEnable = attackerAngle < radius;

        return attatckEnable;
    }

    //
    public bool CheckCounterable(WeaponControl attack, float radius)
    {
        Vector3 counterDireaction = attack.wm.am.gameObject.transform.position - am.transform.position;

        float counterAngle = Vector3.Angle(am.transform.forward, counterDireaction);
        float faceAngle = Vector3.Angle(am.transform.forward, attack.wm.am.transform.forward);

        return counterAngle < 30f && Mathf.Abs(faceAngle - 180f) < 30f;
    }

    public bool CheckCounterable(GameObject item, float radius)
    {
        Vector3 counterDireaction = item.transform.position - am.transform.position;

        float counterAngle = Vector3.Angle(am.transform.forward, counterDireaction);
        float faceAngle = Vector3.Angle(am.transform.forward, item.transform.forward);

        return counterAngle < radius && Mathf.Abs(faceAngle - 180f) < radius;
    }
}


