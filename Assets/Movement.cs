using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;
    float tSpeed;
    public float runSpeed;
    Rigidbody2D rb;
    bool isRunning;
    AnimationPlayback ap;
    public AnimationCurve dashVelFalloff;
    Vector2 dir;

    Vector2 targetVel;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ap = GetComponent<AnimationPlayback>();
        ap.PlayAnim(24, "Idle", "Mur_Idle");
    }
    private void Update()
    {
        if (!GameObject.FindObjectOfType<ConsoleController>().isTyping)
        {
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");
    
            dir = new(x, y);
    
            if (Input.GetKeyDown(KeyCode.LeftShift))
                isRunning = true;
            else if (Input.GetKeyUp(KeyCode.LeftShift))
                isRunning = false;
            tSpeed = isRunning ? runSpeed : speed;
            targetVel = tSpeed * rb.drag / 0.75f * dir.normalized;
            rb.AddForce(targetVel * Time.deltaTime, ForceMode2D.Impulse);
    
            if (Input.GetKeyDown(KeyCode.Space))
                Dash();
        }
    }

    private void Dash()
    {
        if (dir == Vector2.zero)
            rb.AddForce(10 * Vector2.right, ForceMode2D.Impulse);
        else
            rb.AddForce(10 * dir, ForceMode2D.Impulse);
    }
}
