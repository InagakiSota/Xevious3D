using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryCollision : MonoBehaviour
{
    //�C��̗̑�
    [SerializeField]
    int Life = 1;

    //���j�G�t�F�N�g��Prefab
    [SerializeField]
    GameObject Explosion;

    //�|�����Ƃ��̃X�R�A
    [SerializeField]
    int Score = 300;

    //�I�[�f�B�I�\�[�X
    AudioSource m_audio;

    //������
    [SerializeField] AudioClip m_bombSE;


    // Start is called before the first frame update
    void Start()
    {
        m_audio = GameObject.Find("Audio Source").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //�̗͂�0�Ȃ甚�j����
        if (Life <= 0)
        {
            //�������Đ�
            m_audio.PlayOneShot(m_bombSE);

            //�e���Ƃ�����
            Destroy(transform.parent.gameObject);

            //�����̃p�[�e�B�N���Đ�
            var exlotion = Instantiate(Explosion, this.transform.position, this.transform.rotation);
            var explotsionParticle = exlotion.GetComponent<ParticleSystem>();
            explotsionParticle.Play();

            //�X�R�A���Z
            ShareData.Instance.score += Score;
        }

    }

    //�����蔻��
    void OnCollisionEnter(Collision collision)
    {
        //�v���C���[�̒e�������������̔���
        if (collision.gameObject.tag == "PlayerBullet")
        {
            //�̗͂����炷
            Life--;
        }
    }
}
