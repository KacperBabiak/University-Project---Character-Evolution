using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainBodyInfo : MonoBehaviour
{

    public List<GameObject> legList;


    // Start is called before the first frame update
    void Start()
    {
        legList = new List<GameObject>();

        legList.Add(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
