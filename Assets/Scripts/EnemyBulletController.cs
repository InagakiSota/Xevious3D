using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    //���x
    [SerializeField]
    float speed;

    //�v���C���[
    GameObject player;

    //�ǔ���ւ̃x�N�g��
    Vector3 targetVector;

    // Start is called before the first frame update
    void Start()
    {
        //�v���C���[�T��
        player = GameObject.Find("Player");

        //�ǔ���ւ̒P�ʃx�N�g�����쐬
        targetVector = player.transform.position - this.transform.position;
        targetVector.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        //����
        transform.position += targetVector * speed;

        //��ʊO�����������
        if (transform.position.z <= -20.0f || Mathf.Abs(transform.position.x) >= 50.0f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //�v���C���[�ɓ������������
        if (collision.collider.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
