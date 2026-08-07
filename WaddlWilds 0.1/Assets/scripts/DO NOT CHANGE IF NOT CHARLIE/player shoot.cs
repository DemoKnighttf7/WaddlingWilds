using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playershoot : MonoBehaviour
{
    private UnityEngine.Vector3 mousePos;
    private UnityEngine.Vector3 goTo;
    public float rotation;
    public GameObject player;
    public Vector3 offsetAmount;

    //Flipping head horizontally intializing
    private SpriteRenderer spriteRend;
    public float bodyOffsetAmount;
    private float deadZone = 0.5f;

    void Awake() {
        spriteRend = GetComponent<SpriteRenderer>();
    }

    float findAngle()
    {
        float selfX = transform.position.x;
        float selfY = transform.position.y;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        goTo = (mousePos - transform.position - new Vector3(0f, 0f, (mousePos - transform.position).z)).normalized;

        if (mousePos.y > selfY)
        {
            return Mathf.Acos(goTo.x) * (180 / Mathf.PI) - 90;
        }
        else if (mousePos.y < selfY)
        {
            return 270 - (Mathf.Acos(goTo.x) * (180 / Mathf.PI));
            // if (mousePos.x > selfX) {
            //     return -90f;
            // } else {
            //     return 90f;
            // } //270 - (Mathf.Acos(goTo.x) * (180 / Mathf.PI)); -> REMOVED THIS SO PLAYER CANNOT LOOK DOWN (ANIMATION LOOKS WEIRD)
        }
        else if (mousePos.x == selfX && mousePos.y > selfY)
        {
            return 0f;
        }
        else if (mousePos.x == selfX && mousePos.y < selfY)
        {
            return 180f;
        }
        else if (mousePos.x > selfX && mousePos.y == selfY)
        {
            return -90f;
        }
        else if (mousePos.x < selfX && mousePos.y == selfY)
        {
            return 90f;
        }
        else
        {
            return transform.rotation.z;
        }


    }
    void Update()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        if (player.GetComponent<playermovement>().xvelocity > 0) {
            bodyOffsetAmount = 0.3f;
        } else if (player.GetComponent<playermovement>().xvelocity < 0) {
            bodyOffsetAmount = 0f;
        }

        if (player.GetComponent<playermovement>().state != "swimming")
        {
            transform.position = player.transform.position + offsetAmount; 
            renderer.enabled = true;
        }
        else
        {
            renderer.enabled = false;
        }
            rotation = findAngle() - 90f; // Rotation was off when adding duck head sprite
        transform.Rotate(0f, 0f, rotation - transform.eulerAngles.z, Space.Self);
        //print(rotation);

        //FLIPPING PLAYER HEAD HORIZONTALLY
        // if (rotation > 90.01f || rotation < -90.01f) {
        //     spriteRend.flipY = true;
        //     offsetAmount = new Vector3(-0.055f + bodyOffsetAmount, 0.375f, 0f);
        // } else {
        //     spriteRend.flipY = false;
        //     offsetAmount = new Vector3(-0.3f + bodyOffsetAmount, 0.375f, 0f);
        // }

        if (mousePos.x > transform.position.x + deadZone) {
            spriteRend.flipY = true;
            offsetAmount = new Vector3(-0.055f + bodyOffsetAmount, 0.325f, 0f);
        } else if (mousePos.x < transform.position.x - deadZone) {
            spriteRend.flipY = false;
            offsetAmount = new Vector3(-0.3f + bodyOffsetAmount, 0.325f, 0f);
        }
    }
}
