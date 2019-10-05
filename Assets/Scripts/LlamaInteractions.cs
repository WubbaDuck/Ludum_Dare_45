using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LlamaInteractions : MonoBehaviour
{
    public Collider2D interactableCollider;

    private bool interactTrigger = false;
    private bool harvestingTimerStarted = false;
    private bool harvestingKeyHeld = false;
    private float startTime = 0f;
    private float timer = 0f;
    private float harvestingTime = 2.0f;
    private GameObject thingToHarvest;
    private string successfulHarvestOutputName = "";
    private int successfulHarvestOutputQuantity = 0;
    InventoryManager im;

    void Start()
    {
        im = gameObject.transform.GetComponent<InventoryManager>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            interactTrigger = true;
            timer += Time.deltaTime;
        }
        else
        {
            interactTrigger = false;

            ResetHarvesting();
        }

        if (harvestingTimerStarted && timer > (startTime + harvestingTime))
        {
            for (int i = 0; i < successfulHarvestOutputQuantity; i++)
            {
                im.AddItemToInventory(successfulHarvestOutputName);
            }

            Destroy(thingToHarvest);
            harvestingTimerStarted = false;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (interactTrigger)
        {
            interactTrigger = false;

            if (interactableCollider.IsTouchingLayers(LayerMask.GetMask("Interactables")))
            {
                switch (other.name)
                {
                    case "Tree":
                        Harvest(other.gameObject, "Sticks", 2, 1.5f);
                        break;
                    case "Stick":
                        Harvest(other.gameObject, "Sticks", 1, 0f);
                        break;
                    case "Rock":
                        Harvest(other.gameObject, "Stones", 2, 3f);
                        break;
                    case "Stone":
                        Harvest(other.gameObject, "Stones", 1, 0f);
                        break;
                    case "Grass":
                        Harvest(other.gameObject, "Grass", 1, 1);
                        break;
                    case "Egg":
                        Harvest(other.gameObject, "Eggs", 1, 0f);
                        break;
                    default:
                        break;
                }
            }
        }
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
    }
}
