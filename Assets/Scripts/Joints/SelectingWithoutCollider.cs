using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectingWithoutCollider : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameObject selectedAnchor;
    public GameObject SelectingManager;
    public MoveAndDrag mad;
    //NewAnchor na;



   
    public bool moving = false; //czy aktualnie zmieniasz pozycje anchora
    public bool selected = false;//czy jest w tej chwili zaznaczony>
    private Vector3 offset = new Vector3(0, 0, 10);

    GameObject myLine;
    LineRenderer lr;
  
   
    void Start()
    {
        
        SelectingManager = GameObject.Find("SelectingManager");
        mad = SelectingManager.GetComponent<MoveAndDrag>(); // zdobycie wartosci z innego skryptu




        
    }

// Update is called once per frame.
void Update()
    {
        //sprawdza czy anchor jest wybierany, nie u¿ywa do tego colliderów zeby reszta dzia³a³a
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);


        //if mouse is over an object and clicks, its selected and moves 
        if (IsOver(mousePosition,gameObject.transform.position, gameObject.GetComponent<Renderer>().bounds.size.x/2))
        {
            gameObject.GetComponent<Renderer>().material.color = Color.yellow;
            if (Input.GetMouseButtonDown(0))
            {
                

                gameObject.transform.parent = null;//odczepia od rodzica
                moving = true;
                selected = true;

            }
        }
        else
        {
            gameObject.GetComponent<Renderer>().material.color = Color.red;
            selected = false;
        }
        
        if (moving)
        {
            gameObject.transform.position = mousePosition + offset;

            Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);
            if (Input.GetMouseButtonDown(0) && targetObject)
            {
                moving = false;
                
                gameObject.transform.SetParent(targetObject.transform);
               
            }

        }

        mad.anchorMoving = moving;//move and drag musi wiedzieæ czy aktualnie anchor jest poruszany
       
    }

    private bool IsOver(Vector3 mp, Vector3 op,float objectRadius)//mouse position, object position, object radius - checks mouse over an obejct
    {
        if ((mp.x < (gameObject.transform.position.x + objectRadius))    && (mp.x > (gameObject.transform.position.x - objectRadius)))
        {
            if ((mp.y < (gameObject.transform.position.y + objectRadius)) && (mp.y > (gameObject.transform.position.y - objectRadius)))
            {
                //Debug.Log("over!");
                return true;
                
            }
        }
        return false;
    }

   
}
