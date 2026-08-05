using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class water : MonoBehaviour
{
    public float exitSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per fram
    public void OnTriggerEnter2D(Collider2D collision)
    {
        print("something entered water");
        if (collision.gameObject.CompareTag("Player"))
        {
            print("Player entered");
            collision.gameObject.GetComponent<playermovement>().checkState("swimming");
            collision.gameObject.GetComponent<playermovement>().swimAngleFinder();
        }
        else if (collision.gameObject.CompareTag("PlayerHead"))
        {
            collision.transform.position = new Vector3(0, 0, -1);
            print("Player head entered");
        }
        else
        {
            Destroy(collision.gameObject);
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<playermovement>().checkState("walking");
        collision.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos((transform.eulerAngles.z + 90) * Mathf.Deg2Rad), Mathf.Sin((transform.eulerAngles.z + 90) * Mathf.Deg2Rad))*exitSpeed;
        collision.transform.eulerAngles = new Vector3(0, 0, 0);
        collision.GetComponent<playermovement>().canDash = true;
    }
}
