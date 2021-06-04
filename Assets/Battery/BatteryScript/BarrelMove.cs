using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelMove : MonoBehaviour
{
    //�_���I�u�W�F�N�g
    [SerializeField]
    GameObject TargetObject;

    //��]���x
    [SerializeField]
    float RotateSpeed = 2.5f;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //�C�g�����^�[�Q�b�g�̕��Ɍ�����
        RotateBarrel();
    }

    private void RotateBarrel()
    {
        //�^�[�Q�b�g�����ւ̃x�N�g�����v�Z
        Vector3 toTargetVec = TargetObject.transform.position - this.transform.position;

        //��]����p�x���v�Z����        
        Vector3 toTargetRot = Vector3.RotateTowards(transform.forward, toTargetVec, RotateSpeed * Time.deltaTime, 0f);

        //��]�p�x���o��
        Quaternion toQuaternion = Quaternion.LookRotation(toTargetRot);
    
        //��]������
        transform.rotation = toQuaternion;


       
    }
}
