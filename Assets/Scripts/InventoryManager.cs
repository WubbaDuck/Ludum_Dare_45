﻿using System.Collections;
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

    void FixedUpdate() { }

    void InitInventoryValues(int initialValue)
    {
        for (int i = 0; i < inventoryPanel.transform.childCount; i++)
        {
            inventoryPanel.transform.GetChild(i).Find("Count").GetComponent<Text>().text = initialValue.ToString();
        }
    }

    void AddItemToInventory(string itemName)
    {
        Text itemCountText = inventoryPanel.transform.Find(itemName).Find("Count").GetComponent<Text>();

        int itemCount = int.Parse(itemCountText.text);

        itemCount++;

        itemCountText.text = itemCount.ToString();
    }

    void RemoveItemFromInventory(string itemName)
    {
        Text itemCountText = inventoryPanel.transform.Find(itemName).Find("Count").GetComponent<Text>();

        int itemCount = int.Parse(itemCountText.text);

        itemCount--;

        itemCountText.text = itemCount.ToString();
    }
}
