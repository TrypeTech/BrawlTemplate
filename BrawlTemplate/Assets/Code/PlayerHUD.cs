using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{

    public Slider HealthBar;
    public Text CoinsText;
    PlayerStats stats;
    // Use this for initialization
    void Start()
    {

        // stats = FindObjectOfType<PlayerStats>();
        stats = gameObject.GetComponent<PlayerStats>();

        HealthBar.maxValue = stats.MaxPlayerHealth;
        HealthBar.value = stats.health;


    }

    // Update is called once per frame
    void Update()
    {

        updateCoins();
        updateHealth();
    }


    public void updateHealth()
    {
        HealthBar.value = stats.health;
    }

    public void updateCoins()
    {
        CoinsText.text = "COINS " + stats.CoinItems.ToString();
    }
}
