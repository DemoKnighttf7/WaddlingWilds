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
            GameObject fired = Instantiate(projectile, new Vector3(transform.position.x, transform.position.y, 0),transform.rotation);
            fired.transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
            fireCoolDown = fireRate;
        }
        fireCoolDown -= Time.deltaTime;
    }
}
