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
	float m_totalTime;

	//オーディオソース
	private AudioSource m_audio;
	//イントロのBGM
	[SerializeField] AudioClip m_introBGM;

	//1位のスコアテキスト
	[SerializeField] Text m_1stScoreText;
	//2位のスコアテキスト
	[SerializeField] Text m_2ndScoreText;
	//3位のスコアテキスト
	[SerializeField] Text m_3rdScoreText;

	//マニュアルのテキスト
	[SerializeField] GameObject m_manual;


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

		m_totalTime = 0.0f;


		//オーディオソースの取得
		m_audio = this.GetComponent<AudioSource>();
		//イントロ再生
		if(m_audio.isPlaying == false)
			m_audio.PlayOneShot(m_introBGM);

	}

	// Update is called once per frame
	void Update()
	{
		//残機が無くなったらゲームオーバー
		if(ShareData.Instance.life < 0)
		{
			m_gameOverText.SetActive(true);
			m_manual.SetActive(false);
			m_UI.SetActive(true);
			m_lifeUI.SetActive(false);

			if (Input.anyKeyDown)
			{
				FadeManager.FadeOut("TitleScene");
			}

			//ハイスコア用
			//ハイスコアかの判定をしハイスコアなら更新する
			ShareData.Instance.UpdateHighScore();

			//スコアをテキストに反映
			m_1stScoreText.text = "1st　" + ShareData.Instance.ranking[0];
			m_2ndScoreText.text = "2nd　" + ShareData.Instance.ranking[1];
			m_3rdScoreText.text = "3rd　" + ShareData.Instance.ranking[2];


		}


		m_totalTime += Time.deltaTime;
		if (m_totalTime >= TEXT_DESTROY_SECONDS && ShareData.Instance.life >= 0)
		{
			m_UI.SetActive(true);
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
	}


}
