using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerForNonHealthThings : MonoBehaviour
{
    public Image seedBar;
    private GameObject player;
    private float seeds = 0f;
    public Image seedUI;

    public Image healthBar;
    public Image healthMask;

    public Image transFade;

    public float masks = 0f;

    private float blackFaceLen = 0f;
    private float blackFadeWait = 0f;

    private bool blackFade = false;

    private float fadeEnd = 0f;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float playerSeedCount = 0;
        float playerMaskCount = 0;
        if(player != null) {
            playerSeedCount = player.GetComponent<BasicMovementScript>().seeds;
            playerMaskCount = player.GetComponent<Health>().currentHP;
        }
        if(playerSeedCount > seeds) {
            for(int i = (int)seeds; i < playerSeedCount; i++) {
                seeds++;
                Instantiate(seedUI, seedBar.transform);
            }
        } else if (playerSeedCount < seeds) {
            Destroy(seedBar.transform.GetChild(0).gameObject);
            seeds--;
        }

        if(playerMaskCount > masks) {
            for(int i = (int)masks; i < playerMaskCount; i++) {
                masks++;
                Instantiate(healthMask, healthBar.transform);
            }
        } else if (playerMaskCount < masks) {
            Destroy(healthBar.transform.GetChild(0).gameObject);
            masks--;
        }

        if(blackFade) {
            float alpha = transFade.GetComponent<Image>().color.a;

            alpha += 255f/blackFaceLen * Time.deltaTime;

            alpha = Mathf.Min(alpha, 255f);

            transFade.GetComponent<Image>().color = new Color(0f, 0f, 0f, alpha/255f);
            if(alpha == 255f) {
                blackFade = false;
                fadeEnd = Time.time;
            }
        } else if(Time.time - fadeEnd > blackFadeWait) {
            float alpha = transFade.GetComponent<Image>().color.a;

            alpha -= 255f/blackFaceLen * Time.deltaTime;

            alpha = Mathf.Max(alpha, 0f);
            transFade.GetComponent<Image>().color = new Color(0f, 0f, 0f, alpha/255f);
        }
    }

    public void fade(float fadeDuration, float waitDuration) {
        blackFaceLen = fadeDuration;
        blackFadeWait = waitDuration;
        blackFade = true;
        transFade.GetComponent<Image>().color = new Color(0f, 0f, 0f, 0f);
    }
}
