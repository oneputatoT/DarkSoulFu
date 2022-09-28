using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInput : IInputController
{

    WaitForSeconds attackTime;
    private void Awake()
    {
        attackTime = new WaitForSeconds(1.0f);
    }


    private IEnumerator Start()
    {
        while (true)
        {
            rb = true;
            yield return attackTime;
        }
    }

    private void Update()
    {
        
    }
}
