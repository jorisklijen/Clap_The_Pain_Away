﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    Animator anim;
    [SerializeField] GameObject vfx;
    [SerializeField] int index;

    private void Start()
    {
        anim = GetComponent<Animator>();
        Invoke("FindMyIndex", 1.0f);
    }

    void FindMyIndex()
    {
        for(int i = 0; i < GameManager.instance.lives.Length; i++)
        {
            if(GameManager.instance.lives[i] == this)
            {
                index = i+1;
                gameObject.name = "Life " + index;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemey"))
            anim.SetTrigger("Explode");
    }

    void Die()
    {
        GameManager.instance.player.lives -= 1;
        Instantiate(vfx, transform.position, Quaternion.identity);
        //Time.timeScale += 0.1f;
        Destroy(gameObject);
    }
}
