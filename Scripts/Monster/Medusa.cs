using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medusa : MonoBehaviour
{
    public Sprite[] sprites;
    public GameObject eyeLight;
    public GameObject winWindow;
    public bool live;

    float cooldown;
    int spriteIndex = 0;
    SpriteRenderer spriteRenderer;
    GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
        live = true;
        cooldown = 4f;
        player.transform.position = new Vector2(0.626f, -1.2f);
    }

    void Update()
    {
        if (live)
        {
            if (cooldown > 0)
            {
                cooldown -= Time.deltaTime;
            }
            else
            {
                //cast petrifaction
                eyeLight.GetComponent<Animator>().SetBool("Cast", true);
                eyeLight.GetComponent<AudioSource>().Play();

                cooldown = Random.Range(3f, 8f);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Bounceback")
        {
            Destroy(collision.collider.gameObject);
            if (spriteIndex < sprites.Length)
            {
                spriteRenderer.sprite = sprites[spriteIndex];
                spriteIndex += 1;
                if (spriteIndex == sprites.Length)
                {
                    StartCoroutine(Die());
                }
            }
        }
    }

    IEnumerator Die()
    {
        live = false;
        GameObject[] snakes = GameObject.FindGameObjectsWithTag("Flysnake");
        foreach (GameObject snake in snakes) Destroy(snake);
        yield return new WaitForSeconds(1f);
        Instantiate(winWindow, player.transform.position, Quaternion.identity);
        player.GetComponent<Controller>().enabled = false;
        player.GetComponent<BoxCollider2D>().enabled = false;
        yield return null;
    }
}
