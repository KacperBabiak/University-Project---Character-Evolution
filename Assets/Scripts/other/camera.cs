using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    Vector3 offset  = new Vector3(4,1,-10);
    
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player )
        {;
            transform.position = new Vector3(player.transform.position.x + offset.x,
                player.transform.position.y + offset.y,
                 player.transform.position.z + offset.z);

        }
    }
}
