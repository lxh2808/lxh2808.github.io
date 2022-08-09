using System.Collections.Generic;
using UnityEngine;

public class OtherPlayerControl : MonoBehaviour
{
    private float moveSpeed = 2.0f;
    
    private CharacterController controller;
    public Transform target;
    
    
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 3.0f;
    private float jumpHeight = 2.0f;
    private float gravityValue = -9.81f;

    private float crossTime = 0 ;
    void Start()
    {
        this.controller = GetComponent<CharacterController>();
        targetPosition = transform.position;
    }

    
    void Update()
    {
        updatePosition();
        Move();
    }

    private Vector3 targetPosition;
    private bool isJump;
    private bool isjumpkeyDown;

    private Vector3 jumpPosition;
    //每三秒获取一个新的目标位置
    private void updatePosition()
    {
        crossTime += Time.deltaTime;
        if (crossTime > 3.0f)
        {
            targetPosition = new Vector3(target.position.x, transform.position.y, target.position.z);
            //Save jump skill property
            isjumpkeyDown = target.GetComponent<PlayerControl>().isjumpkeyDown;
            jumpPosition = target.GetComponent<PlayerControl>().jumpPosition;
            
            crossTime = 0;
        }
    }

    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;
    private void Move()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        /***
         * 同步位置
         * 如果放技能了就优先移动到技能位置，如果什么技能也没有就同步player位置
         */
        var newTargetPosition = isjumpkeyDown ? jumpPosition : targetPosition;//如果释放了跳跃技能，就先移动到jumpPosition
        Vector3 moveDir = newTargetPosition - transform.position;
        moveDir.y = 0;
        // Debug.Log("方向:"+moveDir.normalized);
        // Debug.Log("angle:"+Vector3.Angle(moveDir, transform.forward));
        //sqrMagnitude效率高于Distance因为没有开方运算
        // Debug.Log("距离2："+Vector3.Distance(targetPosition,transform.position)+"距离1："+moveDir.sqrMagnitude);
        if (moveDir.sqrMagnitude > 0.1f) //距离大于0.3才移动
            controller.Move(moveDir.normalized * Time.deltaTime * playerSpeed);
        // transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * playerSpeed);
        // transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * playerSpeed);
        // transform.position = Vector3.RotateTowards(transform.position, targetPosition, Time.deltaTime * playerSpeed,0.0f);
        // transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        else
        {
            if(groundedPlayer && !isJump)
                transform.position = newTargetPosition;
        }

        
        
        
        
        
        
        /***
        * 同步动画
        */
        if (!isJump)//没有跳才播放走的动画
        {
            if (moveDir.sqrMagnitude < 0.1f)
            {
                GetComponent<Animator>().SetBool("isrun",false);
            }
            else
            {
                gameObject.transform.forward = moveDir;
                // gameObject.transform.rotation = Quaternion.LookRotation(moveDir);
                GetComponent<Animator>().SetBool("isrun",true);
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
        
        
        
        /***
       * 同步技能
       */
        if (isjumpkeyDown && groundedPlayer && Vector3.Distance(jumpPosition,transform.position)<0.2f)
        {
            isJump = true;
            target.GetComponent<PlayerControl>().isjumpkeyDown  = false;//那边发送true，按没按，这边按完，设置为false
            isjumpkeyDown = false;
            
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            GetComponent<Animator>().SetBool("isground", false);
            GetComponent<Animator>().SetBool("isrun", false);
            GetComponent<Animator>().CrossFade("Jump", 0.2f);
        }
        
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
       
    }
  
    
    
}
