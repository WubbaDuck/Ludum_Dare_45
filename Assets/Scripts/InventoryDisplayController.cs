using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDisplayController : MonoBehaviour
{
    float baseX = 2f;
    float baseY = -9f;
    float yStep = 15;

    private string selectedItem = "";

    public string GetSelectedItem()
    {
        return selectedItem;
    }

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
        GameObject[] enabledObjects = new GameObject[transform.childCount];
        bool selectedRemoved = false;

        // Update the displayed inventory items
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject thisChild = transform.GetChild(i).gameObject;

            if (int.Parse(thisChild.transform.Find("Count").GetComponent<Text>().text) > 0)
            {
                if (!thisChild.activeInHierarchy)
                {
                    thisChild.SetActive(true);
                    thisChild.GetComponent<Image>().color = new Color(255, 255, 255, 0.75f);
                }

                thisChild.transform.localPosition = new Vector2(baseX, baseY - enabledCount * yStep + GetComponentInParent<RectTransform>().rect.height / 2);
                enabledObjects[enabledCount] = thisChild;
                enabledCount++;
            }
            else
            {
                thisChild.transform.localPosition = new Vector2(baseX, baseY);
                thisChild.SetActive(false);

                if (thisChild.name == selectedItem)
                {
                    selectedRemoved = true;
                }
            }
        }

        if (enabledCount == 0)
        {
            selectedItem = "";
        }
        else if (selectedRemoved)
        {
            selectedItem = enabledObjects[0].name;
            transform.Find(selectedItem).GetComponentInChildren<Image>().color = new Color(240, 200, 0, 0.75f);
        }

        // Switch to the next selected item
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (enabledCount > 1) // If multiple inventory items
            {
                for (int i = 0; i < enabledCount; i++)
                {
                    // Debug.Log(enabledObjects[i].transform.Find("Label").GetComponentInChildren<Text>().text);
                    if (enabledObjects[i].transform.Find("Label").GetComponent<Text>().text == selectedItem || selectedItem == "") // Find the currently selected item
                    {
                        if (i + 1 >= enabledCount) // If this is the last item in the list
                        {
                            // Color the selected item
                            transform.Find(selectedItem).GetComponentInChildren<Image>().color = new Color(255, 255, 255, 0.75f);
                            selectedItem = enabledObjects[0].gameObject.transform.Find("Label").GetComponent<Text>().text;
                            transform.Find(selectedItem).GetComponentInChildren<Image>().color = new Color(240, 200, 0, 0.75f);
                            break;
                        }
                        else
                        {
                            transform.Find(selectedItem).GetComponentInChildren<Image>().color = new Color(255, 255, 255, 0.75f);
                            selectedItem = enabledObjects[i + 1].gameObject.transform.Find("Label").GetComponent<Text>().text;
                            transform.Find(selectedItem).GetComponentInChildren<Image>().color = new Color(240, 200, 0, 0.75f);
                            break;

                        }
                    }
                }
            }
        }

        // If one inventory item
        if (enabledCount == 1)
        {
            transform.Find(selectedItem).GetComponentInChildren<Image>().color = new Color(255, 255, 255, 0.75f);
            selectedItem = enabledObjects[0].transform.Find("Label").GetComponent<Text>().text;
            transform.Find(selectedItem).GetComponentInChildren<Image>().color = new Color(240, 200, 0, 0.75f);
        }
        else if (enabledCount == 0) // If no inventory items
        {
            selectedItem = "";
        }
    }
}
