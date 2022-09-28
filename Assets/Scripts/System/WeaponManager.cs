using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : IActorManager
{
    public Collider collL;
    public Collider collR;
    [SerializeField]Transform whL;
    [SerializeField]Transform whR;


    private void Start()
    {
        whL = transform.DeepFind("weaponHandleL");
        whR = transform.DeepFind("weaponHandleR");
        collR = whR.GetComponentInChildren<Collider>();
        collL = whL.GetComponentInChildren<Collider>();
    }
    public void WeaponEnable()
    {
        if (am.ac.CheckStateTag("attackR"))
        {
            collR.enabled = true;
        }
        else
        {       
        collL.enabled = true;
        }
    }

    public void WeaponDisable()
    {
        collL.enabled = false;
        collR.enabled = false;
    }
}
