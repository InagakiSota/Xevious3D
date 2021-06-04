using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoundationMove : MonoBehaviour
{
    //狙うオブジェクト
    [SerializeField]
    GameObject TargetObject;

    //砲身オブジェクト
    [SerializeField]
    GameObject BarrelObject;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //土台をターゲットの方に回転させる
        RotateFoundtion();
    }


    //土台をターゲットの方に回転させる
    private void RotateFoundtion()
    {
        //砲身から角度を取得する
        this.transform.rotation = Quaternion.Euler(0, BarrelObject.transform.localEulerAngles.y, 0);      
    }
}
