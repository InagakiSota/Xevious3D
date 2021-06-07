using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingBoardController : MonoBehaviour
{
	//����
	[SerializeField]
	float speed = 0.2f;

	//��]���鑬��
	[SerializeField]
	float rotateSpeed = -5.0f;

	[SerializeField]
	Vector3 destroyPos = new Vector3(30.0f, 20.0f, -20.0f);

	//�I�[�f�B�I�\�[�X
	AudioSource m_audio;

	//�e�q�b�g��
	[SerializeField] AudioClip m_hitSE;


	// Start is called before the first frame update
	void Start()
	{
		m_audio = GameObject.Find("Audio Source").GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update()
	{
		//���x�ݒ�
		Vector3 vel = new Vector3(0, 0, speed);
		//����
		transform.position -= vel * Time.deltaTime;
		//��]
		transform.Rotate(rotateSpeed * Time.deltaTime, 0, 0);

		//����
		if (transform.position.z <= destroyPos.z)
		{
			Destroy(this.gameObject);
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.tag == "PlayerBullet")
		{
			m_audio.PlayOneShot(m_hitSE);
		}            
	}

}
