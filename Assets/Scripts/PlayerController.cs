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
	//移動速度
	[SerializeField] float MOVE_SPEED;
	//ザッパーの発射速度
	[SerializeField] float ZAPPER_SHOT_SPEED;
	//ブラスターの発射速度
	[SerializeField] float BLASTER_SHOT_SPEED;

	//移動量
	Vector3 m_vel;
	//座標
	Vector3 m_pos;
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
	}

	//移動処理
	void Move()
	{
		//上下入力
		m_vel.y = Input.GetAxis("Vertical");
		//左右入力
		m_vel.x = Input.GetAxis("Horizontal");
		//正規化
		m_vel.Normalize();
		//移動量を座標に加算する
		m_pos += m_vel * MOVE_SPEED * Time.deltaTime;
		//クランプ関数を用いて、プレイヤーを画面内に収める
		m_pos.x = Mathf.Clamp(m_pos.x, -7.0f, 7.0f);
		m_pos.y = Mathf.Clamp(m_pos.y, -4.0f, 4.0f);

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
		if (Input.GetKeyDown(KeyCode.X))
		{
			//ブラスターを生成
			GameObject blaster = (GameObject)Instantiate(m_blasterPrefab, m_blasterMuzzle.position, Quaternion.identity);
			//前方に力を加える
			blaster.GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, 0.0f, BLASTER_SHOT_SPEED), ForceMode.Impulse);
		}

	}
}
