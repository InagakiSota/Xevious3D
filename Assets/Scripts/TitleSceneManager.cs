using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;
using UnityEngine.UI;

public class TitleSceneManager : MonoBehaviour
{
	//�O���t�@�C���̃p�X
	private string path;
	//�t�@�C����
	private string fileName = "hiScore.txt";
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

		path = Application.dataPath + "/ScoreData/" + fileName;
		//�O���t�@�C���̓ǂݍ���
		ReadFile();
		//�O���t�@�C���̃n�C�X�R�A���Q�[�������傫����΃n�C�X�R�A���㏑��
		if(hiScoreData > ShareData.Instance.hiScore)
		{
			ShareData.Instance.hiScore = hiScoreData;
		}
		//�Q�[�����̃n�C�X�R�A���O���t�@�C���̃n�C�X�R�A�����傫����Ώ㏑��
		else 
		{
			WriteFile(ShareData.Instance.hiScore.ToString());
		}	
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
				BLIND_INTERVAL = 0.1f;
				FadeManager.FadeOut("PlayScene");
				//SE�Đ�
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

	void WriteFile(string txt)
	{
		//FileInfo fi = new FileInfo(path);

		File.WriteAllText(path,txt);
	}

	void ReadFile()
	{
		FileInfo fi = new FileInfo(path);
		try
		{
			using (StreamReader sr = new StreamReader(fi.OpenRead(), Encoding.UTF8))
			{
				string readTxt = sr.ReadToEnd();
				hiScoreData = Convert.ToInt32(readTxt);
				Debug.Log(hiScoreData);
			}
		}
		catch (Exception e)
		{
			Debug.Log(e);
		}
	}
}
