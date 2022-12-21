using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bug1 : MonoBehaviour
{
    private bool live = true;
    private bool back = false;
    private float moveSpeed = 0.4f;
    private float bounceSpeed = 1.8f;

    Vector2 lookDirection = new Vector2(0, -1);
    Vector2 position1 = new Vector2(-5.6f, 1.7f);
    Vector2 position2 = new Vector2(-5f, 1.7f);

    Animator animator;
    GameObject player;
    Rigidbody2D rig;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        rig = GetComponent<Rigidbody2D>();
        rig.position = position1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (live)
        {
            if (!back)
            {
                Vector2 dir = position2 - rig.position;
                dir.Normalize();

                lookDirection = dir;
                animator.SetFloat("Move X", lookDirection.x);
                animator.SetFloat("Move Y", lookDirection.y);

                Vector2 position = rig.position;
                position = position + dir * moveSpeed * Time.deltaTime;
                rig.MovePosition(position);

                if (Vector2.Distance(rig.position, position2) < 0.01f) back = true;
            }
            else
            {
                Vector2 dir = position1 - rig.position;
                dir.Normalize();

                lookDirection = dir;
                animator.SetFloat("Move X", lookDirection.x);
                animator.SetFloat("Move Y", lookDirection.y);

                Vector2 position = rig.position;
                position = position + dir * moveSpeed * Time.deltaTime;
                rig.MovePosition(position);

                if (Vector2.Distance(rig.position, position1) < 0.01f) back = false;
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
