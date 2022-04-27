using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IInputController : MonoBehaviour
{
    [Header("==== OutPut Signals ====")]
    [SerializeField] public float dUp;
    [SerializeField] public float dRight;
    public float JUp;    //旋转上下
    public float JRight;  //旋转水平
    public float dMag;
    public Vector3 dVec;
    public bool run;
    public bool jump;
    public bool roll;
    public bool defense;
    public bool attack;
    public bool lockOn;



    [Header("==== Other ====")]
    public bool isEnable = true;
    protected float dupTarget;
    protected float dRightTarget;
    protected float dUpVelocity = 0.0f;
    protected float dRightVelocity = 0.0f;


    protected Vector2 SquareToCircle(Vector2 input)
    {
        Vector2 output;
        output.x = input.x * Mathf.Sqrt(1 - (input.y * input.y) / 2.0f);
        output.y = input.y * Mathf.Sqrt(1 - (input.x * input.x) / 2.0f);
        return output;
    }
}
