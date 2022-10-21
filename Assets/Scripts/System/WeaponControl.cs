using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponControl : MonoBehaviour
{
    public WeaponManager wm;
    public WeaponData data;


    private void Start()
    {
        data = GetComponentInChildren<WeaponData>();
    }

    public float GetATK()
    {
        return data.ATK + wm.am.sm.ATK;
    }
}
