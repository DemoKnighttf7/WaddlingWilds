using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float speedMod = 0.5f;
    public GameObject player;
    public GameObject cameraZone;
    public Vector3 velocity = new Vector3(0, 0, 0);

    public float maxCamDist = 10f;
    
    void LateUpdate()
    {
        float playerX = 0;
        float playerY = 0;

        if(player != null) {
            playerX = player.transform.position.x;
            playerY = player.transform.position.y;
        }

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float mouseOffsetX = (mousePos.x + playerX)/2;
        float mouseOffsetY = (mousePos.y + playerY)/2;

        mouseOffsetX = Mathf.Min(mouseOffsetX, playerX + maxCamDist);
        mouseOffsetY = Mathf.Min(mouseOffsetY, playerY + maxCamDist);

        mouseOffsetX = Mathf.Max(mouseOffsetX, playerX - maxCamDist);
        mouseOffsetY = Mathf.Max(mouseOffsetY, playerY - maxCamDist);
        
        playerX = mouseOffsetX;
        playerY = mouseOffsetY;

        if (cameraZone != null) {
            BoxCollider2D bcd = cameraZone.GetComponent<BoxCollider2D>();

            float xRange = bcd.size.x / 2f;
            float yRange = bcd.size.y / 2f;

            Camera cam = GetComponent<Camera>();

            float camHeight = cam.orthographicSize;
            float camWidth = camHeight * cam.aspect;
            
            xRange -= camWidth;
            yRange -= camHeight;

            

            float zoneX = cameraZone.transform.position.x + bcd.offset.x;
            float zoneY = cameraZone.transform.position.y + bcd.offset.y;

            float lowXBound = zoneX - xRange;
            float highXBound = zoneX + xRange;

            float lowYBound = zoneY - yRange;
            float highYBound = zoneY + yRange;

            playerX = Mathf.Min(playerX, highXBound);
            playerX = Mathf.Max(playerX, lowXBound);
            playerY = Mathf.Min(playerY, highYBound);
            playerY = Mathf.Max(playerY, lowYBound);
        }

        
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(playerX, playerY, transform.position.z), ref velocity, speedMod);
    }
    public void shake(float heavyness) {
        transform.position = new Vector3(transform.position.x + Random.Range(-heavyness, heavyness), transform.position.y + Random.Range(-heavyness, heavyness), transform.position.z);
    }
}


