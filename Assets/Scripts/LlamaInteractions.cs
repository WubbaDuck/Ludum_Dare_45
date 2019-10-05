using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LlamaInteractions : MonoBehaviour
{
    public Collider2D interactableCollider;

    // Harvesting stuff
    private bool interactTrigger = false;
    private bool harvestingTimerStarted = false;
    private float startTime = 0f;
    private float timer = 0f;
    private float harvestingTime = 2.0f;
    private GameObject thingToHarvest;
    private string successfulHarvestOutputName = "";
    private int successfulHarvestOutputQuantity = 0;
    InventoryManager im;

    // Progress bar stuff
    private Image progressBar;

    void Start()
    {
        im = gameObject.transform.GetComponent<InventoryManager>();
        progressBar = transform.Find("InteractionProgressCanvas").Find("CircuilarProgressBar").GetComponent<Image>();
        ResetHarvesting();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            interactTrigger = true;
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

            Destroy(thingToHarvest);
            harvestingTimerStarted = false;
            ResetHarvesting();
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
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
