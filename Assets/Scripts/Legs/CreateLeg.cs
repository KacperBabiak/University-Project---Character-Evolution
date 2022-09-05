using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateLeg : MonoBehaviour
{
    public Button rectBut, circBut, capBut;
    public GameObject rect, circle, cap;
    private void Start()
    {
        rectBut.onClick.AddListener(delegate { addLeg("rect"); });
        circBut.onClick.AddListener(delegate { addLeg("circle"); });
        capBut.onClick.AddListener(delegate { addLeg("capsule"); });
    }

    private void addLeg(string shape = "rect")
    {

        //GameObject obj;
        //tworzy obiekt, ró¿ne kszta³ty
        if (shape == "circle")
        {
            
            Instantiate(circle, new Vector3(0, 0, 0), Quaternion.identity);

        }
        else if(shape == "capsule")
        {
            
            Instantiate(cap, new Vector3(0, 0, 0), Quaternion.identity);
        }
        else
        {
            
            Instantiate(rect, new Vector3(0, 0, 0), Quaternion.identity);
        }

        
        
        
        
    }
}
