using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LlamaMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float smoothTime = 0.3f;
    public Rigidbody2D rb;
    private Vector2 movement;
    private Vector2 newMovement;

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        Vector2 clampedMove = Vector2.ClampMagnitude(movement * moveSpeed, moveSpeed);
        Vector2 movePos = rb.position + clampedMove;
        Vector2 smoothedMove = Vector2.SmoothDamp(rb.position, movePos, ref newMovement, smoothTime);
        rb.MovePosition(smoothedMove);
    }
}
