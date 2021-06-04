using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreate : MonoBehaviour
{
    // Start is called before the first frame update
    //�G�̐������鏇��
    enum eEnemyCreateState
    {
        eEnemy01,
        eEnemy02,
        eFlyingBoard,

        eEnemyNum
    }

    //�G01
    [SerializeField]
    GameObject enemy01;

    //�G02
    [SerializeField]
    GameObject enemy02;

    //�G03
    [SerializeField]
    GameObject enemy03;

    //�o�L����
    [SerializeField]
    GameObject flyingBoard;

    //�{�X
    [SerializeField]
    GameObject boss;

    //�G�̐؂�ւ��X�e�[�g
    eEnemyCreateState eECS;

    //��ʏ�̓G�̍ő吔
    [SerializeField]
    int maxEnemyNum = 1;

    //��ʏ�̓G�̐�
    int enemyNum;

    //�G01�����^�C�}�[
    [SerializeField]
    float create01Timer = 1;

    //�G01�����^�C�}�[
    [SerializeField]
    float create02Timer = 1;

    //�o�L���������^�C�}�[
    [SerializeField]
    float createFBTimer = 1;

    //�G�̐����؂�ւ�����
    [SerializeField]
    float createChangeTimer;

    [SerializeField]
    float createBossTimer;

    //�Q�[���J�n���̃e�L�X�g
    [SerializeField] GameObject m_startText;

    //���݂̃^�C�}�[
    float[] timer = new float[3];

    GameObject player;


    void Start()
    {
        //�G�̐�
        enemyNum = 0;

        //[0]�G�������܂ł̃^�C�}�[ [1]��������G��؂�ւ���܂ł̎���
        //[2]�{�X���o��������܂ł̎���
        timer[0] = create01Timer;
        timer[1] = createChangeTimer;
        timer[2] = createBossTimer;

        //�ŏ��ɐ�������G�ݒ�
        eECS = eEnemyCreateState.eEnemy01;
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.Find("Player");

        if (m_startText != null) return;

        //��������G
        switch (eECS)
        {
            case eEnemyCreateState.eEnemy01:
            {
               //��C�ɐ������鐔�̐���
               if (enemyNum < maxEnemyNum)
               {
                   //�ꏊ�������_������
                   Vector3 pos = new Vector3(Random.Range(-20.0f, 20.0f), 0.0f, 15.0f);
                   
                   if(player != null)
                    Instantiate(enemy01, pos, Quaternion.identity);

                   timer[0] = create01Timer;

                   enemyNum++;
               }

               //�}�b�N�X�܂ł�������
               if (enemyNum >= maxEnemyNum)
               {
                   //������Ƒ҂�
                   if (timer[0] > 0)
                   {
                       timer[0] -= Time.deltaTime;
                   }
                   else
                   {
                       //�Ăю��Ԑݒ肵��
                       timer[0] = create01Timer;
                       //�G�̐���0�Ƃ���
                       enemyNum = 0;
                   }
               }
                    break;
            }

            case eEnemyCreateState.eEnemy02:
                {
                    //��C�ɐ������鐔�̐���
                    if (enemyNum < maxEnemyNum)
                    {
                        //�ꏊ�������_������
                        Vector3 pos = new Vector3(Random.Range(-20.0f, 20.0f), 0.0f, 15.0f);
                        if (player != null)
                            Instantiate(enemy02, pos, Quaternion.identity);

                        timer[0] = create02Timer;

                        enemyNum++;
                    }

                    //�}�b�N�X�܂ł�������
                    if (enemyNum >= maxEnemyNum)
                    {
                        //������Ƒ҂�
                        if (timer[0] > 0)
                        {
                            timer[0] -= Time.deltaTime;
                        }

                        else
                        {
                            //�Ăю��Ԑݒ肵��
                            timer[0] = create02Timer;
                            //�G�̐���0�Ƃ���
                            enemyNum = 0;
                        }
                    }
                    break;
                }

            case eEnemyCreateState.eFlyingBoard:
            {
                //���Ԍ��炷
                if (timer[0] > 0)
                {
                    timer[0] -= Time.deltaTime;
                }
              
                else
                {
                    //�ꏊ���߂Đ���
                    Vector3 pos = new Vector3(Random.Range(-20.0f, 20.0f), 0.0f, 19.0f);
                        if (player != null)
                            Instantiate(flyingBoard, pos, Quaternion.identity);
                    
                    //���Ԃ�ݒ�
                    timer[0] = createFBTimer;
                }
                break;
            }

            
        }

        //�i���j��������G��ς���
        timer[1] -= Time.deltaTime;
        if (timer[1] <= 0.0f)
        {
            //���̓G�ɂ���
            eECS++;
            
            if (eECS >= eEnemyCreateState.eEnemyNum)
            {
                eECS = eEnemyCreateState.eEnemy01;
            }

            //���Ԃ�ݒ�
            timer[1] = createChangeTimer;
        }

        //�{�X�𐶐�
        timer[2] -= Time.deltaTime;
        if (timer[2] <= 0.0f)
        {
            //�f�o�b�O�i�K�ŕ����ł��炾�邢��Ō�łȂ�Ƃ����悤
            timer[2] = 999999999999999999999.0f;

            //�����ʒu���߂Đ���
            Vector3 pos = new Vector3(0.0f, -5.0f, 32.0f);
            //���f���΂߂ɂȂ��Ă����炢�������ɒ������܂����B
            Instantiate(boss, pos, Quaternion.Euler(0.0f, -68.563f, 0.0f));
        }
       
    }
}
