using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingBoardController : MonoBehaviour
{
    //速さ
    [SerializeField]
    float speed = 0.2f;

    //回転する速さ
    [SerializeField]
    float rotateSpeed = -5.0f;

    [SerializeField]
    Vector3 destroyPos = new Vector3(30.0f, 20.0f, -20.0f);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //速度設定
        Vector3 vel = new Vector3(0, 0, speed);
        //動く
        transform.position -= vel;
        //回転
        transform.Rotate(rotateSpeed, 0, 0);

        //消去
        if (transform.position.z <= destroyPos.z)
        {
            Destroy(this.gameObject);
        }
    }
}
