using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float speedMod = 0.01;
    public GameObject player;
    public GameObject cameraZone;
    
    void LateUpdate()
    {
        float newX;
        float newY;
        if(player != null) {
            float distX = player.transform.position.x - transform.position.x;
            float distY = player.transform.position.y - transform.position.y;
            newX = transform.position.x + distX*speedMod;
            newY = transform.position.y + distY*speedMod;
        }

        BoxCollider2D bcd = cameraZone.GetComponent<BoxCollider2D>();

        xRange = bcd.size.x / 2f;
        yRange = bcd.size.y / 2f;

        newX = Mathf.Min(newX, cameraZone.transform.position.x + distX);
        newX = Mathf.Max(newX, cameraZone.transform.position.x - distX);
        newY = Mathf.Min(newX, cameraZone.transform.position.y + distY);
        newY = Mathf.Max(newX, cameraZone.transform.position.y - distY);

        transform.position = new Vector3(newX, newY, transform.position.z);
    }
    public void shake(float heavyness) {
        transform.position = new Vector3(transform.position.x + Random.Range(-heavyness, heavyness), transform.position.y + Random.Range(-heavyness, heavyness), transform.position.z);
    }
}


