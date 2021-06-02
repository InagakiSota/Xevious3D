using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySceneManager : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{
		FadeManager.FadeIn();
	}

	// Update is called once per frame
	void Update()
	{
	    if(Input.GetKeyDown(KeyCode.Space))	
		{
			FadeManager.FadeOut("TitleScene");
		}
	}
}
