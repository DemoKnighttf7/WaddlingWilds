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
        if (Input.GetMouseButtonDown(0) && fireCoolDown <= 0 && player.GetComponent<playermovement>().state != "swimming" && player.GetComponent<playermovement>().seeds > 0)
        {
            Vector3 mouseScreenPos = Input.mousePosition;
            mouseScreenPos.z = Mathf.Abs(Camera.main.transform.position.z); 

            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        
            mouseWorldPos.z = 0f;
            Vector3 shootDir = (mouseWorldPos - transform.position).normalized;

            Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, shootDir);

            GameObject fired = Instantiate(projectile, new Vector3(transform.position.x, transform.position.y, 0), lookRotation);
            fired.transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z+90);
            fireCoolDown = fireRate;

            player.GetComponent<playermovement>().seeds -= 1;
        }
        fireCoolDown -= Time.deltaTime;
    }
}
