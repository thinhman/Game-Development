using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctoEnemy : EnemyAbstract
{
    int travel_path, paths_traveled = 0;
    float timer;
    public GameObject bullet;
    private Rigidbody2D rb;
    Animator anim;

    // Use this for initialization
    void Start()
    {
        health = gameObject.AddComponent<HealthSystem>();
        health.setMaxHealth(1);
        health.ResetHealth();
        rb = GetComponent<Rigidbody2D>();
        speed = 5;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (!health.isAlive())
        {
            Die();
        }
    }

    public override void Move()
    {
        if ((timer > 0.3f && paths_traveled < 4) || (timer == 0.0f && paths_traveled == 0))
        {
            travel_path = Random.Range(1, 5);
            if (travel_path == 1)
            {
                transform.Rotate(0, 0, 90);
            }
            else if (travel_path == 2)
            {
                transform.Rotate(0, 0, -90);
            }
            else if (travel_path == 3)
            {
                transform.Rotate(0, 0, 180);
            }
            Vector2 movement = transform.up;
            movement = movement.normalized * speed;
            rb.velocity = movement;
            anim.SetBool("moving", true);
            timer = 0.0f;
            paths_traveled += 1;
        }
        else if (paths_traveled < 4)
        {
            timer += Time.deltaTime;
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
            anim.SetBool("moving", false);
            if (timer > 0.5f)
            {
                Attack();
                timer = 0.0f;
                paths_traveled = 0;
            }
            else
            {
                timer += Time.deltaTime;
            }

        }
    }

     public override void Attack()
    {
        GameObject bulletinstance;
        bulletinstance = Instantiate(bullet, transform.position, transform.rotation);
        bulletinstance.GetComponent<bullet>().setVelocity(transform.up);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            transform.Rotate(0, 0, 180);
            Vector2 movement = transform.up;
            movement = movement.normalized * speed;
            rb.velocity = movement;
        }
        
        else if (collision.CompareTag("EnemyTrigger"))
        {
            print("AREA!!!!!");
            transform.Rotate(0, 0, 180);
            Vector2 movement = transform.up;
            movement = movement.normalized * speed;
            rb.velocity = movement;
            //transform.Translate(rb.velocity);
        }
        else if (collision.CompareTag("Player"))
        {
            collision.GetComponent<CiscoTesting>().health.takeDamage(1);
            transform.Rotate(0, 0, 180);
            Vector2 movement = transform.up;
            movement = movement.normalized * speed;
            rb.velocity = movement;
        }
    }

    void Die()
    {
        Drop();
        Destroy(gameObject);
    }
}
