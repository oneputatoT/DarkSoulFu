using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : IActorManager
{
    public float MAXHP = 15;

    public float HP = 15;

    [Header("=====State Flags=====")]
    public bool isGround;
    public bool isJump;
    public bool isFall;
    public bool isRoll;
    public bool isJab;
    public bool isAttack;
    public bool isHit;
    public bool isDie;
    public bool isDefense;

    private void Update()
    {
        isGround = am.ac.CheckState("ground");
        isJump = am.ac.CheckState("jump");
        isJab = am.ac.CheckState("jab");
        isHit = am.ac.CheckState("hit");
        isDie = am.ac.CheckState("die");
        isFall = am.ac.CheckState("fall");
        isRoll = am.ac.CheckState("roll");
        isAttack = am.ac.CheckStateTag("attackR")|| am.ac.CheckStateTag("attackL");
        isDefense = am.ac.CheckState("defense1h", "defense");
    }


    public void UpdateHP(float value, float temp=1.0f)
    {
        HP = Mathf.Clamp(HP + temp * value, 0, MAXHP);
        if (HP > 0)
        {
            am.Hit();
        }
        else
        {
            am.Death();
        }
        Debug.Log(am.gameObject.name + " : "+HP);
    }
}
