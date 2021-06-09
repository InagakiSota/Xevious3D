using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShotController : MonoBehaviour
{
	//�{�X
	GameObject boss;

	//�e
	[SerializeField]
	GameObject bullet;

	//�Ăёł܂ł̎���
	[SerializeField]
	float shotTimer;

	//���Ԃ��v��^�C�}�[
	float nowShotTimer;

	//�ǂ�����e�o��
	Vector3 shotPos;

	//�����̃p�[�e�B�N��
	[SerializeField] GameObject m_explosion;


	//�_���[�W���󂯂��ۂǂꂮ�炢���C�t�����邩
	[SerializeField]
	int damegePoint;
	

	// Start is called before the first frame update
	void Start()
	{
		//�Q�[����ɏo�Ă�{�X��T��
		boss = GameObject.Find("BossPrefab(Clone)");

		//�^�C�}�[�Ɏ��Ԑݒ�
		nowShotTimer = shotTimer;
	}

	// Update is called once per frame
	void Update()
	{
		//�{�X���̂��e�łĂ�󋵂Ȃ�
		if (boss.GetComponent<BossController>().GetBossShotFlag())
		{
			//���Ԍ��炷
			nowShotTimer -= Time.deltaTime;

			//0�ɂȂ���
			if (nowShotTimer <= 0.0f)
			{
				Vector3 shotPos = new Vector3(transform.position.x, 0.0f, transform.position.z);

				//�e��������
				Instantiate(bullet, shotPos, Quaternion.identity);

				//���Ԑݒ�
				nowShotTimer = shotTimer;
			}
		}

		//�{�X�����񂾂�C�������
		if(boss.GetComponent<BossController>().GetDamage() <= 0)
		{
			//�����̃p�[�e�B�N���Đ�
			var exlotion = Instantiate(m_explosion, transform.position, Quaternion.identity);
			var explotsionParticle = exlotion.GetComponent<ParticleSystem>();
			explotsionParticle.Play();

			//���̃p�[�c�͏���
			Destroy(this.gameObject);
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		//�v���C���[�̒e��������
		if (collision.collider.tag == "PlayerBullet")
		{
			//�{�X�Ƀ_���[�W
			boss.GetComponent<BossController>().DamegePoint(damegePoint);

			//�����̃p�[�e�B�N���Đ�
			var exlotion = Instantiate(m_explosion, transform.position, Quaternion.identity);
			var explotsionParticle = exlotion.GetComponent<ParticleSystem>();
			explotsionParticle.Play();

			//�X�R�A���Z
			ShareData.Instance.score += 1000;

			//���̃p�[�c�͏���
			Destroy(this.gameObject);
		}
	}
}
