using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class tabButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public tabGroup tabgroup;

    [HideInInspector]
    public Image background;

    public void OnPointerClick(PointerEventData eventData)
    {
        tabgroup.OnTabSelected(this);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        tabgroup.OnTabEnter(this);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        tabgroup.OnTabExit(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        background = GetComponent<Image>();
        tabgroup.Enter(this);
    }
    
}
