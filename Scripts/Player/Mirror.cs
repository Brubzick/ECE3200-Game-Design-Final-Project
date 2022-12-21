using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    public GameObject bounceback;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Petrifaction")
        {
            Transform t_trans = collision.collider.transform;
            Vector2 dir = -1f * collision.collider.GetComponent<Petrifaction>().dir;
            GameObject t_bounceback = Instantiate(bounceback, t_trans.position, t_trans.rotation);
            t_bounceback.GetComponent<Rigidbody2D>().AddForce(dir * 80f);
            Destroy(collision.collider.gameObject);
        }
    }
}
