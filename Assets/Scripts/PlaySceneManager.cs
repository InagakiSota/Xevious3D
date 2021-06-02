using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaySceneManager : MonoBehaviour
{
	//�Q�[���J�n���̃e�L�X�g
	[SerializeField] GameObject m_startText;

	//�c�@���̃e�L�X�g
	[SerializeField] Text m_lifeText;

	// Start is called before the first frame update
	void Start()
	{
		//�t�F�[�h�C��
		FadeManager.FadeIn();
		//�c�@�����e�L�X�g�ɔ��f
		m_lifeText.text = ShareData.Instance.life.ToString();
		//�Q�[���J�n���̃e�L�X�g��5�b��ɏ���
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
