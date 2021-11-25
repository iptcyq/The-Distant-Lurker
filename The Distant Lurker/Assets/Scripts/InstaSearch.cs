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


    //list thing for all people and names and stuff
    private string[][] results = new string[][]
    {
         new string[] {"Default1","Default2","Default3"}, //quick test
         new string[] { "squid", "blank","homescreen" }, //JohnDoe
         new string[] {"Albino1","Albino2", "Albino3"}, //ALBINO LANGE
         new string[] {"Simon1", "Simon2", "Simon3"},//SIMON SHARP
         new string[] {"Nerthus1", "Nerthus2"}, //NERTHUS ALBANI
         new string[] {"Viona1","Viona2"}, //VIONA SALLER
         new string[] {"Louvre1", "Louvre2"}, //LOUVRE SALLER
         new string[] {"Ipt1","Ipt2"},
    };

    private string[] input = new string[]
    {
        "",//Set as HomeScreen
        "JOHN DOE",
        "ALBINO LANGE",
        "SIMON SHARP",
        "NERTHUS ALBANI",
        "VIONA SALLER",
        "LOUVRE SALLER",
        "IPTCYQ"
    };



    public void EnterSearch()
    {
        DeleteResults();
        //enter input
        searched = inputField.text;

        if(searched != null) //when is not empty or has nothing
        {
            searched = searched.ToUpper(); //to ensure all inputs are the same
            searched = searched.Trim( );
        }
        else
        {
            searched = "";
        }
        
        //check against the private list to find names of searched images
        for(int i=0; i < input.Length; i++)
        {
            if(searched == input[i])
            {
                for (int j=0; j<(input[i].Length -1); j++)
                {
                    LoadedImage = results[i][j];
                    //load images in
                    GameObject instance = Instantiate(Resources.Load(LoadedImage, typeof(GameObject))) as GameObject;
                    instance.transform.SetParent(gameObject.transform);
                    instance.transform.localScale = new Vector2(1, 1);

                    FindObjectOfType<AudioManager>().Play("click");
                }
                return; 
            }

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
        EnterSearch();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            DeleteResults();
            EnterSearch();
        }
    }
}
