using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryBullet : MonoBehaviour
{
    //’e‚Ì‘¬“x
    [SerializeField]
    float BulletSpeed = 2.0f;

    //’e‚ÌÁ–Å‚Ü‚Å‚ÌŠÔ
    [SerializeField]
    float BulletDisappearTime = 6;

    //’e‚ÌŠp“x
    private Quaternion m_BulletRotate;


    

    // Start is called before the first frame update
    void Start()
    {
        //’e‚ÌŠp“x‚Ì‰Šú‰»
        m_BulletRotate = new Quaternion(0, 0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //’e‚ÌˆÚ“®ƒxƒNƒgƒ‹‚Ì¶¬
        Vector3 movementSpeed = new Vector3(0, 0, BulletSpeed);
        movementSpeed = m_BulletRotate * movementSpeed * Time.deltaTime;
        //’e‚ğˆÚ“®‚³‚¹‚é
        transform.Translate(movementSpeed);

        //’e‚ªÁ–Å‚·‚éŠÔ‚É‚È‚Á‚½‚çÁ‚·
        if (BulletDisappearTime <= 0)
        {
            Destroy(this.gameObject);
        }
        //’e‚ÌÁ–Å‚Ü‚Å‚ÌŠÔ‚ğŒ¸‚ç‚·
        BulletDisappearTime -= Time.deltaTime;
    }

   
    void OnCollisionEnter(Collision collision)
    {
        //ƒvƒŒƒCƒ„[‚É“–‚½‚Á‚½ê‡‚Ìˆ—
        if (collision.gameObject.tag == "Player")
        {
            //’e‚ğÁ‚·
            Destroy(this.gameObject);
        }
    }

}
