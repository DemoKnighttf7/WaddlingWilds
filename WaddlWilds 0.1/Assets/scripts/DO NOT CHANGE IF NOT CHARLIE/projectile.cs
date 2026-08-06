using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class projectile : MonoBehaviour
{
    public float lifeSpan, speed, damage;
    public List<string> friendly;
    public List<string> target;

    private float timer = 0.1f;
    private bool waiting = false;

    void Start()
    {
        Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifeSpan);
        rb2d.velocity = new Vector2(Mathf.Cos((gameObject.transform.eulerAngles.z+90)*Mathf.Deg2Rad), Mathf.Sin((gameObject.transform.eulerAngles.z + 90)*Mathf.Deg2Rad))*speed;
    }

    void Update() {
        if (waiting) {
            if (timer > 0) {
                timer -= Time.deltaTime;
                if (timer <= 0) {
                    Destroy(gameObject);
                }
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!friendly.Contains(collision.tag))
        {
            if (target.Contains(collision.tag))
            {
                collision.GetComponent<Health>().Damage(damage);
            }
            waiting = true;
        }
    }

}
