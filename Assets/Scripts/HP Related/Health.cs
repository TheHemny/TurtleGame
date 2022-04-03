using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    //Health works in multiples of 4
    //1 heart = 4 HP
    public int maxHealth = 12;
    public int currentHealth;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;    
    }

    public void TakeDamage(int amount)
    {

        currentHealth -= amount;
        Debug.Log("Current health: " + currentHealth);
        if(currentHealth <= 0)
        {
            Debug.Log("Dead!");
        }
    }//end TakeDamage()

    public void Heal(int amount)
    {
        currentHealth += amount;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }//end Heal()

    public void AddMaxHealth()
    {
        maxHealth += 4;
    }
}
