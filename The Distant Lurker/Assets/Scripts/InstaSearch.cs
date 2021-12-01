using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InstaSearch : MonoBehaviour
{
    //text disp gameobjects
    public TMP_InputField inputField;

    public string searched = "";
    private string LoadedImage;

    //hint stuff
    private bool fourthTime = true;
    private bool fifthTime = true;

    public GameObject hint4;
    public GameObject hint5;

    //list thing for all people and names and stuff
    private string[][] results = new string[][]
    {
         new string[] {"Default1","Default2","Default3"}, //default
         new string[] { "squid", "blank","homescreen" }, //JohnDoe
         new string[] {"Luciana1", "Luciana2"}, //LUCIANA JOUBERT
         new string[] {"Albino1","Albino2", "Albino3"}, //ALBINO LANGE
         new string[] {"Simon1", "Simon2", "Simon3"},//SIMON SHARP
         new string[] {"Nerthus1", "Nerthus2"}, //NERTHUS ALBANI
         new string[] {"Viona1","Viona2"}, //VIONA SALLER
         new string[] {"Louvre1", "Louvre2"}, //LOUVRE SALLER
         new string[] {"Ipt1","Ipt2"},
         new string[] {"Default1","Default2","Default3"}, // help
    };

    private string[] input = new string[]
    {
        "",//Set as HomeScreen
        "JOHNDOE",
        "LUCIANAJOUBERT",
        "ALBINOLANGE",
        "SIMONSHARP",
        "NERTHUSALBANI",
        "VIONASALLER",
        "LOUVRESALLER",
        "IPTCYQ",
        "HELP"
    };



    private void EnterSearch()
    {
        //enter input
        searched = inputField.text;

        if(searched != null) //when is not empty or has nothing
        {
            searched = searched.ToUpper(); //to ensure all inputs are the same
            searched = searched.Trim( );
            searched = searched.Replace(" ", "");
        }
        else
        {
            searched = "";
        }
        
        //check against the private list to find names of searched images
        for(int i=0; i < input.Length ; i++)
        {
            if(searched == input[i])
            {
                for (int j=0; j<(input[i].Length ); j++)
                {
                    LoadedImage = results[i][j];
                    //load images in
                    GameObject instance = Instantiate(Resources.Load(LoadedImage, typeof(GameObject))) as GameObject;
                    instance.transform.SetParent(gameObject.transform);
                    instance.transform.localScale = new Vector2(1, 1);

                    FindObjectOfType<AudioManager>().Play("click");

                    if (i == 2 && fourthTime)
                    {FindObjectOfType<AudioManager>().Play("connect"); hint4.SetActive(true);fourthTime = false; } // if its albino lange
                    if (i == 3 && fifthTime)
                    { FindObjectOfType<AudioManager>().Play("connect"); hint5.SetActive(true); fifthTime = false; } // if its simon sharp
                }
                return; 
            }

        }

        //if what is searched is not on the list
        for (int j = 0; j < (input[0].Length - 1); j++)
        {
            LoadedImage = results[0][j];
            //load images in
            GameObject instance = Instantiate(Resources.Load(LoadedImage, typeof(GameObject))) as GameObject;
            instance.transform.SetParent(gameObject.transform);
            instance.transform.localScale = new Vector2(1, 1);
            
        }
        FindObjectOfType<AudioManager>().Play("clickfail");

    }

    private void DeleteResults()
    {
        int childs = transform.childCount;
        for(int i = 0; i < (childs ); i++)
        {
            GameObject.Destroy(transform.GetChild(i).gameObject);
        }
    }
    private void Start()
    {
        fourthTime = true;
        fifthTime = true;

        EnterSearch();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Search();
        }
    }

    public void Search()
    {
        DeleteResults();
        EnterSearch();
    }
}
