using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpecialFlag : MonoBehaviour
{
    //�U��
    [SerializeField]
    float amplitude = 0.03f;

    //�������x
    [SerializeField]
    float MoveSpeed = 4.0f;

    //�t���[���J�E���g
    private int frameCount = 0;

    //�t���[���J�E���g�̃��[�v����
    const int frameCountLoop = 10000;

    //�ړ��̃t���O
    public static bool moveFlag;

    // Start is called before the first frame update
    void Start()
    {
        moveFlag = false;
    }

    // Update is called once per frame
    void Update()
    {

        if(moveFlag==false)
        {
            return;
        }
        frameCount++;
        //�J�E���g�����[�v������
        if (frameCountLoop <= frameCount)
        {
            frameCount = 0;
        }
        //2�t���[����1��Ăяo��
        if (0 == frameCount % 2)
        {
            // �㉺�ɐU��������i�ӂ�ӂ��\���j
            float posYSin = Mathf.Sin(MoveSpeed * Mathf.PI * (float)(frameCount % 200) / (200.0f - 1.0f));
            this.transform.Translate(new Vector3(0, amplitude * posYSin, 0));
        }
    }

   

}
