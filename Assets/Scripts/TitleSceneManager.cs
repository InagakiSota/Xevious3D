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

	}

	// Update is called once per frame
	void Update()
	{
		//�n�C�X�R�A�̃e�L�X�g�𔽉f
		m_hiScoreText.text = ShareData.Instance.hiScore.ToString();

		if(Input.GetKeyDown(KeyCode.Space))
		{
			FadeManager.FadeOut("PlayScene");
		}
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
