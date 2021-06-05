using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy01Controller : MonoBehaviour
{
	//�v���C���[
	GameObject player;

	//�v���C���[�̍��W
	Vector3 m_playerPos;

	//�e
	[SerializeField]
	GameObject bullet;

	//����
	[SerializeField]
	float speed = 0.3f;

	//��]���x
	[SerializeField]
	float rotateSpeed = 30.0f;

	//���C�t
	[SerializeField]
	float life = 1;

	//�����ʒu
	[SerializeField]
	Vector3 destroyPos = new Vector3(30.0f, 20.0f, -11.0f);

	//���񂾂Ƃ��Ⴆ��X�R�A
	[SerializeField]
	int getScore;

	//�����̃p�[�e�B�N��
	[SerializeField] GameObject m_explosionParticle;

	//�ǔ����镨�̈ʒu
	Vector3 targetPosition;

	//�ǔ����镨�܂ł̃x�N�g��
	Vector3 targetVector;

	//�łĂ邩
	bool shotFlag = true;

	//�ł܂ł̎���
	float shotTimer;

	//����ł�
	int shotNum = 1;

	//���x
	Vector3 vel = Vector3.zero;

	//�I�[�f�B�I�\�[�X
	AudioSource m_audio;

	//������
	[SerializeField] AudioClip m_bombSE;

	// Start is called before the first frame update
	void Start()
	{
		//�v���C���[��T��
		player = GameObject.Find("Player");
		//�v���C���[�����݂��Ă���΍��W����
		if (player != null)
		{
			m_playerPos = player.transform.position;
		}
		//�ǔ���܂ł̒P�ʃx�N�g��
		targetVector = m_playerPos - this.transform.position;
		targetVector.Normalize();

		//�ǔ���̈ʒu
		targetPosition = m_playerPos;

		//�ł܂ł̎��Ԃ������_���ݒ�
		shotTimer = Random.Range(0.0f, 0.5f);

		vel = Vector3.zero;

		shotFlag = true;

		m_audio = GameObject.Find("Audio Source").GetComponent<AudioSource>();

	}

	// Update is called once per frame
	void Update()
	{
		//�łĂ�
		if (shotFlag == true)
		{
			//�ǔ���܂ňړ��i�ǔ���͍X�V���Ă��Ȃ����ߒǂ��Ă͂��Ȃ��j
			transform.position += targetVector * speed * Time.deltaTime;

			//x���W�������ɂȂ�����
			//if ((int)this.transform.position.x == (int)player.transform.position.x)
			if (Mathf.Abs(transform.position.x - m_playerPos.x) <= 1.0f)
			{
				//�ł��܂�
				shotFlag = false;
			}
		}

		else if (shotFlag == false)
		{
			//�{�����ɂ�����E�����̉�ʊO��
			if (transform.position.x > 0.0f)
			{
				//�����x�ݒ�
				Vector3 accel = new Vector3(speed, 0.0f, speed * 0.01f);

				//����
				vel += accel * Time.deltaTime;
				transform.position += vel * Time.deltaTime;

				//��]
				transform.Rotate(0.0f, 0.0f, -rotateSpeed * Time.deltaTime);
			}
			//-�����ɂ����獶�����̉�ʊO��
			else
			{
				//�����x�ݒ�
				Vector3 accel = new Vector3(-speed, 0.0f, speed * 0.01f);

				//����
				vel += accel * Time.deltaTime;
				transform.position += vel * Time.deltaTime;

				//��]
				transform.Rotate(0.0f, 0.0f, rotateSpeed * Time.deltaTime);
			}

			//�^�C�}�[��0�ɂȂ��Ēe������Ȃ�
			if (shotTimer <= 0.0f && shotNum > 0 && Time.time >= 30.0f)
			{
				//�e���ݒ�
				Vector3 shotPos = this.transform.position;
				shotPos.z -= 1.0f;

				//�e���炷
				shotNum--;

				if (player != null)
				{
					//�e����
					Instantiate(bullet, shotPos, Quaternion.identity);
				}
			}
			else
			{
				shotTimer -= Time.deltaTime;
			}

		}


		//��ʊO�܂ōs���������
		if (transform.position.z <= destroyPos.z || Mathf.Abs(transform.position.x) >= destroyPos.x)
		{
			Destroy(this.gameObject);
		}
		
	}

	private void OnCollisionEnter(Collision collision)
	{
		//����̒e����������
		if (collision.collider.tag == "PlayerBullet")
		{
			//�����̃p�[�e�B�N���Đ�
			var exlotion = Instantiate(m_explosionParticle, transform.position, Quaternion.identity);
			var explotsionParticle = exlotion.GetComponent<ParticleSystem>();
			explotsionParticle.Play();

			//�������Đ�
			m_audio.PlayOneShot(m_bombSE);

			//�X�R�A���Z
			ShareData.Instance.score += getScore;
			//����
			Destroy(this.gameObject);
		}
	}
}
