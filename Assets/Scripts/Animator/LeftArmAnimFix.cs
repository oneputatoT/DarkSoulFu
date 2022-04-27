using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftArmAnimFix : MonoBehaviour
{
    Animator anim;
    [SerializeField] Vector3 rotateVec;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (!anim.GetBool("defense"))
        { 
        Transform leftArm = anim.GetBoneTransform(HumanBodyBones.LeftLowerArm);
        leftArm.localEulerAngles += rotateVec;
        anim.SetBoneLocalRotation(HumanBodyBones.LeftLowerArm, Quaternion.Euler(leftArm.localEulerAngles));
        }
    }
}
