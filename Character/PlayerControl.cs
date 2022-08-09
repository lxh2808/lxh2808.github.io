using System;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private float moveSpeed = 2.0f;
    
    private CharacterController controller;
    public Transform cameraTransform;
    
    
    private Vector3 playerVelocity;
    public bool isJump;
    public bool isjumpkeyDown;
    public Vector3 jumpPosition;
    private bool groundedPlayer;
    private float playerSpeed = 3.0f;
    private float jumpHeight = 2.0f;
    private float gravityValue = -9.81f;
    void Start()
    {
        this.controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Move();
        CameraControler();
    }

    private void Move()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * playerSpeed);
        
        if (!isJump)//没有跳才播放走的动画
        {
            if (move != Vector3.zero)
            {
                gameObject.transform.forward = move;
                GetComponent<Animator>().SetBool("isrun",true);
            }
            else
            {
                GetComponent<Animator>().SetBool("isrun",false);
            }
        }
        else
        {
            if (groundedPlayer)
            {
                isJump = false;
                GetComponent<Animator>().SetBool("isground",true);
                GetComponent<Animator>().CrossFade("Idle",0.2f);
            }
        }




        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            isJump = true;
            isjumpkeyDown = true;
            jumpPosition = this.transform.position;
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            GetComponent<Animator>().SetBool("isground", false);
            GetComponent<Animator>().SetBool("isrun", false);
            GetComponent<Animator>().CrossFade("Jump", 0.2f);
        }
        // Debug.Log(groundedPlayer+":::"+controller.isGrounded);//一个神奇的debug，按理说这俩应该相等，但是为什么不想等，估计值传递和引用传递的区别吧
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    void CameraControler()
    {
        //Rotation
        if (Input.GetMouseButton(0))
            CameraRotation();
        
        //Position
        // cameraTransform.LookAt(this.transform);//第一人称视角
        cameraTransform.position = new Vector3(this.transform.position.x ,cameraTransform.position.y,this.transform.position.z-16f);
    }

    private float mouseY = 0f;
    private float mouseX = 0f;
    private const float xMouseSensitivity = 3f;
    private const float yMouseSensitivity = 3f;
    private const float smoothCameraRotation = 12f;
    void CameraRotation()
    {
        var y = Input.GetAxis("Mouse Y");
        var x = Input.GetAxis("Mouse X");
        
        // free rotation 
        mouseX += x * xMouseSensitivity;
        mouseY -= y * yMouseSensitivity;
        
        Quaternion newRot = Quaternion.Euler(mouseY, mouseX, 0);
        // cameraTransform.rotation = newRot;
        cameraTransform.rotation = Quaternion.Slerp(cameraTransform.rotation, newRot, smoothCameraRotation * Time.deltaTime);
    }

    
}
