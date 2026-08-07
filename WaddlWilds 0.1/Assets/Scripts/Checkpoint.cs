using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public float useDist = 3f;

    private GameObject player;

    private GameObject pressE;

    private bool curPoint = false;

    Animator anim;

    void Start()
    {
        pressE = transform.Find("Canvas").gameObject;
        pressE.SetActive(false);
        player = GameObject.FindWithTag("Player");

        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float dist = 999;
        if(player != null) {
            dist = Mathf.Pow(Mathf.Pow(Mathf.Abs(player.transform.position.x-transform.position.x), 2f) + Mathf.Pow(Mathf.Abs(player.transform.position.y-transform.position.y), 2f), 0.5f);
        }

        if(player.GetComponent<Health>().checkpoint != gameObject) {
            curPoint = false;
        }

        if (dist < useDist) {
            if(curPoint == false) {
                pressE.SetActive(true);
            } else {
                pressE.SetActive(false);
            }
            if (Input.GetKeyDown(KeyCode.E) && curPoint == false) {
                player.GetComponent<Health>().checkpoint = gameObject;
                curPoint = true;
            }
        } else {
            pressE.SetActive(false);
        }

        anim.SetBool("Activated", curPoint);
    }
}
