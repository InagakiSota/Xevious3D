using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingBoardController : MonoBehaviour
{
    //ë¨Ç≥
    [SerializeField]
    float speed = 0.2f;

    //âÒì]Ç∑ÇÈë¨Ç≥
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
        //ë¨ìxê›íË
        Vector3 vel = new Vector3(0, 0, speed);
        //ìÆÇ≠
        transform.position -= vel;
        //âÒì]
        transform.Rotate(rotateSpeed, 0, 0);

        //è¡ãé
        if (transform.position.z <= destroyPos.z)
        {
            Destroy(this.gameObject);
        }
    }
}
