using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCounts : MonoBehaviour
{
    //Used for coin parent
    private Transform[] children;
    public PlayerManager player;
    public int totalCoins;
    public int coinCount;

    void Start()
    {
        coinCount = 0;
        children = gameObject.GetComponentsInChildren<Transform>();
        totalCoins = children.Length;
    }//end Start()

    void LateUpdate()
    {
        //Used when player collects all coins
        if(HasCollectedAllCoins())
        {
            player.GoldenFreddy();
        }
    }//end LateUpdate()

    public void CollectCoin()
    {
        coinCount += 1;
        Debug.Log(GetCollectedCoins() + "/" + GetTotalCoins() + " collected");
    }//end CollectCoin()

    public int GetCollectedCoins()
    {
        return coinCount;
    }//end GetCollectedCoins()

    public int GetTotalCoins()
    {
        return totalCoins;
    }//end GetTotalCoins()

    public bool HasCollectedAllCoins()
    {
        if (coinCount == totalCoins)
        {
            return true;
        }
        else
        {
            return false;
        }
    }//end HasCollectedAllCoins()

}//end script
