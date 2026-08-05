using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooting : MonoBehaviour
{
    public float fireRate, fireCoolDown;
    public GameObject projectile;
    public GameObject player;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && fireCoolDown <= 0 && player.GetComponent<playermovement>().state != "swimming")
        {
            Quaternion offsetRotation = transform.rotation * Quaternion.Euler(0, 0, -90);
            GameObject fired = Instantiate(projectile, new Vector3(transform.position.x, transform.position.y, 0), offsetRotation);
            fired.transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z+90);
            fireCoolDown = fireRate;
        }
        fireCoolDown -= Time.deltaTime;
    }
}
