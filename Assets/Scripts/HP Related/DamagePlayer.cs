using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public int damageVal;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            var healthComponent = other.GetComponent<Health>();
            if(healthComponent != null)
            {
                healthComponent.TakeDamage(damageVal);
            }
        }
    }
}
