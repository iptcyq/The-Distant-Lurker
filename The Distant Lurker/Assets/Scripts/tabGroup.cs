using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tabGroup : MonoBehaviour
{
    public List<tabButton> tabbuttons;
    
    //public for debugging
    public tabButton selectedTab;

    public List<GameObject> pages;

    public void Enter(tabButton button)
    {
        if(tabbuttons == null)
        {
            tabbuttons = new List<tabButton>();
        }

        tabbuttons.Add(button);
    }

    public void OnTabEnter(tabButton button)
    {
        ResetTabs();
    }

    public void OnTabExit(tabButton button)
    {
        ResetTabs();
    }

    public void OnTabSelected(tabButton button)
    {
        FindObjectOfType<AudioManager>().Play("click");
        selectedTab = button;
        ResetTabs();
        
        int index = button.transform.GetSiblingIndex();
        for (int i = 0; i < pages.Count; i++)
        {
            if (i == index) { pages[i].SetActive(true); }
            else { pages[i].SetActive(false); }
        }
    }

    public void ResetTabs()
    {
        foreach(tabButton button in tabbuttons)
        {
            if(selectedTab != null && button == selectedTab)
            {
                continue;
            }
        }
    }

}
