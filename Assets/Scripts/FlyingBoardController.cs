using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingBoardController : MonoBehaviour
{
	//速さ
	[SerializeField]
	float speed = 0.2f;

	//回転する速さ
	[SerializeField]
	float rotateSpeed = -5.0f;

	[SerializeField]
	Vector3 destroyPos = new Vector3(30.0f, 20.0f, -20.0f);

	//オーディオソース
	AudioSource m_audio;

	//弾ヒット音
	[SerializeField] AudioClip m_hitSE;


	// Start is called before the first frame update
	void Start()
	{
		m_audio = GameObject.Find("Audio Source").GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update()
	{
		//速度設定
		Vector3 vel = new Vector3(0, 0, speed);
		//動く
		transform.position -= vel * Time.deltaTime;
		//回転
		transform.Rotate(rotateSpeed * Time.deltaTime, 0, 0);

		//消去
		if (transform.position.z <= destroyPos.z)
		{
			Destroy(this.gameObject);
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		//プレイヤーの弾が当たったらSEを再生する
		if(collision.gameObject.tag == "PlayerBullet")
		{
			m_audio.PlayOneShot(m_hitSE);
		}            
	}

}
