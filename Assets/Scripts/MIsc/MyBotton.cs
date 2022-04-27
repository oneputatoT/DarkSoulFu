using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyBotton 
{
    public bool isPressing;
    public bool onPressed;
    public bool onRelease;
    public bool isExtending = false;
    public bool isDelaying;

    bool curState;
    bool lastState;

    MyTimer extTimer = new MyTimer();
    MyTimer delayTimer = new MyTimer();

    public void Tick(bool state)
    {
        extTimer.Tick();
        delayTimer.Tick();

        curState = state;
        isPressing = curState;

        onPressed = false;
        onRelease = false;
        isExtending = false;
        isDelaying = false;

        if (curState != lastState)
        {
            if (curState)
            {
                onPressed = true;
                StartTimer(delayTimer, 0.1f);
            }
            else
            {
                onRelease = true;
                StartTimer(extTimer, 0.1f);
            }
        }

        lastState = curState;

        if (extTimer.state == MyTimer.STATE.RUN)
        {
            isExtending = true;
        }
        if (delayTimer.state == MyTimer.STATE.RUN)
        {
            isDelaying = true;
        }
    }

    void StartTimer(MyTimer timer,float dt)
    {
        timer.duration = dt;
        timer.Go();
    }
}
