using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermovement : MonoBehaviour
{
    public float yvelocity;
    public float xvelocity;

    public float maxSpeed;
    public string state;
    public float acceleration;
    public float jumpSpeed;

    public bool canDash;
    public float dashTime;
    public float dashRemaining;

    public float groundCheckRadius;
    public Transform GroundCheckPoint;
    public LayerMask groundLayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    bool checkGrounded()
    {
        return Physics2D.OverlapCircle(GroundCheckPoint.position, groundCheckRadius, groundLayer);
    }
    // Update is called once per frame
    public void checkState()
    {
        if (Input.GetKeyDown(KeyCode.F) && canDash || dashRemaining > 0)
        {
            state = "dashing";
        }
        else
        {
            state = "walking";
        }
    }
    void Update()
    {

        if (state == "walking") {
            Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
            float xAxis = Input.GetAxis("Horizontal");
            xvelocity += xAxis * acceleration;
            if (xAxis == 0)
            {
                xvelocity = 0;
            }
                yvelocity = rb2d.velocity.y;
            print(Physics2D.OverlapCircle(GroundCheckPoint.position, groundCheckRadius, groundLayer));
            if (Mathf.Abs(xvelocity) > maxSpeed)
            {
                xvelocity = maxSpeed * Mathf.Abs(xvelocity) / xvelocity;
            }
            
            if (checkGrounded() && Input.GetKeyDown(KeyCode.Space))
            {
                yvelocity = jumpSpeed;
            }
            rb2d.velocity = new Vector2 (xvelocity, yvelocity);
        }
    }
}
