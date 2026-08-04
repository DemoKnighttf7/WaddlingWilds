using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedMaker : MonoBehaviour
{
    public int seeds;
    public int randomizer;
    public float pickDist = 3f;

    private GameObject player;
    public GameObject loot;

    private GameObject pressF;

    void Start()
    {
        pressF = transform.Find("Canvas").gameObject;
        pressF.SetActive(false);
        seeds += Random.Range(-randomizer, randomizer+1);
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Mathf.Pow(Mathf.Pow(Mathf.Abs(player.transform.position.x-transform.position.x), 2f) + Mathf.Pow(Mathf.Abs(player.transform.position.y-transform.position.y), 2f), 0.5f);

        if (dist < pickDist) {
            if(seeds > 0) {
                pressF.SetActive(true);
            } else {
                pressF.SetActive(false);
            }
            if (Input.GetKeyDown(KeyCode.F) && seeds > 0) {
                for(int i = 0; i < seeds; i++) {
                    Instantiate(loot, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), transform.rotation);
                }
                seeds = 0;
            }
        } else {
            pressF.SetActive(false);
        }
    }
}
