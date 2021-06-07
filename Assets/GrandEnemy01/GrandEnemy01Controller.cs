using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrandEnemy01Controller : MonoBehaviour
{
	//�l���X�R�A
	[SerializeField] int m_score;

	//�I�[�f�B�I�\�[�X
	AudioSource m_audio;

	//������
	[SerializeField] AudioClip m_bombSE;

	//�����̃p�[�e�B�N��
	[SerializeField] GameObject m_bomb;


	// Start is called before the first frame update
	void Start()
	{
		m_audio = GameObject.Find("Audio Source").GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	private void OnCollisionEnter(Collision collision)
	{
		//�v���C���[�̒e�Ƀq�b�g
		if(collision.gameObject.tag == "PlayerBullet")
		{
			//����
			Destroy(gameObject);
			//�������Đ�
			m_audio.PlayOneShot(m_bombSE);
			//�����̃p�[�e�B�N���Đ�
			var exlotion = Instantiate(m_bomb, transform.position, Quaternion.identity);
			var explotsionParticle = exlotion.GetComponent<ParticleSystem>();
			explotsionParticle.Play();

			//�X�R�A���Z
			ShareData.Instance.score += m_score;

		}
	}
}
