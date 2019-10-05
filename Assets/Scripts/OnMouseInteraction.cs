using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OnMouseInteraction : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Image image;

    [TextArea]
    public string craftingInfo;

    private Text infoBody;

    void Start()
    {
        infoBody = transform.parent.Find("Info Panel").Find("Info Body").GetComponent<Text>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.color = new Color(255, 255, 255, 200);
        infoBody.text = craftingInfo;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = new Color(255, 255, 255, 0);
        infoBody.text = "";
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked");
    }
}
