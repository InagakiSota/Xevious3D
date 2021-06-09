using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionPlayer : MonoBehaviour
{


	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	private void OnParticleSystemStopped()
	{
		//残機数が0未満でなければプレイシーンをリロード
		if (ShareData.Instance.life >= 0)
		{
			FadeManager.FadeOut("PlayScene");
		}
		//残機数が0ならゲームオーバーのフラグを立てる
		else
		{
			PlaySceneManager.m_isGameOver = true;
		}

	}
}
