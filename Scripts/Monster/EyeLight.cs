using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeLight : MonoBehaviour
{
    public GameObject petrifaction;

    Animator animator;
    GameObject player;

    private void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player");
    }

    public void Cast()
    {
        Vector2 dir = player.transform.position - transform.position;
        dir.Normalize();
        GameObject t_p = Instantiate(petrifaction, transform.position, Quaternion.FromToRotation(Vector3.up, dir));
        t_p.GetComponent<Rigidbody2D>().AddForce(dir * 80f);
        t_p.GetComponent<Petrifaction>().dir = dir;

        animator.SetBool("Cast", false);
    }

}
