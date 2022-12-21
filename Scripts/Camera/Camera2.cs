using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Camera2 : MonoBehaviour
{
    GameObject player;
    CinemachineVirtualCamera cv;
    void Start()
    {
        player = GameObject.Find("Player");
        cv = GetComponent<CinemachineVirtualCamera>();
        cv.Follow = player.transform;
    }
}
