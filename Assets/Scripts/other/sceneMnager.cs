using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnitySharpNEAT;

public class sceneMnager : MonoBehaviour
{

    bool tutorialShown = false;
    public GameObject pan;
    public GameObject mainBody;


    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextScene(string scene)
    {
        if (scene == "Evolution")
        {
            savePrefab();
            SceneManager.LoadScene(scene);
            
        }
        else
        {
            SceneManager.LoadScene(scene);
        }
       // mainBody.SetActive(false);

    }

    void changeSupervisor()
    {

        GameObject charac= (GameObject)Resources.Load("Character");
        Debug.Log(GameObject.FindGameObjectWithTag("Supervisor").name) ;
    }

    void savePrefab()
    {
       
        GameObject[] anchorlist = GameObject.FindGameObjectsWithTag("Anchor");
        foreach (GameObject a in anchorlist)
        {
            a.transform.SetParent(mainBody.transform);
            a.layer = 3;
            

            Destroy(a.GetComponent<Sticking>());
            Rigidbody2D rba = a.GetComponent<Rigidbody2D>();

            rba.gravityScale = 1;
            rba.mass = 1;
            rba.constraints = RigidbodyConstraints2D.None;
            
        }

        foreach (GameObject leg in mainBody.GetComponent<mainBodyInfo>().legList)
        {
            leg.transform.SetParent(mainBody.transform);
            Rigidbody2D rba = leg.GetComponent<Rigidbody2D>();

            rba.gravityScale = 1;
            rba.mass = 1;
            rba.constraints = RigidbodyConstraints2D.None;
            
        }

        mainBody.AddComponent<LegMove>();
        mainBody.transform.position = new Vector3(-7, 0, -1);

        
        //CreatePrefab(mainBody);
    }


    static void CreatePrefab(GameObject mainBody)
    {
        
            // Create folder Prefabs and set the path as within the Prefabs folder,
            // and name it as the GameObject's name with the .Prefab format
            
            string localPath = "Assets/Resources/Character.prefab";

            // Make sure the file name is unique, in case an existing Prefab has the same name.
           // localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);

            // Create the new Prefab and log whether Prefab was saved successfully.
            /*
            bool prefabSuccess;
            PrefabUtility.SaveAsPrefabAsset(mainBody, localPath, out prefabSuccess);
            if (prefabSuccess == true)
                Debug.Log("Prefab was saved successfully");
            else
                Debug.Log("Prefab failed to save" + prefabSuccess);
            */
        
    }


    public void doTutorial()
    {
        if (tutorialShown)
        {

            pan.SetActive(false);
        }
        else pan.SetActive(true);

        tutorialShown = !tutorialShown;
    }


    private void Awake()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "CharacterCreator")
        {
            DontDestroyOnLoad(mainBody);
        }
            
        
    }

}
