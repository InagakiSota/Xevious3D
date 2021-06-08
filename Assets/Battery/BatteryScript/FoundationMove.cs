using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoundationMove : MonoBehaviour
{
    //�_���I�u�W�F�N�g
    GameObject TargetObject;

    //�C�g�I�u�W�F�N�g
    [SerializeField]
    GameObject BarrelObject;


    // Start is called before the first frame update
    void Start()
    {
        TargetObject = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //�y����^�[�Q�b�g�̕��ɉ�]������
        RotateFoundtion();
    }


    //�y����^�[�Q�b�g�̕��ɉ�]������
    private void RotateFoundtion()
    {
        //�C�g����p�x���擾����
        this.transform.rotation = Quaternion.Euler(0, BarrelObject.transform.localEulerAngles.y, 0);      
    }
}
