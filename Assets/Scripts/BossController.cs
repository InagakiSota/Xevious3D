using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
	// Start is called before the first frame update
	//����
	[SerializeField]
	float speed;

	//�f�[�^����������ʒu
	[SerializeField]
	Vector3 destroyPos;

	//���񂾎��ɖႦ��_��
	[SerializeField]
	int getScore;

	//�ł��Ă�����
	bool shotFlag = false;

	//�����Ă��邩
	bool aliveFlag = true;

	//5�p�[�c������1�p�[�c�󂳂�邲�Ƃ�1���郉�C�t
	//�^�񒆂̃R�A��j�󂳂ꂽ�ꍇ���C�t�S�������܂�
	int life = 5;

	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		//�{�X�o��E�ޏ�̓���
		//transform.position += new Vector3(0.0f, 0.0f, -speed);

		//�w��̈ʒu�܂ōs������s���J�n
		if ((int)transform.position.z >= 0)
		{
			transform.position += new Vector3(0.0f, 0.0f, -speed) * Time.deltaTime;
			shotFlag = true;
		}

		if (!aliveFlag)
		{
			transform.position += new Vector3(0.0f, 0.0f, -speed) * Time.deltaTime;
		}

		//���C�t�Ȃ��Ȃ����Ƃ�
		if (life <= 0)
		{
			//�łĂȂ�
			shotFlag = false;

			//����
			aliveFlag = false;
		}

		//��ʊO�s���������
		if (transform.position.z <= -50.0f || Mathf.Abs(transform.position.x) >= 30.0f)
		{
			//����ł����ԂŃX�R�A���Z
			if (shotFlag == false && aliveFlag == false)
			{
				ShareData.Instance.score += getScore;
			}

			//����
			Destroy(this.gameObject);
		}

	}

	//�e�p�[�c�ɖ{�̂��s���ł��邩�n���֐�
	//BossShotController�ŌĂ΂�܂�
	public bool GetBossShotFlag()
	{
		return shotFlag;
	}

	//�e�p�[�c���󂳂�邲�ƂɃ��C�t������֐�
	//BossShotController�ŌĂ΂�܂�
	public void DamegePoint(int damege)
	{
		life -= damege;
	}

	//�{�X�̃_���[�W�̎擾
	public int GetDamage()
	{
		return life;
	}
}
