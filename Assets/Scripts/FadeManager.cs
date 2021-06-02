using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/********************************************************************************************
 * FadeManagerの使い方                                                                      *
 * ・遷移した先のシーンのStartでFadeManager.FadeIn();と記述する                             *
 * ・シーンを遷移させたいタイミングでFadeManager.FadeOut("遷移先のシーン名");と記述する     *
 ********************************************************************************************/

public class FadeManager : MonoBehaviour
{
    private static Canvas fadeCanvas;               //フェード用キャンバス
    private static Image fadeImage;                 //フェード用画像

    private static float alpha = 0.0f;              //画像の透明度

    public static bool fadeInFlag = false;          //フェードイン用フラグ
    public static bool fadeOutFlag = false;         //フェードアウト用フラグ

    private static string nextScene = ""; //次のシーン

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //フェードアウトフラグが立ったら
        if (fadeOutFlag == true)
        {
            //画像の透明度を増やす
            alpha += Time.deltaTime;
            if (alpha >= 1.0f)
            {
                alpha = 1.0f;
                fadeOutFlag = false;

                SceneManager.LoadScene(nextScene);
            }
        }
        //フェードインフラグが立ったら
        else if(fadeInFlag == true)
        {
            //画像の透明度を減らす
            alpha -= Time.deltaTime;
            if (alpha <= 0.0f)
            {
                alpha = 0.0f;
                fadeInFlag = false;
            }
        }

        fadeImage.color = new Color(0.0f, 0.0f, 0.0f, alpha);
    }

    ///////////////////////////
    //フェードイン
    //引数:なし
    //戻り値:なし
    //////////////////////////
    public static void FadeIn()
    {
        if (fadeOutFlag == false && fadeInFlag == false)
        {
            if (fadeImage == null) Initialize();

            //フェードアウトのフラグを立てる
            alpha = 1.0f;
            fadeInFlag = true;
        }
    }

    ///////////////////////////
    //フェードアウト
    //引数:遷移先のシーン名
    //戻り値:なし
    //////////////////////////
    public static void FadeOut(string nextSceneName)
    {
        if (fadeOutFlag == false && fadeInFlag == false)
        {
            if (fadeImage == null) Initialize();

            //フェードアウトのフラグを立てる
            alpha = 0.0f;
            fadeOutFlag = true;

            nextScene = nextSceneName;
        }
    }


    //初期化処理
    static void Initialize()
    {
        //フェード用のキャンバスの生成
        GameObject FadeCanvasObject = new GameObject("FadeCanvas");
        fadeCanvas = FadeCanvasObject.AddComponent<Canvas>();
        FadeCanvasObject.AddComponent<FadeManager>();
        fadeCanvas.renderMode = RenderMode.ScreenSpaceOverlay;

        //最前面になるよう適当なソートオーダー設定
        fadeCanvas.sortingOrder = 100;

        //フェード用の画像の生成
        fadeImage = new GameObject("FadeImage").AddComponent<Image>();
        fadeImage.transform.SetParent(fadeCanvas.transform, false);
        fadeImage.rectTransform.anchoredPosition = Vector3.zero;

        fadeImage.rectTransform.sizeDelta = new Vector2(10000, 10000);
    }


}
