using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorManager : MonoBehaviour
{
    public ActorController ac;
    GameObject model;
    [SerializeField] WeaponManager wm;
    [SerializeField] BattleManager bm;
    [SerializeField] DirectiorManager dm;
    [SerializeField] InteractionManager im;
    public StateManager sm;


    private void Awake()
    {
        ac = GetComponent<ActorController>();
        model = ac.model;
        GameObject sensor = transform.Find("Sensor").gameObject;

        bm = Bind<BattleManager>(sensor);
        wm = Bind<WeaponManager>(model);
        sm = Bind<StateManager>(gameObject);
        dm = Bind<DirectiorManager>(gameObject);
        im = Bind<InteractionManager>(sensor);
    }

    private void OnEnable()
    {
        ac.OnActionEvent += DoAction;
    }


    private void OnDisable()
    {
        ac.OnActionEvent -= DoAction;
    }

    public void DoAction()
    {
        if (im.eventCasterList.Count != 0)
        {
            if (!im.eventCasterList[0].active) return;

            if (im.eventCasterList[0].eventName == "FrontStab")
            {
                dm.PlaySpecialTimeLine("FrontStab", this, im.eventCasterList[0].am);
            }
            else if (im.eventCasterList[0].eventName == "OpenBox")
            {
                if (bm.CheckCounterable(im.eventCasterList[0].Parent, 30f))
                {
                    im.eventCasterList[0].active = false;
                    transform.position = im.eventCasterList[0].Offset;
                    ac.model.transform.LookAt(im.eventCasterList[0].Parent.transform, Vector3.up);
                    dm.PlaySpecialTimeLine("OpenBox", this, im.eventCasterList[0].anim);
                }
            }
            else if (im.eventCasterList[0].eventName == "LeverUp")
            {
                if (bm.CheckCounterable(im.eventCasterList[0].Parent, 30f))
                {
                    im.eventCasterList[0].active = false;
                    transform.position = im.eventCasterList[0].Offset;
                    ac.model.transform.LookAt(im.eventCasterList[0].Parent.transform, Vector3.up);
                    dm.PlaySpecialTimeLine("LeverUp", this, im.eventCasterList[0].anim);
                }
            }
        }
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

    public void SetBool(string name,bool value)
    {
        ac.SetBool(name, value);
    }

    public void LockAnimation(bool value)
    {
        ac.SetBool("lock", value);
    }
}
