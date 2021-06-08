using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelMove : MonoBehaviour
{
    //狙うオブジェクト
    GameObject TargetObject;

    //回転速度
    [SerializeField]
    float RotateSpeed = 2.5f;


    // Start is called before the first frame update
    void Start()
    {
        TargetObject = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //砲身ををターゲットの方に向ける
        RotateBarrel();
    }

    private void RotateBarrel()
    {
        //ターゲット方向へのベクトルを計算
        Vector3 toTargetVec = TargetObject.transform.position - this.transform.position;

        //回転する角度を計算する        
        Vector3 toTargetRot = Vector3.RotateTowards(transform.forward, toTargetVec, RotateSpeed * Time.deltaTime, 0f);

        //回転角度を出す
        Quaternion toQuaternion = Quaternion.LookRotation(toTargetRot);
    
        //回転させる
        transform.rotation = toQuaternion;


       
    }
}
