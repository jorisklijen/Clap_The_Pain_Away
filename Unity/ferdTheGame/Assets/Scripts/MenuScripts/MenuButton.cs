using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
	[SerializeField] MennuButtonControler mennuButtonControler;
	[SerializeField] Animator animator;
	[SerializeField] AnimatorFunctions animatorFunctions;
	[SerializeField] int thisIndex;
	[Space]
	public float desiredLevel = 70;


	// Update is called once per frame
	void Update()
	{
		if (mennuButtonControler.index == thisIndex)
		{
			animator.SetBool("isSelected", true);

			//hiero klap regestratie
			if (Input.GetAxisRaw("Submit") == 1 || IOManager.audioLevel > desiredLevel)
			{
				animator.SetBool("isPressed", true);
			}
			else if (animator.GetBool("isPressed"))
			{
				animator.SetBool("isPressed", false);
				animatorFunctions.disableOnce = true;
			}
		}
		else
		{
			animator.SetBool("isSelected", false);
		}
	}
}
