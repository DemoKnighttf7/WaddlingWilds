using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SeedMaker : MonoBehaviour
{
    public int seeds;
    public int randomizer;
    public float pickDist = 3f;

    private int realSeeds;


    private GameObject player;
    public GameObject loot;

    private GameObject pressQ;

    private float lastUsed;

    public float resetTime = 10f;

    private TMP_Text pressText;

    void Start()
    {
        pressQ = transform.Find("Canvas").gameObject;
        pressQ.SetActive(false);
        player = GameObject.FindWithTag("Player");

        pressText = pressQ.transform.Find("pressQ/Text").GetComponent<TMP_Text>();

        lastUsed = Time.time;

        realSeeds = seeds + Random.Range(-randomizer, randomizer+1);
    }

    // Update is called once per frame
    void Update()
    {
        float dist = 999;
        if(player != null) {
            dist = Mathf.Pow(Mathf.Pow(Mathf.Abs(player.transform.position.x-transform.position.x), 2f) + Mathf.Pow(Mathf.Abs(player.transform.position.y-transform.position.y), 2f), 0.5f);
        }

        if(Time.time - lastUsed > resetTime) {
            realSeeds = seeds + Random.Range(-randomizer, randomizer+1);
        }

        if (dist < pickDist) {
            pressQ.SetActive(true);
            if(realSeeds > 0) {
                pressText.text = "Q";
            } else {
                pressText.text = "" + (int)(resetTime-(Time.time-lastUsed));
            }
            if (Input.GetKeyDown(KeyCode.Q) && seeds > 0) {
                for(int i = 0; i < realSeeds; i++) {
                    Instantiate(loot, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), transform.rotation);
                }
                lastUsed = resetTime;
                realSeeds = 0;
            }
        } else {
            pressQ.SetActive(false);
        }
    }
}
