using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Destroyer : MonoBehaviour
{
    // Start is called before the first frame update

    public bool isActive;
    public GameObject mainBody;
    public Button butt;
    

    void Start()
    {
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive )
        {
            GameObject myEventSystem = GameObject.Find("EventSystem");
            myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(butt.gameObject); 

            if ( Input.GetMouseButtonDown(0))
            {
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);//pozycja myszki

                Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);
                if (targetObject)
                {

                
                    if (!targetObject.CompareTag("mainBody"))// jesli to nie jest mainBody - nieusuwalne
                    {


                        if (targetObject.CompareTag("Anchor")) // jesli to anchor
                        {

                            Sticking st = targetObject.gameObject.GetComponent<Sticking>();

                            //Debug.Log(st.isAnchor + " "+ st.isAnchor2);
                            //jesli po³aczone z dwoma obiektami to nie moze byæ usuniete
                            if (st.isAnchor == true)
                            {
                                if (st.isAnchor2 == false)
                                {

                                    Destroy(st.connectedBody.GetComponent<FixedJoint2D>());
                                    Destroy(targetObject.gameObject);
                                }
                            }
                            else
                            {

                                Destroy(targetObject.gameObject);
                            }

                        }

                        else if (targetObject.CompareTag("Leg")) //jesli to noga
                        {
                            HingeJoint2D[] hjlist = GameObject.FindObjectsOfType<HingeJoint2D>();

                            bool isConnected = false;
                            HingeJoint2D hjj = new HingeJoint2D();
                            foreach (HingeJoint2D hj in hjlist)
                            {
                                Debug.Log(hj.gameObject.name);
                                if (hj.connectedBody == targetObject.gameObject.GetComponent<Rigidbody2D>())
                                {
                                    isConnected = true;
                                    hjj = hj;
                                    break;
                                }
                            }

                            if (!isConnected)// w ogole nie jest po³aczony
                            {
                                
                                Destroy(targetObject.gameObject);
                                

                            }
                            else
                            {
                                if (targetObject.GetComponent<FixedJoint2D>() == null)//jesli nie ma drugiego po³aczenia
                                {
                                    hjj.gameObject.GetComponent<Sticking>().isAnchor2 = false;
                                    Destroy(targetObject.gameObject);
                                    
                                }
                            }

                        }



                        }
                }


            }
         }
       
    }

    

    public void setActive()
    {
        GameObject myEventSystem = GameObject.Find("EventSystem");
        isActive = !isActive;
        if (!isActive)
        {
            
            myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);

        }
        
        

    }
   
}
