using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy01Controller : MonoBehaviour
{
	//プレイヤー
	GameObject player;

	//プレイヤーの座標
	Vector3 m_playerPos;

	//弾
	[SerializeField]
	GameObject bullet;

	//速さ
	[SerializeField]
	float speed = 0.3f;

	//回転速度
	[SerializeField]
	float rotateSpeed = 30.0f;

	//ライフ
	[SerializeField]
	float life = 1;

	//消去位置
	[SerializeField]
	Vector3 destroyPos = new Vector3(30.0f, 20.0f, -11.0f);

	//死んだとき貰えるスコア
	[SerializeField]
	int getScore;

	//爆発のパーティクル
	[SerializeField] GameObject m_explosionParticle;

	//追尾する物の位置
	Vector3 targetPosition;

	//追尾する物までのベクトル
	Vector3 targetVector;

	//打てるか
	bool shotFlag = true;

	//打つまでの時間
	float shotTimer;

	//何回打つか
	int shotNum = 1;

	//速度
	Vector3 vel = Vector3.zero;

	// Start is called before the first frame update
	void Start()
	{
		//プレイヤーを探す
		player = GameObject.Find("Player");
		//プレイヤーが存在していれば座標を代入
		if (player != null)
		{
			m_playerPos = player.transform.position;
		}
		//追尾先までの単位ベクトル
		targetVector = m_playerPos - this.transform.position;
		targetVector.Normalize();

		//追尾先の位置
		targetPosition = m_playerPos;

		//打つまでの時間をランダム設定
		shotTimer = Random.Range(0.0f, 0.5f);

		vel = Vector3.zero;

		shotFlag = true;
	}

	// Update is called once per frame
	void Update()
	{
		//打てる
		if (shotFlag == true)
		{
			//追尾先まで移動（追尾先は更新していないため追ってはこない）
			transform.position += targetVector * speed;

			//x座標が同じになったら
			//if ((int)this.transform.position.x == (int)player.transform.position.x)
			if (Mathf.Abs(transform.position.x - m_playerPos.x) <= 1.0f)
			{
				//打ちます
				shotFlag = false;
			}
		}

		else if (shotFlag == false)
		{
			//＋方向にいたら右方向の画面外に
			if (transform.position.x > 0.0f)
			{
				//加速度設定
				Vector3 accel = new Vector3(speed * 0.05f, 0.0f, speed * 0.01f);

				//加速
				vel += accel;
				transform.position += vel;

				//回転
				transform.Rotate(0.0f, 0.0f, -rotateSpeed);
			}
			//-方向にいたら左方向の画面外に
			else
			{
				//加速度設定
				Vector3 accel = new Vector3(-speed * 0.05f, 0.0f, speed * 0.01f);

				//加速
				vel += accel;
				transform.position += vel;

				//回転
				transform.Rotate(0.0f, 0.0f, rotateSpeed);
			}

			//タイマーが0になって弾があるなら
			if (shotTimer <= 0.0f && shotNum > 0 && Time.time >= 30.0f)
			{
				//銃口設定
				Vector3 shotPos = this.transform.position;
				shotPos.z -= 1.0f;

				//弾減らす
				shotNum--;

				if (player != null)
				{
					//弾生成
					Instantiate(bullet, shotPos, Quaternion.identity);
				}
			}
			else
			{
				shotTimer -= Time.deltaTime;
			}

		}


		//画面外まで行ったら消去
		if (transform.position.z <= destroyPos.z || Mathf.Abs(transform.position.x) >= destroyPos.x)
		{
			Destroy(this.gameObject);
		}
		
	}

	private void OnCollisionEnter(Collision collision)
	{
		//相手の弾当たったら
		if (collision.collider.tag == "PlayerBullet")
		{
			//爆発のパーティクル再生
			var exlotion = Instantiate(m_explosionParticle, transform.position, Quaternion.identity);
			var explotsionParticle = exlotion.GetComponent<ParticleSystem>();
			explotsionParticle.Play();


			//スコア加算
			ShareData.Instance.score += getScore;
			//消去
			Destroy(this.gameObject);
		}
	}
}
