using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulationUpdater : MonoBehaviour
{
    private Text popCounter;

    // Start is called before the first frame update
    void Start()
    {
        popCounter = GetComponent<Text>();
    }

    void FixedUpdate()
    {
        int duckCount = 0;

        GameObject[] objects = Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[];

        for (int i = 0; i < objects.Length; i++)
        {
            if (objects[i].name == "Duck")
            {
                duckCount++;
            }
        }

        popCounter.text = "Duck Population  " + (duckCount - 1).ToString();
    }
}
