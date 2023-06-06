using UnityEngine.InputSystem;
using UnityEngine;

public class custom_manager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("�e�̔��ˏꏊ")]
    private GameObject firingPoint;

    [SerializeField]
    [Tooltip("�e�̔��ˏꏊ���")]
    private GameObject firingPoint2;

    [SerializeField]
    [Tooltip("�e�̔��ˏꏊ3��")]
    private GameObject firingPoint3;

    [SerializeField]
    [Tooltip("�e�̔��ˏꏊ4��")]
    private GameObject firingPoint4;

    [SerializeField]
    [Tooltip("�e�̔��ˏꏊ5��")]
    private GameObject firingPoint5;

    [SerializeField]
    [Tooltip("�e")]
    private GameObject bullet;

    [SerializeField]
    [Tooltip("�e2")]
    private GameObject bullet2;

    [SerializeField]
    [Tooltip("�e3")]
    private GameObject bullet3;

    [SerializeField]
    [Tooltip("�e4")]
    private GameObject bullet4;

    [SerializeField]
    [Tooltip("�e5")]
    private GameObject bullet5;

    [SerializeField]
    [Tooltip("�e6")]
    private GameObject bullet6;

    private GameObject custom;


    [Tooltip("�e�̑���")]
    private float speed;




    // Update is called once per frame
    void Update()
    {

        BulletLv();

        // �Q�[���p�b�h�p
        var GamePad = Gamepad.current;
        bool bButtonDown = false;
        bool bButton = false;

        if (GamePad != null)
        {
            if (GamePad.bButton.wasPressedThisFrame) // b�{�^������������
            {
                bButtonDown = true;
            }
            if (GamePad.bButton.isPressed) // b�{�^���������Ă����
            {
                bButton = true;
            }
        }

        //���˃{�^��
        if (Input.GetKeyDown(KeyCode.G) || bButtonDown)
        {
            //���C���J�X�^��
            if (CPData.CustomNo1)
            {
                if (CPData.ArmShot_norml_Lv1)
                {
                    // �m�[�}���̒e
                    ShotNolmal_Lv1();
                }
                if (CPData.ArmShot_norml_Lv2)
                {
                    //�m�[�}���̒e���x��2
                    ShotNolmal_Lv2();
                }
            }
            if (Input.GetKey(KeyCode.G) || bButton)
            {
                if (CPData.ArmShot_norml_Lv3)
                {
                    //�m�[�}���̒e���x��3
                    ShotNolmal_Lv3();
                }
            }

            //�T�u�J�X�^��
            if (CPData.CustomNo2)
            {
                //  Debug.Log("aaaaaaaaaaaaaaaaaaa");
                ShotPenetrationLv1();

            }
        }
        //�n�C�W�����v
        if (Input.GetKeyDown(KeyCode.F3))
        {
            Leg_high();
        }









    }


    private void BulletLv()
    {
        // �Q�[���p�b�h�p
        var GamePad = Gamepad.current;
        bool yButtonDown = false;
        bool xButtonDown = false;

        if (GamePad != null)
        {
            if (GamePad.bButton.isPressed) // y�{�^������������
            {
                yButtonDown = true;
            }
            if (GamePad.bButton.isPressed) // x�{�^������������
            {
                xButtonDown = true;
            }
        }

        //���x���A�b�v
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("���x���A�b�v");
            Debug.Log(CPData.ArmShot_norml_Lv);
            CPData.ArmShot_norml_Lv += 1;
        }
        //���x���_�E��
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("���x���_�E��");
            CPData.ArmShot_norml_Lv -= 1;
        }
        //���C���E�F�|��
        if (Input.GetKeyDown(KeyCode.F1) || yButtonDown)
        {
            Custom_false();
            CPData.CustomNo1 = true;
            // Debug.Log("���C���E�F�|���ɕς����");
        }
        //�T�u�E�F�|��
        if (Input.GetKeyDown(KeyCode.F2) || xButtonDown)
        {
            Custom_false();
            CPData.CustomNo2 = true;
            // Debug.Log("�T�u�E�F�|���ɕς����");
        }




        if (CPData.ArmShot_norml_Lv == 1)
        {
            //  Debug.Log("���x��1");
            Bullet_false();
            CPData.ArmShot_norml_Lv1 = true;

        }
        if (CPData.ArmShot_norml_Lv == 2)
        {
            //  Debug.Log("���x��2");
            Bullet_false();
            CPData.ArmShot_norml_Lv2 = true;
        }
        if (CPData.ArmShot_norml_Lv == 3)
        {
            //  Debug.Log("���x��2");
            Bullet_false();
            CPData.ArmShot_norml_Lv3 = true;
        }
    }

    private void Bullet_false()
    {
        CPData.ArmShot_norml_Lv1 = false;
        CPData.ArmShot_norml_Lv2 = false;
        CPData.ArmShot_norml_Lv3 = false;
    }

    private void Custom_false()
    {
        CPData.CustomNo1 = false;
        CPData.CustomNo2 = false;
    }



    /// �m�[�}���e�̔���
    private void ShotNolmal_Lv1()
    {
        //�e�̑���
        speed = 600;

        // �e�𔭎˂���ꏊ���擾
        Vector3 bulletPosition = firingPoint.transform.position;
        // ��Ŏ擾�����ꏊ�ɁA"bullet"��Prefab���o��������
        GameObject newBall = Instantiate(bullet, bulletPosition, transform.rotation);
        // �o���������{�[����forward(z������)
        Vector3 direction = newBall.transform.right;
        // �e�̔��˕�����newBall��z����(���[�J�����W)�����A�e�I�u�W�F�N�g��rigidbody�ɏՌ��͂�������
        if (CPData.Right)
        {
            newBall.GetComponent<Rigidbody2D>().AddForce(direction * speed, ForceMode2D.Force);
        }
        else
        {
            //�e���ł��������]
            newBall.GetComponent<Rigidbody2D>().AddForce(direction * -speed, ForceMode2D.Force);
        }
        // �o���������{�[���̖��O��"bullet"�ɕύX
        newBall.name = bullet.name;
        // �o���������{�[����0.8�b��ɏ���
        Destroy(newBall, 5f);


        //   newBall;
    }
    /// �m�[�}���e�̔���Lv2
    private void ShotNolmal_Lv2()
    {


        //�e�̑���
        speed = 1000;

        // �e�𔭎˂���ꏊ���擾
        //  Vector3 bulletPosition = firingPoint.transform.position;
        Vector3 bulletPosition2 = firingPoint2.transform.position;
        // ��Ŏ擾�����ꏊ�ɁA"bullet"��Prefab���o��������
        // GameObject Bullet = Instantiate(bullet, bulletPosition, transform.rotation);
        GameObject BulletLv2 = Instantiate(bullet2, bulletPosition2, transform.rotation);
        // �o���������{�[����forward(z������)
        Vector3 direction = BulletLv2.transform.right;
        //  Vector3 direction2 = BulletLv2.transform.right;
        // �e�̔��˕�����newBall��z����(���[�J�����W)�����A�e�I�u�W�F�N�g��rigidbody�ɏՌ��͂�������
        if (CPData.Right)
        {
            //  Bullet.GetComponent<Rigidbody2D>().AddForce(direction * speed, ForceMode2D.Force);
            BulletLv2.GetComponent<Rigidbody2D>().AddForce(direction * speed, ForceMode2D.Force);
        }
        else
        {
            //�e���ł��������]
            // Bullet.GetComponent<Rigidbody2D>().AddForce(direction * -speed, ForceMode2D.Force);
            BulletLv2.GetComponent<Rigidbody2D>().AddForce(direction * -speed, ForceMode2D.Force);
        }
        // �o���������{�[���̖��O��"bullet"�ɕύX
        // Bullet.name = bullet.name;
        BulletLv2.name = bullet2.name;
        // �o���������{�[����0.8�b��ɏ���
        // Destroy(Bullet, 5f);
        Destroy(BulletLv2, 5f);




    }


    private void ShotNolmal_Lv3()
    {
         Debug.Log("reberu3333333");

        //float bulletSpeed = 10f; //�e�̑��x
        //float bulletAmplitude = 0.5f; //�e�̐U��
        //float bulletFrequency = 1f; //�e�̎��g��
        //                            // float bulletLifetime = 2f; //�e�̎���

        //�e�̑���
        speed = 600;

        // �e�𔭎˂���ꏊ���擾
        //  Vector3 bulletPosition = firingPoint.transform.position;
        Vector3 bulletPosition3 = firingPoint3.transform.position;
        // ��Ŏ擾�����ꏊ�ɁA"bullet"��Prefab���o��������
        // GameObject Bullet = Instantiate(bullet, bulletPosition, transform.rotation);
        GameObject BulletLv3 = Instantiate(bullet4, bulletPosition3, transform.rotation);
        // �o���������{�[����forward(z������)
        Vector3 direction = BulletLv3.transform.right;
        //  Vector3 direction2 = BulletLv2.transform.right;
        // �e�̔��˕�����newBall��z����(���[�J�����W)�����A�e�I�u�W�F�N�g��rigidbody�ɏՌ��͂�������
        // float bulletAngle = Mathf.Sin(Time.time * bulletFrequency) * bulletAmplitude;
      
        // �o���������{�[���̖��O��"bullet"�ɕύX
        // Bullet.name = bullet.name;
        BulletLv3.name = bullet4.name;
        // �o���������{�[����0.8�b��ɏ���
        // Destroy(Bullet, 5f);
        Destroy(BulletLv3, 5f);




    }



    void ShotPenetrationLv1()
    {
        //�e�̑���
        speed = CPData.Penetration_speed;

        // �e�𔭎˂���ꏊ���擾
        Vector3 bulletPosition = firingPoint3.transform.position;
        // ��Ŏ擾�����ꏊ�ɁA"bullet"��Prefab���o��������
        GameObject Penetration_Bullet = Instantiate(bullet3, bulletPosition, transform.rotation);
        // �o���������{�[����forward(z������)
        Vector3 direction = Penetration_Bullet.transform.right;
        // �e�̔��˕�����newBall��z����(���[�J�����W)�����A�e�I�u�W�F�N�g��rigidbody�ɏՌ��͂�������
        if (CPData.Right)
        {
            Penetration_Bullet.GetComponent<Rigidbody2D>().AddForce(direction * speed, ForceMode2D.Force);
        }
        else
        {
            //�e���ł��������]
            Penetration_Bullet.GetComponent<Rigidbody2D>().AddForce(direction * -speed, ForceMode2D.Force);
        }
        // �o���������{�[���̖��O��"bullet"�ɕύX
        Penetration_Bullet.name = bullet.name;
        // �o���������{�[����0.8�b��ɏ���
        Destroy(Penetration_Bullet, 1.0f);

    }


    void Body_armerLv1()
    {

    }


    //�n�C�W�����v
    void Leg_high()
    {
        if (!CPData.CustomHigh)
        {
            CPData.CustomHigh = true;
            Debug.Log("�n�C�W�����v");
        }
        else if (CPData.CustomHigh)
        {
            CPData.CustomHigh = false;
            Debug.Log("�n�C�W�����vowari");
        }
    }









}
















