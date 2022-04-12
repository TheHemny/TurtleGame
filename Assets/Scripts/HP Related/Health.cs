using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    //Health works in multiples of 4
    //1 heart = 4 HP
    public int maxHealth;
    public int currentHealth;
    public Animator m_animator;
    public AudioSource damageSFX;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        m_animator = this.gameObject.GetComponent<Animator>();
        m_animator.SetInteger("Health", currentHealth);
    }

    public void TakeDamage(int amount)
    {
        if(this.gameObject.tag == "Player" && damageSFX != null)
        {
            damageSFX.Play();
            Debug.Log("Ouch!");
        }
        currentHealth -= amount;
        m_animator.SetInteger("Health", currentHealth);
        Debug.Log("Current health: " + currentHealth);
        if(currentHealth <= 0)
        {
            Debug.Log("Dead!");
        }
    }//end TakeDamage()

    public void Heal(int amount)
    {
        currentHealth += amount;
        m_animator.SetInteger("Health", currentHealth);
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }//end Heal()

    public void AddMaxHealth()
    {
        maxHealth += 4;
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}
