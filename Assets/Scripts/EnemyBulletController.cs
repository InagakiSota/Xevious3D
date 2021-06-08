using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    //速度
    [SerializeField]
    float speed;

    //プレイヤー
    GameObject player;

    //追尾先へのベクトル
    Vector3 targetVector;

    // Start is called before the first frame update
    void Start()
    {
        //プレイヤー探す
        player = GameObject.Find("Player");

        //追尾先への単位ベクトルを作成
        targetVector = player.transform.position - this.transform.position;
        targetVector.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        //動く
        transform.position += targetVector * speed * Time.deltaTime;

        //画面外いったら消去
        if (transform.position.z <= -50.0f || Mathf.Abs(transform.position.x) >= 50.0f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //プレイヤーに当たったら消去
        if (collision.collider.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
