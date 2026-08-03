using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovementScript : MonoBehaviour
{
    Rigidbody2D rb2d;
    float horizontalInput;

    public float moveSpeed = 10f;
    public float jumpSpeed = 12f;

    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        float nextVelocityX = horizontalInput * moveSpeed;
        float nextVelocityY = rb2d.velocity.y;

        if(Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W))
        {
            nextVelocityY = jumpSpeed;
        }

        rb2d.velocity = new Vector2(nextVelocityX, nextVelocityY);
    }
}
