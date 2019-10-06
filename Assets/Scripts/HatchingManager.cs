using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class HatchingManager : MonoBehaviour
{
    public GameObject duckPrefab;
    private GameObject duckParent;
    private float hatchingTime = 0f;
    private int numberOfDucks = 0;
    private float startTime = 0f;

    void Start()
    {
        hatchingTime = Random.Range(15, 30);
        numberOfDucks = Random.Range(1, 10);
        startTime = Time.time;

        GameObject[] objects = Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[];

        for (int i = 0; i < objects.Length; i++)
        {
            if (objects[i].name == "Animals")
            {
                duckParent = objects[i];
                break;
            }
        }
    }

    void Update()
    {
        float elapsedTime = Time.time - startTime;

        if (elapsedTime >= hatchingTime)
        {
            Hatch();
        }
    }

    void Hatch()
    {
        for (int i = 0; i < numberOfDucks; i++)
        {
            GameObject duck = Instantiate(duckPrefab, transform.position, Quaternion.identity);
            duck.transform.parent = duckParent.transform;
        }

        // Udpate A* grid
        Bounds bounds = new Bounds(transform.position, new Vector3(10, 10, 0));
        AstarPath.active.UpdateGraphs(bounds, 0.1f);
        Destroy(gameObject);
    }
}
