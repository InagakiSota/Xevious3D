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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //�̗͂�0�Ȃ甚�j����
        if (Life <= 0)
        {
            //�e���Ƃ�����
            Destroy(transform.parent.gameObject);

            //�����̃p�[�e�B�N���Đ�
            var exlotion = Instantiate(Explosion, this.transform.position, this.transform.rotation);
            var explotsionParticle = exlotion.GetComponent<ParticleSystem>();
            explotsionParticle.Play();

            //�X�R�A���Z
            ShareData.Instance.score = Score;
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
