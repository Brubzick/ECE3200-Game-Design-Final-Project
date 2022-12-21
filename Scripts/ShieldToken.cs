using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldToken : MonoBehaviour
{
    private static ShieldToken instance;
    public ShieldToken ControllerInstance
    {
        get { return instance; }
    }
    void Start()
    {
        DontDestroyOnLoad(this);
    }
    void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
    }
}
