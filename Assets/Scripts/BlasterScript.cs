using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlasterScript : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	private void OnCollisionEnter(Collision collision)
	{
		//プレイヤーかプレイヤーの弾以外に当たったら消滅
		if (collision.gameObject.tag != "Player" &&
			collision.gameObject.tag != "PlayerBullet")
			Destroy(gameObject);
	}
}
