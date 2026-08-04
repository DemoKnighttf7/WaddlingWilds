using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTrigger : MonoBehaviour
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

    private void OnCollisionEnter2D(Collision2D collision) {

        if (collision.gameObject.CompareTag("ground")) {
            rb = platform.GetComponent<Rigidbody2D>();
            rb.constraints = RigidbodyConstraints2D.FreezePositionY;
            Destroy(gameObject);
        }
    }
}
