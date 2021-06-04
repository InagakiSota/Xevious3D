using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpecialFlag : MonoBehaviour
{
    //振幅
    [SerializeField]
    float amplitude = 0.03f;

    //動く速度
    [SerializeField]
    float MoveSpeed = 4.0f;

    //フレームカウント
    private int frameCount = 0;

    //フレームカウントのループ時間
    const int frameCountLoop = 10000;

    //移動のフラグ
    public static bool moveFlag;

    // Start is called before the first frame update
    void Start()
    {
        moveFlag = false;
    }

    // Update is called once per frame
    void Update()
    {

        if(moveFlag==false)
        {
            return;
        }
        frameCount++;
        //カウントをループさせる
        if (frameCountLoop <= frameCount)
        {
            frameCount = 0;
        }
        //2フレームに1回呼び出す
        if (0 == frameCount % 2)
        {
            // 上下に振動させる（ふわふわを表現）
            float posYSin = Mathf.Sin(MoveSpeed * Mathf.PI * (float)(frameCount % 200) / (200.0f - 1.0f));
            this.transform.Translate(new Vector3(0, amplitude * posYSin, 0));
        }
    }

   

}
