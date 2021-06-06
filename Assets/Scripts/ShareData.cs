using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System;

/// <summary>
/////スコアの書き込み方
//ShareData.Instance.score = 値;
//スコアの読み込み方
//変数 = ShareData.Instance.score;
/// </summary>

public class ShareData : MonoBehaviour
{
	public static readonly ShareData Instance = new ShareData();

	//外部ファイルのパス
	private string path;
	//ファイル名
	private string fileName = "hiScore.txt";


	public int score = 0;//スコア

	//ハイスコア
	public int hiScore = 0;


	public int life = 2;

	//ハイスコア用===========================================
	//ランキングの登録数
	const int RANKING_NUM = 3;

	//ランキング [0]一位、[1]二位、[2]三位
	public int[] ranking = new int[RANKING_NUM];

	//スコア更新が終わったか(現状シーンの開始時にfalasにすればOK)
	public bool FinishUpdateHighScore;


	// Start is called before the first frame update
	public void Start()
	{
		//ハイスコア用===========================================
		FinishUpdateHighScore = false;
		path = Application.dataPath + "/ScoreData/" + fileName;
		ShareData.Instance.ReadFile();
		//ここまで===================================================
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	//ハイスコア用===========================================
	//スコアを更新するときに呼び出す
	public void UpdateHighScore()
	{
		//更新後であればもう更新しない
		if (FinishUpdateHighScore == true)
		{
			return;
		}
		//ハイスコア更新1位の場合
		if (ranking[0] < ShareData.Instance.score)
		{
			ranking[2] = ranking[1];
			ranking[1] = ranking[0];
			ranking[0] = ShareData.Instance.score;
			Debug.Log("1位");
		}
		//ハイスコア更新2位の場合
		else if (ranking[1] < ShareData.Instance.score && ranking[0] >= ShareData.Instance.score)
		{
			ranking[2] = ranking[1];
			ranking[1] = ShareData.Instance.score;
			Debug.Log("2位");
		}
		//ハイスコア更新3位の場合
		else if (ranking[2] < ShareData.Instance.score && ranking[1] >= ShareData.Instance.score)
		{

			ranking[2] = ShareData.Instance.score;
			Debug.Log("3位");
		}


		//===========================================
		//書き込まれるデータ
		//===========================================
		//1位のスコア
		//2位のスコア
		//3位のスコア
		//===========================================

		//ファイル書き込み
		WriteFile(ranking[0].ToString() + "\n" + ranking[1].ToString() + "\n" + ranking[2].ToString() + "\n");

		//更新後にする
		FinishUpdateHighScore = true;
	}

	void WriteFile(string txt)
	{
		//FileInfo fi = new FileInfo(path);

		File.WriteAllText(path, txt);
	}

	void ReadFile()
	{
		FileInfo fi = new FileInfo(path);
		try
		{
			using (StreamReader sr = new StreamReader(fi.OpenRead(), Encoding.UTF8))
			{
				int i = 0;
				//1行ずつ読み込む
				while (sr.Peek() != -1)
				{
					string readTxt = sr.ReadLine();

					ranking[i] = Convert.ToInt32(readTxt);
					Debug.Log(ranking[i]);
					i++;
				}
			}
		}
		catch (Exception e)
		{
			Debug.Log(e);
		}
	}

}
