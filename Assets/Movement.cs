using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;
    float tSpeed;
    public float runSpeed;
    Rigidbody2D rb;
    bool isRunning;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector2 dir = new(x, y);

        if (Input.GetKeyDown(KeyCode.LeftShift))
            isRunning = true;
        else if (Input.GetKeyUp(KeyCode.LeftShift))
            isRunning = false;
        tSpeed = isRunning ? runSpeed : speed;

        rb.velocity = tSpeed * dir.normalized;
    }
}
