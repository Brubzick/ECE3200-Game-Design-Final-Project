using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    private float bounceSpeed = 2f;
    private float speed = 15f;
    private float randomMoveCooldown;
    private bool live = true;
    private bool goToPlayer = false;
    private bool randomMoving = false;
    Vector2 randomDir;

    Rigidbody2D rig;
    Animator animator;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        randomMoveCooldown = Random.Range(2.5f, 5f);
    }

    void FixedUpdate()
    {
        if (live)
        {
            if (goToPlayer)
            {
                Vector2 dir = player.transform.position - transform.position;
                dir.Normalize();
                rig.velocity = dir * speed * Time.deltaTime;
            }
            else if (randomMoving)
            {
                //random move one clip
                rig.velocity = randomDir * speed * Time.deltaTime;
            }
            else
            {
                rig.velocity = Vector2.zero;
                randomMoveCooldown -= Time.deltaTime;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Fireball")
        {
            live = false;
            animator.SetBool("Die", true);
        }
        else if (collision.collider.tag == "Thunder")
        {
            live = false;
            animator.SetBool("Die", true);
        }
        else if (collision.collider.gameObject == player || collision.collider.tag == "Shield")
        {
            live = false;
            Vector2 dir = transform.position - player.transform.position;
            dir.Normalize();
            StartCoroutine(BounceBack(dir));
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    void CheckPlayerDistance()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance <= 1f)
        {
            goToPlayer = true;
            animator.SetBool("Move", true);
        }
        else
        {
            randomDir = Random.insideUnitCircle;
            randomDir.Normalize();
            goToPlayer = false;
            if (randomMoveCooldown < 0)
            {
                randomMoving = true;
                animator.SetBool("Move", true);
                randomMoveCooldown = Random.Range(2.5f, 5f);
            }
            else
            {
                randomMoving = false;
                animator.SetBool("Move", false);
            }
        }
    }

    IEnumerator BounceBack(Vector2 dir)
    {
        rig.velocity = dir * bounceSpeed;
        yield return new WaitForSeconds(0.125f);
        rig.velocity = dir * bounceSpeed * 0.75f;
        yield return new WaitForSeconds(0.125f);
        rig.velocity = dir * bounceSpeed * 0.5f;
        yield return new WaitForSeconds(0.125f);
        rig.velocity = dir * bounceSpeed * 0.25f;
        yield return new WaitForSeconds(0.125f);
        rig.velocity = Vector2.zero;
        yield return new WaitForSeconds(1.5f);
        live = true;
    }
}
