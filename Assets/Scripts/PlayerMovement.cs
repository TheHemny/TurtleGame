using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Animator animator;
    public string attackKey;
    public AudioSource attackSound;

    private bool isAttacking;
    private bool movingVertical;

    Vector2 movement;

    void Start()
    {
        attackKey = attackKey.ToLower();
        isAttacking = false;
        movingVertical = false;
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        //left = -1, right = 1, nothing = 0
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        if (!movingVertical)
        {
            switch (movement.x)
            {
                case -1:
                    animator.SetBool("FacingLeft", true);
                    animator.SetBool("FacingRight", false);
                    if(movement.y == 0)
                    {
                        animator.SetBool("FacingUp", false);
                        animator.SetBool("FacingDown", false);
                    }
                    break;
                    /*
                case 0:
                    animator.SetBool("FacingLeft", false);
                    animator.SetBool("FacingRight", false);
                    break;
                    */
                case 1:
                    animator.SetBool("FacingLeft", false);
                    animator.SetBool("FacingRight", true);
                    if (movement.y == 0)
                    {
                        animator.SetBool("FacingUp", false);
                        animator.SetBool("FacingDown", false);
                    }
                    break;
            }
        }
        
        switch (movement.y)
        {
            case -1:
                movingVertical = true;
                animator.SetBool("FacingDown", true);
                animator.SetBool("FacingLeft", false);
                animator.SetBool("FacingRight", false);
                animator.SetBool("FacingUp", false);
                break;
            case 0:
                movingVertical = false;
                /*
                animator.SetBool("FacingDown", false);
                //animator.SetBool("FacingLeft", false);
                //animator.SetBool("FacingRight", false);
                animator.SetBool("FacingUp", false);
                */
                break;
            case 1:
                movingVertical = true;
                animator.SetBool("FacingDown", false);
                animator.SetBool("FacingLeft", false);
                animator.SetBool("FacingRight", false);
                animator.SetBool("FacingUp", true);
                break;
        }

        if(movement.x == 0 && movement.y == 0)
        {

        }


        if (Input.GetKeyDown(attackKey) && !isAttacking)
        {
            isAttacking = true;
            attackSound.Play();
            animator.SetBool("IsAttacking", isAttacking);
            StopCoroutine(TurtleAttack());
            StartCoroutine(TurtleAttack());
        }

    }//end Update()

    void FixedUpdate()
    {
        //Only move if not attacking
        if (isAttacking)
        {
            rb.MovePosition(rb.position + (movement * 0));
        }
        else
        {
            rb.MovePosition(rb.position + (movement * moveSpeed * Time.fixedDeltaTime));
        }
        
    }//end FixedUpdate()

    IEnumerator TurtleAttack()
    {
        yield return new WaitForSeconds(0.4333f);
        isAttacking = false;
        animator.SetBool("IsAttacking", isAttacking);
    }
}//end script
