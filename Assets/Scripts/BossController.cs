using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
	// Start is called before the first frame update
	//速さ
	[SerializeField]
	float speed;

	//データを消去する位置
	[SerializeField]
	Vector3 destroyPos;

	//死んだ時に貰える点数
	[SerializeField]
	int getScore;

	//打っていいか
	bool shotFlag = false;

	//生きているか
	bool aliveFlag = true;

	//5パーツあって1パーツ壊されるごとに1つ減るライフ
	//真ん中のコアを破壊された場合ライフ全部消えます
	int life = 5;

	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		//ボス登場・退場の動き
		//transform.position += new Vector3(0.0f, 0.0f, -speed);

		//指定の位置まで行ったら行動開始
		if ((int)transform.position.z >= 8)
		{
			transform.position += new Vector3(0.0f, 0.0f, -speed);
			shotFlag = true;
		}

		if (!aliveFlag)
		{
			transform.position += new Vector3(0.0f, 0.0f, -speed);
		}

		//ライフなくなったとき
		if (life <= 0)
		{
			//打てない
			shotFlag = false;

			//死んだ
			aliveFlag = false;
		}

		//画面外行ったら消す
		if (transform.position.z <= -20.0f || Mathf.Abs(transform.position.x) >= 30.0f)
		{
			//死んでいる状態でスコア加算
			if (shotFlag == false && aliveFlag == false)
			{
				ShareData.Instance.score = getScore;
			}

			//消す
			Destroy(this.gameObject);
		}

	}

	//各パーツに本体が行動できるか渡す関数
	//BossShotControllerで呼ばれます
	public bool GetBossShotFlag()
	{
		return shotFlag;
	}

	//各パーツが壊されるごとにライフが減る関数
	//BossShotControllerで呼ばれます
	public void DamegePoint(int damege)
	{
		life -= damege;
	}
}
