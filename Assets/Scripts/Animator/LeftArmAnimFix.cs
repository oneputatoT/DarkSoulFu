using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftArmAnimFix : MonoBehaviour
{
    Animator anim;
    [SerializeField] Vector3 rotateVec;
    ActorController ac;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        ac = GetComponentInParent<ActorController>();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (!anim.GetBool("defense")&&ac.isLeftSheid)
        { 
        Transform leftArm = anim.GetBoneTransform(HumanBodyBones.LeftLowerArm);
        leftArm.localEulerAngles += rotateVec;
        anim.SetBoneLocalRotation(HumanBodyBones.LeftLowerArm, Quaternion.Euler(leftArm.localEulerAngles));
        }
    }
}
