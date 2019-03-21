using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Invoke("DestroyMe", 1);
    }

    void DestroyMe()
    {
        Destroy(gameObject);
    }

}
