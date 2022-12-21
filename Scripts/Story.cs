using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Story : MonoBehaviour
{
    RectTransform rectTransform;
    float speed = 30f;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            SceneManager.LoadScene(2);
        }

    }

    private void FixedUpdate()
    {
        Vector2 t_pos = rectTransform.position;

        if (t_pos.y > 720f)
        {
            t_pos.y = -175f;
        }

        t_pos.y += speed * Time.deltaTime;
        rectTransform.position = t_pos;
    }
}
