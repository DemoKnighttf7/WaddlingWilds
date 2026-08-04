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
    public float dashSpeed;
    public float dashTimer;
    public float dashLength;

    public float groundCheckRadius;
    public Transform GroundCheckPoint;
    public LayerMask groundLayer;
    // Start is called before the first frame update
    void Start()
    {
        canDash = true;
    }
    bool checkGrounded()
    {
        
        return Physics2D.OverlapCircle(GroundCheckPoint.position, groundCheckRadius, groundLayer);
    }
    // Update is called once per frame
    public void checkState()
    {
        bool grounded = checkGrounded();
        if ((Input.GetKeyDown(KeyCode.F) && canDash) || state == "dashing")
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
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
        bool grounded = checkGrounded();
        if (grounded && state != "dashing")
        {
            canDash = true;
        }
        checkState();
        if (state == "walking") {
            float xAxis = Input.GetAxis("Horizontal");
            if (grounded || xvelocity*(Mathf.Abs(xAxis)/xAxis) < maxSpeed)
            {
                xvelocity += xAxis * acceleration;
            }
            if (xAxis == 0 && grounded)
            {
                xvelocity = 0;
            }
                yvelocity = rb2d.velocity.y;
            if (Mathf.Abs(xvelocity) > maxSpeed && grounded)
            {
                xvelocity = maxSpeed * Mathf.Abs(xvelocity) / xvelocity;
            }
                
            if (grounded && Input.GetKeyDown(KeyCode.Space))
            {
                yvelocity = jumpSpeed;
            }
            rb2d.velocity = new Vector2 (xvelocity, yvelocity);
        }
        if (state == "dashing")
        {
            print("dashed");
            if (canDash)
            {
                dashTimer = dashLength;
                Vector2 dashDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
                rb2d.velocity = dashDirection * dashSpeed;

            }
            else if (grounded && rb2d.velocity.y < 0)
            {
                print("Fucking up in else if grounded");
            state = "walking";
            if ((Input.GetKey(KeyCode.Space)))
                {
                    Vector2 dashDirection = new Vector2(Input.GetAxis("Horizontal"), -(Input.GetAxis("Vertical"))).normalized;
                    print(dashDirection);
                    rb2d.velocity = new Vector2(dashDirection.x, dashDirection.y/3) * dashSpeed;
                }
                else
                {

                    rb2d.velocity = new Vector2(0, 0);
                }
                dashTimer = 0;
            }
            else if (dashTimer <= 0)
            {

                rb2d.velocity = new Vector2(0, 0);
                state = "walking";
            }
            dashTimer -= Time.deltaTime;
            canDash = false;
        }
    }
}
