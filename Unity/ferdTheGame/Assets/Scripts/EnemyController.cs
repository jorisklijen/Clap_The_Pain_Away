using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    
    public int exitScreenPoint;
    public int screenEndLeft;
    public int screenEndRight;
    public int variation;

    private SpriteRenderer spriteRenderer;

    private Vector2 spawnLocationLeft;
    private Vector2 spawnLocationRight;
    private int spawnLocationX;

    public GameObject thumbStickPrefab;

    // Start is called before the first frame update
    void Start()
    {
        spawnLocationLeft = new Vector2(screenEndLeft, 10);
        spawnLocationRight = new Vector2(screenEndRight, 10);
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y <= 0)
        {
            Instantiate(thumbStickPrefab, new Vector3(GetRandomSpawn(), 10, 0), new Quaternion());
            Instantiate(thumbStickPrefab, new Vector3(GetRandomSpawn(), 10, 0), new Quaternion());
            Destroy(gameObject);
        }
    }

    int GetRandomSpawn()
    {
        Random rnd = new Random();
        spawnLocationX = Random.Range(screenEndLeft, screenEndRight);
        return spawnLocationX;
    }
}
