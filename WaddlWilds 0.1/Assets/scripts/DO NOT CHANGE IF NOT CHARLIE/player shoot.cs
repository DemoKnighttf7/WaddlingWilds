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
        transform.position = player.transform.position+offsetAmount;
        rotation = findAngle();
        transform.Rotate(0f, 0f, rotation - transform.eulerAngles.z, Space.Self);
    }
}
