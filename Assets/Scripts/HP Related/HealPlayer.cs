using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPlayer : MonoBehaviour
{
    public int hpRestoreVal = 4;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            var healthComponent = other.GetComponent<Health>();
            if (healthComponent != null)
            {
                healthComponent.Heal(hpRestoreVal);
                Debug.Log("Health restored!");
                this.gameObject.SetActive(false);
            }
        }
    }

}
