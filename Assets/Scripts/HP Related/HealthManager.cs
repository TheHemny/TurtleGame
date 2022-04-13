using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    //Used for the UI health
    public GameObject heartPrefab;
    public Health playerHealth;
    List<HealthHeart> hearts = new List<HealthHeart>();


    private void Start()
    {
        DrawHearts();
    }//end Start()

    private void LateUpdate()
    {
        DrawHearts();
    }//end LateUpdate();

    public void DrawHearts()
    {
        ClearHearts();
        float maxHealthRemainder = playerHealth.maxHealth % 4;
        int heartsToMake = (int)((playerHealth.maxHealth / 4) + maxHealthRemainder);
        for(int i = 0; i < heartsToMake; i++)
        {
            CreateEmptyHeart();
        }

        for(int i = 0; i < hearts.Count; i++)
        {
            int heartStatusRemainder = (int)Mathf.Clamp(playerHealth.currentHealth - (i * 4), 0, 4);
            hearts[i].SetHeartImage((HeartStatus)heartStatusRemainder);
        }
    }//end DrawHearts()

    public void CreateEmptyHeart()
    {
        GameObject newHeart = Instantiate(heartPrefab);
        newHeart.transform.SetParent(transform);

        HealthHeart heartComponent = newHeart.GetComponent<HealthHeart>();
        heartComponent.SetHeartImage(HeartStatus.Empty);
        hearts.Add(heartComponent);
    }//end CreateEmptyHeart()

    public void ClearHearts()
    {
        foreach(Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        hearts = new List<HealthHeart>();
    }//end ClearHearts()
    
}//end script
