using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreate : MonoBehaviour
{
    // Start is called before the first frame update
    //敵の生成する順番
    enum eEnemyCreateState
    {
        eEnemy01,
        eEnemy02,
        eFlyingBoard,

        eEnemyNum
    }

    //敵01
    [SerializeField]
    GameObject enemy01;

    //敵02
    [SerializeField]
    GameObject enemy02;

    //敵03
    [SerializeField]
    GameObject enemy03;

    //バキュラ
    [SerializeField]
    GameObject flyingBoard;

    //ボス
    [SerializeField]
    GameObject boss;

    //敵の切り替えステート
    eEnemyCreateState eECS;

    //画面上の敵の最大数
    [SerializeField]
    int maxEnemyNum = 1;

    //画面上の敵の数
    int enemyNum;

    //敵01生成タイマー
    [SerializeField]
    float create01Timer = 1;

    //敵01生成タイマー
    [SerializeField]
    float create02Timer = 1;

    //バキュラ生成タイマー
    [SerializeField]
    float createFBTimer = 1;

    //敵の生成切り替え時間
    [SerializeField]
    float createChangeTimer;

    [SerializeField]
    float createBossTimer;

    //現在のタイマー
    float[] timer = new float[3];

    GameObject player;


    void Start()
    {
        //敵の数
        enemyNum = 0;

        //[0]敵が作られるまでのタイマー [1]生成する敵を切り替えるまでの時間
        //[2]ボスを出現させるまでの時間
        timer[0] = create01Timer;
        timer[1] = createChangeTimer;
        timer[2] = createBossTimer;

        //最初に生成する敵設定
        eECS = eEnemyCreateState.eEnemy01;
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.Find("Player");

        //生成する敵
        switch (eECS)
        {
            case eEnemyCreateState.eEnemy01:
            {
               //一気に生成する数の制限
               if (enemyNum < maxEnemyNum)
               {
                   //場所をランダム生成
                   Vector3 pos = new Vector3(Random.Range(-20.0f, 20.0f), 0.0f, 15.0f);
                   
                   if(player != null)
                    Instantiate(enemy01, pos, Quaternion.identity);

                   timer[0] = create01Timer;

                   enemyNum++;
               }

               //マックスまでいったら
               if (enemyNum >= maxEnemyNum)
               {
                   //ちょっと待つ
                   if (timer[0] > 0)
                   {
                       timer[0] -= Time.deltaTime;
                   }
                   else
                   {
                       //再び時間設定して
                       timer[0] = create01Timer;
                       //敵の数を0とする
                       enemyNum = 0;
                   }
               }
                    break;
            }

            case eEnemyCreateState.eEnemy02:
                {
                    //一気に生成する数の制限
                    if (enemyNum < maxEnemyNum)
                    {
                        //場所をランダム生成
                        Vector3 pos = new Vector3(Random.Range(-20.0f, 20.0f), 0.0f, 15.0f);
                        if (player != null)
                            Instantiate(enemy02, pos, Quaternion.identity);

                        timer[0] = create02Timer;

                        enemyNum++;
                    }

                    //マックスまでいったら
                    if (enemyNum >= maxEnemyNum)
                    {
                        //ちょっと待つ
                        if (timer[0] > 0)
                        {
                            timer[0] -= Time.deltaTime;
                        }

                        else
                        {
                            //再び時間設定して
                            timer[0] = create02Timer;
                            //敵の数を0とする
                            enemyNum = 0;
                        }
                    }
                    break;
                }

            case eEnemyCreateState.eFlyingBoard:
            {
                //時間減らす
                if (timer[0] > 0)
                {
                    timer[0] -= Time.deltaTime;
                }
              
                else
                {
                    //場所決めて生成
                    Vector3 pos = new Vector3(Random.Range(-20.0f, 20.0f), 0.0f, 19.0f);
                        if (player != null)
                            Instantiate(flyingBoard, pos, Quaternion.identity);
                    
                    //時間を設定
                    timer[0] = createFBTimer;
                }
                break;
            }

            
        }

        //（仮）生成する敵を変える
        timer[1] -= Time.deltaTime;
        if (timer[1] <= 0.0f)
        {
            //次の敵にする
            eECS++;
            
            if (eECS >= eEnemyCreateState.eEnemyNum)
            {
                eECS = eEnemyCreateState.eEnemy01;
            }

            //時間を設定
            timer[1] = createChangeTimer;
        }

        //ボスを生成
        timer[2] -= Time.deltaTime;
        if (timer[2] <= 0.0f)
        {
            //デバッグ段階で複数でたらだるいんで後でなんとかしよう
            timer[2] = 999999999999999999999.0f;

            //生成位置決めて生成
            Vector3 pos = new Vector3(0.0f, -5.0f, 32.0f);
            //モデル斜めになってたからいい感じに調整しました。
            Instantiate(boss, pos, Quaternion.Euler(0.0f, -68.563f, 0.0f));
        }
       
    }
}
