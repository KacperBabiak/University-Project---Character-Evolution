using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sticking : MonoBehaviour
{

    public GameObject anchor ;

    Vector3 mousePosition;
    private Vector3 offset = new Vector3(0, 0, 10);

    bool selected;//is it selected, when created
    public bool isAnchor;//is it connected to a body
    public GameObject connectedBody;//body that is connected to

    public bool isAnchor2;//is it connected to a second body
    public GameObject connectedBody2;//second body that is connected to

    public Button buttConnect;//buttton that activates connect mode
    


    public bool isConnectOn;

    GameObject mainBody;

    // Start is called before the first frame update
    void Start()
    {
        anchor = this.gameObject;
        selected = true;
        isAnchor = false;
        isAnchor2 = false;
        isConnectOn = false;
        mainBody = GameObject.FindGameObjectWithTag("mainBody");
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //selected object - acnhor- moves with mouse 
        if (selected)
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            anchor.transform.position = mousePosition + offset;

        }

        if (isConnectOn) 
        {
             
                GameObject myEventSystem = GameObject.Find("EventSystem");
                myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(buttConnect.gameObject);
            
        }
    }


    void OnCollisionEnter2D(Collision2D coll)

    {
        //if anchor isnt connecting to anything already, connect it
        if (!isAnchor && mainBody.GetComponent<mainBodyInfo>().legList.Contains( coll.gameObject))
        {
            FixedJoint2D joint = coll.gameObject.AddComponent<FixedJoint2D>();
            joint.connectedBody = anchor.GetComponent<Rigidbody2D>();

            connectedBody = coll.gameObject;
            
            selected = false;
           
            isAnchor = true;
        }
        //if it is and connecting mode is on, we connect second objecct
        else if (isConnectOn && !isAnchor2)
        {
            HingeJoint2D jointHinge = anchor.AddComponent<HingeJoint2D>();
            jointHinge.connectedBody = coll.gameObject.GetComponent<Rigidbody2D>();

            connectedBody2= coll.gameObject;
            
            Rigidbody2D rba = connectedBody2.GetComponent<Rigidbody2D>();

            rba.constraints = RigidbodyConstraints2D.None;
            

            isAnchor2 = true;
            isConnectOn = false;


            GameObject myEventSystem = GameObject.Find("EventSystem");
            myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);

             mainBody.GetComponent<mainBodyInfo>().legList.Add(connectedBody2);
        }


    }

    
    public void setConnect()//enables mode where parts can be connected
    {
        isConnectOn = !isConnectOn;
        if (!isConnectOn)
        {
            GameObject myEventSystem = GameObject.Find("EventSystem");
            myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
        }
        

    }


    public void assignButton(Button button)
    {
        
        buttConnect = button;
        buttConnect.onClick.AddListener(delegate { setConnect(); });
    }
    
    
}

