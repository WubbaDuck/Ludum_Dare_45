using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckDetector : MonoBehaviour
{
    public float duckDetectionRadius = 3f;

    void Start()
    {

    }

    void Update()
    {
        Quaternion rotation;

        // Draw debug rays
        for (int i = -45; i < 45; i += 5)
        {
            rotation = Quaternion.AngleAxis(i, transform.forward);
            Debug.DrawRay(transform.position, rotation * transform.up * duckDetectionRadius, Color.red);
        }

        // Cast the real rays
        for (int i = -45; i < 45; i += 5)
        {
            rotation = Quaternion.AngleAxis(i, transform.forward);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, rotation * transform.up, duckDetectionRadius, LayerMask.GetMask("Ducks"));

            if (hit)
            {
                GameObject hitDuck = hit.collider.gameObject;
                hitDuck.GetComponent<RunAwayDetector>().Hit(rotation);
            }
        }

    }
}
