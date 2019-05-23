using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffaloEnemy : EnemyAbstract
{
    public float moveSpeed;
    private Animator anim;
    private Rigidbody2D rb;
    public bool isWalking;
    public float walkTime, walkCounter;
    public float waitTime, waitCounter;
    private int walkDirection;
    private Vector2 lastMove;

    Transform target;
    bool playerFound = false;
    float minDistance = 3f;
    Vector3 targetPos;
    Vector3 thisPos;
    Vector3 movement;
    // Use this for initialization
    void Start()
    {
        health = gameObject.AddComponent<HealthSystem>();
        health.setMaxHealth(4);
        health.ResetHealth();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        walkCounter = walkTime;
        waitCounter = waitTime;
        chooseDirection();
    }

    // Update is called once per frame
    void Update()
    {
        if (!health.isAlive())
        {
            Die();
        }

        if (playerFound)
        {
            Move();
        }
        else if (isWalking)
        {
            walkCounter -= Time.deltaTime;
            walkAnimation();
            FindPlayer();
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
            FindPlayer();
            if (waitCounter < 0)
            {
                chooseDirection();
                walkAnimation();
            }
        }
        anim.SetBool("Moving", isWalking);

    }
    void FindPlayer()
    {
        foreach (RaycastHit2D collide in Physics2D.CircleCastAll(transform.position, minDistance, new Vector2(0, 0)))
        {
            if (collide.collider.CompareTag("Player"))
            {
                target = collide.transform;
                print("Found");
                playerFound = true;
                targetPos = target.position;
                thisPos = transform.position;
                if (Mathf.Abs((thisPos.x - targetPos.x)) > Mathf.Abs((thisPos.y - targetPos.y)))
                {
                    if ((thisPos.x - targetPos.x) < 0)
                    {
                        anim.SetFloat("MoveX", 1f);
                        anim.SetFloat("MoveY", 0f);
                        lastMove = new Vector2(1f, 0f);
                        anim.SetFloat("LastMoveX", lastMove.x);
                        anim.SetFloat("LastMoveY", 0f);
                    }
                    else
                    {
                        anim.SetFloat("MoveX", -1f);
                        anim.SetFloat("MoveY", 0f);
                        lastMove = new Vector2(-1f, 0f);
                        anim.SetFloat("LastMoveX", lastMove.x);
                        anim.SetFloat("LastMoveY", 0f);
                    }

                }
                else
                {
                    if ((thisPos.y - targetPos.y) < 0)
                    {
                        anim.SetFloat("MoveY", 1f);
                        anim.SetFloat("MoveX", 0f);
                        lastMove = new Vector2(0f, 1f);
                        anim.SetFloat("LastMoveY", lastMove.y);
                        anim.SetFloat("LastMoveX", 0f);
                    }
                    else
                    {
                        anim.SetFloat("MoveY", -1f);
                        anim.SetFloat("MoveX", 0f);
                        lastMove = new Vector2(0f, -1f);
                        anim.SetFloat("LastMoveY", lastMove.y);
                        anim.SetFloat("LastMoveX", 0f);
                    }
                }
                //targetPos.x = targetPos.x - thisPos.x;
                //targetPos.y = targetPos.y - thisPos.y;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            collision.gameObject.GetComponent<CiscoTesting>().health.takeDamage(1);
            playerFound = false;
            isWalking = false;
            anim.SetBool("Moving", isWalking);
            waitCounter = 3;
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            chooseDirection();
            walkAnimation();
        }
    }
    public override void Move()
    {
        if (Vector3.Distance(transform.position, targetPos) == 0f)
        {
            isWalking = false;
            anim.SetBool("Moving", isWalking);
            waitCounter = 3;
            playerFound = false;
        }
        else if (waitCounter > 1.5f)
        {
            isWalking = false;
            anim.SetBool("Moving", isWalking);
            waitCounter -= Time.deltaTime;
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            isWalking = true;
            anim.SetBool("Moving", isWalking);
            transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        }

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


    public override void Attack()
    {
        throw new NotImplementedException();
    }

    void Die()
    {
        Drop();
        Destroy(gameObject);
    }
}

