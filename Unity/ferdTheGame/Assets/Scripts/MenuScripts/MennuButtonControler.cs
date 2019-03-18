using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MennuButtonControler : MonoBehaviour
{
	public int index;
	[SerializeField] bool keyDown;
	[SerializeField] int maxIndex;
	public AudioSource audioSource;

	// Start is called before the first frame update
	void Start()
    {
		audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetAxisRaw("Vertical") != 0)
		{
			if (!keyDown)
			{

				//hiero input voor de sensors
				if (Input.GetAxisRaw("Vertical") < 0)
				{
					if (index < maxIndex)
					{
						index++;
					}
					else
					{
						index = 0;
					}


				}
				//hiero input voor de sensors
				else if (Input.GetAxisRaw("Vertical") > 0)
				{
					if (index > 0)
					{
						index--;
					}
					else
					{
						index = maxIndex;
					}
				}
				keyDown = true;
			}
		}
		else {
			keyDown = false;
		}
    }
}
