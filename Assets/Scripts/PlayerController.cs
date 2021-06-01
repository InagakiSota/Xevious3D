using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	//移動量
	Vector3 vel;
	//座標
	Vector3 pos;
	// Start is called before the first frame update
	void Start()
	{
		pos = this.transform.position;
	}

	// Update is called once per frame
	void Update()
	{
		//上下入力
		vel.y = Input.GetAxis("Vertical");

		//左右入力
		vel.x = Input.GetAxis("Horizontal");


		vel.Normalize();

		pos += vel;

		pos.x = Mathf.Clamp(pos.x, -7.0f, 7.0f);
		pos.y = Mathf.Clamp(pos.y, -4.0f, 4.0f);


		this.transform.position = pos;
	}
}
