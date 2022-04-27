using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ActorController : MonoBehaviour
{
    public GameObject model;
    CapsuleCollider col;
    [SerializeField] CameraHandle cameraHandle;
    [SerializeField] Animator anim;
    public IInputController input;
    Rigidbody rd;

    [Header("==== FrictionMaterial ====")]
    [SerializeField] PhysicMaterial frictionOne;
    [SerializeField] PhysicMaterial frictionZero;


    [Header("==== Move ====")]
    [SerializeField] float moveSpeed;
    [SerializeField] float runSpeed;
    [SerializeField] float jumpVelocity;
    [SerializeField] float rollVelocity;
    [SerializeField] float jabVelocity;
    Vector3 planarVelocity;

    [SerializeField] bool lockPlanar = false;
    bool lockTarget=false;
    bool canAttack;
    float targetWeight;

    Vector3 thrustVec;
    Vector3 deltaPos;   //动画RootMotion偏移量
    private void Awake()
    {
        anim = model.GetComponentInChildren<Animator>();

        IInputController[] inputs = GetComponents<IInputController>();
        foreach (var input in inputs)
        {
            if (input.enabled)
            {
                this.input = input;
            }
        }
        rd = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
    }

    private void Update()
    {
        //float targetInt = input.isRun ? 2.0f : 1.0f;
        if (!cameraHandle.lockState)
        {
            anim.SetFloat("forward",input.dMag * Mathf.Lerp(anim.GetFloat("forward"), input.run ? 2.0f : 1.0f, 0.35f));
            anim.SetFloat("right", 0);
        }
        else
        {
            Vector3 localVec = transform.InverseTransformVector(input.dVec);
            anim.SetFloat("forward", localVec.z * (input.run ? 2.0f : 1.0f));
            anim.SetFloat("right", localVec.x * (input.run ? 2.0f : 1.0f));
        }
        

        anim.SetBool("defense", input.defense);

        if (input.lockOn)
        {
            cameraHandle.LockUnlock();
        }

        if (input.roll || rd.velocity.magnitude >7.0f)
        {
            anim.SetTrigger("roll");
            canAttack = false;
        }

        if (input.jump)
        {
            anim.SetTrigger("jump");
            canAttack = false;
        }

        if (input.attack && CheckState("ground")&&canAttack)
        {
            anim.SetTrigger("attack");
        }

        //旋转模型方向
        if (!cameraHandle.lockState)
        {
            if (input.dMag > 0.1f)
            {
                model.transform.forward = Vector3.Slerp(model.transform.forward, input.dVec, 0.2f);
            }
            if (!lockPlanar)
            {
                planarVelocity = input.dMag * model.transform.forward * moveSpeed * (input.run ? runSpeed : 1.0f);
            }
        }
        else
        {
            if (!lockTarget)
            {
            model.transform.forward = transform.forward;
            }
            else 
            {
                model.transform.forward = planarVelocity.normalized;
            }
            if (!lockPlanar)
            { 
                planarVelocity = input.dVec * moveSpeed * (input.run ? runSpeed : 1.0f);
            }
        }


    }

    private void FixedUpdate()
    {
        //rd.position += moveVelocity * Time.fixedDeltaTime;
        rd.position += deltaPos;
        rd.velocity = new Vector3(planarVelocity.x, rd.velocity.y, planarVelocity.z)+ thrustVec;
        thrustVec = Vector3.zero;
        deltaPos = Vector3.zero;
    }


    private bool CheckState(string stateName, string layerName = "Base Layer") 
        => anim.GetCurrentAnimatorStateInfo(anim.GetLayerIndex(layerName)).IsName(stateName);


    ////////
    ////////
    ////////

    public void OnJumpEnter()
    {
        input.isEnable = false;
        lockPlanar = true;
        lockTarget = true;
        thrustVec = new Vector3(0.0f, jumpVelocity, 0.0f);
    }
    //public void OnJumpExit()
    //{
    //    input.isEnable = true;
    //    lockPlanar = false;
    //}

    public void IsGround()
    {
        anim.SetBool("isGround", true);
    }
    public void IsNotGround()
    {
        anim.SetBool("isGround", false);
    }
    public void OnGroundEnter()
    {
        input.isEnable = true;
        lockPlanar = false;
        lockTarget = false;
        canAttack = true;
        col.material = frictionOne;
    }
    public void OnGroundExit()
    {
        col.material = frictionZero;
    }
    public void OnFallEnter()
    {
        input.isEnable = false;
        lockPlanar = true;
    }
    public void OnRollEnter()
    {
        input.isEnable = false;
        lockPlanar = true;
        lockTarget = true;
        thrustVec = new Vector3(0.0f, rollVelocity, 0.0f);
    }

    public void OnJabEnter()
    {
        input.isEnable = false;
        lockPlanar = true;
    }

    public void OnJabUpdate()
    { 
        thrustVec = model.transform.forward * anim.GetFloat("jabVelocity");  
    }

    public void OnAttack1hAEnter()
    {
        input.isEnable = false;
        targetWeight = 1.0f;
    }

    public void OnAttack1hAUpdate()
    {
        thrustVec = model.transform.forward * anim.GetFloat("attack1hAVelocity");
        anim.SetLayerWeight(anim.GetLayerIndex("attack"), 
            Mathf.Lerp(anim.GetLayerWeight(anim.GetLayerIndex("attack")),targetWeight, 0.1f));
    }

    public void OnAttackIdleEnter()
    {
        input.isEnable = true;
        targetWeight = 0.0f;
    }

    public void OnAttackIdleUpdate()
    {
        anim.SetLayerWeight(anim.GetLayerIndex("attack"),
            Mathf.Lerp(anim.GetLayerWeight(anim.GetLayerIndex("attack")), targetWeight, 0.1f));
    }

    public void OnUpdateRM(object deltaPos)
    {
        if (CheckState("attack1hC", "attack"))
        { 
        this.deltaPos += this.deltaPos * 0.9f + 0.1f * (Vector3)deltaPos;
        }
    }
}
