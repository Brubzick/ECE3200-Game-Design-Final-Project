using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlysnakeSpawner : MonoBehaviour
{
    public GameObject Flysnake;
    public Transform[] spawners;
    public Medusa medusa;
    public bool canSpawn;

    private float cooldown = 0;

    void Start()
    {
        //test
        canSpawn = true;
    }


    void Update()
    {
        if(canSpawn)
        {
            if (cooldown <= 0)
            {
                int number = GameObject.FindGameObjectsWithTag("Flysnake").Length;
                if (number < 4)
                {
                    int index = Random.Range(0, spawners.Length);
                    Instantiate(Flysnake, spawners[index].position, Quaternion.identity);
                    cooldown = 2f;
                }
            }
            else
            {
                cooldown -= Time.deltaTime;
            }
            if (!medusa.live)
            {
                canSpawn = false;
            }
        }
    }
}
