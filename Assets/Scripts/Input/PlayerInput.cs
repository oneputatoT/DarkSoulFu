using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : IInputController
{
    [Header("==== Key Setting ====")]
    [SerializeField] string up;
    [SerializeField] string down;
    [SerializeField] string left;
    [SerializeField] string right;

    [SerializeField] string keyA;
    [SerializeField] string keyB;
    [SerializeField] string keyC;
    [SerializeField] string keyD;
    [SerializeField] string keyLock;

    [SerializeField] string keyJUp;
    [SerializeField] string keyJDown;
    [SerializeField] string keyJRight;
    [SerializeField] string keyJLeft;

    [Header("==== Mouse Setting ====")]
    public bool mouseEnable = true;
    [SerializeField] float sensitivityX;
    [SerializeField] float sensitivityY;

    MyBotton bottonA = new MyBotton();
    MyBotton buttonB = new MyBotton();
    MyBotton buttonC = new MyBotton();
    MyBotton buttonD = new MyBotton();
    MyBotton buttonLock = new MyBotton();

    private void Update()
    {
        bottonA.Tick(Input.GetKey(keyA));
        buttonB.Tick(Input.GetKey(keyB));
        buttonC.Tick(Input.GetKey(keyC));
        buttonD.Tick(Input.GetKey(keyD));
        buttonLock.Tick(Input.GetKey(keyLock));

        //Debug.Log(bottonB.isExtending && bottonB.onPressed);

        if (mouseEnable)
        {
            JUp = Input.GetAxis("Mouse Y") * sensitivityY;
            JRight = Input.GetAxis("Mouse X") * sensitivityX;
        }
        else
        {
            JUp = (Input.GetKey(keyJUp) ? 1.0f : 0.0f) - (Input.GetKey(keyJDown) ? 1.0f : 0.0f);
            JRight = (Input.GetKey(keyJRight) ? 1.0f : 0.0f) - (Input.GetKey(keyJLeft) ? 1.0f : 0.0f);
        }

        dupTarget = (Input.GetKey(up) ? 1.0f : 0) - (Input.GetKey(down) ? 1.0f : 0);
        dRightTarget = (Input.GetKey(right) ? 1.0f : 0) - (Input.GetKey(left) ? 1.0f : 0);

        if (!isEnable)
        {
            dupTarget = 0;
            dRightTarget = 0;
        }

        dUp = Mathf.SmoothDamp(dUp, dupTarget, ref dUpVelocity, 0.1f);
        dRight = Mathf.SmoothDamp(dRight, dRightTarget, ref dRightVelocity, 0.1f);

        Vector2 temp = SquareToCircle(new Vector2(dRight, dUp));
        float dUp2 = temp.y;
        float dRight2 = temp.x;

        dMag = Mathf.Sqrt(dUp2 * dUp2 + dRight2 * dRight2);         //输入信号改变动画
        dVec = dUp2 * transform.forward + dRight2 * transform.right;   //方向

        run = (buttonB.isPressing && !buttonB.isDelaying)||buttonB.isExtending;

        defense = buttonD.isPressing;

        jump = buttonB.onPressed && buttonB.isExtending;

        attack = buttonC.onPressed;

        roll = buttonB.onRelease && buttonB.isDelaying;

        lockOn = buttonLock.onPressed;
        //bool newJump = Input.GetKey(keyB);
        //if (newJump != lastJump && newJump == true)
        //{
        //    jump = true;
        //}
        //else
        //{
        //    jump = false;
        //}
        //lastJump = newJump;
    }

}
