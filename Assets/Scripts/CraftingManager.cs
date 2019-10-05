using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CraftingManager : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Image image;

    [TextArea]
    public string craftingInfo;

    private Text infoBody;
    private string itemName;
    private Dictionary<string, int> craftingRequirements;
    private string[] craftingInfoLines;
    private bool haveAllCompmonents = false;

    private InventoryManager inventoryManager;
    private Color originalColor;

    void Start()
    {
        infoBody = transform.parent.Find("Info Panel").Find("Info Body").GetComponent<Text>();
        itemName = gameObject.name.Split('_') [1];

        craftingInfoLines = craftingInfo.Split('\n');

        // Find the inventory panel
        GameObject[] objects = Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[];

        for (int i = 0; i < objects.Length; i++)
        {
            if (objects[i].name == "Llama")
            {
                inventoryManager = objects[i].GetComponent<InventoryManager>();
                Debug.Log(inventoryManager);
                break;
            }
        }

        originalColor = transform.Find("Item Thumbnail").GetComponent<SpriteRenderer>().color;
    }

    void Update()
    {
        // Check to see if we have all needed components
        for (int i = 0; i < craftingInfoLines.Length; i++)
        {
            string componentName = craftingInfoLines[i].Split(' ') [2];
            int componentAmountNeeded = int.Parse(craftingInfoLines[i].Split(' ') [0]);
            int amountInInventory = inventoryManager.AmountInInventory(componentName);

            if (amountInInventory >= componentAmountNeeded)
            {
                haveAllCompmonents = true;
            }
            else
            {
                haveAllCompmonents = false;
                break;
            }
        }

        if (haveAllCompmonents)
        {
            transform.Find("Item Thumbnail").GetComponent<SpriteRenderer>().color = originalColor;
        }
        else
        {
            transform.Find("Item Thumbnail").GetComponent<SpriteRenderer>().color = Color.grey;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.color = new Color(255, 255, 255, 0.9f);
        infoBody.text = craftingInfo;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = new Color(255, 255, 255, 0.1f);
        infoBody.text = "";
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (haveAllCompmonents)
        {
            for (int i = 0; i < craftingInfoLines.Length; i++)
            {
                string componentName = craftingInfoLines[i].Split(' ') [2];
                int componentAmountNeeded = int.Parse(craftingInfoLines[i].Split(' ') [0]);

                for (int j = 0; j < componentAmountNeeded; j++)
                {
                    inventoryManager.RemoveItemFromInventory(componentName);
                }
            }

            inventoryManager.AddItemToInventory(itemName);
        }
    }
}
