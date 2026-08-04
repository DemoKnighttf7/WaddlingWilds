using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToungeLatch : MonoBehaviour
{
    private GameObject frog;
    private GameObject latched;

    private bool canLatch = false;

    //private Vector3 offset;

    void Start()
    {
        frog = transform.parent.gameObject;
    }

    void Update() {
        if (latched != null) {
            latched.transform.position = transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.CompareTag("Player") && canLatch) {
            canLatch = false;
            frog.GetComponent<bullfrogAI>().retract();
            latched = collision.gameObject;
            //offset = latched.transform.position - offset;
        }
    }

    public void release() {
        latched = null;
    }

    public void prep(bool thing) {
        canLatch = thing;
    }
}
