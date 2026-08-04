using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ratAI : MonoBehaviour
{
    private GameObject player;

    public float moveSpeed;
    public float damage;
    public float attackInterval = 1f;
    public float attackRange = 1f;

    public float atkDist = 5f;
    public float forgetDist = 10f;

    public float idleMoveStr = 2f;
    public float idleMoveInterval = 1f;
    public float idleMoveChance = 0.5f;

    private float lastIdleMove;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private bool attacking = false;

    private float lastAttack;

    private GameObject cam;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        cam = GameObject.FindWithTag("MainCamera");

        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        lastIdleMove = Time.time;
        lastAttack = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Mathf.Pow(Mathf.Pow(Mathf.Abs(player.transform.position.x-transform.position.x), 2f) + Mathf.Pow(Mathf.Abs(player.transform.position.y-transform.position.y), 2f), 0.5f);
        if(dist <= atkDist || attacking == true) { //BEGIN ATTACKING
            float dir = player.transform.position.x - transform.position.x;
            dir /= Mathf.Abs(dir);

            attacking = true;

            rb.velocity = new Vector2(moveSpeed * dir, rb.velocity.y);
        }

        if(dist < attackRange) {
            if(Time.time - lastAttack > attackInterval) { //ATTACK HERE
                player.GetComponent<Health>().Damage(damage);
                cam.GetComponent<CameraFollow>().shake(0.2f);
                lastAttack = Time.time;
            }
        }

        if(dist > forgetDist) { //CANCEL ATTACK AT RANGE
            attacking = false;
        }

        if(Time.time-lastIdleMove > idleMoveInterval && Mathf.Abs(rb.velocity.x) < 0.05) { //IDLE MOVEMENT
            if (idleMoveChance >= Random.value) {
                float dir = 1f;
                if(Random.value > 0.5f) {
                    dir = -1f;
                    
                }
                rb.velocity = new Vector2(idleMoveStr * dir, rb.velocity.y);
                lastIdleMove = Time.time;
            }
            
        }

        if(rb.velocity.x > 0) { //ROTATE TO FACE MOVE DIR
            spriteRenderer.flipX = false;
        } else {
            spriteRenderer.flipX = true;
        }
    }
}
