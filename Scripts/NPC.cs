using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public GameObject dialogBox;
    public GameObject[] dialogs;

    private int dialogIndex;
    GameObject player;
    void Start()
    {
        player = GameObject.Find("Player");
        dialogBox.SetActive(false);
        foreach (GameObject i in dialogs) i.SetActive(false);
        dialogIndex = 0;
        player.transform.position = new Vector2(-8f, -3.3f);
    }

    void Update()
    {
        if (Vector2.Distance(player.transform.position, transform.position) > 0.72f)
        {
            dialogBox.SetActive(false);
        }
    }

    public void DisplayDialog()
    {
        if (dialogIndex < dialogs.Length)
        {
            dialogBox.SetActive(true);
            dialogs[dialogIndex].gameObject.SetActive(true);
            if (dialogIndex != 0)
            {
                dialogs[dialogIndex - 1].SetActive(false);
            }
            dialogIndex += 1;
        }
        else
        {
            dialogBox.SetActive(false);
            dialogs[dialogIndex - 1].SetActive(false);
            dialogIndex = 0;

            player.GetComponent<Controller>().haveMirror = true;
            player.GetComponent<Controller>().canMirror = true;
        }
    }
}
