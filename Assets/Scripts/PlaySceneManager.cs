using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaySceneManager : MonoBehaviour
{
	//ゲーム開始時のテキスト
	[SerializeField] GameObject m_startText;

	//残機数のテキスト
	[SerializeField] Text m_lifeText;

	//ゲームオーバーのテキスト
	[SerializeField] GameObject m_gameOverText;

	//スコアのテキスト
	[SerializeField] Text m_scoreText;
	//ハイスコアのテキスト
	[SerializeField] Text m_hiScoreText;

	//ゲーム開始時のテキストを消す秒数
	[SerializeField] float TEXT_DESTROY_SECONDS;
	//上部の残機数のテキスト
	[SerializeField] Text m_lifeTextTop;

	//UI
	[SerializeField] GameObject m_UI;

	//残機数のUI
	[SerializeField] GameObject m_lifeUI;

	//経過時間
	private float m_totalTime;

	//オーディオソース
	private AudioSource m_audio;
	//イントロのBGM
	[SerializeField] AudioClip m_introBGM;
	//BGM
	[SerializeField] AudioClip m_BGM;

	//1位のスコアテキスト
	[SerializeField] Text m_1stScoreText;
	//2位のスコアテキスト
	[SerializeField] Text m_2ndScoreText;
	//3位のスコアテキスト
	[SerializeField] Text m_3rdScoreText;

	//マニュアルのテキスト
	[SerializeField] GameObject m_manual;

	//ゲームオーバーフラグ
	public static bool m_isGameOver;

	//ランキングの描画タイマー
	private float m_rankingDrawTimer;

	// Start is called before the first frame update
	void Start()
	{
		//フェードイン
		FadeManager.FadeIn();
		//残機数をテキストに反映
		m_lifeText.text = ShareData.Instance.life.ToString();
		//ゲーム開始時のテキストを指定秒数後に消す
		Destroy(m_startText, TEXT_DESTROY_SECONDS);
		
		m_gameOverText.SetActive(false);

		//UIを非表示にする
		m_UI.SetActive(false);
		m_manual.SetActive(false);

		m_totalTime = 0.0f;

		m_isGameOver = false;


		//オーディオソースの取得
		m_audio = this.GetComponent<AudioSource>();
		//イントロ再生
		if(m_audio.isPlaying == false)
			m_audio.PlayOneShot(m_introBGM);

		m_rankingDrawTimer = 0.0f;

	}

	// Update is called once per frame
	void Update()
	{

		//残機がなくなったら残機数のUIを消す
		if(ShareData.Instance.life < 0)
		{
			m_lifeUI.SetActive(false);
		}
		//残機が無くなったらゲームオーバー
		if (m_isGameOver == true)
		{
			//表示されるUIを切り替える
			m_gameOverText.SetActive(true);
			m_manual.SetActive(false);
			m_UI.SetActive(true);

			//ハイスコア用
			//ハイスコアかの判定をしハイスコアなら更新する
			ShareData.Instance.UpdateHighScore();

			//スコアをテキストに反映
			//3位から順に描画されるようにする
			//ランキング描画用のタイマーを加算
			m_rankingDrawTimer += Time.deltaTime;

			//1位描画
			if(m_rankingDrawTimer >= 0.5f * 3.0f)
			{
				//何かしらのキーが入力されたらタイトルに戻る
				if (Input.anyKeyDown)
				{
					FadeManager.FadeOut("TitleScene");
				}
				
				m_1stScoreText.text = "1st　" + ShareData.Instance.ranking[0];

			}
			//2位描画
			else if(m_rankingDrawTimer >= 0.5f * 2.0f)
			{
				m_2ndScoreText.text = "2nd　" + ShareData.Instance.ranking[1];
			}
			//3位描画
			else if(m_rankingDrawTimer >= 0.5f * 1.0f)
			{
				m_3rdScoreText.text = "3rd　" + ShareData.Instance.ranking[2];
			}



			//BGM停止
			if (m_audio.isPlaying == true)
			{
				m_audio.Stop();
			}

		}

		//指定秒数後にUI等を描画
		m_totalTime += Time.deltaTime;
		if (m_totalTime >= TEXT_DESTROY_SECONDS && ShareData.Instance.life >= 0)
		{
			m_UI.SetActive(true);
			m_manual.SetActive(true);
		}

		//テキストにスコアを反映
		m_scoreText.text = ShareData.Instance.score.ToString();

		//もしスコアがハイスコアを上回ったら現在のスコアをハイスコアにする
		if(ShareData.Instance.score >= ShareData.Instance.hiScore)
		{
			ShareData.Instance.hiScore = ShareData.Instance.score;
		}
		//テキストにハイスコアを反映
		m_hiScoreText.text = ShareData.Instance.hiScore.ToString();

		//残機数をテキストに反映
		m_lifeTextTop.text = "x" + ShareData.Instance.life;


		if(m_audio.isPlaying == false)
		{
			m_audio.loop = true;
			m_audio.PlayOneShot(m_BGM);
		}
	}


}
