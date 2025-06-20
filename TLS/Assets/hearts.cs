using System;
using UnityEngine;
using UnityEngine.UI;
[Serializable]

public class hearts : MonoBehaviour{

    public int maxNumberHearts;
    public int currentHealth;
    public Image[] healthBar;

    public Sprite fullHeart;
    public Sprite emptyHeart;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth > maxNumberHearts)
            currentHealth = maxNumberHearts;

        if (currentHealth < 0)
            currentHealth = 0;

        for (int i = 0; i < healthBar.Length; i++)
        {
            healthBar[i].sprite = i < currentHealth ? fullHeart : emptyHeart;
            healthBar[i].enabled = (i < maxNumberHearts);
        }

        if (Input.GetButtonDown("Cancel"))
        {
            currentHealth = maxNumberHearts;
        }
    }

    public void LoseHealth(int dmg)
    {
        currentHealth -= dmg;
    }
}
