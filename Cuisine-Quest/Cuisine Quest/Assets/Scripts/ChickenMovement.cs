using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenMovement : EnemyAbstract
{
    public float moveSpeed;
    private Animator anim;
    private Rigidbody2D rb;
    public bool isWalking;
    public float walkTime, walkCounter;
    public float waitTime, waitCounter;
    private int walkDirection;
    private Vector2 lastMove;
    public int maxhealth;
    // Use this for initialization
    void Start ()
    {
        health = gameObject.AddComponent<HealthSystem>();
        health.setMaxHealth(maxhealth);
        health.ResetHealth();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        walkCounter = walkTime;
        waitCounter = waitTime;
        chooseDirection();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(!health.isAlive())
        {
            Drop();
            Destroy(gameObject);
        }

		if(isWalking)
        {
            walkCounter -= Time.deltaTime;

            walkAnimation();
            if (walkCounter < 0)
            {
                isWalking = false;
                waitCounter = waitTime;
            }
        }
        else
        {
            waitCounter -= Time.deltaTime;
            rb.velocity = Vector2.zero;
            if (waitCounter < 0)
            {
                chooseDirection();
                walkAnimation();
            }
        }
        anim.SetBool("Moving", isWalking);
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        chooseDirection();
        walkAnimation();
    }

    public void walkAnimation()
    {
        switch (walkDirection)
        {
            case 0:
                rb.velocity = new Vector2(0, moveSpeed);
                anim.SetFloat("MoveY", 1f);
                anim.SetFloat("MoveX", 0f);
                lastMove = new Vector2(0f, 1f);
                anim.SetFloat("LastMoveY", lastMove.y);
                anim.SetFloat("LastMoveX", 0f);
                break;
            case 1:
                rb.velocity = new Vector2(moveSpeed, 0);
                anim.SetFloat("MoveX", 1f);
                anim.SetFloat("MoveY", 0f);
                lastMove = new Vector2(1f, 0f);
                anim.SetFloat("LastMoveX", lastMove.x);
                anim.SetFloat("LastMoveY", 0f);
                break;
            case 2:
                rb.velocity = new Vector2(0, -moveSpeed);
                anim.SetFloat("MoveY", -1f);
                anim.SetFloat("MoveX", 0f);
                lastMove = new Vector2(0f, -1f);
                anim.SetFloat("LastMoveY", lastMove.y);
                anim.SetFloat("LastMoveX", 0f);
                break;
            case 3:
                rb.velocity = new Vector2(-moveSpeed, 0);
                anim.SetFloat("MoveX", -1f);
                anim.SetFloat("MoveY", 0f);
                lastMove = new Vector2(-1f, 0f);
                anim.SetFloat("LastMoveX", lastMove.x);
                anim.SetFloat("LastMoveY", 0f);
                break;
        }
    }
    public void chooseDirection()
    {
        walkDirection = UnityEngine.Random.Range(0, 4);
        isWalking = true;
        walkCounter = walkTime;
    }

    public override void Move()
    {

    }

    public override void Attack()
    {

    }
}
