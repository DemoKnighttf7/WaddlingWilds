using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineSnap : MonoBehaviour
{
    private GameObject platform;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        platform = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.CompareTag("player projectile")) {
            print("Collided!");
            rb = platform.GetComponent<Rigidbody2D>();
            rb.gravityScale = 1f;
            rb.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
            Destroy(gameObject);
        }
    }
}
