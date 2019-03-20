using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    Animator anim;
    public int lives = 9;
    bool clapped;
    bool moved;
    [SerializeField] float desiredLevel = 70;

    private void Start()
    {
        InvokeRepeating("PopUpCheck", 1, 1f);
        anim = GetComponent<Animator>();
    }

    void PopUpCheck()
    {
        if (moved && GameManager.instance.movePopup != null && GameManager.instance.gameReady)
            Destroy(GameManager.instance.movePopup);
        if (clapped && GameManager.instance.shootPopup != null && GameManager.instance.gameReady)
            Destroy(GameManager.instance.shootPopup);

        if (clapped && moved)
            CancelInvoke("PopUpCheck");
    }

    void Update()
    {
        if(IOManager.distanceLeft >= -20)
        {
            if (!moved && Time.time > 1)
                moved = true;

            if(!(transform.position.x <= -10))
            {
                transform.position += new Vector3(IOManager.distanceLeft * (moveSpeed / 10) * Time.deltaTime, 0);
                //Debug.Log("Player move left!");
            }
        }

        if(IOManager.distanceRight <= 20 )
        {
            if (!moved && Time.time > 1)
                moved = true;

            if(!(transform.position.x >= 10))
            {
                transform.position += new Vector3(IOManager.distanceRight * (moveSpeed / 10) * Time.deltaTime, 0);
                //Debug.Log("Player move right!");
            }
        }

        if(Input.GetKeyDown(KeyCode.J) || IOManager.audioLevel > desiredLevel && !clapped)
        {
            Clap();
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.up * 100, out hit, 50))
            {
                Debug.Log("Firing raycast");
                if (hit.transform.CompareTag("Enemy"))
                {
                    Debug.Log("I hit an enemy!");
                }
            }
        }

        Debug.DrawRay(transform.position, Vector3.up * 50, Color.blue, 1);

        if(IOManager.audioLevel > 90)
        {
            StartCoroutine(IOManager.instance.TakePhoto());
        }
    }

    void ResetClapper()
    {
        clapped = false;
    }

    void Clap()
    {
        clapped = true;
        if(GameManager.instance.shootPopup)
        {
            PopUpCheck();
        }

        anim.SetTrigger("Clap");
        Debug.Log("Clapping!");
    }
}