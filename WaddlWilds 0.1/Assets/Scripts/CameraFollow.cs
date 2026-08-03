using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float speedMod = 0.01f;
    public GameObject player;
    public GameObject cameraZone;
    
    void LateUpdate()
    {
        float newX = transform.position.x;
        float newY = transform.position.y;
        if(player != null) {
            float distX = player.transform.position.x - transform.position.x;
            float distY = player.transform.position.y - transform.position.y;
            newX = transform.position.x + distX*speedMod;
            newY = transform.position.y + distY*speedMod;
        }

        if (cameraZone != null) {
            BoxCollider2D bcd = cameraZone.GetComponent<BoxCollider2D>();

            float xRange = bcd.size.x / 2f;
            float yRange = bcd.size.y / 2f;

            newX = Mathf.Min(newX, cameraZone.transform.position.x + xRange);
            newX = Mathf.Max(newX, cameraZone.transform.position.x - xRange);
            newY = Mathf.Min(newX, cameraZone.transform.position.y + yRange);
            newY = Mathf.Max(newX, cameraZone.transform.position.y - yRange);

            transform.position = new Vector3(newX, newY, transform.position.z);
        }
    }
    public void shake(float heavyness) {
        transform.position = new Vector3(transform.position.x + Random.Range(-heavyness, heavyness), transform.position.y + Random.Range(-heavyness, heavyness), transform.position.z);
    }
}


