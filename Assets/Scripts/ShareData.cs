using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShareData : MonoBehaviour
{
    public static readonly ShareData Instance = new ShareData();

    public int score = 0;//スコア

    public int life = 3;

    //スコアの書き込み方
    //ShareData.Instance.chip = 値;

    //スコアの読み込み方
    //変数 = ShareData.Instance.chip;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
