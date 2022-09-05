using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAndDrag : MonoBehaviour
{
    public GameObject movedObject;
    Vector3 offset;
    GameObject anchor;

    public GameObject des;
    public GameObject mainBody;
    public bool anchorMoving = false;
    void Update()
    {


        if (!anchorMoving)//jesli juz nie porusza siê anchor
        {


            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);//pozycja myszki
            if (Input.GetMouseButtonDown(0))
            {
                Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);
                if (targetObject)//jesli uzytkownik klika lpm na obiekt, ten obeikt jest zaznaczany i poruszany myszka
                {
                    movedObject = targetObject.transform.gameObject;

                    offset = movedObject.transform.position - mousePosition;


                    if (targetObject.CompareTag("Anchor") && !des.GetComponent<Destroyer>().isActive)
                    {


                        Sticking st = targetObject.GetComponent<Sticking>();
                        if (st.connectedBody)
                        {
                            Destroy(st.connectedBody.GetComponent<FixedJoint2D>());
                        }

                        st.connectedBody = null;
                        st.isAnchor = false;

                    }
                }
            }
            if (movedObject)
            {
                /*
                if (movedObject.GetComponent<FixedJoint2D>() != null)
                {
                    anchor = movedObject.GetComponent<FixedJoint2D>().connectedBody.gameObject;
                    Rigidbody2D rba = anchor.GetComponent<Rigidbody2D>();

                    rba.constraints = RigidbodyConstraints2D.None;
                    rba.constraints = RigidbodyConstraints2D.FreezeRotation;
                }


                //movedObject.transform.position = mousePosition + offset;
                Rigidbody2D rb = movedObject.GetComponent<Rigidbody2D>();
                rb.MovePosition(mousePosition + offset);

                //cc.Move(mousePosition + offset);

                rb.constraints = RigidbodyConstraints2D.None;
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                */





                bool isConnected = false;

                if(movedObject.CompareTag("Leg") || movedObject.CompareTag("mainBody"))
                {
                    foreach (GameObject leg in mainBody.GetComponent<mainBodyInfo>().legList)
                    {
                        if(leg == movedObject)
                        {
                            isConnected = true;
                            break;
                        }
                    }
                }

                if (!isConnected)
                {
                    Rigidbody2D rb = movedObject.GetComponent<Rigidbody2D>();
                    rb.constraints = RigidbodyConstraints2D.None;
                    

                    if (Input.GetAxis("Mouse ScrollWheel") > 0)
                    {
                        movedObject.transform.Rotate(Vector3.forward * 2f);
                        //movedObject.transform.rotation = Quaternion.Euler(  Vector3.forward * 50);
                    }
                    if (Input.GetAxis("Mouse ScrollWheel") < 0)
                    {
                        movedObject.transform.Rotate(Vector3.back * 2f);
                    }

                    //rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                    rb.MovePosition(mousePosition + offset);

                }
                else
                {
                    GameObject[] anchorlist = GameObject.FindGameObjectsWithTag("Anchor");
                    foreach (GameObject a in anchorlist)
                    {

                        Rigidbody2D rba = a.GetComponent<Rigidbody2D>();

                        rba.constraints = RigidbodyConstraints2D.None;
                        rba.constraints = RigidbodyConstraints2D.FreezeRotation;
                    }

                    foreach (GameObject leg in mainBody.GetComponent<mainBodyInfo>().legList)
                    {
                        Rigidbody2D rba = leg.GetComponent<Rigidbody2D>();

                        rba.constraints = RigidbodyConstraints2D.None;
                        rba.constraints = RigidbodyConstraints2D.FreezeRotation;
                    }

                    Rigidbody2D rb = movedObject.GetComponent<Rigidbody2D>();
                    rb.MovePosition(mousePosition + offset);
                }

               



            }
            if (Input.GetMouseButtonUp(0) && movedObject)//gdy uzytkownik pusci przycisk obiekt przestaje byæ poruszany;
            {

                Rigidbody2D rb = movedObject.GetComponent<Rigidbody2D>();
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
                //rb.freezeRotation=true;

                //czy to jest potrzebne?
                if (anchor)
                {
                    Rigidbody2D rba = anchor.GetComponent<Rigidbody2D>();

                    rba.constraints = RigidbodyConstraints2D.FreezeAll;

                    anchor = null;
                }


                movedObject = null;


            }
        }
    }


}
    
