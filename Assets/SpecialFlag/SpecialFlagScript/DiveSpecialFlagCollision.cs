using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiveSpecialFlagCollision : MonoBehaviour
{
    //�v���C���[�̒e������������
    private bool m_HitFlag;

    //��ɏオ��X�s�[�h
    [SerializeField]
    float MoveSpeed = 3.0f;

    //�ړ���̍��W
    private Vector3 m_MovePos;

    //�������铖���蔻��
    [SerializeField]
    GameObject TakenokoHitCollisionObj;

    //�v���C���[
    [SerializeField]
    GameObject PlayerObj;



    // Start is called before the first frame update
    void Start()
    {
        m_HitFlag = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        //�����������ɏオ��
        if (m_HitFlag == true)
        {
            //�ړ���̍��W���v�Z���ĕۑ�����
            m_MovePos = new Vector3(transform.parent.position.x, PlayerObj.transform.position.y, transform.parent.position.z);

            //�ړ�����v�Z����
            Vector3 moveVel = Vector3.MoveTowards(transform.parent.transform.position, m_MovePos, MoveSpeed * Time.deltaTime);

            //�����ɏオ�肫���Ă�����
            if (moveVel == transform.parent.transform.position)
            {
                //����
                Destroy(this.gameObject);
                //�X�y�V�����t���b�O�̓����蔻��𐶐�����
                Instantiate(TakenokoHitCollisionObj, transform.parent.transform.position, Quaternion.identity, transform.parent.transform);

                //�ړ��̊J�n
                MoveSpecialFlag.moveFlag = true;

            }
            //�ړ�������
            transform.parent.transform.position = moveVel;


        }

    }

    void OnCollisionEnter(Collision collision)
    {
        //�v���C���[�̒e������������̓����蔻��͍s��Ȃ�
        if (m_HitFlag == true)
        {
            return;
        }

        //�v���C���[�̒e�������������̔���
        if (collision.gameObject.tag == "PlayerBullet")
        {

            m_HitFlag = true;
            //�����蔻�������
            this.GetComponent<Collider>().enabled = false;
        }
    }
}
