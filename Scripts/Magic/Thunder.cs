using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder : MonoBehaviour
{
    void Over()
    {
        Destroy(gameObject);
    }

    void DisableCollider()
    {
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
