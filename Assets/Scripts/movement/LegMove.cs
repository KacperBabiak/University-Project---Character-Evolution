using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySharpNEAT;
using SharpNeat.Phenomes;
using System.Timers;
using System;

public class LegMove : UnitController
{
    Timer myTimer;

    public HingeJoint2D[] tempArray;

    public legInfo[] legList;
    public HingeJointPlus[] hjList;
    public GameObject mainBody;

    public int currentCheckpoint = 0;
    public int maxCheckpoint = 0;
    public int timesBack = 0;
    public float distance = 0;
    public float SensorRange = 5;

    private Vector3 _initialPosition = default;
    private Quaternion _initialRotation = default;

   
    bool minusAngle = false;
    private float timer = 0.0f;

   

    // Start is called before the first frame update
    void Start()
    {
        mainBody = gameObject;
        //what is the initial parameters that we will be returning to
        _initialPosition = mainBody.transform.position;
        _initialRotation = mainBody.transform.rotation;

         tempArray = gameObject.GetComponentsInChildren<HingeJoint2D>();

        hjList = new HingeJointPlus[tempArray.Length];

        int i = 0;
        foreach (HingeJoint2D hj in tempArray)
        {
            hjList[i] = new HingeJointPlus(hj, 0, 0, 0);
            i++;
        }
        
        List<GameObject> arrayTemp = gameObject.GetComponent<mainBodyInfo>().legList;
        //creating an array with all legs and addditional info about them
        
        legList = new legInfo[arrayTemp.Count];



        i = 0;
        foreach (GameObject go in arrayTemp)
        {
            //Debug.Log(i);
            legList[i] = new legInfo(go);
            i++;
        }
        

        //settting up timer
        myTimer = new Timer();
        myTimer.Elapsed += new ElapsedEventHandler(moveLeg);

        myTimer.Start();

        changeTimer();
    }

    //method for moving legs, activates itslef every few seconds
    public void moveLeg(object source, ElapsedEventArgs e)
    {
        minusAngle = !minusAngle;
        /*
        //Debug.Log("x");
        foreach (legInfo li in legList)
        {
            //Debug.Log("y");
            li.angle = -li.angle;
            Debug.Log(li.angle +"  "+ li.force + " timescale " );
        }
        */

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        foreach (HingeJointPlus hj in hjList)
        {

            JointMotor2D motor = hj.baseJoint.motor;
            hj.checkTime(timer);

            if (hj.minusAngle)
            {
                motor.motorSpeed = -hj.speed;
                hj.baseJoint.motor = motor;
            }
            else
            {
                motor.motorSpeed = hj.speed;
                hj.baseJoint.motor = motor;

            }
        }
            /*
            foreach (legInfo li in legList)
            {
                Rigidbody2D rb = li.leg.GetComponent<Rigidbody2D>();

                if (minusAngle)
                {
                    rb.MoveRotation(Mathf.LerpAngle((float)li.angle, rb.rotation, (float)li.force * Time.deltaTime));
                }
                else
                {
                    rb.MoveRotation(Mathf.LerpAngle(rb.rotation, (float)li.angle, (float)li.force * Time.deltaTime));

                }

                li.checkTime(timer);
                if (li.minusAngle)
                {
                    rb.MoveRotation(Mathf.LerpAngle((float)li.angle, rb.rotation, (float)li.force * Time.deltaTime));
                }
                else
                {
                    rb.MoveRotation(Mathf.LerpAngle(rb.rotation, (float)li.angle, (float)li.force * Time.deltaTime));

                }

            }
            */


            distance = mainBody.transform.position.x - _initialPosition.x;
        // Debug.Log(currentCheckpoint);

    }

    public void changeTimer()
    {
        myTimer.Enabled = false;
        myTimer.Interval = 3000 / Time.timeScale;
        myTimer.Enabled = true;
    }

