using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class DrawCricle : MonoBehaviour
{
    private LineRenderer line;
    private int r = 2;
    private int n = 360;
    void Start()
    {
        this.line = GetComponent<LineRenderer>();
        draw1();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void draw1()
    {
        for (int i = 0; i < n; i++)
        {
            float x = Mathf.Sin((360 * (i+1) / n) * Mathf.Deg2Rad) * r;
            float z = Mathf.Cos((360 * (i+1) / n) * Mathf.Deg2Rad) * r;
            // line.SetPosition(i,new Vector3(x,0,z));
            line.SetPosition(i,new Vector3(x,3,z));
        }
    }
    
    void draw2()
    {
        for (int i = 0; i < n; i++)
        {
            this.transform.Rotate(0,1,0);
            line.SetPosition(i,this.transform.forward * r);
           // this.transform.Rotate(0,0,1);
           // line.SetPosition(i,this.transform.right * r);
        }
    }
}
