using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullfrogAI : MonoBehaviour
{
    Animator anim;
    
    private GameObject player;

    public float damage = 1f;
    public float damageInterval = 1f;
    public float attackInterval = 3f;

    public float atkDist = 10f;

    public float shotSpeed = 5f;

    private Rigidbody2D rb;

    private float lastAttack;
    private float lastDamage;

    private GameObject tounge;
    private GameObject toungeLine;

    private LineRenderer toungeLineControl;
    private SpriteRenderer spriteRenderer;

    private bool latchable = true;
    private bool latched = false;
    private bool isAttacking = false;

    private Vector2 shotDir;

    public float backCoeif = -0.1f;

    private GameObject cam;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        tounge = transform.Find("ToungeAttack").gameObject;
        toungeLine = transform.Find("Tounge").gameObject;
        cam = GameObject.FindWithTag("MainCamera");

        toungeLineControl = toungeLine.GetComponent<LineRenderer>();
        toungeLineControl.positionCount = 2;

        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        lastAttack = Time.time;
        lastDamage = Time.time;

        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float dist = 999;
        if(player != null) {
            dist = getDist((Vector2)player.transform.position, (Vector2)transform.position);
        }
        float toungeDist = getDist((Vector2)tounge.transform.position, (Vector2)transform.position);
        if(dist <= atkDist && Time.time - lastAttack > attackInterval && isAttacking == false) { //BEGIN ATTACKING (SHOOT PROJ)
            isAttacking = true;

            tounge.GetComponent<ToungeLatch>().prep(true);

            shotDir = ((Vector2)player.transform.position - (Vector2)transform.position).normalized;
            //print(shotDir);
        }

        if (isAttacking) {
            print(toungeDist);
            tounge.transform.position = new Vector3(tounge.transform.position.x + shotDir.x * shotSpeed * Time.deltaTime, tounge.transform.position.y + shotDir.y * shotSpeed * Time.deltaTime, tounge.transform.position.z);
            if(toungeDist >= atkDist && latchable == true) {
                shotDir *= -1;
                latchable = false;
                tounge.GetComponent<ToungeLatch>().prep(false);
            }
        }

        if (latchable == false && toungeDist < 2.5) { //RELEASE
            tounge.GetComponent<ToungeLatch>().release();
            latched = false;
        }

        if (latchable == false && toungeDist < 1) {
            shotDir = new Vector2(0, 0);
            latchable = true;
            isAttacking = false;
            lastAttack = Time.time;
        }

        if(latched) {
            if(Time.time - lastDamage > damageInterval) {
                player.GetComponent<Health>().Damage(damage);
                lastDamage = Time.time;
            }
        }

        if (isAttacking == false) {
            tounge.transform.position = transform.position;
        }

        if(isAttacking == false) {
            if(dist <= atkDist && player.transform.position.x - transform.position.x > 0) { //ROTATE TO FACE PLAYER
                spriteRenderer.flipX = true; //CHANGE WHEN GET ACTUAL SPRITE
            } else {
                spriteRenderer.flipX = false;
            }
        }

        Vector3 toungeLoac = tounge.transform.position-transform.position;
        toungeLineControl.SetPosition(1, toungeLoac);

        anim.SetBool("isAttacking", isAttacking);
        //check
    }

    float getDist(Vector2 pos1, Vector2 pos2) {
        float dist = Mathf.Pow(Mathf.Pow(Mathf.Abs(pos1.x-pos2.x), 2f) + Mathf.Pow(Mathf.Abs(pos1.y-pos2.y), 2f), 0.5f);
        return dist;
    }

    public void retract() {
        shotDir *= backCoeif;
        latchable = false;
        latched = true;
        cam.GetComponent<CameraFollow>().shake(0.2f);
    }

    public void safeRetract() {
        shotDir *= -1;
        latchable = false;
        tounge.GetComponent<ToungeLatch>().prep(false);
        tounge.GetComponent<ToungeLatch>().release();
    }
}
