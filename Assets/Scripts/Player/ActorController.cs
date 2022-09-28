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

    [SerializeField] bool lockPlanar = false;     //锁定地面速度 
    bool lockTarget=false;          
    bool canAttack;
    public bool isLeftSheid = true;


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
            Vector3 localVec = transform.InverseTransformVector(input.dVec);     //将世界坐标转化为本地坐标,实际上是拿到方向向量乘以  变换后矩阵的逆矩阵
            anim.SetFloat("forward", localVec.z * (input.run ? 2.0f : 1.0f));
            anim.SetFloat("right", localVec.x * (input.run ? 2.0f : 1.0f));
        }


        //anim.SetBool("defense", input.defense);
        #region Shield
        if (isLeftSheid)
        {
            if (CheckState("ground"))
            {
                anim.SetLayerWeight(anim.GetLayerIndex("defense"), 1);
                anim.SetBool("defense", input.defense);
            }
            else
            {
                anim.SetBool("defense", false);
            }
        }
        else
        {
            anim.SetLayerWeight(anim.GetLayerIndex("defense"), 0);
        }
        #endregion

        //锁定信号键
        if (input.lockOn)
        {
            cameraHandle.LockUnlock();
        }

        
        if (input.roll || rd.velocity.magnitude >7.0f) //当刚体速度大于某值时候，就会翻滚
        {
            anim.SetTrigger("roll");
            canAttack = false;
        }

        if (input.jump)
        {
            anim.SetTrigger("jump");
            canAttack = false;
        }

        //判断攻击
        if ((input.lb||input.rb) && (CheckState("ground") || CheckStateTag("attackL")|| CheckStateTag("attackR")) &&canAttack)
        {
            if (input.lb && !isLeftSheid)
            {
                anim.SetBool("R0L1", true);
                anim.SetTrigger("attack");
            }
            else if (input.rb)
            {
                anim.SetBool("R0L1", false);
                anim.SetTrigger("attack");
            }
        }

        //旋转模型方向
        if (!cameraHandle.lockState)   //没有锁定时
        {
            if (input.dMag > 0.1f)
            {
                model.transform.forward = Vector3.Slerp(model.transform.forward, input.dVec, 0.2f);
            }
            if (!lockPlanar)      //锁速度
            {
                planarVelocity = input.dMag * model.transform.forward * moveSpeed * (input.run ? runSpeed : 1.0f); //根据model的forward来转向
            }
        }
        else                     //锁定物体时 
        {
            if (!lockTarget)//跳与翻滚时候，加上这个条件，人物就不会对着锁定物体，而是速度方向
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
        rd.velocity = new Vector3(planarVelocity.x, rd.velocity.y, planarVelocity.z)+ thrustVec;    //y分量控制重力
        thrustVec = Vector3.zero;
        deltaPos = Vector3.zero;
    }


    private bool CheckState(string stateName, string layerName = "Base Layer") 
        => anim.GetCurrentAnimatorStateInfo(anim.GetLayerIndex(layerName)).IsName(stateName);

    public bool CheckStateTag(string tagName, string layerName = "Base Layer")
    => anim.GetCurrentAnimatorStateInfo(anim.GetLayerIndex(layerName)).IsTag(tagName);


    ////////
    ////////
    ////////

    public void OnJumpEnter()
    {
        input.isEnable = false;        //锁死键盘输入
        lockPlanar = true;             //锁死最后一次速度
        lockTarget = true;             //Lock模式的下的跳跃 
        thrustVec = new Vector3(0.0f, jumpVelocity, 0.0f);     //跳跃时向上冲量
    }
    //public void OnJumpExit()
    //{
    //    input.isEnable = true;
    //    lockPlanar = false;
    //}

    #region CheckGround
    public void IsGround()      //检测地面（是否落地）
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
    #endregion


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
    }

    public void OnAttackExit()
    {
        model.SendMessage("WeaponDisable");
    }

    public void OnAttack1hAUpdate()
    {
        thrustVec = model.transform.forward * anim.GetFloat("attack1hAVelocity");
        //anim.SetLayerWeight(anim.GetLayerIndex("attack"), 
        //    Mathf.Lerp(anim.GetLayerWeight(anim.GetLayerIndex("attack")),targetWeight, 0.1f));
    }

    public void OnAttackIdleEnter()
    {
        input.isEnable = true;
    }

    //public void OnAttackIdleUpdate()
    //{
    //    anim.SetLayerWeight(anim.GetLayerIndex("attack"),
    //        Mathf.Lerp(anim.GetLayerWeight(anim.GetLayerIndex("attack")), targetWeight, 0.1f));
    //}

    public void OnUpdateRM(object deltaPos)
    {
        if (CheckState("attack1hC"))
        { 
        this.deltaPos += this.deltaPos * 0.9f + 0.1f * (Vector3)deltaPos;
        }
    }

    public void OnHitEnter()
    {
        input.isEnable = false;
        planarVelocity = Vector3.zero;
    }

    public void issueTrigger(string name)
    {
        anim.SetTrigger(name);
    }
}
