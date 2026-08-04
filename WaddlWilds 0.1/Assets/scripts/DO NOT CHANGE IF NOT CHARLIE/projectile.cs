using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    public float lifeSpan, speed, damage;
    public string friendly;
    public string target;
    void Start()
    {
        Destroy(gameObject, lifeSpan);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag(friendly))
        {
            if (collision.CompareTag(target))
            {
                
            }
        }
    }

}
