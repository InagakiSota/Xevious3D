using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy02Controller : MonoBehaviour
{
 //プレイヤー
    GameObject player;
    //プレイヤーの座標
    Vector3 m_playerPos;

    //弾
    [SerializeField]
    GameObject bullet;

    //速さ
    [SerializeField]
    float speed = 0.4f;

    //回転速度
    [SerializeField]
    float rotateSpeed = 30;

    //ライフ
    [SerializeField]
    float life = 1;

    //消去位置
    [SerializeField]
    Vector3 destroyPos = new Vector3(30.0f, 20.0f, -11.0f);

    //死んだとき貰えるスコア
    [SerializeField]
    int getScore;

    //爆発のパーティクル
    [SerializeField] GameObject m_explosionParticle;

    //追尾する物の位置
    Vector3 targetPosition;

    //追尾する物までのベクトル
    Vector3 targetVector;

    //打てるか
    bool shotFlag = true;

    //打つまでの時間
    float shotTimer;

    //何回打つか
    int shotNum = 1;

    // Start is called before the first frame update
    void Start()
    {
        //プレイヤーを探す
        player = GameObject.Find("Player");

        //追尾先までの単位ベクトル
        targetVector = player.transform.position - this.transform.position;
        targetVector.Normalize();

        //追尾先の位置
        targetPosition = player.transform.position;

        //打つまでの時間をランダム設定
        shotTimer = Random.Range(0.0f, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        //プレイヤーが存在していれば座標を代入
        if (player != null)
        {
            m_playerPos = player.transform.position;
        }
        //打てる
        if (shotFlag == true)
        {
            //追尾先まで移動（追尾先は更新していないため追ってはこない）
            transform.position += targetVector * speed * Time.deltaTime;

            //xの値の差が1.0f以下なら打つ
            if (Mathf.Abs(transform.position.x - player.transform.position.x) <= 2.0f)
            {
                shotFlag = false;
            }
        }
        else
        {
            //＋方向にいたら右方向の画面外に
            if (transform.position.x > 0.0f)
            {
                //速度設定
                Vector3 vel = new Vector3(-1.1f * targetVector.x * speed, 0.0f, targetVector.z * speed);
               
                //動く
                transform.position += vel * Time.deltaTime;

                //回転
                transform.Rotate(0.0f, 0.0f, -rotateSpeed * Time.deltaTime);
            }
            //―方向にいたら左方向の画面外に
            else
            {
                //速度設定
                Vector3 vel = new Vector3(-1.1f * targetVector.x * speed, 0.0f, targetVector.z * speed);
                
                //動く
                transform.position += vel * Time.deltaTime;

                //回転
                transform.Rotate(0.0f, 0.0f, rotateSpeed * Time.deltaTime);
            }

            //タイマーが0になって弾があるなら
            if (shotTimer <= 0.0f && shotNum > 0)
            {
                //銃口設定
                Vector3 shotPos = this.transform.position;
                shotPos.z -= 1.0f;

                //弾減らす
                shotNum--;

                //弾生成
                if (player != null)
                    Instantiate(bullet, shotPos, Quaternion.identity);
            }
            else
            {
                shotTimer -= Time.deltaTime;
            }

        }

        //画面外まで行ったら消去
        if (transform.position.z <= -11.0f || Mathf.Abs(transform.position.x) >= 30.0f)
        {
            Destroy(this.gameObject);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        //相手の弾当たったら
        if (collision.collider.tag == "PlayerBullet")
        {
            //爆発のパーティクル再生
            var exlotion = Instantiate(m_explosionParticle, transform.position, Quaternion.identity);
            var explotsionParticle = exlotion.GetComponent<ParticleSystem>();
            explotsionParticle.Play();

            //スコア加算
            ShareData.Instance.score += getScore;
            //消去
            Destroy(this.gameObject);
        }

        //if (collision.collider.tag == "Player")
        //{
        //    Destroy(this.gameObject);
        //}
    }
}
