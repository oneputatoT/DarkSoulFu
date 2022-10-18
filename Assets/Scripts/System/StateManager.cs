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
    public bool isBlocked;
    public bool isDefense;
    public bool isCountBack;
    public bool isCountBackEnable;


    [Header("======Allow Trigger====")]
    public bool isAllowDefense;
    public bool isImmortal;
    public bool isCountBackFail;

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
        isBlocked = am.ac.CheckState("blocked");
        isCountBack = am.ac.CheckState("counterBack");

        isAllowDefense = isGround || isBlocked;
        isDefense = isAllowDefense&&am.ac.CheckState("defense1h", "defense");

        isImmortal = isRoll || isJab;

        isCountBackFail = isCountBack && !isCountBackEnable;
    }


    public void UpdateHP(float value, float temp=1.0f)
    {
        HP = Mathf.Clamp(HP + temp * value, 0, MAXHP);
        //Debug.Log(am.gameObject.name + " : "+HP);
    }
}
