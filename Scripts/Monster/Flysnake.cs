using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flysnake : MonoBehaviour
{
    private bool live = true;
    private float moveSpeed;
    private float bounceSpeed = 1.8f;

    Animator animator;
    Rigidbody2D rig;
    GameObject player;

    void Awake()
    {
        animator = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        moveSpeed = Random.Range(20f, 40f);
    }

    void FixedUpdate()
    {
        if (live)
        {
            Vector3 dir = player.transform.position - transform.position;
            dir.z = 0;
            Quaternion rotation = Quaternion.FromToRotation(Vector3.up, dir);
            transform.rotation = rotation;

            dir.Normalize();
            rig.velocity = dir * moveSpeed * Time.deltaTime;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Fireball")
        {
            live = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            animator.SetBool("Die", true);
        }
        else if (collision.collider.tag == "Thunder")
        {
            live = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            animator.SetBool("Die", true);
        }
        else if (collision.collider.gameObject == player || collision.collider.tag == "Shield")
        {
            live = false;
            Vector2 dir = transform.position - player.transform.position;
            dir.Normalize();
            StartCoroutine(BounceBack(dir));
        }
        else if (collision.collider.tag == "Petrifaction" || collision.collider.tag == "Bounceback")
        {
            Destroy(collision.collider.gameObject);
            live = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            animator.SetBool("Die", true);
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
