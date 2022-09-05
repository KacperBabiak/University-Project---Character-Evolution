using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class NewAnchor : MonoBehaviour
{
    //bool clicked = false;//czy przycisk zosta³ nacisniety, anchor jeszcze nie jest dodany
    
    GameObject go;
    //joins button to another script
    public Button buttonConnect;
    public GameObject anchor;

    // Start is called before the first frame update
    void Start()
    {
      

    }

    

    public void createAnchor()
    {
        //stworzenie anchora
        go = Instantiate(anchor, new Vector3(0, 0, 0), Quaternion.identity);
        Sticking st = go.GetComponent<Sticking>();
        st.assignButton(buttonConnect);//button is assigned to the anchor
    }



    



}
