using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    public int lives = 9;
    bool clapped;
    bool moved;

    private void Start()
    {
        InvokeRepeating("PopUpCheck", 1, 1f);
    }

    void PopUpCheck()
    {
        if (moved && GameManager.instance.movePopup != null)
            Destroy(GameManager.instance.movePopup);
        if (clapped && GameManager.instance.shootPopup != null)
            Destroy(GameManager.instance.shootPopup);

        if (clapped && moved)
            CancelInvoke("PopUpCheck");
    }

    void Update()
    {
        if(IOManager.distanceLeft >= -20)
        {
            if (!moved)
                moved = true;

            if(!(transform.position.x <= -10))
            {
                transform.position += new Vector3(IOManager.distanceLeft * (moveSpeed / 10) * Time.deltaTime, 0);
                Debug.Log("Player move left!");
            }
        }

        if(IOManager.distanceRight <= 20 )
        {
            if (!moved)
                moved = true;

            if(!(transform.position.x >= 10))
            {
                transform.position += new Vector3(IOManager.distanceRight * (moveSpeed / 10) * Time.deltaTime, 0);
                Debug.Log("Player move right!");
            }
        }
    }
}