using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialFlagHitCollision : MonoBehaviour
{
   

    // Start is called before the first frame update
    void Start()
    {
        //���W������������
        this.transform.position = transform.parent.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
       

    }

    //�����蔻��
    void OnCollisionEnter(Collision collision)
    {
        //�v���C���[�̒e�������������̔���
        if (collision.gameObject.tag == "Player")
        {
            //�e���Ƃ�����
            Destroy(transform.parent.gameObject);

            //�c�@�𑝂₷
        }
    }
}
