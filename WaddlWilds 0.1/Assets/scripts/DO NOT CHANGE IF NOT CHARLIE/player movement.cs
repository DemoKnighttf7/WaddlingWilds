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
    public Vector3 maxdistance = new Vector3(0, 999999, 0);
    public RaycastHit dashHit;
    public bool noHold;

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
        if (((Input.GetKeyDown(KeyCode.F) && canDash) || state == "dashing") && !noHold)
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
        if (!Input.GetKeyDown(KeyCode.F)) 
        {
            noHold = false;
        }
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
                print("Can dash is true");
                dashTimer = dashLength;
                Vector2 dashDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
                rb2d.velocity = dashDirection * dashSpeed;
                
            }
            else if (grounded)
            {
            state = "walking";
            if (!(Input.GetKeyDown(KeyCode.Space)))
                {
                    rb2d.velocity = Vector2.zero;
                }
                else
                {
                    rb2d.velocity = new Vector2(rb2d.velocity.x ,-(rb2d.velocity.y));
                }
                if (!Input.GetKey(KeyCode.F))
                {
                    noHold = true;
                }
                dashTimer = 0;
            }
            else if (dashTimer <= 0)
            {

                rb2d.velocity = new Vector2(0, 0);
                state = "walking";
                if (Input.GetKeyDown(KeyCode.F))
                {
                    noHold = true;
                }
            }
            dashTimer -= Time.deltaTime;
            canDash = false;
        }
    }
}
