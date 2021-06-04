using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialFlagHitCollision : MonoBehaviour
{
   

    // Start is called before the first frame update
    void Start()
    {
        //座標を初期化する
        this.transform.position = transform.parent.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
       

    }

    //当たり判定
    void OnCollisionEnter(Collision collision)
    {
        //プレイヤーの弾が当たった時の判定
        if (collision.gameObject.tag == "Player")
        {
            //親ごとを消す
            Destroy(transform.parent.gameObject);

            //残機を増やす
        }
    }
}
