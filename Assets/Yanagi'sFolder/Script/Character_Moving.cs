using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Moving : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float moveSpeed;
    [SerializeField] bool CollisionGround;
    private Rigidbody rg;
    public float sensitivityX=1, sensitivityY=1,delay=2.0f;//マウスの感度設定標準設定で1
    [SerializeField] float X, Z,fallSpeed,Y;
    [SerializeField] float mouseX,mouseY,cameraX;
    private bool moveZ, moveX, mouseXbool, mouseYbool, TimeProgress = true;//TimeProgress=falseで初動delay秒は動けないようにする
    [SerializeField] Transform transCamera;

    void Start()
    {
        rg = GetComponent<Rigidbody>();
        transCamera = Camera.main.transform;
       // cameraX = transCamera.localEulerAngles.x;
        cameraX=0;
        Invoke("Progress", delay);
      //Physics.gravity = new Vector3(0,fallSpeed, 0);

    }

    void Update()
    {
        //Y = -5;

        X = Input.GetAxis("Horizontal");
        mouseX = Input.GetAxis("Mouse X")* sensitivityX;
        Z = Input.GetAxis("Vertical");
        mouseY=Input.GetAxis("Mouse Y")* sensitivityY;


        if (Z != 0)
        {
            moveZ = true;
        }
        if (X != 0)
        {
            moveX = true;
        }
        if (mouseX != 0)
        {
            mouseXbool = true;
        }
        if (mouseY != 0)
        {
            mouseYbool = true;
        }
        

    }

    void FixedUpdate()//地面との当たり判定を用いて、自由落下を作る
    {
        if (TimeProgress==true) {
            if ((moveZ || moveX || mouseXbool || mouseYbool)&& CollisionGround)
            {
                moveZ = false;
                moveX = false;
                mouseXbool = false;
                mouseYbool = false;
               
                rg.velocity = transform.rotation * new Vector3(X, 0, Z) * moveSpeed * Time.deltaTime * 100;
                //rg.AddForce(X*moveSpeed, 0, Z* moveSpeed);
                rg.MoveRotation(Quaternion.Euler(0.0f, rg.rotation.eulerAngles.y + mouseX * Time.deltaTime * 100.0f, 0.0f));
                cameraX = Mathf.Clamp(cameraX - mouseY * Time.deltaTime * 100.0f, -40, 40);
                transCamera.localEulerAngles = new Vector3(cameraX, 0, 0);
                //rg.velocity = transform.rotation * new Vector3(X * moveSpeed, 0, Z * moveSpeed) * Time.deltaTime * 100;
            }
            else if((moveZ || moveX || mouseXbool || mouseYbool) && CollisionGround==false)
            {
                fallSpeed -= Y;
                rg.velocity = transform.rotation * new Vector3(X, fallSpeed, Z) * moveSpeed * Time.deltaTime * 100;
                //rg.AddForce(X*moveSpeed, 0, Z* moveSpeed);
                rg.MoveRotation(Quaternion.Euler(0.0f, rg.rotation.eulerAngles.y + mouseX * Time.deltaTime * 100.0f, 0.0f));
                cameraX = Mathf.Clamp(cameraX - mouseY * Time.deltaTime * 100.0f, -40, 40);
                transCamera.localEulerAngles = new Vector3(cameraX, 0, 0);
                

            } else
            {
                // rg.velocity =new Vector3(0,Y);
            }
        }
    }
    void OnCollisionExit(Collision other)
    {
        CollisionGround=false;
        
    }
    void OnCollisionStay(Collision collision)
    {
        CollisionGround=true;
            fallSpeed = Y;
        

    }
    void Progress()
    {
        TimeProgress = true;
    }
}

