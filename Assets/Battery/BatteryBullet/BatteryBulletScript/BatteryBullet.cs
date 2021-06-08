using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryBullet : MonoBehaviour
{
    //�e�̑��x
    [SerializeField]
    float BulletSpeed = 2.0f;

    //�e�̏��ł܂ł̎���
    [SerializeField]
    float BulletDisappearTime = 6;

    //�e�̊p�x
    private Quaternion m_BulletRotate;


    

    // Start is called before the first frame update
    void Start()
    {
        //�e�̊p�x�̏�����
        m_BulletRotate = new Quaternion(0, 0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //�e�̈ړ��x�N�g���̐���
        Vector3 movementSpeed = new Vector3(0, 0, BulletSpeed);
        movementSpeed = m_BulletRotate * movementSpeed * Time.deltaTime;
        //�e���ړ�������
        transform.Translate(movementSpeed);

        //�e�����ł��鎞�ԂɂȂ��������
        if (BulletDisappearTime <= 0)
        {
            Destroy(this.gameObject);
        }
        //�e�̏��ł܂ł̎��Ԃ����炷
        BulletDisappearTime -= Time.deltaTime;
    }

   
    void OnCollisionEnter(Collision collision)
    {
        //�v���C���[�ɓ��������ꍇ�̏���
        if (collision.gameObject.tag == "Player")
        {
            //�e������
            Destroy(this.gameObject);
        }
    }

}
