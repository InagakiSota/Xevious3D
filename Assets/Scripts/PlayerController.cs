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
	//�����̃p�[�e�B�N��
	[SerializeField] GameObject m_explosion;

	//�ړ���
	Vector3 m_vel;
	//���W
	Vector3 m_pos;
	//�u���X�^�[�̔��˃^�C�}�[
	float m_blasterShotTimer;

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
		if(Input.GetKeyDown(KeyCode.Z))
		{
			//�U�b�p�[(�E)�𐶐�
			GameObject zapper1 = (GameObject)Instantiate(m_zapperPrefab,m_zapperMuzzleRight.position,Quaternion.identity);
			//�U�b�p�[(��)�𐶐�
			GameObject zapper2 = (GameObject)Instantiate(m_zapperPrefab, m_zapperMuzzleLeft.position, Quaternion.identity);
			//�O���ɗ͂�������
			zapper1.GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, 0.0f, ZAPPER_SHOT_SPEED), ForceMode.Impulse);
			zapper2.GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, 0.0f, ZAPPER_SHOT_SPEED), ForceMode.Impulse);
		}
	}

	//�u���X�^�[����
	void BlasterShot()
	{
		if (Input.GetKeyDown(KeyCode.X) && m_blasterShotTimer == 0.0f)
		{
			//�u���X�^�[�𐶐�
			GameObject blaster = (GameObject)Instantiate(m_blasterPrefab, m_blasterMuzzle.position, Quaternion.identity);
			//�O���ɗ͂�������
			blaster.GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, -BLASTER_SHOT_SPEED, BLASTER_SHOT_SPEED), ForceMode.Impulse);

			m_blasterShotTimer = BLASTER_SHOT_INTERVAL;
		}

		m_blasterShotTimer -= Time.deltaTime;
		if (m_blasterShotTimer <= 0.0f) m_blasterShotTimer = 0.0f;

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
				m_target.transform.position = new Vector3(hit.point.x,hit.point.y +1.0f,hit.point.z);
		}
	}
}
