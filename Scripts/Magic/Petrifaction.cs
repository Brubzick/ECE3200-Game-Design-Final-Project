using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Petrifaction : MonoBehaviour
{
    public Vector2 dir;

    Vector2 startPosition;
    void Awake()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, startPosition) >= 1000f)
        {
            Destroy(gameObject);
        }
    }

    //public void getDir(Vector2 t_dir)
    //{
    //    dir = t_dir;
    //}
}
