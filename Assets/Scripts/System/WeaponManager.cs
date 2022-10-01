using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : IActorManager
{
    public Collider collL;
    public Collider collR;

    public WeaponControl WCL;
    public WeaponControl WCR;

    [SerializeField]Transform whL;
    [SerializeField]Transform whR;


    private void Start()
    {
        whL = transform.DeepFind("weaponHandleL");
        whR = transform.DeepFind("weaponHandleR");

        WCL = BindWeaponControl(whL.gameObject);
        WCR = BindWeaponControl(whR.gameObject);

        collR = whR.GetComponentInChildren<Collider>();
        collL = whL.GetComponentInChildren<Collider>();
        
    }

    public WeaponControl BindWeaponControl(GameObject target)
    {
        WeaponControl temp = target.GetComponent<WeaponControl>();
        if (temp == null)
        {
            temp =  target.AddComponent<WeaponControl>();
        }
        temp.wm = this;
        return temp;
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

    public void CountBackEnable()
    {
        am.SetCountBackState(true);
    }

    public void CountBackDisable()
    {
        am.SetCountBackState(false);
    }
}
