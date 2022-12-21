using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tut1 : MonoBehaviour
{
    public GameObject info;
    private GameObject player;
    private void Start()
    {
        player = GameObject.Find("Player");
        info.SetActive(false);
    }

    private void Update()
    {
        if (Vector2.Distance(player.transform.position, transform.position) > 0.72f)
        {
            info.SetActive(false);
        }
    }

    public void DisplayInfo()
    {
        info.SetActive(!info.activeInHierarchy);
    }
}
