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
		//�c�@����0�����łȂ���΃v���C�V�[���������[�h
		if (ShareData.Instance.life >= 0)
		{
			FadeManager.FadeOut("PlayScene");
		}
		//�c�@����0�Ȃ�Q�[���I�[�o�[�̃t���O�𗧂Ă�
		else
		{
			PlaySceneManager.m_isGameOver = true;
		}

	}
}
