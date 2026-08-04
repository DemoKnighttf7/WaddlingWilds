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
            Instantiate(projectile);
            fireCoolDown = fireRate;
        }
        fireCoolDown -= Time.deltaTime;
    }
}
