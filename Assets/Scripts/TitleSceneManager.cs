using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSceneManager : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{
		//�t�F�[�h�C��
		FadeManager.FadeIn();
		//�c�@���ő�l�ɐݒ肳����
		ShareData.Instance.life = 2;
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