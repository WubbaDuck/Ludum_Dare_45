using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using UnityEngine.UI;

public class LlamaInteractions : MonoBehaviour
{
    public Collider2D interactableCollider;
    public GameObject inventoryPanel;

    // Harvesting stuff
    private bool interactTrigger = false;
    private bool harvestingTimerStarted = false;
    private float startTime = 0f;
    private float timer = 0f;
    private float harvestingTime = 2.0f;
    private GameObject thingToHarvest;
    private string successfulHarvestOutputName = "";
    private int successfulHarvestOutputQuantity = 0;
    private InventoryManager im;
    private InventoryDisplayController ids;

    // Progress bar stuff
    private Image progressBar;

    // Crafting Stuff
    public GameObject craftingMenuCanvas;

    // Place-able Items
    public GameObject[] placeablePrefabs;
    public GameObject objectsParent;

    void Start()
    {
        im = gameObject.transform.GetComponent<InventoryManager>();
        ids = inventoryPanel.transform.GetComponent<InventoryDisplayController>();
        progressBar = transform.Find("InteractionProgressCanvas").Find("CircuilarProgressBar").GetComponent<Image>();
        ResetHarvesting();

        // Disable crafting menu
        craftingMenuCanvas.SetActive(false);
    }

    void Update()
    {
        // Show/Hide the crafting window
        if (Input.GetKeyDown(KeyCode.C))
        {
            craftingMenuCanvas.SetActive(!craftingMenuCanvas.activeInHierarchy);
            Transform menu = craftingMenuCanvas.transform.Find("CraftingMenu");

            for (int i = 0; i < menu.childCount; i++)
            {
                if (menu.GetChild(i).name.Split('_') [0] == "CraftingItem")
                {
                    menu.GetChild(i).GetComponent<Image>().color = new Color(255, 255, 255, 0.1f);
                }
            }
        }

        // Interact with things
        if (Input.GetKeyDown(KeyCode.E) && !craftingMenuCanvas.activeInHierarchy)
        {
            if (interactableCollider.IsTouchingLayers(LayerMask.GetMask("Interactables")))
            {
                interactTrigger = true;
            }
            else
            {
                // Place the currently selected item
                foreach (GameObject obj in placeablePrefabs)
                {
                    if (obj.name == ids.GetSelectedItem())
                    {
                        if (im.AmountInInventory(ids.GetSelectedItem()) > 0)
                        {
                            float spawnDistance = 1f;
                            Vector3 spawnPos = transform.position + transform.up * spawnDistance;

                            GameObject newObj = Instantiate(obj, spawnPos, Quaternion.identity);
                            newObj.transform.parent = objectsParent.transform;
                            newObj.name = obj.name;
                            im.RemoveItemFromInventory(obj.name);

                            // Udpate A* grid
                            Bounds bounds = new Bounds(newObj.transform.position, new Vector3(10, 10, 0));
                            AstarPath.active.UpdateGraphs(bounds, 0.1f);

                            break;
                        }
                    }
                }
            }
        }

        if (Input.GetKey(KeyCode.E))
        {
            timer += Time.deltaTime;

            if (harvestingTime != 0f)
            {
                progressBar.fillAmount = (timer - startTime) / harvestingTime;
            }
        }

        if (harvestingTimerStarted && timer > (startTime + harvestingTime))
        {
            for (int i = 0; i < successfulHarvestOutputQuantity; i++)
            {
                im.AddItemToInventory(successfulHarvestOutputName);
            }

            // Udpate A* grid
            Bounds bounds = new Bounds(thingToHarvest.transform.position, new Vector3(10, 10, 0));
            AstarPath.active.UpdateGraphs(bounds, 0.1f);

            Destroy(thingToHarvest);
            harvestingTimerStarted = false;
            ResetHarvesting();
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            // Toggle Gate
            if ((Time.time - timer) <= 0.1 && thingToHarvest.gameObject.tag == "Gate")
            {
                thingToHarvest.gameObject.GetComponent<GateManager>().ToggleGate();
            }

            interactTrigger = false;
            ResetHarvesting();
        }
    }

    void FixedUpdate()
    {
        progressBar.transform.position = transform.position;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (interactTrigger)
        {
            interactTrigger = false;

            if (interactableCollider.IsTouchingLayers(LayerMask.GetMask("Interactables")))
            {
                switch (other.name.Split(char.Parse(" ")) [0])
                {
                    case "Tree":
                        Harvest(other.gameObject, "Stick", 2, 1.5f);
                        break;
                    case "Stick":
                        Harvest(other.gameObject, "Stick", 1, 0.5f);
                        break;
                    case "Rock":
                        Harvest(other.gameObject, "Stone", 2, 3f);
                        break;
                    case "Stone":
                        Harvest(other.gameObject, "Stone", 1, 0.5f);
                        break;
                    case "Grass":
                        Harvest(other.gameObject, "Grass", 1, 1);
                        break;
                    case "Egg":
                        Harvest(other.gameObject, "Egg", 1, 0.5f);
                        break;
                    case "Nest":
                        Harvest(other.gameObject, "Nest", 1, 1f);
                        break;
                    case "Fence":
                        Harvest(other.gameObject, "Fence", 1, 1.5f);
                        break;
                    case "Gate":
                        Harvest(other.gameObject, "Gate", 1, 2f);
                        break;
                    case "Hay":
                        Harvest(other.gameObject, "Hay", 1, 1f);
                        break;
                    case "Pen":
                        Harvest(other.gameObject, "Pen", 1, 1f);
                        break;
                    default:
                        break;
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        ResetHarvesting();
    }

    void Harvest(GameObject obj, string targetName, int targetQuantity, float timeToHarvest)
    {
        if (!harvestingTimerStarted)
        {
            harvestingTimerStarted = true;
            startTime = Time.time;
            timer = startTime;

            thingToHarvest = obj;
            successfulHarvestOutputName = targetName;
            successfulHarvestOutputQuantity = targetQuantity;
            harvestingTime = timeToHarvest;
        }
    }

    void ResetHarvesting()
    {
        harvestingTimerStarted = false;
        startTime = 0f;
        timer = 0f;
        harvestingTime = 0f;
        successfulHarvestOutputName = "";
        successfulHarvestOutputQuantity = 0;
        progressBar.fillAmount = 0f;
    }
}
