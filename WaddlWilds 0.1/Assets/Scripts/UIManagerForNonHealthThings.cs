using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerForNonHealthThings : MonoBehaviour
{
    public Image seedBar;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        seedBar.GetComponent<RectTransform>().offsetMax = new Vector2(-(937f - player.GetComponent<BasicMovementScript>().seeds * 22f), seedBar.GetComponent<RectTransform>().offsetMax.y);
    }
}
