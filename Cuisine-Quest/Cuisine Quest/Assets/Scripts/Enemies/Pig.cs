using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : EnemyAbstract
{
    Transform target;
    float minDistance = 5f, maxDistance = 6.5f;
    bool playerFound = false, travelStart = false;
    Rigidbody2D rb;
    float movetimer;
    Vector2 direction = new Vector2(0, -1);
    float attackTime;
    Weapon Sword;
    private Animator anim;
    private float H_Axis;
    private float V_Axis;
    private bool moving;
    private Vector2 lastMove;
    Vector3 startPosition;
    public bool patrolUp = false;

    // Use this for initialization
    void Start ()
    {
        health = gameObject.AddComponent<HealthSystem>();
        health.setMaxHealth(2);
        health.ResetHealth();
        Sword = gameObject.GetComponentInChildren<Sword>();
        rb = GetComponent<Rigidbody2D>();
        movetimer = 0.0f;
        attackTime = 0.0f;
        speed = 3;
        anim = GetComponent<Animator>();
        startPosition = transform.position;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!health.isAlive())
        {
            Die();
        }

        moving = false;
        

        if (H_Axis > V_Axis && rb.velocity.x > 0)
        {
            direction = new Vector2(1, 0);
        }
        else if (H_Axis < V_Axis && H_Axis < 0)
        {
            direction = new Vector2(-1, 0);
        }
        else if(V_Axis > H_Axis && V_Axis > 0)
        {
            direction = new Vector2(0, 1);
        }
        else if(V_Axis < H_Axis && V_Axis < 0)
        {
            direction = new Vector2(0, -1);
        }

        Move();
        Attack();
        if(!playerFound)
        {
            FindPlayer();
        }

        H_Axis = rb.velocity.x;
        V_Axis = rb.velocity.y;

        anim.SetBool("Moving", moving);
        anim.SetFloat("LastMoveX", direction.x);
        anim.SetFloat("LastMoveY", direction.y);
        anim.SetFloat("MoveX", H_Axis);
        anim.SetFloat("MoveY", V_Axis);
        
    }

    void FindPlayer()
    {
        foreach (RaycastHit2D collide in Physics2D.CircleCastAll(transform.position, minDistance, new Vector2(0, 0)))
        {
            if (collide.collider.CompareTag("Player"))
            {
                target = collide.transform;
                playerFound = true;
                travelStart = false;
            }
        }
    }

    public override void Move()
    {
        if(!playerFound && !travelStart)
        {
            if (!patrolUp)
            {
                if (movetimer < 3.0f)
                {
                    Vector2 movement = new Vector2(1, 0);
                    movement = movement.normalized * speed;
                    rb.velocity = movement;
                    movetimer += Time.deltaTime;
                    moving = true;
                }
                else if (movetimer < 3.75f)
                {
                    rb.velocity = Vector2.zero;
                    movetimer += Time.deltaTime;
                }
                else if (movetimer < 6.75f)
                {
                    Vector2 movement = new Vector2(-1, 0);
                    movement = movement.normalized * speed;
                    rb.velocity = movement;
                    movetimer += Time.deltaTime;
                    moving = true;
                }
                else if (movetimer < 7.5f)
                {
                    rb.velocity = Vector2.zero;
                    movetimer += Time.deltaTime;
                }
                else
                {
                    movetimer = 0.0f;
                }
            }
            else
            {
                if (movetimer < 1.5f)
                {
                    Vector2 movement = new Vector2(0, 1);
                    movement = movement.normalized * speed;
                    rb.velocity = movement;
                    movetimer += Time.deltaTime;
                    moving = true;
                }
                else if (movetimer < 2.25f)
                {
                    rb.velocity = Vector2.zero;
                    movetimer += Time.deltaTime;
                }
                else if (movetimer < 3.75f)
                {
                    Vector2 movement = new Vector2(0, -1);
                    movement = movement.normalized * speed;
                    rb.velocity = movement;
                    movetimer += Time.deltaTime;
                    moving = true;
                }
                else if (movetimer < 4.5f)
                {
                    rb.velocity = Vector2.zero;
                    movetimer += Time.deltaTime;
                }
                else
                {
                    movetimer = 0.0f;
                }
            }
        }
        else if(playerFound)
        {
            
            Vector2 movement = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y);
            movement = movement.normalized * speed;
            rb.velocity = movement;
            moving = true;
            float dist = Vector3.Distance(transform.position, target.position);
            if (dist > maxDistance)
            {
                playerFound = false;
                travelStart = true;
            }
        }
        else if(travelStart)
        {
            Vector2 movement = new Vector2(startPosition.x - transform.position.x, startPosition.y - transform.position.y);
            movement = movement.normalized * speed;
            rb.velocity = movement;
            moving = true;
            if(Vector3.Distance(transform.position, startPosition) <= 0.7f)
            {
                movetimer = 6.8f;
                travelStart = false;
            }
        }
    }

    public override void Attack()
    {
        if(playerFound)
        {
            float dist = Vector3.Distance(transform.position, target.position);
            if(dist < 2.7f && attackTime <= 0.0f)
            {
                Sword.Attack(direction);
                attackTime = 3.0f;
            }
            else
            {
                attackTime -= Time.deltaTime;
            }
        }
    }

    void Die()
    {
        Drop();
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
            
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
