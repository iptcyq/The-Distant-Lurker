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
        start, stage1, stage2, haha
    };
    public GameState currentGameState;

    //check link
    public TMP_InputField inputLink;
    private string[] links = new string[]
    {
        "bug.com",
        "cctv.com",
        "mobilephone.com",
        "rickroll.com"
    };
    private string link;

    private string[] sentences = new string[]
    {
        "*Musical interlude*",
        "Sentence1",
        "sentence2",
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
        int i = Random.Range(0, 2);

        currentGameState = GameState.haha; //temporary

        if(currentGameState == GameState.start)
        {
            //write starting audio name
            if (i == 0)
            {
                //audio1
                index = 1;
            }
            else
            {
                //audio2
                index = 2;
            }
        }
        else if (currentGameState == GameState.stage1)
        {
            //write next audio name
            if (i == 0)
            {
                //audio1
                index = 3;
            }
            else
            {
                //audio2
                index = 4;
            }
        }
        else if (currentGameState == GameState.stage2)
        {
            //write last audio name
            if (i == 0)
            {
                //audio1
                index = 5;
            }
            else
            {
                //audio2
                index = 6;
            }
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
                    currentGameState = GameState.start;
                }
                else if (i == 1)
                {
                    currentGameState = GameState.stage1;
                }
                else if (i == 2)
                {
                    currentGameState = GameState.stage2;
                }
                else
                {
                    currentGameState = GameState.haha;
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
