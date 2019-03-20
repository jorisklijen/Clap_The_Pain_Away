using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MennuButtonControler : MonoBehaviour
{
	public int index;
	[SerializeField] bool keyDown;
	[SerializeField] int maxIndex;
	public AudioSource audioSource;
	[Space(20f)]
	public float sec = 0.5f;


	// Start is called before the first frame update
	void Start()
    {
		audioSource = GetComponent<AudioSource>();
		StartCoroutine(MenuMove());
    }

	IEnumerator MenuMove()
	{
		while (true)
		{
			//hiero input voor de sensors
			if (Input.GetKeyDown(KeyCode.DownArrow)||(IOManager.distanceLeft >= -20))
			{
				if (index < maxIndex)
					index++;
				else
					index = 0;
				yield return new WaitForSeconds(sec);
			}
			//hiero input voor de sensors
			if (Input.GetKeyDown(KeyCode.UpArrow)||(IOManager.distanceRight <= 20))
			{
				if (index > 0)
					index--;
				else
					index = maxIndex;
				yield return new WaitForSeconds(sec);
			}
			keyDown = true;
			yield return null;
		}
	}

    // Update is called once per frame



/*    void nee()
    {
		if ((Input.GetAxisRaw("Vertical") != 0))
		{
			if (!keyDown)
			{
				//hiero input voor de sensors
				if (Input.GetKeyDown(KeyCode.DownArrow) || (IOManager.distanceLeft >= -20))
				{
					if (index < maxIndex)
						index++;
					else
						index = 0;
					//return;
				}
				//hiero input voor de sensors
				else if (Input.GetKeyDown(KeyCode.DownArrow) || (IOManager.distanceRight <= 20))
				{
					if (index > 0)
						index--;
					else
						index = maxIndex;
					//return;
				}
				keyDown = true;
			}
			keyDown = false;
		}
		else 
			keyDown = false;
    } */
}
