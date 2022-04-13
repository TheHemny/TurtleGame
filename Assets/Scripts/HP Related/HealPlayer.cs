using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPlayer : MonoBehaviour
{
    public int hpRestoreVal = 4;
    public GameObject player;


    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if(other.tag == "Player_Hurtbox")
        {
            var healthComponent = player.GetComponent<Health>();
            if (healthComponent != null)
            {
                healthComponent.Heal(hpRestoreVal);
                //Debug.Log("Health restored!");
                this.gameObject.SetActive(false);
            }
            else if (healthComponent == null)
                Debug.Log("healthComponent is null");
        }
    }

}
