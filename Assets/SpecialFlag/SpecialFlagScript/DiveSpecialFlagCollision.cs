using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiveSpecialFlagCollision : MonoBehaviour
{
    //プレイヤーの弾が当たったか
    private bool m_HitFlag;

    //上に上がるスピード
    [SerializeField]
    float MoveSpeed = 3.0f;

    //移動先の座標
    private Vector3 m_MovePos;

    //生成する当たり判定
    [SerializeField]
    GameObject TakenokoHitCollisionObj;

    //プレイヤー
    [SerializeField]
    GameObject PlayerObj;



    // Start is called before the first frame update
    void Start()
    {
        m_HitFlag = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        //当たったら上に上がる
        if (m_HitFlag == true)
        {
            //移動先の座標を計算して保存する
            m_MovePos = new Vector3(transform.parent.position.x, PlayerObj.transform.position.y, transform.parent.position.z);

            //移動先を計算する
            Vector3 moveVel = Vector3.MoveTowards(transform.parent.transform.position, m_MovePos, MoveSpeed * Time.deltaTime);

            //うえに上がりきっていたら
            if (moveVel == transform.parent.transform.position)
            {
                //消す
                Destroy(this.gameObject);
                //スペシャルフラッグの当たり判定を生成する
                Instantiate(TakenokoHitCollisionObj, transform.parent.transform.position, Quaternion.identity, transform.parent.transform);

                //移動の開始
                MoveSpecialFlag.moveFlag = true;

            }
            //移動させる
            transform.parent.transform.position = moveVel;


        }

    }

    void OnCollisionEnter(Collision collision)
    {
        //プレイヤーの弾が当たった後の当たり判定は行わない
        if (m_HitFlag == true)
        {
            return;
        }

        //プレイヤーの弾が当たった時の判定
        if (collision.gameObject.tag == "PlayerBullet")
        {

            m_HitFlag = true;
            //当たり判定を消す
            this.GetComponent<Collider>().enabled = false;
        }
    }
}
