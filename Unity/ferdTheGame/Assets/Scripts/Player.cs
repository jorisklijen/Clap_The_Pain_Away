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
    [Header("Dev settings")]
    [SerializeField] bool allowPictureTaking;
    [SerializeField] float desiredLevel = 70;
    [SerializeField] float rayWidth = 10;

    int boundsHorizontal;

    private void Start()
    {
        InvokeRepeating("PopUpCheck", 1, 1f);
        InvokeRepeating("UpdateBounds", 1, 0.05f);
        anim = GetComponent<Animator>();
    }

    void UpdateBounds()
    {
        boundsHorizontal = IOManager.GetScreenBoundsInWorldSpace()[0] - 2;
        //calculate horizontal bounds using a IOManager function that returns a int array, use element 0(horizontal) and take off 2 for an offset to make sure the player fits.
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
            if (!moved && GameManager.instance.gameReady)
                moved = true;

            if(!(transform.position.x <= boundsHorizontal * -1))
            {
                transform.position += new Vector3(IOManager.distanceLeft * (moveSpeed / 10) * Time.deltaTime, 0);
                //Debug.Log("Player move left!");
            }
        }

        if(IOManager.distanceRight <= 20 )
        {
            if (!moved && GameManager.instance.gameReady)
                moved = true;

            if(!(transform.position.x >= boundsHorizontal))
            {
                transform.position += new Vector3(IOManager.distanceRight * (moveSpeed / 10) * Time.deltaTime, 0);
                //Debug.Log("Player move right!");
            }
        }

        if(Input.GetKeyDown(KeyCode.J) || IOManager.audioLevel > desiredLevel && !clapped)
        {
            Clap();
        }

        if(Input.GetKeyDown(KeyCode.Space) || IOManager.audioLevel > 90  && allowPictureTaking)
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

        for (int i = 0; i < rayWidth; i++)
        {
            Vector3 pos = new Vector3(transform.position.x - rayWidth / 20 + (i * 0.1f), transform.position.y, transform.position.z);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.up, 10);
            Debug.DrawRay(pos, Vector3.up * 10, Color.blue, 1);
            if (hit.collider != null)
            {
                if (hit.transform.CompareTag("Enemy"))
                {
                    Debug.Log("I hit an enemy!");
                    Destroy(hit.transform.gameObject);
                }
            }
        }

        anim.SetTrigger("Clap");
    }
}