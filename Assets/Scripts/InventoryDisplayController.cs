using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDisplayController : MonoBehaviour
{
    float baseX = 2f;
    float baseY = -9f;
    float yStep = 15;

    // Start is called before the first frame update
    void Start()
    {
        // Disable all inventory items and set them to the inventory origin position
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
            transform.GetChild(i).gameObject.transform.localPosition = new Vector2(baseX, baseY);
        }
    }

    // Update is called once per frame
    void Update()
    {
        int enabledCount = 0;

        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject thisChild = transform.GetChild(i).gameObject;

            if (int.Parse(thisChild.transform.Find("Count").GetComponent<Text>().text) > 0)
            {
                thisChild.SetActive(true);
                thisChild.transform.localPosition = new Vector2(baseX, baseY - enabledCount * yStep + GetComponentInParent<RectTransform>().rect.height / 2);
                enabledCount++;
            }
            else
            {
                transform.GetChild(i).gameObject.transform.localPosition = new Vector2(baseX, baseY);
                thisChild.SetActive(false);
            }
        }
    }
}
