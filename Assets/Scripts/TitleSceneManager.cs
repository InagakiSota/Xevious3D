using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;
using UnityEngine.UI;

public class TitleSceneManager : MonoBehaviour
{
	//外部ファイルのパス
	private string path;
	//ファイル名
	private string fileName = "hiScore.txt";
	//外部ファイルのハイスコア
	private int hiScoreData;

	//ハイスコアのテキスト
	[SerializeField] Text m_hiScoreText;

	//UI
	[SerializeField] GameObject m_UI;

	//初期Y座標
	[SerializeField] float m_startPosY;

	//pushButtonのテキスト
	[SerializeField] GameObject m_pushButtonText;

	//点滅のフラグ
	private bool m_isBlind = true;
	//点滅のタイマー
	private float m_blindTimer = 0.0f;
	//点滅のインターバル
	[SerializeField] float BLIND_INTERVAL;

	//UIの座標
	Vector3 m_UIPos;
	
	// Start is called before the first frame update
	void Start()
	{          
		//スコアをリセット
		ShareData.Instance.score = 0;

		path = Application.dataPath + "/ScoreData/" + fileName;
		//外部ファイルの読み込み
		ReadFile();
		//外部ファイルのハイスコアがゲーム内より大きければハイスコアを上書き
		if(hiScoreData > ShareData.Instance.hiScore)
		{
			ShareData.Instance.hiScore = hiScoreData;
		}
		//ゲーム内のハイスコアが外部ファイルのハイスコアよりも大きければ上書き
		else 
		{
			WriteFile(ShareData.Instance.hiScore.ToString());
		}	
		//フェードイン
		FadeManager.FadeIn();
		//残機を最大値に設定させる
		ShareData.Instance.life = 2;

		//UIの初期座標設定
		m_UI.transform.position = new Vector3(m_UI.transform.position.x, m_startPosY, m_UI.transform.position.z);
		//UIの座標取得
		m_UIPos = m_UI.transform.position;


	}

	// Update is called once per frame
	void Update()
	{
		//ハイスコアのテキストを反映
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
				FadeManager.FadeOut("PlayScene");
			}

		}
		m_UI.transform.position = m_UIPos;

		//点滅のタイマー加算
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
