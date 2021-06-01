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
	//�ړ����x
	[SerializeField] float MOVE_SPEED;
	//�U�b�p�[�̔��ˑ��x
	[SerializeField] float ZAPPER_SHOT_SPEED;
	//�u���X�^�[�̔��ˑ��x
	[SerializeField] float BLASTER_SHOT_SPEED;

	//�ړ���
	Vector3 m_vel;
	//���W
	Vector3 m_pos;
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
	}

	//�ړ�����
	void Move()
	{
		//�㉺����
		m_vel.y = Input.GetAxis("Vertical");
		//���E����
		m_vel.x = Input.GetAxis("Horizontal");
		//���K��
		m_vel.Normalize();
		//�ړ��ʂ����W�ɉ��Z����
		m_pos += m_vel * MOVE_SPEED * Time.deltaTime;
		//�N�����v�֐���p���āA�v���C���[����ʓ��Ɏ��߂�
		m_pos.x = Mathf.Clamp(m_pos.x, -7.0f, 7.0f);
		m_pos.y = Mathf.Clamp(m_pos.y, -4.0f, 4.0f);

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
		if (Input.GetKeyDown(KeyCode.X))
		{
			//�u���X�^�[�𐶐�
			GameObject blaster = (GameObject)Instantiate(m_blasterPrefab, m_blasterMuzzle.position, Quaternion.identity);
			//�O���ɗ͂�������
			blaster.GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, 0.0f, BLASTER_SHOT_SPEED), ForceMode.Impulse);
		}

	}
}
