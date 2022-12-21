using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    Rigidbody2D rig;
    Animator animator;
    Vector2 direction;
    float force = 80f;
    CircleCollider2D circleCollider;
    Vector2 originPosition;

    // Start is called before the first frame update
    void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        circleCollider = GetComponent<CircleCollider2D>();
        circleCollider.enabled = false;
        originPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(originPosition, transform.position) >= 1000f)
        {
            Destroy(gameObject);
        }
    }

    public void GetDirection(Vector2 dir)
    {
        direction = dir;
    }

    void FireLaunch()
    {
        Launch(direction, force);
    }

    void Launch(Vector2 dir, float force)
    {
        
        animator.SetBool("Generate", true);
        rig.AddForce(dir * force);
        circleCollider.enabled = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        animator.SetBool("Hit", true);
        circleCollider.enabled = false;
        rig.velocity = Vector2.zero;
    }

    void Explotion()
    {
        Destroy(gameObject);
    }
}
