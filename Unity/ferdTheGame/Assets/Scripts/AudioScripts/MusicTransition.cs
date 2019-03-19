///weet niet persee of deze script wel nodig is nu ik t gadaan heb via corutines
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MusicTransition : MonoBehaviour
{
	private static MusicTransition instance;
	void Awake()
	{	if (instance == null){
			instance = this;
			DontDestroyOnLoad(instance);
		}else
			Destroy(gameObject);
	}
}