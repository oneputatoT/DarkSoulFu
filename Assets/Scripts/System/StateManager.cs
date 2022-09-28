using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : IActorManager
{
    public int HP = 15;

    public void Test()
    {
        Debug.Log($"HP :{HP}");
    }
}
