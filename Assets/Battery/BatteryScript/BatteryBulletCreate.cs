using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BatteryBulletCreate : MonoBehaviour
{
    //�e�̐����Ԋu
    [SerializeField]
    float CreateBulletInterval = 180;

    //�e�̐����Ԋu�̃J�E���g
    private float  m_BulletIntervalCount = 0;

    //����������e��Prefab
    [SerializeField]
    GameObject BatteryBulletPrefab;

    bool shotFlag = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //�e�̐����Ԋu�J�E���g�������Ԋu�ɂȂ�Βe�𐶐�����
        if (m_BulletIntervalCount >= CreateBulletInterval)
        {
            m_BulletIntervalCount = 0;

            if (shotFlag == true)
            {
                //�e�̐���
                Instantiate(BatteryBulletPrefab, this.transform.position, this.transform.rotation);
            }
            
        }
        //�e�̃J�E���g��i�߂�
        m_BulletIntervalCount += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            shotFlag = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            shotFlag = false;
        }
    }
}
