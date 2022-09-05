using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HingeJointPlus 
{
    public HingeJoint2D baseJoint;
    public float speed;
    public float minAngle;
    public float maxAngle;
    public float time;

    public bool minusAngle = false;
    //public float defaultAngle;

    public Vector2 defaultConnPoosition;
    public Quaternion defaultConnRotation;

    public bool overMinMax = false;

    public HingeJointPlus(HingeJoint2D basej, float s, float min, float max)
    {
        baseJoint = basej;
        speed = s;
        time = 0;
        //zmienic 
        minAngle = 20;
        maxAngle = 60;
        //defaultAngle = basej.jointAngle;
       
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void restart() //return object to start 
    {
        speed = 0;
        time = 0;
        JointMotor2D motor = baseJoint.motor;
        motor.motorSpeed = 0;
        baseJoint.motor = motor;

        
    }

    public bool checkTime(float timer)
    {
        if (timer % time == 0) { minusAngle = !minusAngle; return true; }
        else return false;
    }
}
