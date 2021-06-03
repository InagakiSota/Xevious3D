using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections;
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
	
	// Start is called before the first frame update
	void Start()
	{
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

	}

	// Update is called once per frame
	void Update()
	{
		//ハイスコアのテキストを反映
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
