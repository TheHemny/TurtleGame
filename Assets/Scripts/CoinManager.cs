using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    //Used for individual coins
    private AudioSource coinSound;
    private CoinCounts coinCounter;

    void Start()
    {
        coinCounter = this.transform.parent.GetComponent<CoinCounts>();
        coinSound = this.transform.parent.GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player_Hurtbox")
        {
            coinSound.Play();
            coinCounter.CollectCoin();
            this.gameObject.SetActive(false);
        }
    }


}
