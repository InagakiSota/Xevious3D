using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ザッパー：対空弾
/// ブラスター：対地弾
/// </summary>

public class PlayerController : MonoBehaviour
{
	//ザッパーの銃口　右
	[SerializeField] Transform m_zapperMuzzleRight;
	//ザッパーの銃口　左
	[SerializeField] Transform m_zapperMuzzleLeft;
	//ブラスターの銃口
	[SerializeField] Transform m_blasterMuzzle;
	//ザッパーのプレハブ
	[SerializeField] GameObject m_zapperPrefab;
	//ザッパー(青色)のプレハブ
	[SerializeField] GameObject m_zapperBluePrefab;
	//ブラスターのプレハブ
	[SerializeField] GameObject m_blasterPrefab;
	//ターゲット(通常)
	[SerializeField] GameObject m_target;
	//ターゲット2(ブラスター発射位置)
	[SerializeField] GameObject m_target2;
	//ターゲット3(ブラスター発射中)
	[SerializeField] GameObject m_target3;
	//ターゲット4(ターゲット上に敵がいる)
	[SerializeField] GameObject m_target4;
	//移動速度
	[SerializeField] float MOVE_SPEED;
	//ザッパーの発射速度
	[SerializeField] float ZAPPER_SHOT_SPEED;
	//ブラスターの発射速度
	[SerializeField] float BLASTER_SHOT_SPEED;
	//左右の移動限界値
	[SerializeField] float MOVE_LIMIT_SIDE;
	//前の移動限界値
	[SerializeField] float MOVE_LIMIT_FRONT;
	//後の移動限界値
	[SerializeField] float MOVE_LIMIT_BACK;
	//ブラスターの発射インターバル
	[SerializeField] float BLASTER_SHOT_INTERVAL;
	//ザッパーの発射インターバル
	[SerializeField] float ZAPPER_SHOT_INTERVAL;
	//爆発のパーティクル
	[SerializeField] GameObject m_explosion;
	//プレイヤーのモデル
	[SerializeField] GameObject m_playerModel;

	//オーディオソース
	AudioSource m_audio;
	//ザッパー用のオーディオソース
	AudioSource m_zapperAudio;
	//爆発のサウンド
	[SerializeField] AudioClip m_bombSound;
	//ザッパー発射のサウンド
	[SerializeField] AudioClip m_zapperSound;
	//ブラスター発射のサウンド
	[SerializeField] AudioClip m_blasterSound;
	//1up音
	[SerializeField] AudioClip m_1upSE;

	//移動量
	private Vector3 m_vel;
	//座標
	private Vector3 m_pos;
	//ブラスターの発射タイマー
	float m_blasterShotTimer;
	//ザッパーの発射タイマー
	float m_zapperShotTimer;

	//死亡フラグ
	bool m_isDeath;

	//ザッパーの色フラグ
	bool m_zapperColorFlag = false;

	RaycastHit m_hit;

	//地上のターゲットの座標
	private Vector3 m_targetPos;


	// Start is called before the first frame update
	void Start()
	{
		m_pos = this.transform.position;

		m_isDeath = false;

		m_audio = GameObject.Find("Audio Source").GetComponent<AudioSource>();

		m_zapperAudio = this.GetComponent<AudioSource>();

		//ハイスコア用
		//外部ファイルの読み込み
		ShareData.Instance.Start();
	}

	// Update is called once per frame
	void Update()
	{
		//移動処理
		Move();
		//ザッパーの発射
		ZapperShot();
		//ブラスターの発射
		BlasterShot();
		//ターゲットの処理
		Target();

		//死亡フラグが立ったら
		if(m_isDeath == true)
		{
			//爆発音再生
			m_audio.PlayOneShot(m_bombSound);
			//残機を1減らす
			ShareData.Instance.life = ShareData.Instance.life - 1;
			//消滅
			//Destroy(m_playerModel);
			Destroy(this.gameObject);

			//爆発のパーティクル再生
			var exlotion = Instantiate(m_explosion, transform.position, Quaternion.identity);
			var explotsionParticle = exlotion.GetComponent<ParticleSystem>();
			explotsionParticle.Play();

			//ターゲットを消す
			Destroy(m_target);
			Destroy(m_target2);
			Destroy(m_target3);
			Destroy(m_target4);

			//死亡フラグを消す
			m_isDeath = false;
		}

	}

