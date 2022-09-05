using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectingObject : MonoBehaviour
{
    public EventSystem m_EventSystem;
    public GameObject prevGameObject;
    public GameObject currGameObject;

    public InputField inpX;
    public InputField inpY;


    void OnEnable()
    {
        //Fetch the current EventSystem. Make sure your Scene has one.
        m_EventSystem = EventSystem.current;
    }

    void updateText()
    {
        inpX.SetTextWithoutNotify(currGameObject.transform.localScale.x.ToString());
        inpY.SetTextWithoutNotify(currGameObject.transform.localScale.y.ToString());
    }

    void Update()
    {
        //Debug.Log(m_EventSystem.currentSelectedGameObject.tag);
       
        // Debug.Log("t");
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);
            if (targetObject )


            {
                
                    
                    //ustawienie obiektów
                    prevGameObject = currGameObject;

                    
                    //m_EventSystem.SetSelectedGameObject(targetObject.gameObject);
                //Debug.Log("Current selected GameObject : " + m_EventSystem.currentSelectedGameObject);
                    currGameObject = targetObject.gameObject;

                    //zmiana ich kolorów
                    if (prevGameObject) prevGameObject.GetComponent<Renderer>().material.color = Color.white;
                    currGameObject.GetComponent<Renderer>().material.color = Color.yellow;

                    //ustawienie wielkosci w inputach zmiany rozmiaru
                    updateText();

                    // Debug.Log("Current selected GameObject : " + m_EventSystem.currentSelectedGameObject);
                    //Debug.Log("Previous selected GameObject : " + prevGameObject);

            }
        }

      
    }
}
