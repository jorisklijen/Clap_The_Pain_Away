using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SeneManager : MonoBehaviour
{
	public string seneName;
	[Space]
	public float WaitTime;
	public Animator musicAnim;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
			StartCoroutine(ChangeSene());
	}

	IEnumerator ChangeSene()
	{
		musicAnim.SetTrigger("fadeOut");
		yield return new WaitForSeconds(WaitTime);
		SceneManager.LoadScene(seneName);
	}
}