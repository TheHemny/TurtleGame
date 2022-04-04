using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public int damageVal;
    public PlayerManager player;
  

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(player.GetIsBlocking());
        if(other.tag == "Player" && !player.GetIsBlocking())
        {
            var healthComponent = other.GetComponent<Health>();
            if(healthComponent != null)
            {
                healthComponent.TakeDamage(damageVal);
            }
        }
    }//end OnTriggerEnter2D

}
