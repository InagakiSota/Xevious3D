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
	[SerializeField] GameObject m_gameOverText;

	//�X�R�A�̃e�L�X�g
	[SerializeField] Text m_scoreText;
	//�n�C�X�R�A�̃e�L�X�g
	[SerializeField] Text m_hiScoreText;

	//�Q�[���J�n���̃e�L�X�g�������b��
	[SerializeField] float TEXT_DESTROY_SECONDS;
	//�㕔�̎c�@���̃e�L�X�g
	[SerializeField] Text m_lifeTextTop;

	//UI
	[SerializeField] GameObject m_UI;

	//�c�@����UI
	[SerializeField] GameObject m_lifeUI;

	//�o�ߎ���
	private float m_totalTime;

	//�I�[�f�B�I�\�[�X
	private AudioSource m_audio;
	//�C���g����BGM
	[SerializeField] AudioClip m_introBGM;
	//BGM
	[SerializeField] AudioClip m_BGM;

	//1�ʂ̃X�R�A�e�L�X�g
	[SerializeField] Text m_1stScoreText;
	//2�ʂ̃X�R�A�e�L�X�g
	[SerializeField] Text m_2ndScoreText;
	//3�ʂ̃X�R�A�e�L�X�g
	[SerializeField] Text m_3rdScoreText;

	//�}�j���A���̃e�L�X�g
	[SerializeField] GameObject m_manual;

	//�Q�[���I�[�o�[�t���O
	public static bool m_isGameOver;

	//�����L���O�̕`��^�C�}�[
	private float m_rankingDrawTimer;

	// Start is called before the first frame update
	void Start()
	{
		//�t�F�[�h�C��
		FadeManager.FadeIn();
		//�c�@�����e�L�X�g�ɔ��f
		m_lifeText.text = ShareData.Instance.life.ToString();
		//�Q�[���J�n���̃e�L�X�g���w��b����ɏ���
		Destroy(m_startText, TEXT_DESTROY_SECONDS);
		

		//UI���\���ɂ���
		m_UI.SetActive(false);
		m_manual.SetActive(false);
		m_gameOverText.SetActive(false);

		//�e�ϐ��̏�����
		m_totalTime = 0.0f;
		m_isGameOver = false;
		m_rankingDrawTimer = 0.0f;

		//�I�[�f�B�I�\�[�X�̎擾
		m_audio = this.GetComponent<AudioSource>();
		//�C���g���Đ�
		if(m_audio.isPlaying == false)
			m_audio.PlayOneShot(m_introBGM);


	}

	// Update is called once per frame
	void Update()
	{

		//�c�@���Ȃ��Ȃ�����c�@����UI������
		if(ShareData.Instance.life < 0)
		{
			m_lifeUI.SetActive(false);
		}
		//�c�@�������Ȃ�����Q�[���I�[�o�[
		if (m_isGameOver == true)
		{
			//�\�������UI��؂�ւ���
			m_gameOverText.SetActive(true);
			m_manual.SetActive(false);
			m_UI.SetActive(true);

			//�n�C�X�R�A�p
			//�n�C�X�R�A���̔�������n�C�X�R�A�Ȃ�X�V����
			ShareData.Instance.UpdateHighScore();

			//�X�R�A���e�L�X�g�ɔ��f
			//3�ʂ��珇�ɕ`�悳���悤�ɂ���
			//�����L���O�`��p�̃^�C�}�[�����Z
			m_rankingDrawTimer += Time.deltaTime;

			//1�ʕ`��
			if(m_rankingDrawTimer >= 0.5f * 3.0f)
			{
				//��������̃L�[�����͂��ꂽ��^�C�g���ɖ߂�
				if (Input.anyKeyDown)
				{
					FadeManager.FadeOut("TitleScene");
				}
				
				m_1stScoreText.text = "1st�@" + ShareData.Instance.ranking[0];

			}
			//2�ʕ`��
			else if(m_rankingDrawTimer >= 0.5f * 2.0f)
			{
				m_2ndScoreText.text = "2nd�@" + ShareData.Instance.ranking[1];
			}
			//3�ʕ`��
			else if(m_rankingDrawTimer >= 0.5f * 1.0f)
			{
				m_3rdScoreText.text = "3rd�@" + ShareData.Instance.ranking[2];
			}



			//BGM��~
			if (m_audio.isPlaying == true)
			{
				m_audio.Stop();
			}

		}

		//�w��b�����UI����`��
		m_totalTime += Time.deltaTime;
		if (m_totalTime >= TEXT_DESTROY_SECONDS && ShareData.Instance.life >= 0)
		{
			m_UI.SetActive(true);
			m_manual.SetActive(true);
		}

		//�e�L�X�g�ɃX�R�A�𔽉f
		m_scoreText.text = ShareData.Instance.score.ToString();

		//�����X�R�A���n�C�X�R�A���������猻�݂̃X�R�A���n�C�X�R�A�ɂ���
		if(ShareData.Instance.score >= ShareData.Instance.hiScore)
		{
			ShareData.Instance.hiScore = ShareData.Instance.score;
		}
		//�e�L�X�g�Ƀn�C�X�R�A�𔽉f
		m_hiScoreText.text = ShareData.Instance.hiScore.ToString();

		//�c�@�����e�L�X�g�ɔ��f
		m_lifeTextTop.text = "x" + ShareData.Instance.life;

		//�C���g���̍Đ����I�������BGM�����[�v�ōĐ�����
		if(m_audio.isPlaying == false)
		{
			m_audio.loop = true;
			m_audio.PlayOneShot(m_BGM);
		}
	}


}
