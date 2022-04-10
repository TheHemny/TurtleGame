using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demon : MonoBehaviour
{
    public float moveSpeed;
    Animator m_animator;
    Vector2 movement;
    public Transform target;

    public GameObject[] hurtboxes;
    Rigidbody2D rb;

    private bool facingLeft;
    private bool facingRight;

    public AudioSource deathSound;

    private bool isAggro;
    private bool canMove;
    private bool moveHorizontal;
    private bool moveVertical;

    //Used for random movement
    private int moveInt;
    private int moveDirection;
    private int movePositiveNegative;
    private float moveDistance;
    private float delayMoveAgainTime;
    float moveTime;

    private bool readyForNewPos;

    public Animation deathAnimation;


    //Get origin (spawn location)
    //Walk in random directions at random intervals for random times
    //If player in line of sight in X dist, throw 

    void Start()
    {
        m_animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        isAggro = false;
        canMove = false;
        readyForNewPos = true;
        target.position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (readyForNewPos)
        {
            //Determine which direction to move
            //0 = move vertical, 1 = move horizontal
            moveInt = (int)Random.Range(0, 2);
            moveDirection = (int)Random.Range(0, 4);
            moveDistance = Random.Range(1f, 5f);
            moveTime = Random.Range(0.2f, 2.5f);


            if (!canMove)
            {
                //Debug.Log(moveInt);
                switch (moveDirection)
                {
                    case 0:
                        Debug.Log("Moving Up");
                        canMove = true;
                        movePositiveNegative = 1;
                        moveVertical = true;
                        moveHorizontal = false;
                        readyForNewPos = false;
                        break;
                    case 1:
                        Debug.Log("Moving down");
                        canMove = true;
                        movePositiveNegative = -1;
                        moveVertical = true;
                        moveHorizontal = false;
                        readyForNewPos = false;
                        break;
                    case 2:
                        Debug.Log("Moving right");
                        canMove = true;
                        movePositiveNegative = 1;
                        moveHorizontal = true;
                        moveVertical = false;
                        readyForNewPos = false;
                        break;
                    case 3:
                        Debug.Log("Moving left");
                        canMove = true;
                        movePositiveNegative = -1;
                        moveHorizontal = true;
                        moveVertical = false;
                        readyForNewPos = false;
                        break;
                }
            }
            readyForNewPos = false;
        }
        

        //moveVertical = true;
        /*
        m_animator.SetFloat("Horizontal", movement.x);
        m_animator.SetFloat("Vertical", movement.y);
        m_animator.SetFloat("Speed", movement.sqrMagnitude);
        */
    }

    void LateUpdate()
    {
        //Debug.Log(canMove);
        if (canMove)
        {
            if (moveVertical)
            {
                MoveDestination();
            }
            if (moveHorizontal)
            {
                MoveDestination();
            }
            //for loop to count down timer
        }
    }//end LateUpdate()

    void OnTriggerEnter2D (Collider2D other)
    {
        var healthComponent = this.gameObject.GetComponent<Health>();
        if (other.tag == "Player_Hitbox")
        {
            m_animator.SetInteger("Health", healthComponent.GetCurrentHealth());
            healthComponent.TakeDamage(1);
            Debug.Log("Hit");
            if(healthComponent.GetCurrentHealth() == 0)
            {
                canMove = false;
                readyForNewPos = false;
                deathSound.Play();
                StopCoroutine(DelayDeathDespawn());
                StartCoroutine(DelayDeathDespawn());
            }
            //m_animator.SetInteger
        }
    }//end OnTriggerEnter2D

    private void MoveDestination()
    {
        float step = moveSpeed * Time.deltaTime;
        if (canMove)
        {
            if (moveVertical)
            {
                target.position = new Vector2(0f, moveDistance * movePositiveNegative);
                transform.position = Vector2.MoveTowards(transform.position, target.position, step);
            }
            else if (moveHorizontal)
            {
                target.position = new Vector2(moveDistance * movePositiveNegative, 0f);
                transform.position = Vector2.MoveTowards(transform.position, target.position, step);
            }

            if (target.position == transform.position)
            {
                canMove = false;
                StopCoroutine(DelayMoveAgain());
                StartCoroutine(DelayMoveAgain());
            }
        }
        

    }//end moveDestination

    IEnumerator DelayDeathDespawn()
    {
        for(int i = 0; i < hurtboxes.Length; i++)
        {
            hurtboxes[i].SetActive(false);
        }
        yield return new WaitForSeconds(2f);
        this.gameObject.SetActive(false);
    }//end DelayDeathDespawn

    IEnumerator DelayMoveAgain()
    {
        
        delayMoveAgainTime = Random.Range(0.75f, 1.75f);
        //Debug.Log("DelayMoveAgain for " + delayMoveAgainTime + " sec");
        canMove = false;
        yield return new WaitForSeconds(delayMoveAgainTime);
        canMove = true;
        readyForNewPos = true;
        
    }//end DelayMoveAgain()

}//end script
