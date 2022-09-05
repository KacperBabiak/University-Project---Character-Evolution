using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class legInfo 
{
    // Start is called before the first frame update
    public GameObject leg;
    public double angle;
    public double force;
    public int time;

    public bool minusAngle = false;

    public Quaternion defaultRotation;
    public Vector3 defaultPosition;

    public legInfo(GameObject l)
    {
        leg = l;
        defaultRotation = l.transform.rotation;
        defaultPosition = l.transform.position;
    }


    public void restart()
    {
        leg.transform.rotation = defaultRotation;
        leg.transform.position = defaultPosition;

        angle = 0;
        force = 0;
        time = 0;
        minusAngle = false;
    }

    public bool checkTime(float timer)
    {
        if (timer % time == 0) { minusAngle = !minusAngle; return true; } 
        else return false;
    }
}
