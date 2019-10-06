using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LlamaMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float smoothTime = 0.3f;
    public Rigidbody2D rb;
    public Animator animator;

    private Vector2 movement;
    private Vector2 newMovement;
    private float rotation;

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        if (movement.magnitude < 0.01)
        {
            Debug.Log("Log");
            // transform.localRotation = Quaternion.AngleAxis(0, transform.up);
        }
    }

    void LateUpdate()
    {
        if (animator.GetCurrentAnimatorClipInfo(0) [0].clip.name == "Idle")
        {
            // transform.rotation = Quaternion.Euler(0, 0, rotation);
        }
        else
        {
            // rotation = transform.rotation.eulerAngles.z;
        }
    }

    void FixedUpdate()
    {
        // Move the llama
        Vector2 clampedMove = Vector2.ClampMagnitude(movement * moveSpeed, moveSpeed);
        Vector2 movePos = rb.position + clampedMove;
        Vector2 smoothedMove = Vector2.SmoothDamp(rb.position, movePos, ref newMovement, smoothTime);
        rb.MovePosition(smoothedMove);
    }
}
