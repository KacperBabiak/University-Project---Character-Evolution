using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointList : MonoBehaviour
{
    public GameObject mainBody;
    public HingeJointPlus[] jointArray = new HingeJointPlus[2];


    
    // Start is called before the first frame update
    void Start()
    {
        
        HingeJoint2D[] hjArr = GetComponents<HingeJoint2D>();

        //creates an array of classes of joints with additiional info
        int i = 0;
        foreach(HingeJoint2D hj in hjArr)
        {
            
            jointArray[i] = new HingeJointPlus(hj, hj.motor.motorSpeed, hj.limits.min, hj.limits.max );
            i++;
            
        }

       
       
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public HingeJointPlus[] getArray()
    {
        return jointArray;
    }
}
