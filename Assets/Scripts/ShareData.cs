using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/////スコアの書き込み方
//ShareData.Instance.score = 値;
//スコアの読み込み方
//変数 = ShareData.Instance.score;
/// </summary>

public class ShareData : MonoBehaviour
{
    public static readonly ShareData Instance = new ShareData();

    public int score = 0;//スコア

    //ハイスコア
    public int hiScore = 0;

    public int life = 2;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
