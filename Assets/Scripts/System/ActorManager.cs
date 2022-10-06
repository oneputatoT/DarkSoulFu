using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorManager : MonoBehaviour
{
    public ActorController ac;
    GameObject model;
    [SerializeField] WeaponManager wm;
    [SerializeField] BattleManager bm;
    public StateManager sm;
    private void Awake()
    {
        ac = GetComponent<ActorController>();
        model = ac.model;
        GameObject sensor = transform.Find("Sensor").gameObject;

        bm = Bind<BattleManager>(sensor);
        wm = Bind<WeaponManager>(model);
        sm = Bind<StateManager>(gameObject);
    }

    public T Bind<T>(GameObject go) where T : IActorManager
    {
        T temp;
        temp = go.GetComponent<T>();
        if (temp == null)
        {
            temp = go.AddComponent<T>();
        }
        temp.am = this;
        return temp;
    }

    public void TryDoDamage(WeaponControl attack,bool attatckEnable, bool counterEnable)
    {
        if (sm.isImmortal)
        {
            return;
        }

        if (sm.isCountBackEnable)
        {
            if (counterEnable)
            {
                attack.wm.am.SetStunned();
            }
        }
        else if (sm.isCountBackFail)
        {
            if (attatckEnable)
            {
                HitOrDie(false);
            }
        }
        else if (sm.isDefense)
        {
            Blocked();
        }
        else
        {
            if (sm.HP <= 0)
            {
               
            }
            else
            {
                if (attatckEnable)
                { 
                    HitOrDie(true);               
                }
            }
        }
    }

    public void SetCountBackState(bool value)
    {
        sm.isCountBackEnable = value;
    }

    public void SetStunned()
    {
        ac.issueTrigger("stunned");
    }

    public void HitOrDie(bool showHit)
    {
        sm.UpdateHP(5, -1);
        if (sm.HP > 0)
        {
            if (showHit)
            {
                Hit();
            }
            else
            { 
                
            }
        }
        else
        {
            Death();
        }
    }


    public void Blocked()
    {
        ac.issueTrigger("blocked");
    }

    public void Hit()
    {
        ac.issueTrigger("hit");
    }

    public void Death()
    {
        ac.issueTrigger("die");
        ac.input.isEnable = false;
        if (ac.cameraHandle.lockState)
        {
            ac.cameraHandle.UnLock();
        }
        ac.cameraHandle.enabled = false;
    }

    public void LockAnimation(string name,bool value)
    {
        ac.SetBool(name, value);
    }
}
