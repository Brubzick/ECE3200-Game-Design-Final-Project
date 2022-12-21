using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public int shieldStrenth = 3;
    public GameObject[] shieldTokens;

    private float recoverTime = 10f;
    private int shieldIndex;
    private Color[] colorList;
    Color color0 = new Color(1f, 0.42f, 0.42f, 1f);
    Color color1 = new Color(1f, 1f, 1f, 1f);
    Color color2 = new Color(0.37f, 0.83f, 1f, 1f);

    SpriteRenderer spriteRenderer;
    CapsuleCollider2D capsuleCollider2D;
    Animator animator;

    void Start()
    {
        colorList = new Color[] { color0, color1, color2, color2 };
        spriteRenderer = GetComponent<SpriteRenderer>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
        spriteRenderer.color = color2;
        shieldIndex = shieldTokens.Length - 1;
    }

    void Update()
    {
        if (shieldStrenth < 3)
        {
            recoverTime -= Time.deltaTime;
            if (recoverTime <= 0f)
            {
                shieldStrenth += 1;
                recoverTime = 5f;

                shieldIndex += 1;
                shieldTokens[shieldIndex].SetActive(true);
            }
            CheckCollider();
            spriteRenderer.color = colorList[shieldStrenth];
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        shieldStrenth -= 1;
        shieldTokens[shieldIndex].SetActive(false);
        shieldIndex -= 1;
        animator.SetBool("Hit", true);
    }

    void CheckCollider()
    {
        if (shieldStrenth <= 0)
        {
            capsuleCollider2D.enabled = false;
        }
        else
        {
            capsuleCollider2D.enabled = true;
        }
    }

    void BackToIdle()
    {
        animator.SetBool("Hit", false);
    }
}
