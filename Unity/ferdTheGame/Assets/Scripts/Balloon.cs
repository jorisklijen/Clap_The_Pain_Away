using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    Animator anim;
    [SerializeField] GameObject vfx;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemey"))
            anim.SetTrigger("Explode");
    }

    void Die()
    {
        GameManager.player.lives -= 1;
        Instantiate(vfx, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
