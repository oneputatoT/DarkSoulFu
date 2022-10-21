using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFactory 
{
    MyDataBase myDataBase;

    public WeaponFactory(MyDataBase _myDataBase)
    {
        myDataBase = _myDataBase;
    }

    public GameObject CreatWeapon(string weaponName,Vector3 pos,Quaternion rot)
    {
        GameObject temp = Resources.Load(weaponName) as GameObject;

        WeaponData tempWeaData = temp.AddComponent<WeaponData>();

        tempWeaData.ATK = myDataBase.jsonText[weaponName]["ATK"].floatValue;

        

        return GameObject.Instantiate(temp, pos, rot);
    }

    public Collider CreatWeapon(string weaponName,string side,WeaponManager wm)
    {
        WeaponControl wc;
        if (side == "R")
        {
            wc = wm.WCR;
        }
        else if (side == "L")
        {
            wc = wm.WCL;
        }
        else
        {
            return null;
        }


        GameObject temp = Resources.Load(weaponName) as GameObject;

        //WeaponData tempWeaData = temp.AddComponent<WeaponData>();

        //tempWeaData.ATK = myDataBase.jsonText[weaponName]["ATK"].floatValue;

        GameObject obj = GameObject.Instantiate(temp, Vector3.zero,Quaternion.identity);

        WeaponData tempWeaData =obj.AddComponent<WeaponData>();

        tempWeaData.ATK = myDataBase.jsonText[weaponName]["ATK"].floatValue;

        obj.transform.parent = wc.transform;

        obj.transform.localPosition = Vector3.zero;

        obj.transform.localRotation = Quaternion.identity;

        wc.data = tempWeaData;

        return wc.GetComponentInChildren<Collider>();
    }

    
}
