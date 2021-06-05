using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �U�b�p�[�F�΋�e
/// �u���X�^�[�F�Βn�e
/// </summary>

public class PlayerController : MonoBehaviour
{
	//�U�b�p�[�̏e���@�E
	[SerializeField] Transform m_zapperMuzzleRight;
	//�U�b�p�[�̏e���@��
	[SerializeField] Transform m_zapperMuzzleLeft;
	//�u���X�^�[�̏e��
	[SerializeField] Transform m_blasterMuzzle;
	//�U�b�p�[�̃v���n�u
	[SerializeField] GameObject m_zapperPrefab;
	//�U�b�p�[(�F)�̃v���n�u
	[SerializeField] GameObject m_zapperBluePrefab;
	//�u���X�^�[�̃v���n�u
	[SerializeField] GameObject m_blasterPrefab;
	//�^�[�Q�b�g(�ʏ�)
	[SerializeField] GameObject m_target;
	//�^�[�Q�b�g2(�u���X�^�[���ˈʒu)
	[SerializeField] GameObject m_target2;
	//�^�[�Q�b�g3(�u���X�^�[���˒�)
	[SerializeField] GameObject m_target3;
	//�^�[�Q�b�g4(�^�[�Q�b�g��ɓG������)
	[SerializeField] GameObject m_target4;
	//�ړ����x
	[SerializeField] float MOVE_SPEED;
	//�U�b�p�[�̔��ˑ��x
	[SerializeField] float ZAPPER_SHOT_SPEED;
	//�u���X�^�[�̔��ˑ��x
	[SerializeField] float BLASTER_SHOT_SPEED;
	//���E�̈ړ����E�l
	[SerializeField] float MOVE_LIMIT_SIDE;
	//�O�̈ړ����E�l
	[SerializeField] float MOVE_LIMIT_FRONT;
	//��̈ړ����E�l
	[SerializeField] float MOVE_LIMIT_BACK;
	//�u���X�^�[�̔��˃C���^�[�o��
	[SerializeField] float BLASTER_SHOT_INTERVAL;
	//�U�b�p�[�̔��˃C���^�[�o��
	[SerializeField] float ZAPPER_SHOT_INTERVAL;
	//�����̃p�[�e�B�N��
	[SerializeField] GameObject m_explosion;
	//�v���C���[�̃��f��
	[SerializeField] GameObject m_playerModel;

	//�I�[�f�B�I�\�[�X
	AudioSource m_audio;
	//�U�b�p�[�p�̃I�[�f�B�I�\�[�X
	AudioSource m_zapperAudio;
	//�����̃T�E���h
	[SerializeField] AudioClip m_bombSound;
	//�U�b�p�[���˂̃T�E���h
	[SerializeField] AudioClip m_zapperSound;
	//�u���X�^�[���˂̃T�E���h
	[SerializeField] AudioClip m_blasterSound;


	//�ړ���
	Vector3 m_vel;
	//���W
	Vector3 m_pos;
	//�u���X�^�[�̔��˃^�C�}�[
	float m_blasterShotTimer;
	//�U�b�p�[�̔��˃^�C�}�[
	float m_zapperShotTimer;

	//���S�t���O
	bool m_isDeath;

	//�U�b�p�[�̐F�t���O
	bool m_zapperColorFlag = false;

	// Start is called before the first frame update
	void Start()
	{
		m_pos = this.transform.position;

		m_isDeath = false;

		m_audio = GameObject.Find("Audio Source").GetComponent<AudioSource>();

		m_zapperAudio = this.GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update()
	{
		//�ړ�����
		Move();
		//�U�b�p�[�̔���
		ZapperShot();
		//�u���X�^�[�̔���
		BlasterShot();
		//Ray�̃e�X�g
		Ray();


		if(m_isDeath == true)
		{
			//�������Đ�
			m_audio.PlayOneShot(m_bombSound);
			//�c�@��1���炷
			ShareData.Instance.life = ShareData.Instance.life - 1;
			//����
			//Destroy(m_playerModel);
			Destroy(this.gameObject);

			//�����̃p�[�e�B�N���Đ�
			var exlotion = Instantiate(m_explosion, transform.position, Quaternion.identity);
			var explotsionParticle = exlotion.GetComponent<ParticleSystem>();
			explotsionParticle.Play();


			//�^�[�Q�b�g������
			Destroy(m_target);
			Destroy(m_target2);
			Destroy(m_target3);
			Destroy(m_target4);

			m_isDeath = false;
		}

	}

	//�ړ�����
	void Move()
	{
		//�㉺����
		m_vel.z = Input.GetAxis("Vertical");
		//���E����
		m_vel.x = Input.GetAxis("Horizontal");

		Debug.Log(m_vel);

		//���E�ړ��ɍ��킹�ċ@�̂��X����
		Vector3 axis = new Vector3(0.0f, 0.0f, 1.0f);
		float angle = -45.0f * m_vel.x;
		Quaternion q = Quaternion.AngleAxis(angle, axis);
		m_playerModel.transform.rotation = q;
		
		//���K��
		m_vel.Normalize();
		//�ړ��ʂ����W�ɉ��Z����
		m_pos += m_vel * MOVE_SPEED * Time.deltaTime;
		//�N�����v�֐���p���āA�v���C���[����ʓ��Ɏ��߂�
		m_pos.x = Mathf.Clamp(m_pos.x, -MOVE_LIMIT_SIDE, MOVE_LIMIT_SIDE);
		m_pos.z = Mathf.Clamp(m_pos.z, MOVE_LIMIT_BACK, MOVE_LIMIT_FRONT);

		this.transform.position = m_pos;

	}

	//�U�b�p�[����
	void ZapperShot()
	{
		//�P��
		if (Input.GetButtonDown("Zapper"))
		{
			if (m_zapperColorFlag == false)
			{
				//�U�b�p�[(�E)�𐶐�
				GameObject zapperRight = (GameObject)Instantiate(m_zapperPrefab, m_zapperMuzzleRight.position, Quaternion.identity);
				//�U�b�p�[(��)�𐶐�
				GameObject zapperLeft = (GameObject)Instantiate(m_zapperBluePrefab, m_zapperMuzzleLeft.position, Quaternion.identity);


				//�O���ɗ͂�������
				zapperRight.GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, 0.0f, ZAPPER_SHOT_SPEED), ForceMode.Impulse);
				zapperLeft.GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, 0.0f, ZAPPER_SHOT_SPEED), ForceMode.Impulse);
			}
			else
			{
				//�U�b�p�[(�E)�𐶐�
				GameObject zapperRight = (GameObject)Instantiate(m_zapperBluePrefab, m_zapperMuzzleRight.position, Quaternion.identity);
				//�U�b�p�[(��)�𐶐�
				GameObject zapperLeft = (GameObject)Instantiate(m_zapperPrefab, m_zapperMuzzleLeft.position, Quaternion.identity);

				//�O���ɗ͂�������
				zapperRight.GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, 0.0f, ZAPPER_SHOT_SPEED), ForceMode.Impulse);
				zapperLeft.GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, 0.0f, ZAPPER_SHOT_SPEED), ForceMode.Impulse);
			}

			//�U�b�p�[�̐F�t���O�����ւ���
			m_zapperColorFlag = !m_zapperColorFlag;
			//���ˉ��Đ�
			m_zapperAudio.PlayOneShot(m_zapperSound);

		}

		//�A��
		if (Input.GetButton("ZapperRapit") && m_zapperShotTimer <= 0.0f)
		{
			if (m_zapperColorFlag == false)
			{
				//�U�b�p�[(�E)�𐶐�
				GameObject zapperRight = (GameObject)Instantiate(m_zapperPrefab, m_zapperMuzzleRight.position, Quaternion.identity);
				//�U�b�p�[(��)�𐶐�
				GameObject zapperLeft = (GameObject)Instantiate(m_zapperBluePrefab, m_zapperMuzzleLeft.position, Quaternion.identity);

				//�O���ɗ͂�������
				zapperRight.GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, 0.0f, ZAPPER_SHOT_SPEED), ForceMode.Impulse);
				zapperLeft.GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, 0.0f, ZAPPER_SHOT_SPEED), ForceMode.Impulse);
			}
			else
			{             
				//�U�b�p�[(�E)�𐶐�
				GameObject zapperRight = (GameObject)Instantiate(m_zapperBluePrefab, m_zapperMuzzleRight.position, Quaternion.identity);
				//�U�b�p�[(��)�𐶐�
				GameObject zapperLeft = (GameObject)Instantiate(m_zapperPrefab, m_zapperMuzzleLeft.position, Quaternion.identity);

				//�O���ɗ͂�������
				zapperRight.GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, 0.0f, ZAPPER_SHOT_SPEED), ForceMode.Impulse);
				zapperLeft.GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, 0.0f, ZAPPER_SHOT_SPEED), ForceMode.Impulse);


			}
			//�A�˂̃C���^�[�o���ݒ�
			m_zapperShotTimer = ZAPPER_SHOT_INTERVAL;
			//�U�b�p�[�̐F�t���O�����ւ���
			m_zapperColorFlag = !m_zapperColorFlag;
			//���ˉ��Đ�
			m_zapperAudio.PlayOneShot(m_zapperSound);

		}
		m_zapperShotTimer -= Time.deltaTime;
		if (m_zapperShotTimer <= 0.0f) m_zapperShotTimer = 0.0f;

	}

	//�u���X�^�[����
	void BlasterShot()
	{
		GameObject blasterBullet = GameObject.Find("BlasterPrefab(Clone)");

		//X���͂����̃u���X�^�[�e���Ȃ���Δ��ˁ@
		if (Input.GetButtonDown("Blaster") && blasterBullet == null)
		{
			//�u���X�^�[�𐶐�
			GameObject blaster = (GameObject)Instantiate(m_blasterPrefab, m_blasterMuzzle.position, Quaternion.identity);
			//�O�����ɗ͂�������
			Vector3 vel = new Vector3(0.0f, -1.0f, 1.0f);
			vel.Normalize();
			blaster.GetComponent<Rigidbody>().AddForce(vel * BLASTER_SHOT_SPEED, ForceMode.Impulse);

			//���ˈʒu�Ƀ^�[�Q�b�g2��u��
			m_target2.transform.position = m_target.transform.position;

			m_audio.PlayOneShot(m_blasterSound);
			//m_blasterShotTimer = BLASTER_SHOT_INTERVAL;
		}
		else
		{
			//���i�̓^�[�Q�b�g2,3���\��
			m_target2.gameObject.SetActive(false);
			m_target3.gameObject.SetActive(false);

			//���i�̓^�[�Q�b�g1��\��
			m_target.gameObject.SetActive(true);
		}

		if(blasterBullet != null)
		{
			//�u���X�^�[�e������΃^�[�Q�b�g2,3��\��
			m_target2.gameObject.SetActive(true);
			m_target3.gameObject.SetActive(true);
			//�u���X�^�[�e������΃^�[�Q�b�g1���\��
			m_target.gameObject.SetActive(false);

		}

		//m_blasterShotTimer -= Time.deltaTime;
		//if (m_blasterShotTimer <= 0.0f) m_blasterShotTimer = 0.0f;
	}

	private void OnCollisionEnter(Collision collision)
	{
		//�G�e�ɓ������������
		if (collision.gameObject.tag == "EnemyBullet" || collision.gameObject.tag == "Enemy" && m_isDeath == false)
		{
			m_isDeath = true;

		}

		//�X�y�V�����t���b�O�ɓ���������c�@��1���₷
		if(collision.gameObject.tag == "SpecialFlag")
		{
			ShareData.Instance.life++;
		}
	}

	void Ray()
	{
		//ray�쐬
		Ray ray = new Ray(transform.position, new Vector3(0.0f, -1.0f, 1.0f));

		RaycastHit hit;

		//ray���΂�����
		float distance = 20.0f;
		//ray�̃f�o�b�O�`��
		Debug.DrawRay(ray.origin, ray.direction * distance, Color.red);

		if(Physics.Raycast(ray,out hit,distance))
		{
			//�����G��ray������������^�[�Q�b�g�����̏�ɕ`��
			if(hit.collider.tag =="Plane" || hit.collider.tag == "Enemy")
			{
				m_target.transform.position = new Vector3(hit.point.x, hit.point.y + 1.0f, hit.point.z - 1.0f);
				m_target3.transform.position = new Vector3(hit.point.x, hit.point.y + 1.0f, hit.point.z - 1.0f);
				m_target4.transform.position = new Vector3(hit.point.x, hit.point.y + 1.0f, hit.point.z - 1.0f);

				//�G�l�~�[�����m������^�[�Q�b�g�̉摜��؂�ւ���
				if (hit.collider.tag == "Enemy")
				{
					m_target.SetActive(false);
					m_target4.SetActive(true);
				}
				else
				{
					//m_target.SetActive(true);
					m_target4.SetActive(false);

				}
			}

		}
	}
}
