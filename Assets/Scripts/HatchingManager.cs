using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatchingManager : MonoBehaviour
{
    public GameObject duckPrefab;
    public GameObject duckParent;
    private float hatchingTime = 0f;
    private int numberOfDucks = 0;
    private float startTime = 0f;

    void Start()
    {
        hatchingTime = Random.Range(15, 30);
        numberOfDucks = Random.Range(1, 10);
        startTime = Time.time;
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

        Destroy(gameObject);
    }
}
