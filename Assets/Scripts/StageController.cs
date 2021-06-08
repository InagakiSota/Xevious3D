using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    //�X�e�[�W
    [SerializeField]
    GameObject[] stageData = new GameObject[5];

    //�X�e�[�W
    GameObject[] stage = new GameObject[5];

    [SerializeField]
    Vector3[] stagePos = new Vector3[5];

    //����
    [SerializeField]
    float speed = 0.2f;

    //�����ʒu
    [SerializeField]
    Vector3 destroyPos = new Vector3(0.0f, 0.0f, -120.0f);

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < stageData.Length; i++)
        {
            stage[i] = Instantiate(stageData[i], stagePos[i], Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //�n�ʂ̐�������
        for (int i = 0; i < stage.Length; i++)
        {
            //���x
            Vector3 vel = new Vector3(0, 0, speed);
            stage[i].transform.position += vel * Time.deltaTime;

            //�����ʒu�܂�
            if (stage[i].transform.position.z <= destroyPos.z)
            {
                Destroy(stage[i].gameObject);

                //�ʒu���Ō���܂ňړ�
                Vector3 pos =
                    new Vector3(stage[i].transform.position.x,
                    stage[i].transform.position.y,
                    //Back��scale����ƂȂ�Box��1/100��scale�������̂�/100
                    stage[stage.Length - 1].transform.position.z - stage[i].transform.localScale.z / 100);

                stage[i] = Instantiate(stageData[i], pos, Quaternion.identity);
                    

                GameObject temp;
                temp = stage[i];
                stage[i] = stage[stage.Length - 1];
                stage[stage.Length - 1] = temp;
            }
        }
    }
}
