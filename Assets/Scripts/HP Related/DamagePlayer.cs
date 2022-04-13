using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public int damageVal;
    public PlayerManager player;
    public Animator m_animator;
    private float damageTimer = 1f;
    private bool playerInvincible;
    public GameObject playerObject;

    void Start()
    {
        playerObject = GameObject.Find("Player");
        player = playerObject.GetComponent<PlayerManager>();
        m_animator = playerObject.GetComponent<Animator>();
        playerInvincible = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player_Hurtbox" && !player.GetIsBlocking() && !playerInvincible)
        {
            var healthComponent = playerObject.GetComponent<Health>();
            if (healthComponent != null && healthComponent.GetCurrentHealth() != 0)
            {
                Debug.Log("Dealing " + damageVal + " damage");
                healthComponent.TakeDamage(damageVal);
                InvincibleManager();
            }

            
            
        }
        
    }//end OnTriggerEnter2D

    public void DealDamage(int dmg)
    {
        //I love jank so much
        if (!player.GetIsBlocking() && !playerInvincible)
        {
            var healthComponent = playerObject.GetComponent<Health>();
            if (healthComponent != null && healthComponent.GetCurrentHealth() != 0)
            {
                Debug.Log("Dealing " + dmg + " damage");
                healthComponent.TakeDamage(dmg);
                InvincibleManager();
            }



        }
    }
    private void InvincibleManager()
    {
        playerInvincible = true;
        m_animator.SetBool("Invulnerable", playerInvincible);
        StopCoroutine(InvulnerableState());
        StartCoroutine(InvulnerableState());
    }//end InvincibleMangager()

    IEnumerator InvulnerableState()
    {
        yield return new WaitForSeconds(damageTimer);
        playerInvincible = false;
        m_animator.SetBool("Invulnerable", playerInvincible);
    }
}