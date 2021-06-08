using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    //ステージ
    [SerializeField]
    GameObject[] stageData = new GameObject[5];

    //ステージ
    GameObject[] stage = new GameObject[5];

    [SerializeField]
    Vector3[] stagePos = new Vector3[5];

    //速さ
    [SerializeField]
    float speed = 0.2f;

    //消去位置
    [SerializeField]
    Vector3 destroyPos = new Vector3(0.0f, 0.0f, -120.0f);

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < stageData.Length; i++)
        {
            stage[i] = Instantiate(stageData[i], stagePos[i], Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //地面の数だけ回す
        for (int i = 0; i < stage.Length; i++)
        {
            //速度
            Vector3 vel = new Vector3(0, 0, speed);
            stage[i].transform.position += vel * Time.deltaTime;

            //消去位置まで
            if (stage[i].transform.position.z <= destroyPos.z)
            {
                Destroy(stage[i].gameObject);

                //位置を最後尾まで移動
                Vector3 pos =
                    new Vector3(stage[i].transform.position.x,
                    stage[i].transform.position.y,
                    //Backのscaleが基準となるBoxの1/100のscaleだったので/100
                    stage[stage.Length - 1].transform.position.z - stage[i].transform.localScale.z / 100);

                stage[i] = Instantiate(stageData[i], pos, Quaternion.identity);
                    

                GameObject temp;
                temp = stage[i];
                stage[i] = stage[stage.Length - 1];
                stage[stage.Length - 1] = temp;
            }
        }
    }
}
