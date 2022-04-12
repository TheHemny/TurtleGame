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
    public GameObject[] pitchforks;
    Rigidbody2D rb;

    private bool facingLeft;
    private bool facingRight;

    public AudioSource deathSound;
    public AudioSource attackSound;

    private bool isAggro;
    private bool canMove;
    private bool moveHorizontal;
    private bool moveVertical;

    //Used for random movement
    private int moveInt;
    private int moveDirection;
    private int movePositiveNegative;
    private int attackChance;
    private float moveDistance;
    private float delayMoveAgainTime;
    float moveTime;

    private bool readyToAttack;
    private bool isDead;
    private bool readyForNewPos;


    public GameObject lootDrop;

    private string direction;
   

    //Get origin (spawn location)
    //Walk in random directions at random intervals for random times
    //If player in line of sight in X dist, throw 

    void Start()
    {
        isDead = false;
        readyToAttack = false;
        m_animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        for(int i = 0; i < hurtboxes.Length; i++)
        {
            hurtboxes[i].SetActive(false);
            //Left hurtbox = hurtboxes[0];
            //Right hurtbox = hurtboxes[1];
        }

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
            moveDirection = (int)Random.Range(0, 4);
            moveDistance = Random.Range(2f, 5f);
            moveTime = Random.Range(0.2f, 2.5f);


            if (!canMove && !isDead)
            {
                switch (moveDirection)
                {
                    //Assign moveposnegative in the switch
                    case 0:
                        //Debug.Log("Moving Up");
                        canMove = true;
                        movePositiveNegative = 1;
                        moveVertical = true;
                        moveHorizontal = false;
                        readyForNewPos = false;
                        direction = "Up";
                        SetAnimatorBools(direction, true);
                        break;
                    case 1:
                        //Debug.Log("Moving down");
                        canMove = true;
                        movePositiveNegative = -1;
                        moveVertical = true;
                        moveHorizontal = false;
                        readyForNewPos = false;
                        direction = "Down";
                        SetAnimatorBools(direction, true);
                        break;
                    case 2:
                        //Debug.Log("Moving right");
                        canMove = true;
                        movePositiveNegative = 1;
                        moveHorizontal = true;
                        moveVertical = false;
                        readyForNewPos = false;
                        direction = "Right";
                        SetAnimatorBools(direction, true);
                        break;
                    case 3:
                        //Debug.Log("Moving left");
                        canMove = true;
                        movePositiveNegative = -1;
                        moveHorizontal = true;
                        moveVertical = false;
                        readyForNewPos = false;
                        direction = "Left";
                        SetAnimatorBools(direction, true);
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

    private void SetAnimatorBools(string theDirection, bool trueFalse)
    {
        //Debug.Log("SetAnimatorBools() - Moving " + direction);
        if (theDirection == "Up")
        {
            hurtboxes[1].SetActive(true);
            hurtboxes[0].SetActive(false);
            m_animator.SetBool("IsMoving", true);
            m_animator.SetBool("Up", true);
            m_animator.SetBool("Left", false);
            m_animator.SetBool("Right", false);
            m_animator.SetBool("Down", false);
        } else if(theDirection == "Down")
        {
            hurtboxes[1].SetActive(false);
            hurtboxes[0].SetActive(true);
            m_animator.SetBool("IsMoving", true);
            m_animator.SetBool("Down", true);
            m_animator.SetBool("Left", false);
            m_animator.SetBool("Right", false);
            m_animator.SetBool("Up", false);
        }else if(theDirection == "Left")
        {
            hurtboxes[1].SetActive(false);
            hurtboxes[0].SetActive(true);
            m_animator.SetBool("IsMoving", true);
            m_animator.SetBool("Left", true);
            m_animator.SetBool("Up", false);
            m_animator.SetBool("Right", false);
            m_animator.SetBool("Down", false);
        } else if(theDirection == "Right")
        {
            hurtboxes[1].SetActive(true);
            hurtboxes[0].SetActive(false);
            m_animator.SetBool("IsMoving", true);
            m_animator.SetBool("Right", true);
            m_animator.SetBool("Left", false);
            m_animator.SetBool("Up", false);
            m_animator.SetBool("Down", false);
        }
        else if(theDirection == "Idle")
        {
            m_animator.SetBool("IsMoving", false);
            /*
            m_animator.SetBool("Up", false);
            m_animator.SetBool("Left", false);
            m_animator.SetBool("Right", false);
            m_animator.SetBool("Down", false);
            */
        }
    }//end SetAnimatorBools()

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
            Debug.Log("Enemyt hit");
            if(healthComponent.GetCurrentHealth() == 0)
            {
                canMove = false;
                isDead = true;
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
        if (canMove && !isDead)
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
                readyToAttack = true;
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
        
        //Instantiates a gameobject after death, if enemy is assigned one
        if(lootDrop != null)
        {
            Instantiate(lootDrop, transform.position, lootDrop.transform.rotation);
        }
    }//end DelayDeathDespawn

    IEnumerator DelayMoveAgain()
    {
        
        delayMoveAgainTime = Random.Range(0.75f, 1.75f);
        //Debug.Log("DelayMoveAgain for " + delayMoveAgainTime + " sec");
        canMove = false;

        //50-50 chance for idle or attack
        //attack  - instantiate spear based on direction
        //use array for spear
        //despawn after 4 seconds
        //separate script for the projectile movement/collision

        attackChance = (int)Random.Range(0, 2);
        //Debug.Log("attackChance = " + attackChance);
        attackChance = 0;
        if (readyToAttack)
        {
            switch (attackChance)
            {
                case 0: //attack
                    //Debug.Log("Enemy attacking");
                    yield return new WaitForSeconds(0.25f);
                    if(attackSound != null)
                    {
                        attackSound.Play();
                    }
                    if (direction == "Up")
                    {
                        m_animator.SetBool("AttackingRight", true);
                        Instantiate(pitchforks[3], this.transform.position, transform.rotation);
                    }
                    else if (direction == "Down")
                    {
                        m_animator.SetBool("AttackingLeft", true);
                        Instantiate(pitchforks[0], this.transform.position, transform.rotation);
                    }
                    else if (direction == "Left")
                    {
                        m_animator.SetBool("AttackingLeft", true);
                        Instantiate(pitchforks[1], this.transform.position, transform.rotation);
                    }
                    else if (direction == "Right")
                    {
                        m_animator.SetBool("AttackingRight", true);
                        Instantiate(pitchforks[2], this.transform.position, transform.rotation);
                    }

                    yield return new WaitForSeconds(1f);
                    m_animator.SetBool("AttackingRight", false);
                    m_animator.SetBool("AttackingLeft", false);
                    SetAnimatorBools("Idle", true);
                    readyToAttack = false;
                    break;
                case 1: //idle
                    readyToAttack = false;
                    break;

            }//end switch(attackChance)
        }//end if(readyToAttack)
        
        Debug.Log("Enemy idling");
        SetAnimatorBools("Idle", true);
        yield return new WaitForSeconds(delayMoveAgainTime);


        canMove = true;
        readyForNewPos = true;
        readyToAttack = false;
        m_animator.SetBool("IsMoving", true);
        
    }//end DelayMoveAgain()

}//end script
