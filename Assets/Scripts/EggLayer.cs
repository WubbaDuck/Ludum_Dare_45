using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class EggLayer : MonoBehaviour
{
    public GameObject egg;
    public float eggTime;
    public float startTime;

    private GameObject eggParent;

    void Start()
    {
        GameObject[] objects = Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[];

        for (int i = 0; i < objects.Length; i++)
        {
            if (objects[i].name == "Objects")
            {
                eggParent = objects[i];
                break;
            }
        }

        eggTime = Random.Range(60, 180);
        startTime = Time.time;
    }

    void Update()
    {
        if ((Time.time - startTime) >= eggTime)
        {
            GameObject thisEgg = Instantiate(egg, transform.position + transform.up, Quaternion.identity);
            thisEgg.transform.SetParent(eggParent.transform);
            startTime = Time.time;
            eggTime = Random.Range(60, 180);
            thisEgg.name = "Egg";

            // Udpate A* grid
            Bounds bounds = new Bounds(thisEgg.transform.position, new Vector3(10, 10, 0));
            AstarPath.active.UpdateGraphs(bounds, 0.1f);
        }
    }
}
