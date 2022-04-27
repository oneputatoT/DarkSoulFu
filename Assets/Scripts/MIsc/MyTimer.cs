using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTimer
{
    public enum STATE
    { 
        IDLE,
        RUN,
        FINISHED   
    }

    public STATE state;

    public float duration = 1.0f;
    public float elapsedTime = 0;

    public void Tick()
    {
        switch (state)
        {
            case STATE.IDLE:
                break;
            case STATE.RUN:
                elapsedTime += Time.deltaTime;
                if (elapsedTime >= duration)
                {
                    state = STATE.FINISHED;
                }
                break;
            case STATE.FINISHED:
                break;
            default:
                Debug.Log("Error");
                break;
        }


        //if (state == STATE.IDLE)
        //{

        //}
        //else if (state == STATE.RUN)
        //{
        //    elapsedTime += Time.deltaTime;
        //    if (elapsedTime >= duration)
        //    {
        //        state = STATE.FINISHED;
        //    }
        //}
        //else if (state == STATE.FINISHED)
        //{

        //}
        //else
        //{
        //    Debug.Log("Error");
        //}
    }

    public void Go()
    {
        elapsedTime = 0f;
        state = STATE.RUN;
    }


}
