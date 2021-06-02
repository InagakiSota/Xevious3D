using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSceneManager : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{
		//フェードイン
		FadeManager.FadeIn();
		//残機を最大値に設定させる
		ShareData.Instance.life = 3;
	}

	// Update is called once per frame
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			FadeManager.FadeOut("PlayScene");
		}
	}
}
