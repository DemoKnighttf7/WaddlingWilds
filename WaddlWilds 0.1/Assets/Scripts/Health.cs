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
    public GameObject onDamage;
    public float baseTrans = 50f;
    public float multiPerPer = 0.1f;
    public float fadeCoeif = 0.05f;

    public bool doHitpts = true;

    // Start is called before the first frame update
    void Start()
    {
        currentHP = MaxHP;
        if(hitPoints == null && doHitpts) {
            hitPoints = transform.Find("Canvas/Hitpts").GetComponent<TMP_Text>();
        }

        string hitString = "";
        for (int i = 0; i < currentHP; i++) {
            hitString += ". ";
        }

        
        if(hitPoints != null) {
            hitPoints.text = hitString;
        }

        if(onDamage != null) {
            onDamage.SetActive(true);
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
        if(onDamage != null) {
            onDamage.GetComponent<Image>().color = new Color(1f, 1f, 1f, onDamage.GetComponent<Image>().color.a - onDamage.GetComponent<Image>().color.a * fadeCoeif * Time.deltaTime);
        }
    }
    public void Damage(float amt) {
        currentHP -= amt;
        print(currentHP);
        if(onDamage != null) {
            float alpha = baseTrans *= (1 + multiPerPer * (MaxHP - currentHP)/MaxHP);
            alpha = Mathf.Min(255, alpha);

            onDamage.GetComponent<Image>().color = new Color(1f, 1f, 1f, alpha/255f);
        }
        if(currentHP <= 0) {
            int lootAmt = Random.Range(lootBottomRange, lootTopRange);

            for(int i = 0; i < lootAmt; i++) {
                if(loot != null) {
                    Instantiate(loot, transform.position, transform.rotation);
                }
            }

            if(hitPoints != null) {
                hitPoints.text = "";
            }
            Destroy(gameObject);
        }
    }
}
