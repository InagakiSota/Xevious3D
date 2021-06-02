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
	//�u���X�^�[�̃v���n�u
	[SerializeField] GameObject m_blasterPrefab;
	//�^�[�Q�b�g
	[SerializeField] GameObject m_target;
	//�^�[�Q�b�g2
	[SerializeField] GameObject m_target2;
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

	//�ړ���
	Vector3 m_vel;
	//���W
	Vector3 m_pos;
	//�u���X�^�[�̔��˃^�C�}�[
	float m_blasterShotTimer;
	//�U�b�p�[�̔��˃^�C�}�[
	float m_zapperShotTimer;

	// Start is called before the first frame update
	void Start()
	{
		m_pos = this.transform.position;
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
	}

	//�ړ�����
	void Move()
	{
		//�㉺����
		m_vel.z = Input.GetAxis("Vertical");
		//���E����
		m_vel.x = Input.GetAxis("Horizontal");
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
		if (Input.GetKeyDown(KeyCode.Z))
		{
			//�U�b�p�[(�E)�𐶐�
			GameObject zapper1 = (GameObject)Instantiate(m_zapperPrefab, m_zapperMuzzleRight.position, Quaternion.identity);
			//�U�b�p�[(��)�𐶐�
			GameObject zapper2 = (GameObject)Instantiate(m_zapperPrefab, m_zapperMuzzleLeft.position, Quaternion.identity);
			//�O���ɗ͂�������
			zapper1.GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, 0.0f, ZAPPER_SHOT_SPEED), ForceMode.Impulse);
			zapper2.GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, 0.0f, ZAPPER_SHOT_SPEED), ForceMode.Impulse);
		}

		//�A��
		if (Input.GetKey(KeyCode.LeftShift) && m_zapperShotTimer <= 0.0f)
		{
			//�U�b�p�[(�E)�𐶐�
			GameObject zapper3 = (GameObject)Instantiate(m_zapperPrefab, m_zapperMuzzleRight.position, Quaternion.identity);
			//�U�b�p�[(��)�𐶐�
			GameObject zapper4 = (GameObject)Instantiate(m_zapperPrefab, m_zapperMuzzleLeft.position, Quaternion.identity);
			//�O���ɗ͂�������
			zapper3.GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, 0.0f, ZAPPER_SHOT_SPEED), ForceMode.Impulse);
			zapper4.GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, 0.0f, ZAPPER_SHOT_SPEED), ForceMode.Impulse);

			m_zapperShotTimer = ZAPPER_SHOT_INTERVAL;

		}
		m_zapperShotTimer -= Time.deltaTime;
		if (m_zapperShotTimer <= 0.0f) m_zapperShotTimer = 0.0f;

	}

	//�u���X�^�[����
	void BlasterShot()
	{
		GameObject blasterBullet = GameObject.Find("BlasterPrefab(Clone)");
		//Debug.Log(blasterBullet);

		//X���͂����̃u���X�^�[�e���Ȃ���Δ��ˁ@
		if (Input.GetKey(KeyCode.X) && blasterBullet == null)
		{
			//�u���X�^�[�𐶐�
			GameObject blaster = (GameObject)Instantiate(m_blasterPrefab, m_blasterMuzzle.position, Quaternion.identity);
			//�O�����ɗ͂�������
			Vector3 vel = new Vector3(0.0f, -1.0f, 1.0f);
			vel.Normalize();
			blaster.GetComponent<Rigidbody>().AddForce(vel * BLASTER_SHOT_SPEED, ForceMode.Impulse);

			//���ˈʒu�Ƀ^�[�Q�b�g2��u��
			m_target2.transform.position = m_target.transform.position;

			//m_blasterShotTimer = BLASTER_SHOT_INTERVAL;
		}
		else
		{
			//���i�̓^�[�Q�b�g2���\��
			m_target2.gameObject.SetActive(false);
		}

		if(blasterBullet != null)
		{
			//�u���X�^�[�e������΃^�[�Q�b�g2��\��
			m_target2.gameObject.SetActive(true);

		}

		//m_blasterShotTimer -= Time.deltaTime;
		//if (m_blasterShotTimer <= 0.0f) m_blasterShotTimer = 0.0f;
	}

	private void OnCollisionEnter(Collision collision)
	{
		//�G�e�ɓ������������
		if (collision.gameObject.tag == "EnemyBullet" || collision.gameObject.tag == "Enemy")
		{
			//�����̃p�[�e�B�N���Đ�
			var exlotion = Instantiate(m_explosion, transform.position, Quaternion.identity);
			var explotsionParticle = exlotion.GetComponent<ParticleSystem>();
			explotsionParticle.Play();

			//����
			Destroy(gameObject);

			//�c�@��1���炷
			ShareData.Instance.life = ShareData.Instance.life - 1;
			//�c�@����0�łȂ���΃v���C�V�[���������[�h
			if (ShareData.Instance.life > 0)
			{
				FadeManager.FadeOut("PlayScene");
			}

		}
	}

	void Ray()
	{
		Ray ray = new Ray(transform.position, new Vector3(0.0f, -1.0f, 1.0f));

		RaycastHit hit;

		float distance = 20.0f;

		Debug.DrawRay(ray.origin, ray.direction * distance, Color.red);

		if(Physics.Raycast(ray,out hit,distance))
		{
			//Debug.Log(ray.GetPoint(distance));
			if(hit.collider.tag =="Plane")
			{
				m_target.transform.position = new Vector3(hit.point.x, hit.point.y + 1.0f, hit.point.z - 1.0f);

			}

		}
	}
}
