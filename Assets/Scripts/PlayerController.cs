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
	//ブラスターのプレハブ
	[SerializeField] GameObject m_blasterPrefab;
	//ターゲット
	[SerializeField] GameObject m_target;
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
	//爆発のパーティクル
	[SerializeField] GameObject m_explosion;

	//移動量
	Vector3 m_vel;
	//座標
	Vector3 m_pos;
	//ブラスターの発射タイマー
	float m_blasterShotTimer;

	// Start is called before the first frame update
	void Start()
	{
		m_pos = this.transform.position;
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

		//Rayのテスト
		Ray();
	}

	//移動処理
	void Move()
	{
		//上下入力
		m_vel.z = Input.GetAxis("Vertical");
		//左右入力
		m_vel.x = Input.GetAxis("Horizontal");
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
		if(Input.GetKeyDown(KeyCode.Z))
		{
			//ザッパー(右)を生成
			GameObject zapper1 = (GameObject)Instantiate(m_zapperPrefab,m_zapperMuzzleRight.position,Quaternion.identity);
			//ザッパー(左)を生成
			GameObject zapper2 = (GameObject)Instantiate(m_zapperPrefab, m_zapperMuzzleLeft.position, Quaternion.identity);
			//前方に力を加える
			zapper1.GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, 0.0f, ZAPPER_SHOT_SPEED), ForceMode.Impulse);
			zapper2.GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, 0.0f, ZAPPER_SHOT_SPEED), ForceMode.Impulse);
		}
	}

	//ブラスター発射
	void BlasterShot()
	{
		if (Input.GetKeyDown(KeyCode.X) && m_blasterShotTimer == 0.0f)
		{
			//ブラスターを生成
			GameObject blaster = (GameObject)Instantiate(m_blasterPrefab, m_blasterMuzzle.position, Quaternion.identity);
			//前方に力を加える
			blaster.GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, -BLASTER_SHOT_SPEED, BLASTER_SHOT_SPEED), ForceMode.Impulse);

			m_blasterShotTimer = BLASTER_SHOT_INTERVAL;
		}

		m_blasterShotTimer -= Time.deltaTime;
		if (m_blasterShotTimer <= 0.0f) m_blasterShotTimer = 0.0f;

	}

	private void OnCollisionEnter(Collision collision)
	{
		//敵弾に当たったら消滅
		if (collision.gameObject.tag == "EnemyBullet" || collision.gameObject.tag == "Enemy")
		{
			//爆発のパーティクル再生
			var exlotion = Instantiate(m_explosion, transform.position, Quaternion.identity);
			var explotsionParticle = exlotion.GetComponent<ParticleSystem>();
			explotsionParticle.Play();

			//消滅
			Destroy(gameObject);
		}
	}

	void Ray()
	{
		Ray ray = new Ray(transform.position, new Vector3(0.0f, -1.0f, 1.0f));

		RaycastHit hit;

		float distance = 20.0f;

		Debug.DrawRay(ray.origin, ray.direction * distance, Color.red);

		if(Physics.Raycast(ray,out hit,distance))
		{
			//Debug.Log(ray.GetPoint(distance));
			if(hit.collider.tag =="Plane")
				m_target.transform.position = new Vector3(hit.point.x,hit.point.y +1.0f,hit.point.z);
		}
	}
}
