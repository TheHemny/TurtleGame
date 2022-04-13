using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //The one in use
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Animator animator;

    public AudioSource attackSound;
    public AudioSource blockSound;

    public GameObject[] hurtboxes;
    public GameObject[] hitboxes;
    private GameObject previousActiveHurtbox;

    private int coinCount;

    private bool isAttacking;
    private bool isBlocking;
    private bool movingVertical;

    public GameObject veryWealthy;

    Vector2 movement;


    void Start()
    {
        ClearHurtboxes();
        ClearHitboxes();
        previousActiveHurtbox = hurtboxes[0];
        previousActiveHurtbox.SetActive(false);
        isAttacking = false;
        movingVertical = false;
        hurtboxes[0].SetActive(true);

        veryWealthy.SetActive(false);
    }//end Start()

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        //left = -1, right = 1, nothing = 0
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);


        if (!isBlocking)
        {
            if (!movingVertical)
            {
                switch (movement.x)
                {
                    case -1:
                        animator.SetBool("FacingLeft", true);
                        animator.SetBool("FacingRight", false);
                        if (movement.y == 0)
                        {
                            animator.SetBool("FacingUp", false);
                            animator.SetBool("FacingDown", false);
                        }
                        ClearHurtboxes();
                        hurtboxes[1].SetActive(true);
                        previousActiveHurtbox = hurtboxes[1];
                        break;
                    case 1:
                        animator.SetBool("FacingLeft", false);
                        animator.SetBool("FacingRight", true);
                        if (movement.y == 0)
                        {
                            animator.SetBool("FacingUp", false);
                            animator.SetBool("FacingDown", false);
                        }
                        ClearHurtboxes();
                        hurtboxes[0].SetActive(true);
                        previousActiveHurtbox = hurtboxes[0];
                        break;
                }//end Switch
            }//end if

            switch (movement.y)
            {
                case -1:
                    movingVertical = true;
                    animator.SetBool("FacingDown", true);
                    animator.SetBool("FacingLeft", false);
                    animator.SetBool("FacingRight", false);
                    animator.SetBool("FacingUp", false);
                    ClearHurtboxes();
                    hurtboxes[2].SetActive(true);
                    previousActiveHurtbox = hurtboxes[2];
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
                    ClearHurtboxes();
                    hurtboxes[2].SetActive(true);
                    previousActiveHurtbox = hurtboxes[2];
                    break;
            }//end switch
        }//end if(!isBlocking)
        


        if (Input.GetKeyDown("z") && !isAttacking)
        {
            isAttacking = true;
            attackSound.Play();
            animator.SetBool("IsAttacking", isAttacking);
            
            StopCoroutine(TurtleAttack());
            StartCoroutine(TurtleAttack());
        }

        if(Input.GetKeyDown(KeyCode.Space) && !isAttacking)
        {
            isBlocking = true;
            blockSound.Play();
            //DamagePlayer.SetIsBlocking(isBlocking);
            ClearHurtboxes();
            if(animator.GetBool("FacingUp") || animator.GetBool("FacingRight"))
            {
                hurtboxes[3].SetActive(true);
            }

            if (animator.GetBool("FacingLeft") || animator.GetBool("FacingDown"))
            {
                hurtboxes[4].SetActive(true);
            }
            animator.SetBool("IsBlocking", isBlocking);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isBlocking = false;
            animator.SetBool("IsBlocking", isBlocking);
            hurtboxes[3].SetActive(false);
            hurtboxes[4].SetActive(false);
            previousActiveHurtbox.SetActive(true);

        }

    }//end Update()

    void ToggleAttackHitbox()
    {
        if (animator.GetBool("FacingUp"))
            hitboxes[3].SetActive(true);
        else if (animator.GetBool("FacingDown"))
            hitboxes[2].SetActive(true);
        else if (animator.GetBool("FacingLeft"))
            hitboxes[1].SetActive(true);
        else if (animator.GetBool("FacingRight"))
            hitboxes[0].SetActive(true);
    }

    void FixedUpdate()
    {
        //Only move if not attacking
        if (isAttacking || isBlocking)
        {
            rb.MovePosition(rb.position + (movement * 0));
        }
        else
        {
            rb.MovePosition(rb.position + (movement * moveSpeed * Time.fixedDeltaTime));
        }


        
    }//end FixedUpdate()

    public bool GetIsBlocking()
    {
        return isBlocking;
    }//end GetIsBlocking()

    void ClearHurtboxes()
    {
        for(int i = 0; i < hurtboxes.Length; i++)
        {
            hurtboxes[i].SetActive(false);
        }
    }//end ClearHurtboxes()

    void ClearHitboxes()
    {
        for(int i = 0; i < hitboxes.Length; i++)
        {
            hitboxes[i].SetActive(false);
        }
    }//end ClearHitboxes()

    
    public void GoldenFreddy()
    {
        //Activates "VERY wealthy" sprite when all coins collected
        //Called from CoinCounts script
        veryWealthy.SetActive(true);
    }

    IEnumerator TurtleAttack()
    {
        yield return new WaitForSeconds(0.2f);
        ToggleAttackHitbox();
        yield return new WaitForSeconds(0.4333f);
        isAttacking = false;
        animator.SetBool("IsAttacking", isAttacking);
        ClearHitboxes();
    }//end TurtleAttack()

}//end script
