using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Button that goes back to main menu
public class Button2 : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    private bool clicked;
    private GameObject player;

    public Sprite[] pic;
    void Awake()
    {
        player = GameObject.Find("Player");
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
        Destroy(player);
        SceneManager.LoadScene(0);
        yield return null;
    }
}
