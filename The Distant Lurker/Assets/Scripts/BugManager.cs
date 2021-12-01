using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BugManager : MonoBehaviour
{
    //bug manager
    public TextMeshProUGUI disconnectedText;
    public string audioName = "interlude";
    public GameObject visualizer;
    private AudioManager audioManager;

    public Button connectBut;
    public GameObject endTab;
    public tabButton endtab;

    private tabGroup tg;

    public enum GameState //stage names? stage audio
    {
        haha, start, stage1, stage2, end, interlude,
    };
    public GameState currentGameState;

    //debugging
    private int i = 0;
    private bool typing = false;

    //hints stuff
    private bool firstTime = true;
    private bool secondTime = true;

    public GameObject hint1;
    public GameObject hint2;

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
        "*Musical interlude* \n Please enter a link in the field above to connect to your bug. ",
        "UNKNOWN: Hey Luciana, what are your email details again? \n LUCIANA: Full names are always usernames, so Luciana Joubert, and my password is 112233. \n UNKNOWN: Got it, thanks!",
        "UNKNOWN: Hey boss. \n BOSS: We're in a secret organisation, don't call me by my role! \n UNKNOWN: Okay sorry boss. \n \n (Remember that you can click connect again to listen into different bugged conversations!)", 
        "UNKNOWN: Hey boss, I mean, Mr Louvre Saller, have you seen Simon? \n LOUVRE SALLER: Yeah, he's probably by the coffee stand. \n UNKNOWN: Thanks!",
        "VIONA SALLER: Dad, what is your bank account password? I want to get a new toy! \n LOUVRE SALLER: Its  c0mp1ic4t3d \n I should probably type it out for you. \n VIONA SALLER: Oh wow ok. \n \n (Remember that you can click connect again to listen into different bugged conversations!)",
        "VIONA SALLER: Dad, why do your accounts all have the same password? \n LOUVRE SALLER: It's so that it is easy to remember, you'll understand when you grow older. \n \n (Remember that you can click connect again to listen into different bugged conversations!)",
        "LOUVRE SALLER: Muahahaha we finally got you in our trap. \n You thought that this was just another regular old job, working for a powerful but illegal organisation to dig up government secrets for a hefty sum of money. \n But no, we are now one step ahead, distant lurker, and you are finally going to face justice. \n Didn’t you ever question the robotic voices that we planted? Or why was this job such a piece of cake? You now have plenty of time to do that, behind bars. \n So long!",
        "Link not found. \n Please enter a link in the field above to connect to your bug. \n (Remember that you can click connect again to listen into different bugged conversations!)",
    };
    public TextMeshProUGUI textdisp;
    private int index;
    public float typeSpeed;

    // Start is called before the first frame update
    void Start()
    {
        firstTime = true;
        secondTime = true;

        endTab.SetActive(false);
        tg = FindObjectOfType<tabGroup>();
        audioManager = FindObjectOfType<AudioManager>();

        disconnectedText.enabled = true;
        currentGameState = GameState.interlude;
    }


    private void OnDisable()
    {
        if (!FindObjectOfType<AudioManager>().audioPlay("theme") || (typing))
        {
            typing = false;
            FindObjectOfType<AudioManager>().Stop(audioName);
            StopVideo();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            EnterLink();
        }

        if (typing)
        {
            connectBut.enabled = false;
        }
        else
        {
            connectBut.enabled = true;
        }
    }

    //add a index - [0 for playing audio] [1 for playing video]
    public void PlayVideo()
    {
        if (!typing)
        {
            if (currentGameState == GameState.start)
            {
                audioName = "1start";
                index = 1;
            }
            else if (currentGameState == GameState.stage1)
            {
                //write next audio name
                if (i == 0)
                {
                    audioName = "2stage1";
                    index = 2;
                }
                else
                {
                    audioName = "3stage1";
                    index = 3;
                }
            }
            else if (currentGameState == GameState.stage2)
            {
                //write last audio name
                if (i == 0)
                {
                    audioName = "4stage2";
                    index = 4;
                }
                else
                {
                    audioName = "5stage2";
                    index = 5;
                }
            }
            else if (currentGameState == GameState.end)
            {
                audioName = "6end";
                index = 6;
            }
            else if (currentGameState == GameState.haha)
            {
                audioName = "rickroll";
                index = 0;
            }
            else
            {
                audioName = "interlude";
            }
            disconnectedText.enabled = false;

            FindObjectOfType<AudioManager>().Play("connect");
            visualizer.SetActive(true);

            float l = FindObjectOfType<AudioManager>().audioLength(audioName);
            StartCoroutine(Reactivate(l));

            StartCoroutine(Type());
            if (i == 0) { i++; }
            else { i--; }
        }
        
    }

    public void StopVideo()
    {
        FindObjectOfType<AudioManager>().Play("clickfail");
        visualizer.SetActive(false);
        disconnectedText.enabled = true;

        if (!FindObjectOfType<AudioManager>().audioPlay("theme"))
        {
            FindObjectOfType<AudioManager>().Play("theme");
            typing = false;
        }
    }

    public void EnterLink()
    {
        
        link = inputLink.text;
        for(int i =0; i < links.Length; i++)
        {
            if (link == links[i])
            {
                if(i == 4)
                {
                    currentGameState = GameState.end;
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
                else if (i == 0)
                {
                    currentGameState = GameState.haha;
                }
                else
                {
                    currentGameState = GameState.interlude;
                }

                FindObjectOfType<AudioManager>().Play("click");
                PlayVideo();

                return;
            }
        }

        index = 7;
        StartCoroutine(Type());

        FindObjectOfType<AudioManager>().Play("clickfail");
    }


    //to make sure it doesnt do funky stuff, check textdisp.text == sentences[index] before calling this
    IEnumerator Type()
    {
        typing = true;

        textdisp.text = "";
        foreach(char letter in sentences[index].ToCharArray())
        {
            textdisp.text += letter;
            yield return new WaitForSeconds(typeSpeed);
            if (!typing) { typing = true; }
        }
        if (FindObjectOfType<AudioManager>().audioPlay("theme"))
        {
            typing = false;
        }


        if (currentGameState == GameState.end)
        {
            if (FindObjectOfType<AudioManager>().audioPlay("theme"))
            {
                endTab.SetActive(true);
                FindObjectOfType<AudioManager>().Play("end");
                tg.OnTabSelected(endtab);
            }
        }
        else if (currentGameState == GameState.interlude && firstTime)
        {  FindObjectOfType<AudioManager>().Play("connect"); hint1.SetActive(true); firstTime = false; }
        else if (currentGameState == GameState.start && secondTime)
        {  FindObjectOfType<AudioManager>().Play("connect"); hint2.SetActive(true); secondTime = false; }
    }

    IEnumerator Reactivate(float time)
    {
        FindObjectOfType<AudioManager>().Stop("theme");
        yield return new WaitForSeconds(time);
        if (!FindObjectOfType<AudioManager>().audioPlay("theme"))
        {
            if (currentGameState == GameState.end) //end everything
            {
                endTab.SetActive(true);
                
                FindObjectOfType<AudioManager>().Play("end");
                tg.OnTabSelected(endtab);
            }
            StopVideo();
        }
        
    }
    
}
