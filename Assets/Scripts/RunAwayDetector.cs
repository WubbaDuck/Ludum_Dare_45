using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class RunAwayDetector : MonoBehaviour
{
    public GameObject mainTarget;
    public GameObject runAwayTarget;
    public float runAwayTargetDistance = 100f;
    public float runAwayTime = 1f;
    public float runAwaySpeed = 5f;
    public float defaultSpeed = 3f;

    private AIDestinationSetter aiDest;
    private AIPath aIPath;
    private float startTime = 0f;
    private float elapsedTime = 0f;
    private float runAwayTimeModifier = 0f;

    void Start()
    {
        aiDest = transform.GetComponent<AIDestinationSetter>();
        aIPath = transform.GetComponent<AIPath>();
        runAwayTimeModifier = Random.Range(0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the timer has run out
        elapsedTime = Time.time - startTime;
        if (elapsedTime > (runAwayTime + runAwayTimeModifier))
        {
            aiDest.target = mainTarget.transform;
            aIPath.maxSpeed = defaultSpeed;
            gameObject.layer = 10; // Switch the duck to the ducks layer
            runAwayTimeModifier = Random.Range(0, 1);
        }
    }

    public void Hit(Quaternion rotation)
    {
        Debug.Log("hit duck");
        Vector3 runAwayTargetPos = mainTarget.transform.position + rotation * mainTarget.transform.up * runAwayTargetDistance;
        runAwayTarget.transform.position = runAwayTargetPos;
        aiDest.target = runAwayTarget.transform;
        aIPath.maxSpeed = runAwaySpeed;

        gameObject.layer = 11; // Switch the duck to the hit ducks layer

        startTime = Time.time; // Restart the timer
    }
}
