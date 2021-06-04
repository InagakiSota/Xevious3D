using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryBulletCreate : MonoBehaviour
{
    //�e�̐����Ԋu
    [SerializeField]
    int CreateBulletInterval = 180;

    //�e�̐����Ԋu�̃J�E���g
    private float m_BulletIntervalCount = 0;

    //����������e��Prefab
    [SerializeField]
    GameObject BatteryBulletPrefab;

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
            //�e�̐���
            Instantiate(BatteryBulletPrefab, this.transform.position, this.transform.rotation);
        }
        //�e�̃J�E���g��i�߂�
        m_BulletIntervalCount+= Time.deltaTime;
    }
}
