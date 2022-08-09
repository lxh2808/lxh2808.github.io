using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class IKTest : MonoBehaviour
{
    public Transform target;
    public float qx;
    public float qy;
    public float qz;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Quaternion.FromToRotation
        Vector3 targetdir = target.position - this.transform.position ;
        // Quaternion q = Quaternion.FromToRotation(this.transform.forward,targetdir);
        // Vector3 finalDir = q * this.transform.forward;
        // this.transform.forward = finalDir;
        
        //Quaternion.LookRotation
        this.transform.rotation = Quaternion.LookRotation(targetdir, Vector3.up);
    }
    
    // private void OnAnimatorIK(int layerIndex)
    // {
    //     //IK Animator Layer 开启IKPass
    //     //Animator 可以多设置几个transitions, 多个条件，在一个transition，代表逻辑与&&，多个transitions，每个一个条件，代表逻辑或||
    //     //Avatrt Configer 可以使用Pose->Enforce T-Pose,自动调整骨骼
    //     GetComponent<Animator>().SetIKPosition(AvatarIKGoal.LeftHand,target.position);
    //     GetComponent<Animator>().SetIKPositionWeight(AvatarIKGoal.LeftHand,1);
    //
    //     
    // }

    //重新这个方法则root motion位移旋转交给这个动画来处理
    // private void OnAnimatorMove()
    // {
    //     GetComponent<Rigidbody>().velocity = GetComponent<Animator>().velocity;
    //     //rootmotion 动画位移，偏移量
    //     this.transform.position = GetComponent<Animator>().deltaPosition;
    //     this.transform.rotation = GetComponent<Animator>().deltaRotation;
    //     //rootmotion 中心点
    //     Vector3 position = GetComponent<Animator>().bodyPosition;
    //     Vector3 rootposition = GetComponent<Animator>().rootPosition;//根据bodyposition计算出来的rootposition，存在一定偏差；
    // }
}
