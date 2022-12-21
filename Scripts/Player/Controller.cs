using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public float speed;
    public bool canMove;
    public bool canShoot;
    public bool haveMirror = false;
    public bool canMirror = false;
    public GameObject fireball;
    public GameObject thunder;
    public GameObject mirror;
    public GameObject mirrorToken;
    public GameObject loseWindow;
    public GameObject shield;

    Animator animator;
    Rigidbody2D rig;
    Vector2 lookDirction = new Vector2(1, 0);
    float horizontal;
    float vertical;
    float cooldown;
    bool danger = false;

    private static Controller instance;
    public Controller ControllerInstance
    {
        get { return instance; }
    }

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        transform.position = new Vector2(-8f, 2.5f);
        canMove = true;
        canShoot = true;
        DontDestroyOnLoad(gameObject);
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

    void Update()
    {
        if (shield.GetComponent<Shield>().shieldStrenth > 0) danger = false;

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);

        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
        else
        {
            canMove = true;
            canShoot = true;
        }

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirction.Set(move.x, move.y);
            lookDirction.Normalize();
        }

        if (canMove)
        {
            animator.SetFloat("Move X", lookDirction.x);
            animator.SetFloat("Move Y", lookDirction.y);
            animator.SetFloat("speed", move.magnitude);
        }

        //cast fireball
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (canShoot)
            {
                Fireball(lookDirction);
                canShoot=false;
                canMove=false;
                rig.velocity = Vector2.zero;
                cooldown = 1f;
            }
        }

        //cast thunder
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (canShoot)
            {
                Thunder(lookDirction);
                canShoot = false;
                canMove = false;
                rig.velocity = Vector2.zero;
                cooldown = 0.6f;
            }
        }

        //talk to npc
        if (Input.GetKeyDown(KeyCode.C))
        {
            RaycastHit2D hit = Physics2D.Raycast(rig.position + Vector2.up * 0.128f, lookDirction, 0.32f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                if (hit.collider.tag == "NPC")
                {
                    NPC npc = hit.collider.GetComponent<NPC>();
                    if (npc != null)
                    {
                        npc.DisplayDialog();
                    }
                }
                else if (hit.collider.tag == "Board")
                {
                    Tut1 tut = hit.collider.GetComponent<Tut1>();
                    if (tut != null)
                    {
                        tut.DisplayInfo();
                    }
                }
            }
        }

        //use mirror
        if (haveMirror)
        {
            if (Input.GetKeyDown(KeyCode.V))
            {
                if (canMirror)
                {
                    canMirror = false;
                    StartCoroutine(useMirror());
                }
            }

            //affect UI
            if (canMirror)
            {
                mirrorToken.SetActive(true);
            }
            else
            {
                mirrorToken.SetActive(false);
            }
        }
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            Vector2 position = rig.position;

            position.x = position.x + speed * horizontal * Time.deltaTime;
            position.y = position.y + speed * vertical * Time.deltaTime;

            rig.MovePosition(position);
        }
        
    }

    void Fireball(Vector2 dir)
    {
        Vector2 offset = new Vector2(0, 0.128f);
        GameObject fireballObject = Instantiate(fireball, rig.position + offset + dir * 0.15f, Quaternion.identity);
        Fireball fireballScript = fireballObject.GetComponent<Fireball>();
        fireballScript.GetDirection(dir);
    }

    void Thunder(Vector2 dir)
    {
        Vector2 offset = new Vector2(0, 0.15f);
        Instantiate(thunder, rig.position + offset + dir * 0.22f, Quaternion.identity);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Petrifaction")
        {
            Destroy(collision.collider.gameObject);
            if (!mirror.activeInHierarchy)
            {
                Die();
            }
        }
        else if (collision.collider.tag == "Monster" || collision.collider.tag == "FlyMonster")
        {
            if (shield.GetComponent<Shield>().shieldStrenth <= 0)
            {
                if (danger)
                {
                    Die();
                }
                else
                {
                    danger = true;
                }
            }
        }
    }

    void Die()
    {
        shield.SetActive(false);
        GetComponent<BoxCollider2D>().enabled = false;
        Instantiate(loseWindow, transform.position, Quaternion.identity);
        rig.velocity = Vector3.zero;
        GetComponent<Controller>().enabled = false;
    }

    IEnumerator useMirror()
    {
        mirror.SetActive(true);
        yield return new WaitForSeconds(2f);
        mirror.SetActive(false);
        yield return new WaitForSeconds(3f);
        canMirror = true;
    }
}
