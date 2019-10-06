using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventoryPanel;

    void Start()
    {
        InitInventoryValues(0); // Set the initial value of the inventory items
    }

    void InitInventoryValues(int initialValue)
    {
        for (int i = 0; i < inventoryPanel.transform.childCount; i++)
        {
            inventoryPanel.transform.GetChild(i).Find("Count").GetComponent<Text>().text = initialValue.ToString();
        }
    }

    public void AddItemToInventory(string itemName)
    {
        Text itemCountText = inventoryPanel.transform.Find(itemName).Find("Count").GetComponent<Text>();

        int itemCount = int.Parse(itemCountText.text);

        itemCount++;

        itemCountText.text = itemCount.ToString();
    }

    public void RemoveItemFromInventory(string itemName)
    {
        Text itemCountText = inventoryPanel.transform.Find(itemName).Find("Count").GetComponent<Text>();

        int itemCount = int.Parse(itemCountText.text);

        itemCount--;

        itemCountText.text = itemCount.ToString();
    }

    public int AmountInInventory(string itemName)
    {
        int itemCount = 0;
        if (inventoryPanel == null)
        {
            Debug.Log("NULL IP");
            GameObject[] objects = Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[];

            for (int i = 0; i < objects.Length; i++)
            {
                if (objects[i].name == "Inventory Canvas")
                {
                    inventoryPanel = objects[i].transform.Find("Inventory Panel").gameObject;
                    break;
                }
            }
        }

        Text itemCountText = inventoryPanel.transform.Find(itemName).Find("Count").GetComponent<Text>();
        itemCount = int.Parse(itemCountText.text);

        return itemCount;
    }
}
