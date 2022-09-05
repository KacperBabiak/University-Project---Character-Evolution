using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EditLeg : MonoBehaviour
{

    public EventSystem m_EventSystem;
    private GameObject selObject;
    public SelectingObject selObj;
    public GameObject text;

    public InputField inpX;
    public InputField inpY;

    private float timer = 0.0f;


    private void Start()
    {
        
        m_EventSystem = EventSystem.current;
        //selObject = GameObject.FindGameObjectWithTag("mainBody");



    }



    private void Update()
    {
        selObject = selObj.currGameObject;
        //Debug.Log("Current selected GameObject : " + m_EventSystem.currentSelectedGameObject);
        //Debug.Log(selectedObject);
         if (timer > 20)
        {
            timer = 0;
            text.SetActive(false);
        }

        if (timer > 0)
        {
            timer += Time.deltaTime;
        }

        
    }


    public void editSelected()
    {


        int resx;
        bool isNumberx =int.TryParse(inpX.text,out resx);

        int resy;
        bool isNumbery = int.TryParse(inpY.text, out resy);

        if(isNumberx && isNumbery )
        {
            selObject.transform.localScale = new Vector3(resx,resy, 0);
        }
        else if(selObject)
        {
            text.SetActive(true);
            timer += 0.1f;
        }

        

            
    }
}
