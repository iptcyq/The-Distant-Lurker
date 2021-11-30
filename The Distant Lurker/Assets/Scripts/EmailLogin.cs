using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EmailLogin : MonoBehaviour
{
    public TMP_InputField idInput;
    public TMP_InputField pwInput;

    //login id 
    [HideInInspector]
    private string id;
    private string pw;
    public int indexNo;
    public string currentID = "  ";

    public TextMeshProUGUI Alert;
    public TextMeshProUGUI AccID;
    
    private string[] userID = new string[]
    {
        "  ", //default
        "JOHNDOE",
        "LUCIANAJOUBERT",
        "ALBINOLANGE",
        "LOUVRESALLER",
        "VIONASALLER"
    };
    private string[] loginDetails = new string[]
    {
        "  ",
        "password",
        "112233",
        "iLoveC4ts",
        "c0mp1ic4t3d",
        "UnicornRainbowSparkles"
    };


    //email text
    public TextMeshProUGUI Email1;
    public TextMeshProUGUI Email2;

    private string[] Text1 = new string[]
    {
        "From Admin, \n Welcome to Email, the best place for all your mail.", //tutorial
        "pew pew haha",
        "To Albino Lange, HR Manager, \n Hi, I am Luciana, attached below is my leave application, please have a look through it when you are available. Thanks in advance!",
        "To Simon Sharp, Technical Support, Hi the printer near the lift the printer is broken again.",
        "Connect to final.com to obtain the staff and clients list.", 
        "To Sally, \n Yay my dad got me a ticket to the concert we talked about in class! Want one too? We can go together!",
    };
    private string[] Text2 = new string[]
    {
        "From Admin, \n On the right you can log into your email account with your name and password. While here, you can read the emails you sent and received.", 
        "test?",
        "To Albino Lange," + "\n" + "Hey can you process my application faster, I know you don't check your office mail so I emailed your personal email, don't mind me. :) \n From your really annoyed friend Luciana",
        "To Nerthus Albani, Finance, Luciana Joubert, Admin, \n NO LEAVE FOR BOTH OF YOU FOCUS ON GETTING YOUR JOB DONE BEFORE YOU EVEN THINK ABOUT GETTING TIME OFF TO SLACK",
        "Connect to final.com to obtain the staff and clients list.",
        "To Sally, \n I realised i can use 'mobilephone.com' to connect to my dad's mobile. I wonder what he does every day...",
    };

    //email avatar
    public Image img;
    public Sprite[] avatars; //in order of userID

    //hints stuff
    private bool thirdTime = true;
    public GameObject hint3;

    //start
    void Start()
    {
        thirdTime = true;

        AccID.GetComponent<TextMeshProUGUI>().text = "Account: " + currentID;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            EnterSearch();
        }
    }

    public void EnterSearch()
    {
        //check ID
        id = idInput.text;
        if (id != null)
        {
            id = id.ToUpper();
            id = id.Trim();

            id = id.Replace(" ", "");
        }
        else {
            FailedVerif(0);
            return;
        }

        //check password
        pw = pwInput.text;
        if (pw == null)
        {
            FailedVerif(0);
            return;
        }

        //check against supposed input
        for (int i = 0; i < userID.Length; i++)
        {
            if(id == userID[i])
            {
                if(pw == loginDetails[i])
                {
                    indexNo = i;
                    FindObjectOfType<AudioManager>().Play("click");

                    //passed all 
                    AccID.text = "Account: " + id;
                    currentID = id;

                    //change the emails to contain relative details of the acc owners
                    Email1.text = Text1[i];
                    Email2.text = Text2[i];

                    //change avatar
                    img.sprite = avatars[i];
                    
                    if (i == 2 && thirdTime)
                    {
                        FindObjectOfType<AudioManager>().Play("connect");
                        hint3.SetActive(true);
                        thirdTime = false;
                    }

                    return;

                }
            }

            if (i == (userID.Length - 1))
            {
                //failed
                FailedVerif(1);
            }
        }
    }

    private void FailedVerif(int index)
    {
        FindObjectOfType<AudioManager>().Play("clickfail");
        string ReturnMsg;
        if (index == 0)
        {
            ReturnMsg = "Fill in all blanks!";
        }
        else if (index == 1)
        {
            ReturnMsg = "Incorrect details!";
        }
        else //failsafe idk
        {
            ReturnMsg = "null";
        }

        Alert.text = ReturnMsg;
    }
}
