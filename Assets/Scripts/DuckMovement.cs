using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public Animator animator;
    Vector2 speed;
    Vector3 previousPos;

    // Start is called before the first frame update
    void Start()
    {
        rb = transform.parent.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Vector3 newVec = previousPos - transform.position;
        // animator.SetFloat("Horizontal", newVec.x);
        // animator.SetFloat("Vertical", newVec.y);
        // animator.SetFloat("Speed", newVec.magnitude);
        // previousPos = transform.position;
    }
}
