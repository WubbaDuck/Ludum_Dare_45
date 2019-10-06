using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class GateManager : MonoBehaviour
{
    bool open = false;

    public void ToggleGate()
    {
        if (open)
        {
            // Close
            // Quaternion rotation = Quaternion.AngleAxis(0, transform.parent.transform.forward);
            // transform.rotation = rotation;
            transform.position = transform.position + transform.up * 0.8f;
            open = false;

            // gameObject.GetComponent<Collider2D>().enabled = true;

            // Udpate A* grid
            Bounds bounds = new Bounds(transform.position, new Vector3(10, 10, 0));
            AstarPath.active.UpdateGraphs(bounds, 0.1f);
        }
        else
        {
            // Open
            // Quaternion rotation = Quaternion.AngleAxis(45, transform.parent.transform.forward);
            // transform.rotation = rotation;
            transform.position = transform.position + transform.up * -0.8f;
            open = true;

            // gameObject.GetComponent<Collider2D>().enabled = false;

            // Udpate A* grid
            Bounds bounds = new Bounds(transform.position, new Vector3(10, 10, 0));
            AstarPath.active.UpdateGraphs(bounds, 0.1f);
        }
    }
}
