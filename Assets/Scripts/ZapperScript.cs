using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZapperScript : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{
		//5�b��ɍ폜
		Destroy(gameObject, 5.0f);
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	private void OnCollisionEnter(Collision collision)
	{
		//�v���C���[�ȊO�Ƀq�b�g������폜
		if(collision.gameObject.tag != "Player")
		{
			Destroy(gameObject);
		}
	}
}
