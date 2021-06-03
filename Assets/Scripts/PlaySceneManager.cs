using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaySceneManager : MonoBehaviour
{
	//�Q�[���J�n���̃e�L�X�g
	[SerializeField] GameObject m_startText;

	//�c�@���̃e�L�X�g
	[SerializeField] Text m_lifeText;

	//�Q�[���I�[�o�[�̃e�L�X�g
	[SerializeField] Text m_gameOverText;

	//�X�R�A�̃e�L�X�g
	[SerializeField] Text m_scoreText;

	//�Q�[���J�n���̃e�L�X�g�������b��
	[SerializeField] float TEXT_DESTROY_SECONDS;
	//�㕔�̎c�@���̃e�L�X�g
	[SerializeField] Text m_lifeTextTop;

	//UI
	[SerializeField] GameObject m_UI;


	//�o�ߎ���
	float m_totalTime;



	// Start is called before the first frame update
	void Start()
	{
		//�t�F�[�h�C��
		FadeManager.FadeIn();
		//�c�@�����e�L�X�g�ɔ��f
		m_lifeText.text = ShareData.Instance.life.ToString();
		//�Q�[���J�n���̃e�L�X�g���w��b����ɏ���
		Destroy(m_startText, TEXT_DESTROY_SECONDS);
		
		m_gameOverText.enabled = false;

		//UI���\���ɂ���
		m_UI.SetActive(false);

		m_totalTime = 0.0f;

	}

	// Update is called once per frame
	void Update()
	{
		//�c�@�������Ȃ�����Q�[���I�[�o�[
		if(ShareData.Instance.life < 0)
		{
			m_gameOverText.enabled = true;
			m_UI.SetActive(false);
		}

	    if(Input.GetKeyDown(KeyCode.Space))	
		{
			FadeManager.FadeOut("TitleScene");
		}

		m_totalTime += Time.deltaTime;
		if (m_totalTime >= TEXT_DESTROY_SECONDS && ShareData.Instance.life >= 0)
		{
			m_UI.SetActive(true);
		}

		//�e�L�X�g�ɃX�R�A�𔽉f
		m_scoreText.text = ShareData.Instance.score.ToString();

		//�c�@�����e�L�X�g�ɔ��f
		m_lifeTextTop.text = "x" + ShareData.Instance.life;
	}


}