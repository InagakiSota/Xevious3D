using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrandEnemy01Controller : MonoBehaviour
{
	//獲得スコア
	[SerializeField] int m_score;

	//オーディオソース
	AudioSource m_audio;

	//爆発音
	[SerializeField] AudioClip m_bombSE;

	//爆発のパーティクル
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
		//プレイヤーの弾にヒット
		if(collision.gameObject.tag == "PlayerBullet")
		{
			//消滅
			Destroy(gameObject);
			//爆発音再生
			m_audio.PlayOneShot(m_bombSE);
			//爆発のパーティクル再生
			var exlotion = Instantiate(m_bomb, transform.position, Quaternion.identity);
			var explotsionParticle = exlotion.GetComponent<ParticleSystem>();
			explotsionParticle.Play();

			//スコア加算
			ShareData.Instance.score += m_score;

		}
	}
}
