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

    public float swimTurnSpeed;
    public float swimSpeed;
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
    // has to be called overridevar because override causes errors
    public void checkState(string overrideVar = "none")
    {
        bool grounded = checkGrounded();
        if ((overrideVar == "swimming" || state == "swimming") && overrideVar != "walking")
        {
            state = "swimming";
        }
        else if ((Input.GetKeyDown(KeyCode.F) && canDash) || state == "dashing")
        {
            state = "dashing";
        }
        else
        {
            state = "walking";
        }
    }

    public float swimmingDirection()
    {
        float swimDir = 0;
        if (Input.GetAxis("Horizontal") != 0)
        {
            if (transform.eulerAngles.z > -90 && transform.eulerAngles.z < 90)
            {
                swimDir = (Mathf.Abs(Input.GetAxis("Horizontal")) / Input.GetAxis("Horizontal"));
            }
            else if (transform.eulerAngles.z != 90 && transform.eulerAngles.z != -90)
            {
                swimDir = -(Mathf.Abs(Input.GetAxis("Horizontal")) / Input.GetAxis("Horizontal"));
            }
        }
        if (Input.GetAxis("Vertical") != 0)
        {
            if (transform.eulerAngles.z < 0 && transform.eulerAngles.z > 180)
            {
                swimDir += Mathf.Abs(Input.GetAxis("Vertical")) / Input.GetAxis("Vertical");
            }
            else if (transform.eulerAngles.z != 0 && transform.eulerAngles.z != 180)
            {
                swimDir -= Mathf.Abs(Input.GetAxis("Vertical")) / Input.GetAxis("Vertical");
            }
        }
        if (swimDir != 0)
        {
            swimDir = Mathf.Abs(swimDir) / swimDir;
        }

        return swimDir;
    }

    public void swimAngleFinder()
    {
        float returnAngle = 0;
        Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
        if (rb2d.velocity.y >= 0)
        {
            returnAngle = (Mathf.Acos(rb2d.velocity.normalized.x)*Mathf.Rad2Deg)-90;
        }
        else
        {
            returnAngle = -(Mathf.Acos(rb2d.velocity.normalized.x))*Mathf.Rad2Deg-90;
        }
        transform.eulerAngles = new Vector3(0, 0, returnAngle);
        print(returnAngle);
    }

    void LateUpdate()
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
        else if (state == "dashing")
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
        else if (state == "swimming")
        {
            rb2d.velocity = new Vector2 (0, 0); 
            transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z+swimmingDirection()*swimTurnSpeed*Time.deltaTime);
            transform.position = transform.position + new Vector3(Mathf.Cos((transform.eulerAngles.z + 90) * Mathf.Deg2Rad), Mathf.Sin((transform.eulerAngles.z + 90) * Mathf.Deg2Rad), 0)*Time.deltaTime * swimSpeed;
        }
        if (state != "swimming")
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        else
        {
            rb2d.constraints = RigidbodyConstraints2D.None;
        }
    }
}
