using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish_enemy : EnemyAbstract
{
    Transform target;
    Vector3 targetPos;
    Vector3 thisPos;
    public float offset;
    float angle;
    float minDistance = 3f;
    float range;
    bool playerFound = false;
    Rigidbody2D rb;
    float timer = 10f;
    Animator anim;

    // Use this for initialization
    void Start()
    {
        health = gameObject.AddComponent<HealthSystem>();
        health.setMaxHealth(1);
        health.ResetHealth();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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
        else if (timer > 1f)
        {
            FindPlayer();
        }
        else
        {
            timer += Time.deltaTime;
        }

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
                anim.SetBool("moving", playerFound);
            }
        }
    }

    public override void Move()
    {
        targetPos = target.position;
        thisPos = transform.position;
        targetPos.x = targetPos.x - thisPos.x;
        targetPos.y = targetPos.y - thisPos.y;
        angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + offset));
        Vector2 movement = transform.up;
        movement = movement.normalized * speed;
        rb.velocity = movement;
    }

    public override void Attack()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<CiscoTesting>().health.takeDamage(1);
            rb.velocity = new Vector2(0, 0);
            playerFound = false;
            anim.SetBool("moving", playerFound);
            timer = 0.0f;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void Die()
    {
        Drop();
        Destroy(gameObject);
    }
}