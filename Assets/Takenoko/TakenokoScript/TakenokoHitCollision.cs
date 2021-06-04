using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakenokoHitCollision : MonoBehaviour
{
    //ソルの体力
    [SerializeField]
    int Life = 1;

    //爆破エフェクトのPrefab
    [SerializeField]
    GameObject Explosion;

    //倒したときのスコア
    [SerializeField]
    int Score = 1000;


    // Start is called before the first frame update
    void Start()
    {
        //座標を初期化する
        this.transform.position = transform.parent.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //体力が0なら爆破する
        if (Life <= 0)
        {
            //親ごとを消す
            Destroy(transform.parent.gameObject);

            //爆発のパーティクル再生
            var exlotion = Instantiate(Explosion, this.transform.position, this.transform.rotation);
            var explotsionParticle = exlotion.GetComponent<ParticleSystem>();
            explotsionParticle.Play();

            //スコア加算
            ShareData.Instance.score = Score;
        }

    }

    //当たり判定
    void OnCollisionEnter(Collision collision)
    {
        //プレイヤーの弾が当たった時の判定
        if (collision.gameObject.tag == "PlayerBullet")
        {
            //体力を減らす
            Life--;
        }
    }
}
