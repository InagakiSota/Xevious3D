using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;
using UnityEngine.UI;

public class TitleSceneManager : MonoBehaviour
{
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

	//オーディオソース
	AudioSource m_audio;

	//決定音
	[SerializeField] AudioClip m_decideSound;

	//点滅のフラグ
	private bool m_isBlind = true;
	//点滅のタイマー
	private float m_blindTimer = 0.0f;
	//点滅のインターバル
	[SerializeField] float BLIND_INTERVAL;

	//UIが停止するY座標
	[SerializeField] float UI_STOP_POS_Y = -20.0f;

	//UIが移動するスピード
	[SerializeField] float UI_MOVE_SPEED = 100.0f;

	//UIの座標
	Vector3 m_UIPos;
	
	// Start is called before the first frame update
	void Start()
	{          
		//スコアをリセット
		ShareData.Instance.score = 0;

		//ハイスコア用
		//外部ファイルの読み込み
		ShareData.Instance.Start();

		//ハイスコア用
		//スコア更新をしなくなるときにfalseにする(UpdateHighScoreがなんども呼び出されないようにするため)
		ShareData.Instance.FinishUpdateHighScore = false;

		//ハイスコア用
		//一位をhiScoreに入れる
		ShareData.Instance.hiScore = ShareData.Instance.ranking[0];
		//フェードイン
		FadeManager.FadeIn();
		//残機を最大値に設定させる
		ShareData.Instance.life = 2;

		//UIの初期座標設定
		m_UI.transform.localPosition = new Vector3(0.0f, m_startPosY, m_UI.transform.position.z);
		//UIの座標取得
		m_UIPos = m_UI.transform.localPosition;

		m_audio = GetComponent<AudioSource>();


	}

	// Update is called once per frame
	void Update()
	{
		//ハイスコアのテキストを反映
		m_hiScoreText.text = ShareData.Instance.hiScore.ToString();

		//UIを指定座標まで移動させる
		if(m_UIPos.y < UI_STOP_POS_Y)
		{
			m_UIPos.y += UI_MOVE_SPEED * Time.deltaTime;

			//なにかしらのキーが入力されたらUIを指定座標に移動させる
			if(Input.anyKeyDown)
			{
				m_UIPos.y = UI_STOP_POS_Y;
			}
		}
		else
		{
			//UIの移動が完了した状態でなにかしらのキー入力でプレイシーンに遷移
			if (Input.anyKeyDown)
			{
				//点滅の間隔を早める
				BLIND_INTERVAL = 0.1f;
				FadeManager.FadeOut("PlayScene");
				//SE再生
				if(m_audio.isPlaying == false)
					m_audio.PlayOneShot(m_decideSound);
			}
		}

		//座標を代入
		m_UI.transform.localPosition = m_UIPos;


		//点滅のタイマー加算
		m_blindTimer += Time.deltaTime;
		//一定間隔で文字の点滅を繰り返す
		if(m_blindTimer >= BLIND_INTERVAL)
		{
			//点滅のフラグを反転させる
			m_isBlind = !m_isBlind;
			//点滅のタイマーリセット
			m_blindTimer = 0.0f;
		}
		m_pushButtonText.SetActive(m_isBlind);

	}

}
