using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class SnapToGrid : MonoBehaviour
{
    public float grid = 0.5f;
    float x = 0f;
    float y = 0f;

    void Update()
    {
        if (grid > 0f)
        {
            float reciprocalGrid = 1f / grid;
            x = Mathf.Round(transform.position.x * reciprocalGrid) / reciprocalGrid;
            y = Mathf.Round(transform.position.y * reciprocalGrid) / reciprocalGrid;

            transform.position = new Vector3(x, y, transform.position.z);
        }
    }
}
