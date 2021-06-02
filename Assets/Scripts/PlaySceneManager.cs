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

	// Start is called before the first frame update
	void Start()
	{
		//フェードイン
		FadeManager.FadeIn();
		//残機数をテキストに反映
		m_lifeText.text = ShareData.Instance.life.ToString();
		//ゲーム開始時のテキストを5秒後に消す
		Destroy(m_startText, 5.0f);
	}

	// Update is called once per frame
	void Update()
	{
	    if(Input.GetKeyDown(KeyCode.Space))	
		{
			FadeManager.FadeOut("TitleScene");
		}
	}
}
