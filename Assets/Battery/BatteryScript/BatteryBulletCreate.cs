using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryBulletCreate : MonoBehaviour
{
    //弾の生成間隔
    [SerializeField]
    int CreateBulletInterval = 180;

    //弾の生成間隔のカウント
    private int m_BulletIntervalCount = 0;

    //発生させる弾のPrefab
    [SerializeField]
    GameObject BatteryBulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //弾の生成間隔カウントが生成間隔になれば弾を生成する
        if (m_BulletIntervalCount >= CreateBulletInterval)
        {
            m_BulletIntervalCount = 0;
            //弾の生成
            Instantiate(BatteryBulletPrefab, this.transform.position, this.transform.rotation);
        }
        //弾のカウントを進める
        m_BulletIntervalCount++;
    }
}