    protected override void UpdateBlackBoxInputs(ISignalArray inputSignalArray)
    {
        // Called by the base class on FixedUpdate

        // Feed inputs into the Neural Net (IBlackBox) by modifying its InputSignalArray
        // The size of the input array corresponds to NeatSupervisor.NetworkInputCount


        /* EXAMPLE */
        //inputSignalArray[0] = someSensorValue;
        //inputSignalArray[1] = someOtherSensorValue;
        //...


        inputSignalArray[0] = mainBody.gameObject.transform.position.x;


        float leftSensor = 0;
        float rightSensor = 0;

        // Five raycasts into different directions each measure how far a wall is away.
        RaycastHit hit;



        if (Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(1, 0, 0).normalized), out hit, SensorRange))
        {
            if (hit.collider.CompareTag("Wall"))
            {
                rightSensor = 1 - hit.distance / SensorRange;
            }
        }



        if (Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(-1, 0, 0).normalized), out hit, SensorRange))
        {
            if (hit.collider.CompareTag("Wall"))
            {
                leftSensor = 1 - hit.distance / SensorRange;
            }
        }

        // modify the ISignalArray object of the blackbox that was passed into this function, by filling it with the sensor information.
        // Make sure that NeatSupervisor.NetworkInputCount fits the amount of sensors you have
        inputSignalArray[1] = rightSensor;
        inputSignalArray[2] = leftSensor;



    }

    protected override void UseBlackBoxOutpts(ISignalArray outputSignalArray)
    {
        // Called by the base class after the inputs have been processed

        // Read *the outputs and do something with them
        // The size of the array corresponds to NeatSupervisor.NetworkOutputCount

        //Debug.Log("angle " + outputSignalArray[0] + " force " + outputSignalArray[1]);


        int i = 0;

        /*
        foreach (legInfo li in legList)
        {
            
            
            if (true)
            {
                li.force = outputSignalArray[i] * 50;
                li.angle = outputSignalArray[i + 1] * 50;
                li.time = (int)(outputSignalArray[i + 2] * 6);
                Debug.Log(li.force + " |||| " + li.angle);

            }

            

           

            i = i + 3;
            
        }
        */
        foreach (HingeJointPlus hj in hjList)
        {


            if (true)
            {
                hj.speed = (float)outputSignalArray[i]*30;
                hj.time = (float)outputSignalArray[i] * 5;
                Debug.Log(hj.speed + " " + hj.time);
            }





            i+=2;

        }




    }

    public override float GetFitness()
    {
        // Called during the evaluation phase (at the end of each trail)

        // The performance of this unit, i.e. it's fitness, is retrieved by this function.
        // Implement a meaningful fitness function here

        //float fit = distance - timesBack * 0.2f;
        float fit = distance * 15 + currentCheckpoint * 5 - timesBack * 5;
        if (fit > 0)
        {
            return fit;
        }
        return 0;
    }

    protected override void HandleIsActiveChanged(bool newIsActive)
    {
        // Called whenever the value of IsActive has changed

        // Since NeatSupervisor.cs is making use of Object Pooling, this Unit will never get destroyed. 
        // Make sure that when IsActive gets set to false, the variables and the Transform of this Unit are reset!
        // Consider to also disable MeshRenderers until IsActive turns true again.

        if (newIsActive == false)
        {
            mainBody.transform.position = _initialPosition;
            mainBody.transform.rotation = _initialRotation;

            currentCheckpoint = 0;
            maxCheckpoint = 0;
            timesBack = 0;
            timer = 0;
            minusAngle = false;

           
            foreach (legInfo li in legList)
            {
                //Debug.Log(li.force + " " + li.angle + " " + li.time);
                li.restart();
            }

            foreach(HingeJointPlus hj in hjList)
            {
                hj.restart();
            }


        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (!IsActive)
            return;

        if (collision.CompareTag("Checkpoint"))
        {

            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            int cp = collision.gameObject.GetComponent<checkpoint>().checkpointNumber;
            //Debug.Log(cp);

            if (cp != currentCheckpoint)
            {
                //lastCheckpoint = currentCheckpoint;


                if (cp > maxCheckpoint) maxCheckpoint = cp;
                if (cp < currentCheckpoint)
                {
                    timesBack++;
                }

                currentCheckpoint = cp;
            }
        }
    }


    public void addToList()
    {

    }


}

