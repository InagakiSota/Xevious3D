using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryBullet : MonoBehaviour
{
    //弾の速度
    [SerializeField]
    float BulletSpeed = 2.0f;

    //弾の消滅までの時間
    [SerializeField]
    float BulletDisappearTime = 6;

    //弾の角度
    private Quaternion m_BulletRotate;


    

    // Start is called before the first frame update
    void Start()
    {
        //弾の角度の初期化
        m_BulletRotate = new Quaternion(0, 0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //弾の移動ベクトルの生成
        Vector3 movementSpeed = new Vector3(0, 0, BulletSpeed);
        movementSpeed = m_BulletRotate * movementSpeed * Time.deltaTime;
        //弾を移動させる
        transform.Translate(movementSpeed);

        //弾が消滅する時間になったら消す
        if (BulletDisappearTime <= 0)
        {
            Destroy(this.gameObject);
        }
        //弾の消滅までの時間を減らす
        BulletDisappearTime -= Time.deltaTime;
    }

   
    void OnCollisionEnter(Collision collision)
    {
        //プレイヤーに当たった場合の処理
        if (collision.gameObject.tag == "Player")
        {
            //弾を消す
            Destroy(this.gameObject);
        }
    }

}
