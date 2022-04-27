using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGroundSensor : MonoBehaviour
{
    [SerializeField] CapsuleCollider capCol;
    [SerializeField] LayerMask mask;
    [SerializeField] float offset;

    Vector3 point1;
    Vector3 point2;
    float radius;
    Collider[] outputCols;

    private void Awake()
    {
        radius = capCol.radius - 0.05f;
    }

    private void FixedUpdate()
    {
        point1 = transform.position + transform.up * (radius - offset);
        point2 = transform.position + transform.up * (capCol.height - offset) - transform.up * radius;

        outputCols= Physics.OverlapCapsule(point1, point2, radius,mask);
        if (outputCols.Length != 0)
        {
            SendMessageUpwards("IsGround");
        }
        else
        {
            SendMessageUpwards("IsNotGround");
        }
    }


}
