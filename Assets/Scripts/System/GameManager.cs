using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Defective.JSON;
public class GameManager : MonoBehaviour
{
    public WeaponManager wm;

    public static GameManager Instance;
    MyDataBase myDataBase;
    WeaponFactory weaponFactory;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }


    private void Start()
    {
        InitData();
        InitWeaponData();

        wm.UpdateWeapon("R", weaponFactory.CreatWeapon("Sword", "R", wm));
        wm.ChangeHand(false);
    }


    private void OnGUI()
    {
        if (GUI.Button(new Rect(0, 0, 130, 30), "Sword"))
        {
            wm.UnLoadWeapon("R");
            wm.UpdateWeapon("R", weaponFactory.CreatWeapon("Sword", "R", wm));
            wm.ChangeHand(false);
        }
        
        if (GUI.Button(new Rect(0, 30, 130, 30), "Falchion"))
        {
            wm.UnLoadWeapon("R");
            wm.UpdateWeapon("R", weaponFactory.CreatWeapon("Falchion", "R", wm));
            wm.ChangeHand(true);
        }
    }



    void InitData()
    {
        myDataBase = new MyDataBase();
    }

    void InitWeaponData()
    {
        weaponFactory = new WeaponFactory(myDataBase);
    }
}
