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


    public void UpdateWeapon(string side,Collider col)
    {
        if (side == "R")
        {
            collR = col;
        }
        else if (side == "L")
        {
            collL = col;
        }
    }

    public void UnLoadWeapon(string side)
    {
        if (side == "R")
        {
            collR = null;
            WCR.data = null;
            foreach (Transform obj in WCR.transform)
            {
                obj.gameObject.SetActive(false);
                Destroy(obj.gameObject,2.0f);
            }
        }
        else if (side == "L")
        {
            collL = null;
            WCL.data = null;
            foreach (Transform obj in WCL.transform)
            {
                obj.gameObject.SetActive(false);
                Destroy(obj.gameObject,1.0f);
            }
        }
    }

    public void ChangeHand(bool isTwoHand)
    {
        am.ChangeHand(isTwoHand);
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
