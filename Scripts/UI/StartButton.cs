using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    private bool clicked;

    public Sprite[] pic;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = pic[0];
        clicked = false;
    }

    private void OnMouseEnter()
    {
        spriteRenderer.sprite = pic[1];
    }

    private void OnMouseExit()
    {
        spriteRenderer.sprite = pic[0];
    }

    private void OnMouseDown()
    {
        spriteRenderer.sprite = pic[2];
    }

    private void OnMouseUp()
    {
        spriteRenderer.sprite = pic[1];
        if (!clicked)
        {
            clicked = true;
            StartCoroutine(Load());
        }
    }

    IEnumerator Load()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(1);
        yield return null;
    }
}
