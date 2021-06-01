using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	//ˆÚ“®—Ê
	Vector3 vel;
	//À•W
	Vector3 pos;
	// Start is called before the first frame update
	void Start()
	{
		pos = this.transform.position;
	}

	// Update is called once per frame
	void Update()
	{
		//ã‰º“ü—Í
		vel.y = Input.GetAxis("Vertical");

		//¶‰E“ü—Í
		vel.x = Input.GetAxis("Horizontal");


		vel.Normalize();

		pos += vel;

		pos.x = Mathf.Clamp(pos.x, -7.0f, 7.0f);
		pos.y = Mathf.Clamp(pos.y, -4.0f, 4.0f);


		this.transform.position = pos;
	}
}
