using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoHud : MonoBehaviour
{
    public GameObject[] swords;
    PlayerMovement player;

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        SetAmmo();
    }

    public void SetAmmo()
    {
        for (int i = 0; i < swords.Length; i++)
        {
            //Image sr = swords[i].GetComponent<Image>();
            GameObject sword = swords[i];
            if (i > player.slashesLeft-1)
            {
                sword.SetActive(false);
                //sr.sprite = emptySprite;
            }
            else
            {
                sword.SetActive(true);
                //sr.sprite = fillSprite;
            }
        }
    }
}
