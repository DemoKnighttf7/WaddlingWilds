using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooting : MonoBehaviour
{
    public float fireRate, fireCoolDown;
    public GameObject projectile;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && fireCoolDown <= 0)
        {
            GameObject fired = Instantiate(projectile, new Vector3(transform.position.x, transform.position.y, 0),transform.rotation);
            fired.transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
            fireCoolDown = fireRate;
        }
        fireCoolDown -= Time.deltaTime;
    }
}
