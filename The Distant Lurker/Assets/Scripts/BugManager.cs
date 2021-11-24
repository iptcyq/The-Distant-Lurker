using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BugManager : MonoBehaviour
{
    //bug manager
    public TextMeshProUGUI disconnectedText;
    public string audioName = "rickroll";
    public GameObject visualizer;
    private AudioManager audioManager;

    public enum GameState //stage names? stage audio
    {
        haha, start, stage1, stage2, end, 
    };
    public GameState currentGameState;

    private int i = 0;

    //check link
    public TMP_InputField inputLink;
    private string[] links = new string[]
    {
        "rickroll.com",
        "bug.com",
        "cctv.com",
        "mobilephone.com",
        "final.com",
    };
    private string link;

    private string[] sentences = new string[]
    {
        "*Musical interlude*",
        "UNKNOWN: Hey Luciana, what are your email details again? \n LUCIANA: Full names are always usernames, so Luciana Joubert, and my password is 112233. \n UNKOWN: Got it, thanks!",
        "UNKNOWN: Hey boss. \n BOSS: We're in a secret organisation, don't call me by my role! \n UNKNOWN: Okay sorry boss.", 
        "UNKNOWN: Hey Mr Louvre Saller, have you seen Simon? \n LOUVRE SALLER: Yeah, he's probably by the coffee stand.",
        "VIONA SALLER: Dad, what is your bank account password? I want to get a new toy! \n LOUVRE SALLER: Its c0mp1ic4t3d. \n VIONA SALLER: Oh wow ok.",
        "VIONA SALLER: Dad, why do your accounts all have the same password? \n LOUVRE SALLER: It's so that it is easy to remember, you'll understand when you're older.",
        "Muahahahaha you fell for my trap. blah blah blah",
    };
    public TextMeshProUGUI textdisp;
    private int index;
    public float typeSpeed;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        //visualizer.SetActive(false);
        disconnectedText.enabled = true;
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            EnterLink();
        }
    }

    //add a index - [0 for playing audio] [1 for playing video]
    public void PlayVideo()
    {
        
        if(currentGameState == GameState.start)
        {
            index = 1;
        }
        else if (currentGameState == GameState.stage1)
        {
            //write next audio name
            if (i == 0)
            {
                //audio1
                index = 2;
            }
            else
            {
                //audio2
                index = 3;
            }
        }
        else if (currentGameState == GameState.stage2)
        {
            //write last audio name
            if (i == 0)
            {
                //audio1
                index = 4;
            }
            else
            {
                //audio2
                index = 5;
            }
        }
        else if (currentGameState == GameState.end)
        {
            index = 6;
        }
        else if (currentGameState == GameState.haha)
        {
            audioName = "rickroll";
            index = 0;
        }
        disconnectedText.enabled = false;

        FindObjectOfType<AudioManager>().Play("connect");
        visualizer.SetActive(true);

        StartCoroutine(Type());
        i++;
    }

    public void StopVideo()
    {
        FindObjectOfType<AudioManager>().Play("clickfail");
        visualizer.SetActive(false);
        disconnectedText.enabled = true;
    }

    public void EnterLink()
    {
        link = inputLink.text;
        for(int i =0; i < links.Length; i++)
        {
            if (link == links[i])
            {
                if(i == 0)
                {
                    currentGameState = GameState.haha;
                }
                else if (i == 1)
                {
                    currentGameState = GameState.start;
                }
                else if (i == 2)
                {
                    currentGameState = GameState.stage1;
                }
                else if (i == 3)
                {
                    currentGameState = GameState.stage2;
                }
                else
                {
                    currentGameState = GameState.end;
                }

                FindObjectOfType<AudioManager>().Play("click");
                return;
            }
        }

        FindObjectOfType<AudioManager>().Play("clickfail");
    }


    //to make sure it doesnt do funky stuff, check textdisp.text == sentences[index] before calling this
    IEnumerator Type()
    {
        textdisp.text = "";
        foreach(char letter in sentences[index].ToCharArray())
        {
            textdisp.text += letter;
            yield return new WaitForSeconds(typeSpeed);
        }
    }
}
