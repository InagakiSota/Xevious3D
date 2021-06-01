using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	//�ړ���
	Vector3 vel;
	//���W
	Vector3 pos;
	// Start is called before the first frame update
	void Start()
	{
		pos = this.transform.position;
	}

	// Update is called once per frame
	void Update()
	{
		//�㉺����
		vel.y = Input.GetAxis("Vertical");

		//���E����
		vel.x = Input.GetAxis("Horizontal");


		vel.Normalize();

		pos += vel;

		pos.x = Mathf.Clamp(pos.x, -7.0f, 7.0f);
		pos.y = Mathf.Clamp(pos.y, -4.0f, 4.0f);


		this.transform.position = pos;
	}
}
