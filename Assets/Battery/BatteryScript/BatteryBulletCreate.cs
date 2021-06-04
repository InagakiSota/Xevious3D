using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryBulletCreate : MonoBehaviour
{
    //’e‚Ì¶¬ŠÔŠu
    [SerializeField]
    int CreateBulletInterval = 180;

    //’e‚Ì¶¬ŠÔŠu‚ÌƒJƒEƒ“ƒg
    private float m_BulletIntervalCount = 0;

    //”­¶‚³‚¹‚é’e‚ÌPrefab
    [SerializeField]
    GameObject BatteryBulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //’e‚Ì¶¬ŠÔŠuƒJƒEƒ“ƒg‚ª¶¬ŠÔŠu‚É‚È‚ê‚Î’e‚ğ¶¬‚·‚é
        if (m_BulletIntervalCount >= CreateBulletInterval)
        {
            m_BulletIntervalCount = 0;
            //’e‚Ì¶¬
            Instantiate(BatteryBulletPrefab, this.transform.position, this.transform.rotation);
        }
        //’e‚ÌƒJƒEƒ“ƒg‚ği‚ß‚é
        m_BulletIntervalCount+= Time.deltaTime;
    }
}
