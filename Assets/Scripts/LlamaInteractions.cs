using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LlamaInteractions : MonoBehaviour
{
    public Collider2D interactableCollider;
    private bool interactTrigger;

    private string[] pickups = {
        "Tree",
        "Stick",
        "Rock",
        "Stone",
        "Grass",
        "Hay",
        "Egg"
    };

    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            interactTrigger = true;
        }
        else
        {
            interactTrigger = false;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (interactTrigger)
        {
            interactTrigger = false;

            if (interactableCollider.IsTouchingLayers(LayerMask.GetMask("Interactables")))
            {
                InventoryManager im = gameObject.transform.GetComponent<InventoryManager>();

                switch (other.name)
                {
                    case "Tree":
                        im.AddItemToInventory("Sticks");
                        im.AddItemToInventory("Sticks");
                        Destroy(other.gameObject);
                        break;
                    case "Stick":
                        im.AddItemToInventory("Sticks");
                        Destroy(other.gameObject);
                        break;
                    case "Rock":
                        im.AddItemToInventory("Stones");
                        im.AddItemToInventory("Stones");
                        Destroy(other.gameObject);
                        break;
                    case "Stone":
                        im.AddItemToInventory("Stones");
                        Destroy(other.gameObject);
                        break;
                    case "Grass":
                        im.AddItemToInventory("Grass");
                        Destroy(other.gameObject);
                        break;
                    case "Hay":
                        im.AddItemToInventory("Grass");
                        Destroy(other.gameObject);
                        break;
                    case "Egg":
                        im.AddItemToInventory("Eggs");
                        Destroy(other.gameObject);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
