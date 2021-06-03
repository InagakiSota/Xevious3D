using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShotController : MonoBehaviour
{
    //ボス
    GameObject boss;

    //弾
    [SerializeField]
    GameObject bullet;

    //再び打つまでの時間
    [SerializeField]
    float shotTimer;

    //時間を計るタイマー
    float nowShotTimer;

    //どこから弾出る
    Vector3 shotPos;

    //爆発のパーティクル
    [SerializeField] GameObject m_explosion;


    //ダメージを受けた際どれぐらいライフが減るか
    [SerializeField]
    int damegePoint;
    

    // Start is called before the first frame update
    void Start()
    {
        //ゲーム状に出てるボスを探す
        boss = GameObject.Find("BossPrefab(Clone)");

        //タイマーに時間設定
        nowShotTimer = shotTimer;
    }

    // Update is called once per frame
    void Update()
    {
        //ボス自体が弾打てる状況なら
        if (boss.GetComponent<BossController>().GetBossShotFlag())
        {
            //時間減らす
            nowShotTimer -= Time.deltaTime;

            //０になった
            if (nowShotTimer <= 0.0f)
            {
                Vector3 shotPos = new Vector3(transform.position.x, 0.0f, transform.position.z);

                //弾生成する
                Instantiate(bullet, shotPos, Quaternion.identity);

                //時間設定
                nowShotTimer = shotTimer;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //プレイヤーの弾当たった
        if (collision.collider.tag == "PlayerBullet")
        {
            //ボスにダメージ
            boss.GetComponent<BossController>().DamegePoint(damegePoint);

            //爆発のパーティクル再生
            var exlotion = Instantiate(m_explosion, transform.position, Quaternion.identity);
            var explotsionParticle = exlotion.GetComponent<ParticleSystem>();
            explotsionParticle.Play();

            //スコア加算
            ShareData.Instance.score += 1000;

            //このパーツは消す
            Destroy(this.gameObject);
        }
    }
}
