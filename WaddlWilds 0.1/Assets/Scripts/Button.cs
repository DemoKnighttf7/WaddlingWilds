using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    private GameObject door;
    private Rigidbody2D rb;

    Animator anim;

    private float slideDistance = 4.0f;
    private float slideSpeed = 5.0f;

    private bool pressed = false;
    private bool debounce = false;

    private bool sliding = false;
    private Vector3 targetPosition;
    // Start is called before the first frame update

    void Start()
    {
        door = GameObject.FindWithTag("Door");
        targetPosition = door.transform.position + (Vector3.down * slideDistance);
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("Pressed", pressed);

        if (sliding) {
            door.transform.position = Vector3.Lerp(door.transform.position, targetPosition, slideSpeed * Time.deltaTime);

            if (Vector3.Distance(door.transform.position, targetPosition)< 0.001f) {
                door.transform.position = targetPosition;
                sliding = false;
                pressed = false; // TO STOP BUTTON ANIMATION
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        //print("COLLIDED!");
        if (collision.gameObject.CompareTag("Player") && !debounce) {
            pressed = true;
            debounce = true;
            Slide();
        }
    }

    public void Slide() {
        sliding = true;
    }
}
