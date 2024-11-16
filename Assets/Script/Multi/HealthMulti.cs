using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;

public class HealthMulti : NetworkBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public bool isDead = false;

    public TMP_Text healthText;

    public void TakeDamage(int damage)
    {
        if (IsServer)
        {
            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                isDead = true;
            }
        }
    }

    void Update()
    {
        healthText.text = "Health: " + currentHealth;
    }
}