	//移動処理
	void Move()
	{
		//上下入力
		m_vel.z = Input.GetAxis("Vertical");
		//左右入力
		m_vel.x = Input.GetAxis("Horizontal");


		//左右移動に合わせて機体を傾ける
		Vector3 axis = new Vector3(0.0f, 0.0f, 1.0f);
		float angle = -45.0f * m_vel.x;
		Quaternion q = Quaternion.AngleAxis(angle, axis);
		m_playerModel.transform.rotation = q;
		
		//正規化
		m_vel.Normalize();
		//移動量を座標に加算する
		m_pos += m_vel * MOVE_SPEED * Time.deltaTime;
		//クランプ関数を用いて、プレイヤーを画面内に収める
		m_pos.x = Mathf.Clamp(m_pos.x, -MOVE_LIMIT_SIDE, MOVE_LIMIT_SIDE);
		m_pos.z = Mathf.Clamp(m_pos.z, MOVE_LIMIT_BACK, MOVE_LIMIT_FRONT);

		this.transform.position = m_pos;

	}

	//ザッパー発射
	void ZapperShot()
	{
		//単発
		if (Input.GetButtonDown("Zapper"))
		{
			if (m_zapperColorFlag == false)
			{
				//ザッパー(右)を生成
				GameObject zapperRight = (GameObject)Instantiate(m_zapperPrefab, m_zapperMuzzleRight.position, Quaternion.identity);
				//ザッパー(左)を生成
				GameObject zapperLeft = (GameObject)Instantiate(m_zapperBluePrefab, m_zapperMuzzleLeft.position, Quaternion.identity);


				//前方に力を加える
				zapperRight.GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, 0.0f, ZAPPER_SHOT_SPEED), ForceMode.Impulse);
				zapperLeft.GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, 0.0f, ZAPPER_SHOT_SPEED), ForceMode.Impulse);
			}
			else
			{
				//ザッパー(右)を生成
				GameObject zapperRight = (GameObject)Instantiate(m_zapperBluePrefab, m_zapperMuzzleRight.position, Quaternion.identity);
				//ザッパー(左)を生成
				GameObject zapperLeft = (GameObject)Instantiate(m_zapperPrefab, m_zapperMuzzleLeft.position, Quaternion.identity);

				//前方に力を加える
				zapperRight.GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, 0.0f, ZAPPER_SHOT_SPEED), ForceMode.Impulse);
				zapperLeft.GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, 0.0f, ZAPPER_SHOT_SPEED), ForceMode.Impulse);
			}

			//ザッパーの色フラグを入れ替える
			m_zapperColorFlag = !m_zapperColorFlag;
			//発射音再生
			m_zapperAudio.PlayOneShot(m_zapperSound);

		}

		//連射
		if (Input.GetButton("ZapperRapit") && m_zapperShotTimer <= 0.0f)
		{
			if (m_zapperColorFlag == false)
			{
				//ザッパー(右)を生成
				GameObject zapperRight = (GameObject)Instantiate(m_zapperPrefab, m_zapperMuzzleRight.position, Quaternion.identity);
				//ザッパー(左)を生成
				GameObject zapperLeft = (GameObject)Instantiate(m_zapperBluePrefab, m_zapperMuzzleLeft.position, Quaternion.identity);

				//前方に力を加える
				zapperRight.GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, 0.0f, ZAPPER_SHOT_SPEED), ForceMode.Impulse);
				zapperLeft.GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, 0.0f, ZAPPER_SHOT_SPEED), ForceMode.Impulse);
			}
			else
			{             
				//ザッパー(右)を生成
				GameObject zapperRight = (GameObject)Instantiate(m_zapperBluePrefab, m_zapperMuzzleRight.position, Quaternion.identity);
				//ザッパー(左)を生成
				GameObject zapperLeft = (GameObject)Instantiate(m_zapperPrefab, m_zapperMuzzleLeft.position, Quaternion.identity);

				//前方に力を加える
				zapperRight.GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, 0.0f, ZAPPER_SHOT_SPEED), ForceMode.Impulse);
				zapperLeft.GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, 0.0f, ZAPPER_SHOT_SPEED), ForceMode.Impulse);


			}
			//連射のインターバル設定
			m_zapperShotTimer = ZAPPER_SHOT_INTERVAL;
			//ザッパーの色フラグを入れ替える
			m_zapperColorFlag = !m_zapperColorFlag;
			//発射音再生
			m_zapperAudio.PlayOneShot(m_zapperSound);

		}
		m_zapperShotTimer -= Time.deltaTime;
		if (m_zapperShotTimer <= 0.0f) m_zapperShotTimer = 0.0f;

	}

	//ブラスター発射
	void BlasterShot()
	{
		GameObject blasterBullet = GameObject.Find("BlasterPrefab(Clone)");

		//X入力かつ他のブラスター弾がなければ発射　
		if (Input.GetButtonDown("Blaster") && blasterBullet == null && m_hit.collider.tag != "Enemy")
		{

			//ブラスターを生成
			GameObject blaster = (GameObject)Instantiate(m_blasterPrefab, m_blasterMuzzle.position, Quaternion.identity);
			//前方下に力を加える
			Vector3 vel = new Vector3(0.0f, -1.0f, 1.0f);
			vel.Normalize();
			blaster.GetComponent<Rigidbody>().AddForce(vel * BLASTER_SHOT_SPEED, ForceMode.Impulse);

			//発射位置にターゲット2を置く
			m_target2.transform.position = m_target.transform.position;

			m_audio.PlayOneShot(m_blasterSound);
			//m_blasterShotTimer = BLASTER_SHOT_INTERVAL;
		}

		//ターゲット上に敵がいる状態で発射したら誘導
		else if (Input.GetButtonDown("Blaster") && blasterBullet == null && m_hit.collider.tag == "Enemy")
		{
			//ブラスターを生成
			GameObject blaster = (GameObject)Instantiate(m_blasterPrefab, m_blasterMuzzle.position, Quaternion.identity);
			//前方下に力を加える
			m_targetPos = m_hit.point;
			Vector3 vel =  m_targetPos - this.transform.position;
			vel.Normalize();
			blaster.GetComponent<Rigidbody>().AddForce(vel * BLASTER_SHOT_SPEED, ForceMode.Impulse);
			Debug.Log(vel);
			//発射位置にターゲット2を置く
			m_target2.transform.position = m_target.transform.position;

			m_audio.PlayOneShot(m_blasterSound);

		}
		else
		{
			//普段はターゲット2,3を非表示
			m_target2.gameObject.SetActive(false);
			m_target3.gameObject.SetActive(false);

			//普段はターゲット1を表示
			m_target.gameObject.SetActive(true);
		}

		if(blasterBullet != null)
		{
			//ブラスター弾があればターゲット2,3を表示
			m_target2.gameObject.SetActive(true);
			m_target3.gameObject.SetActive(true);
			//ブラスター弾があればターゲット1を非表示
			m_target.gameObject.SetActive(false);

		}

		//m_blasterShotTimer -= Time.deltaTime;
		//if (m_blasterShotTimer <= 0.0f) m_blasterShotTimer = 0.0f;
	}

	private void OnCollisionEnter(Collision collision)
	{
		//敵弾に当たったら消滅
		if (collision.gameObject.tag == "EnemyBullet" || collision.gameObject.tag == "Enemy" && m_isDeath == false)
		{
			//死亡フラグを立てる
			m_isDeath = true;

		}

		//スペシャルフラッグに当たったら残機を1増やす
		if(collision.gameObject.tag == "SpecialFlag")
		{
			ShareData.Instance.life++;
			//SE再生
			m_audio.PlayOneShot(m_1upSE);
		}
	}


	//ターゲットの処理
	void Target()
	{
		//ray作成
		Ray ray = new Ray(transform.position, new Vector3(0.0f, -1.0f, 1.0f));


		//rayを飛ばす距離
		float distance = 100.0f;
		//rayのデバッグ描画
		Debug.DrawRay(ray.origin, ray.direction * distance, Color.red);
	
		
		if (Physics.Raycast(ray,out m_hit, distance))
		{
			Debug.Log("Ray:" + m_hit.collider.tag);
			//床か敵にrayが当たったらターゲットをその上に描画
			if (m_hit.collider.tag =="Plane" || m_hit.collider.tag == "Enemy")
			{
				m_target.transform.position = new Vector3(m_hit.point.x, m_hit.point.y + 1.0f, m_hit.point.z - 1.0f);
				m_target3.transform.position = new Vector3(m_hit.point.x, m_hit.point.y + 1.0f, m_hit.point.z - 1.0f);
				m_target4.transform.position = new Vector3(m_hit.point.x, m_hit.point.y + 1.0f, m_hit.point.z - 1.0f);


				//エネミーを検知したらターゲットの画像を切り替える
				if (m_hit.collider.tag == "Enemy")
				{
					m_target.SetActive(false);
					m_target4.SetActive(true);
				}
				else
				{
					m_target4.SetActive(false);

				}
			}
		}
	}
}
