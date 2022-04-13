using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonProjectile : MonoBehaviour
{
    public float projectileSpeed;
    private string projectileDirection;
    public GameObject player;
    private Vector3 translate;

    DamagePlayer damagePlayer;

    void Awake()
    {
        this.gameObject.SetActive(true);
        
        damagePlayer = gameObject.GetComponent<DamagePlayer>();
    }
    void Start()
    {
        player = GameObject.Find("Player");
        projectileSpeed = projectileSpeed / 100f; //Doing this b/c it's way too fast otherwise
        projectileDirection = this.gameObject.tag;
        //Debug.Log(projectileDirection);
        

    }//end start()
    
    void FixedUpdate()
    {
        //Debug.Log("Moving projectile");

        if(projectileDirection == "Pitchfork_Up")
        {
            transform.position += new Vector3(0, (transform.position.y + projectileSpeed) * Time.deltaTime, 0);
            
        } else if(projectileDirection == "Pitchfork_Down")
        {
            transform.position += new Vector3(0, (transform.position.y - projectileSpeed) * Time.deltaTime, 0);
        }
        else if (projectileDirection == "Pitchfork_Left")
        {
            transform.position += new Vector3((transform.position.x - projectileSpeed) * Time.deltaTime, 0, 0);
        }
        else if (projectileDirection == "Pitchfork_Right")
        {
            transform.position += new Vector3((transform.position.x + projectileSpeed) * Time.deltaTime, 0, 0);
            //Debug.Log(transform.position);
            
        }



        StopCoroutine(DespawnProjectile());
        StartCoroutine(DespawnProjectile());
    }//end FixedUpdate()

    IEnumerator DespawnProjectile()
    {
        yield return new WaitForSeconds(4f);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player_Hurtbox")
        {
            var parent = other.gameObject.transform.parent.gameObject;
            //player.GetComponent<Health>().TakeDamage(2);
            damagePlayer.DealDamage(2);
            
            Destroy(gameObject);
        }
    }
}//end script
