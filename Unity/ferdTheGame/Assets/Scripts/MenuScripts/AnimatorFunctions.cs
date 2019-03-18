using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorFunctions : MonoBehaviour
{
	[SerializeField] MennuButtonControler mennuButtonControler;
	public bool disableOnce;

	void PlaySound(AudioClip whichSound)
	{
		if (!disableOnce)
		{
			mennuButtonControler.audioSource.PlayOneShot(whichSound);
		}
		else
		{
			disableOnce = false;
		}
	}

}
