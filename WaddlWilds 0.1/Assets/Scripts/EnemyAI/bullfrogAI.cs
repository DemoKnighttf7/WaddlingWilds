using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullfrogAI : MonoBehaviour
{
    public GameObject player;

    public float damage = 2f;
    public float attackInterval = 3f;

    public float atkDist = 10f;

    private Rigidbody2D rb;

    private float lastAttack;

    private GameObject tounge;
    private GameObject toungeLine;

    private LineRenderer toungeLineControl;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        tounge = transform.Find("ToungeAttack").gameObject;
        toungeLine = transform.Find("Tounge").gameObject;

        toungeLineControl = toungeLine.GetComponent<LineRenderer>();

        rb = GetComponent<Rigidbody2D>();
        lastAttack = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Mathf.Pow(Mathf.Pow(Mathf.Abs(player.transform.position.x-transform.position.x), 2f) + Mathf.Pow(Mathf.Abs(player.transform.position.y-transform.position.y), 2f), 0.5f);
        if(dist <= atkDist) { //BEGIN ATTACKING (SHOOT PROJ)
            
        }

        if(dist <= atkDist && player.transform.position.x - transform.position.x > 0) { //ROTATE TO FACE PLAYER
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * 1f, transform.localScale.y, transform.localScale.z);
        } else {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1f, transform.localScale.y, transform.localScale.z);
        }
    }
}
