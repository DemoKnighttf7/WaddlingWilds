using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootDrops : MonoBehaviour
{
    public float spread = 50f;
    private bool picked = false;
    private bool pickable = false;
    public float pickdist = 5f;
    public float pickTime = 3f;
    private Rigidbody2D rb;

    private GameObject player;

    private float startTime;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(Random.Range(-spread, spread), Random.Range(spread*0.5f, spread*2.5f));
        startTime = Time.time;

        player = GameObject.FindWithTag("Player");
        //Physics.IgnoreCollision(GetComponent<Collider>(), player.GetComponent<Collider>());

        transform.rotation = Quaternion.Euler(0f, 0f, Random.Range(-180f, 180f));
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - startTime > pickTime) {
            pickable = true;
        }
        if (player != null) {
            Vector2 dist = new Vector2(player.transform.position.x-transform.position.x, player.transform.position.y-transform.position.y);
            if (Mathf.Pow(Mathf.Pow(Mathf.Abs(dist.x), 2f) + Mathf.Pow(Mathf.Abs(dist.y), 2f), 0.5f) < pickdist && pickable == true && picked == false) {
                picked = true;
                //rb.velocity = -10f * dist.normalized;
            }
            if(picked) {
                rb.velocity = new Vector2(rb.velocity.x + dist.x * 0.4f, rb.velocity.y + dist.y * 0.4f);
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player") && pickable == true) {
            player.GetComponent<playermovement>().seeds += 1;
            Destroy(gameObject);
        }
    }
}
