using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraHandle : MonoBehaviour
{
    IInputController input;
    [SerializeField] float horizontalSpeed;
    [SerializeField] float verticalSpeed;
    [SerializeField] float cameraTime;

    float tempEuler;
    Vector3 cameraVelocity = Vector3.zero;

    GameObject horizontalHandle;
    GameObject verticalHandle;
    GameObject model;
    GameObject cameraPos;

    Vector3 tempModelEuler;

    //[SerializeField] GameObject lockTarget;
    TargetCol lockTarget;

    [SerializeField] LayerMask enemyMask;

    [SerializeField] Image targetIcon;
    public bool lockState;



    private void Awake()
    {
        verticalHandle = transform.parent.gameObject;
        horizontalHandle = verticalHandle.transform.parent.gameObject;
        ActorController ac = horizontalHandle.GetComponent<ActorController>();
        model = ac.model;
        input = ac.input;
        tempEuler = 20f;
        targetIcon.enabled = false;
        lockState = false;
        cameraPos = Camera.main.gameObject;

        Cursor.lockState = CursorLockMode.Locked;
    }


    private void FixedUpdate()
    {

        if (lockTarget == null)
        {
            tempModelEuler = model.transform.eulerAngles;

            horizontalHandle.transform.Rotate(Vector3.up, input.JRight * horizontalSpeed * Time.fixedDeltaTime);

            tempEuler -= input.JUp * verticalSpeed * Time.fixedDeltaTime;
            tempEuler = Mathf.Clamp(tempEuler, -40f, 30f);
            verticalHandle.transform.localEulerAngles = new Vector3(tempEuler, 0, 0);

            model.transform.eulerAngles = tempModelEuler;
        }
        else
        {
            Vector3 targetDir = lockTarget.obj.transform.position - model.transform.position;
            targetDir.y = 0;
            horizontalHandle.transform.forward = targetDir;
            verticalHandle.transform.LookAt(lockTarget.obj.transform);
        }

        cameraPos.transform.position = Vector3.SmoothDamp(cameraPos.transform.position, transform.position, ref cameraVelocity, cameraTime);
        //cameraPos.transform.eulerAngles = transform.eulerAngles;
        cameraPos.transform.LookAt(verticalHandle.transform);
    }

    private void Update()
    {
        if (lockTarget != null)
        {
            targetIcon.rectTransform.position = Camera.main.WorldToScreenPoint
                (lockTarget.obj.transform.position + new Vector3(0.0f, lockTarget.halfHeight, 0.0f));
            if (Vector3.Distance(model.transform.position, lockTarget.obj.transform.position) > 10.0f)
            {
                lockTarget = null;
                targetIcon.enabled = false;
                lockState = false;
            }
        }
    }

    public void LockUnlock()
    {
        //TODO:切换多个目标


        Vector3 vec1 = model.transform.position;
        Vector3 vec2 = vec1 + new Vector3(0, 1.0f, 0);
        Vector3 center = vec2 + model.transform.forward * 5.0f;

        Collider[] cols = Physics.OverlapBox(center, new Vector3(0.5f, 0.5f, 5.0f), model.transform.rotation, enemyMask);

        if (cols.Length == 0)
        {
            lockTarget = null;
            targetIcon.enabled = false;
            lockState = false;
        }
        else
        {
            foreach (var col in cols)
            {
                if ( lockTarget != null &&lockTarget.obj == col.gameObject)
                {
                    lockTarget = null;
                    targetIcon.enabled = false;
                    lockState = false;
                    break;
                }
                lockTarget = new TargetCol(col.gameObject,col.bounds.extents.y);
                targetIcon.enabled = true;
                lockState = true;
                break;
            }
        }
    }

    class TargetCol
   {
        public GameObject obj;
        public float halfHeight;

        public TargetCol(GameObject obj,float halfH)
        {
            this.obj = obj;
            this.halfHeight = halfH;
        }
   }
}
