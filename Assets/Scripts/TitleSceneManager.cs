using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;
using UnityEngine.UI;

public class TitleSceneManager : MonoBehaviour
{
	//�O���t�@�C���̃n�C�X�R�A
	private int hiScoreData;

	//�n�C�X�R�A�̃e�L�X�g
	[SerializeField] Text m_hiScoreText;

	//UI
	[SerializeField] GameObject m_UI;

	//����Y���W
	[SerializeField] float m_startPosY;

	//pushButton�̃e�L�X�g
	[SerializeField] GameObject m_pushButtonText;

	//�I�[�f�B�I�\�[�X
	AudioSource m_audio;

	//���艹
	[SerializeField] AudioClip m_decideSound;

	//�_�ł̃t���O
	private bool m_isBlind = true;
	//�_�ł̃^�C�}�[
	private float m_blindTimer = 0.0f;
	//�_�ł̃C���^�[�o��
	[SerializeField] float BLIND_INTERVAL;

	//UI�̍��W
	Vector3 m_UIPos;
	
	// Start is called before the first frame update
	void Start()
	{          
		//�X�R�A�����Z�b�g
		ShareData.Instance.score = 0;

		//�n�C�X�R�A�p
		//�O���t�@�C���̓ǂݍ���
		ShareData.Instance.Start();

		//�n�C�X�R�A�p
		//�X�R�A�X�V�����Ȃ��Ȃ�Ƃ���false�ɂ���(UpdateHighScore���Ȃ�ǂ��Ăяo����Ȃ��悤�ɂ��邽��)
		ShareData.Instance.FinishUpdateHighScore = false;

		//�n�C�X�R�A�p
		//��ʂ�hiScore�ɓ����
		ShareData.Instance.hiScore = ShareData.Instance.ranking[0];
		//�t�F�[�h�C��
		FadeManager.FadeIn();
		//�c�@���ő�l�ɐݒ肳����
		ShareData.Instance.life = 2;

		//UI�̏������W�ݒ�
		m_UI.transform.position = new Vector3(m_UI.transform.position.x, m_startPosY, m_UI.transform.position.z);
		//UI�̍��W�擾
		m_UIPos = m_UI.transform.position;

		m_audio = GetComponent<AudioSource>();


	}

	// Update is called once per frame
	void Update()
	{
		//�n�C�X�R�A�̃e�L�X�g�𔽉f
		m_hiScoreText.text = ShareData.Instance.hiScore.ToString();

		if(m_UIPos.y < 210.0f)
		{
			m_UIPos.y += 100.0f * Time.deltaTime;

			if(Input.anyKeyDown)
			{
				m_UIPos.y = 210.0f;
			}
		}
		else
		{
			if (Input.anyKeyDown)
			{
				//�_�ł̊Ԋu�𑁂߂�
				BLIND_INTERVAL = 0.1f;
				FadeManager.FadeOut("PlayScene");
				//SE�Đ�
				if(m_audio.isPlaying == false)
					m_audio.PlayOneShot(m_decideSound);
			}

		}
		m_UI.transform.position = m_UIPos;

		//�_�ł̃^�C�}�[���Z
		m_blindTimer += Time.deltaTime;
		if(m_blindTimer >= BLIND_INTERVAL)
		{
			m_isBlind = !m_isBlind;
			m_blindTimer = 0.0f;
		}
		m_pushButtonText.SetActive(m_isBlind);

	}

}
