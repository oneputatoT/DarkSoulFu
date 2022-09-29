using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorManager : MonoBehaviour
{
    public ActorController ac;
    GameObject model;
    [SerializeField] WeaponManager wm;
    [SerializeField] BattleManager bm;
    [SerializeField] StateManager sm;
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

    public void TryDoDamage()
    {
        if (sm.HP > 0)
        {
            sm.UpdateHP(5, -1);
        }
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
}
