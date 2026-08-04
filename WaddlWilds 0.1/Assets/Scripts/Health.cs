using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Health : MonoBehaviour
{
    public float MaxHP;
    public float currentHP;
    public TMP_Text hitPoints;
    public int lootBottomRange;
    public int lootTopRange;

    public GameObject loot;

    // Start is called before the first frame update
    void Start()
    {
        currentHP = MaxHP;

        if(hitPoints == null) {
            hitPoints = transform.Find("Canvas/Hitpts").GetComponent<TMP_Text>();
        }

        string hitString = "";
        for (int i = 0; i < currentHP; i++) {
            hitString += ". ";
        }

        
        if(hitPoints != null) {
            hitPoints.text = hitString;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(hitPoints != null) {
            string hitString = "";
            for (int i = 0; i < currentHP; i++) {
                hitString += ".";
            }

            hitPoints.text = hitString;
        }
    }
    public void Damage(float amt) {
        currentHP -= amt;
        if(currentHP <= 0) {
            int lootAmt = Random.Range(lootBottomRange, lootTopRange);

            for(int i = 0; i < lootAmt; i++) {
                if(loot != null) {
                    Instantiate(loot, transform.position, transform.rotation);
                }
            }

            hitPoints.text = "";
            Destroy(gameObject);
        }
    }
}
