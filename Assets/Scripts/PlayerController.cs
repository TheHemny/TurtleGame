using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed;
    public Rigidbody2D rb;

    private Vector2 moveDirection;

    private bool isWalking;
    private bool walkingLeft;
    private bool walkingUp;

    public GameObject hitbox;

    Animator m_Animator;

    public AudioSource walkSound;
    public string attackKey;

    private bool isAttacking;


    void Start()
    {
        m_Animator = GetComponent<Animator>();
        hitbox.SetActive(false);
        walkingLeft = false;

    }//end Start()

    void Update()
    {
        ProcessInputs();
    }//end Update()

    void FixedUpdate()
    {
        if (!isAttacking)
        {
            Move();
        }
        
        
    }//end FixedUpdate()

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");


        moveDirection = new Vector2(moveX, moveY).normalized;

        //Flips sprite horizontally based on movement
        if((moveX < 0 && !walkingLeft) || (moveX > 0 && walkingLeft))
        {
            flipHorizontal();
        }

        if (Input.GetKeyDown(attackKey))
        {
            Debug.Log("Attacking");
            isAttacking = true;
            StopCoroutine(Attack("turtle"));
            StartCoroutine(Attack("turtle"));
        }

        
    }//end ProcessInputs()

    void Move()
    {
        if (!isAttacking)
        {
            rb.velocity = new Vector2(moveDirection.x * movementSpeed, moveDirection.y * movementSpeed);

            bool hasHorizontalInput = !Mathf.Approximately(moveDirection.x, 0f);
            bool hasVerticalInput = !Mathf.Approximately(moveDirection.y, 0f);
            bool isWalking = hasHorizontalInput || hasVerticalInput;
            m_Animator.SetBool("IsWalking", isWalking);
        } else if (isAttacking)
        {
            rb.velocity = new Vector2(0f, 0f);
        }
        
    }

    void flipHorizontal()
    {
        walkingLeft = !walkingLeft;
        transform.Rotate(0f, 180.0f, 0f);
    }

    IEnumerator Attack(string currentAnimal)
    {
        if(currentAnimal == "turtle")
        {
            yield return new WaitForSeconds(0.25f);
            hitbox.SetActive(isAttacking);
            m_Animator.SetBool("IsAttacking", isAttacking);
            yield return new WaitForSeconds(0.35f);
            isAttacking = false;
            m_Animator.SetBool("IsAttacking", isAttacking);
            hitbox.SetActive(isAttacking);
        }
    }
}//end PlayerMovement
