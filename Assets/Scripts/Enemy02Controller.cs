using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy02Controller : MonoBehaviour
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
    float speed = 0.4f;

    //��]���x
    [SerializeField]
    float rotateSpeed = 30;

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

    // Start is called before the first frame update
    void Start()
    {
        //�v���C���[��T��
        player = GameObject.Find("Player");

        //�ǔ���܂ł̒P�ʃx�N�g��
        targetVector = player.transform.position - this.transform.position;
        targetVector.Normalize();

        //�ǔ���̈ʒu
        targetPosition = player.transform.position;

        //�ł܂ł̎��Ԃ������_���ݒ�
        shotTimer = Random.Range(0.0f, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        //�v���C���[�����݂��Ă���΍��W����
        if (player != null)
        {
            m_playerPos = player.transform.position;
        }
        //�łĂ�
        if (shotFlag == true)
        {
            //�ǔ���܂ňړ��i�ǔ���͍X�V���Ă��Ȃ����ߒǂ��Ă͂��Ȃ��j
            transform.position += targetVector * speed;

            //x�̒l�̍���1.0f�ȉ��Ȃ�ł�
            if (Mathf.Abs(transform.position.x - player.transform.position.x) <= 2.0f)
            {
                shotFlag = false;
            }
        }
        else
        {
            //�{�����ɂ�����E�����̉�ʊO��
            if (transform.position.x > 0.0f)
            {
                //���x�ݒ�
                Vector3 vel = new Vector3(-1.1f * targetVector.x * speed, 0.0f, targetVector.z * speed);
               
                //����
                transform.position += vel;

                //��]
                transform.Rotate(0.0f, 0.0f, -rotateSpeed);
            }
            //�\�����ɂ����獶�����̉�ʊO��
            else
            {
                //���x�ݒ�
                Vector3 vel = new Vector3(-1.1f * targetVector.x * speed, 0.0f, targetVector.z * speed);
                
                //����
                transform.position += vel;

                //��]
                transform.Rotate(0.0f, 0.0f, rotateSpeed);
            }

            //�^�C�}�[��0�ɂȂ��Ēe������Ȃ�
            if (shotTimer <= 0.0f && shotNum > 0)
            {
                //�e���ݒ�
                Vector3 shotPos = this.transform.position;
                shotPos.z -= 1.0f;

                //�e���炷
                shotNum--;

                //�e����
                if (player != null)
                    Instantiate(bullet, shotPos, Quaternion.identity);
            }
            else
            {
                shotTimer -= Time.deltaTime;
            }

        }

        //��ʊO�܂ōs���������
        if (transform.position.z <= -11.0f || Mathf.Abs(transform.position.x) >= 30.0f)
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

            //�X�R�A���Z
            ShareData.Instance.score += getScore;
            //����
            Destroy(this.gameObject);
        }

        //if (collision.collider.tag == "Player")
        //{
        //    Destroy(this.gameObject);
        //}
    }
}
