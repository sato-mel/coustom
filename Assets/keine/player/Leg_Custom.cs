using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leg_Custom : MonoBehaviour
{
    private bool DebugMode = false; // true�̎���Serialize���Q�Ƃ���B
    [SerializeField] private float MoveVelLimit; // �ړ����x����
    [SerializeField] private float JumpPower; // �W�����v�́B�����ɑ�������B
    [SerializeField] private float Gravity; // �d��
    [SerializeField] private int JumpLimit; // �W�����v�񐔐���
    [SerializeField] private float FallVelLimit; // �������x����
    [SerializeField] private int AccelFrame; // �ō����x�ɒB����܂ł̎��ԁi0.02s�j

    private float MoveVel; // �ړ����x

    private float JumpVel = 0.0f; // �㉺�ړ����x
    private float JumpInitVel = 0.0f; // �W�����v�����B�i�������W�����v�����ɋ󒆂ɏo���ꍇ��0�j
    private float JumpHeight; // �W�����v�̍����i�f�o�b�O�E�����p�j
    private float JumpTime; // �؋󎞊�

    private bool PreJump = false; // �W�����v����\��
    private bool isJump = true; // �󒆂ɂ���i�W�����v���Ă���j��
    private int jumpCount; // �W�����v�����񐔁i���n���Z�b�g�j

    private const int LegCustomNum = 3; // �J�X�^���̎��
    int LegCustomCurrent = 0; // �g�p���̃J�X�^��
    private int[] LegCustomLv = new int[LegCustomNum]; // �J�X�^�����Ƃ̃��x��
    private int LegCustomMaxLv = 4;

    private Rigidbody2D rb;

    private int hanten;
    private bool isRight;
    private bool isLeft;
    private bool isWallLeft;
    private bool isWallRight;

    Vector2 scale;

    private void LegCustom0Lv1()
    {
        MoveVelLimit = 10.0f; // �ړ����x
        JumpPower = 10.0f; // �W�����v�����x
        Gravity = 20.0f; // �d��
        JumpLimit = 2; // �W�����v��
        FallVelLimit = 10.0f; // �������x����
        AccelFrame = 25; // ����₷���A��R
    }
    private void LegCustom0Lv2()
    {
        MoveVelLimit = 10.0f; // �ړ����x
        JumpPower = 10.0f; // �W�����v�����x
        Gravity = 20.0f; // �d��
        JumpLimit = 3; // �W�����v��
        FallVelLimit = 20.0f; // �������x����
        AccelFrame = 25; // ����₷���A��R
    }
    private void LegCustom0Lv3()
    {
        MoveVelLimit = 10.0f; // �ړ����x
        JumpPower = 10.0f; // �W�����v�����x
        Gravity = 20.0f; // �d��
        JumpLimit = 3; // �W�����v��
        FallVelLimit = 10.0f; // �������x����
        AccelFrame = 15; // ����₷���A��R
    }
    private void LegCustom0Lv4()
    {
        MoveVelLimit = 10.0f; // �ړ����x
        JumpPower = 10.0f; // �W�����v�����x
        Gravity = 20.0f; // �d��
        JumpLimit = 4; // �W�����v��
        FallVelLimit = 10.0f; // �������x����
        AccelFrame = 15; // ����₷���A��R
    }

    private void LegCustom1Lv1()
    {
        MoveVelLimit = 12.0f; // �ړ����x
        JumpPower = 12.0f; // �W�����v�����x
        Gravity = 28.8f; // �d��
        JumpLimit = 1; // �W�����v��
        FallVelLimit = 12.0f; // �������x����
        AccelFrame = 25; // ����₷���A��R
    }
    private void LegCustom1Lv2()
    {
        MoveVelLimit = 14.0f; // �ړ����x
        JumpPower = 12.0f; // �W�����v�����x
        Gravity = 28.8f; // �d��
        JumpLimit = 1; // �W�����v��
        FallVelLimit = 12.0f; // �������x����
        AccelFrame = 25; // ����₷���A��R
    }
    private void LegCustom1Lv3()
    {
        MoveVelLimit = 14.0f; // �ړ����x
        JumpPower = 14.0f; // �W�����v�����x
        Gravity = 39.2f; // �d��
        JumpLimit = 1; // �W�����v��
        FallVelLimit = 14.0f; // �������x����
        AccelFrame = 15; // ����₷���A��R
    }
    private void LegCustom1Lv4()
    {
        MoveVelLimit = 16.0f; // �ړ����x
        JumpPower = 14.0f; // �W�����v�����x
        Gravity = 39.2f; // �d��
        JumpLimit = 1; // �W�����v��
        FallVelLimit = 14.0f; // �������x����
        AccelFrame = 10; // ����₷���A��R
    }

    private void LegCustom2Lv1()
    {
        MoveVelLimit = 10.0f; // �ړ����x
        JumpPower = 24.0f; // �W�����v�����x
        Gravity = 48.0f; // �d��
        JumpLimit = 1; // �W�����v��
        FallVelLimit = 30.0f; // �������x����
        AccelFrame = 25; // ����₷���A��R
    }
    private void LegCustom2Lv2()
    {
        MoveVelLimit = 10.0f; // �ړ����x
        JumpPower = 28.0f; // �W�����v�����x
        Gravity = 56.0f; // �d��
        JumpLimit = 1; // �W�����v��
        FallVelLimit = 30.0f; // �������x����
        AccelFrame = 25; // ����₷���A��R
    }
    private void LegCustom2Lv3()
    {
        MoveVelLimit = 10.0f; // �ړ����x
        JumpPower = 32.0f; // �W�����v�����x
        Gravity = 64.0f; // �d��
        JumpLimit = 1; // �W�����v��
        FallVelLimit = 30.0f; // �������x����
        AccelFrame = 15; // ����₷���A��R
    }
    private void LegCustom2Lv4()
    {
        MoveVelLimit = 10.0f; // �ړ����x
        JumpPower = 40.0f; // �W�����v�����x
        Gravity = 80.0f; // �d��
        JumpLimit = 1; // �W�����v��
        FallVelLimit = 30.0f; // �������x����
        AccelFrame = 15; // ����₷���A��R
    }

    // Start is called before the first frame update
    void Start()
    {
        LegCustomCurrent = 0;
        jumpCount = 0;
        hanten = 1;
        rb = GetComponent<Rigidbody2D>();

        scale = transform.localScale;

        isRight = true;
        isLeft = false;
        isWallLeft = false;
        isWallRight = false;

        for (int i = 0; i < LegCustomNum; i++)
        {
            LegCustomLv[i] = 1;
        }

        InputLeg(); // �J�X�^���E���x���ɂ��\�͂𔽉f����
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Jump();
        Move();
    }

    private void Update()
    {
        LegCustom(); // �f�o�b�O�p�B�蓮���x���E�J�X�^���؂�ւ�
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (jumpCount < JumpLimit)
            {
                PreJump = true;
            }
        }
    }

    void Move()
    {
        // �����Ă���ԁA�ō����x�܂ŏオ�葱����
        // ������Ă��Ȃ��Ƃ��A���O�̑��x���牺���葱����
        // �ړ��͉�����Ă��Ȃ��Ƃ����K�v

        //Vector2 scale = gameObject.transform.localScale;
        //�ړ�����
        if (!isWallLeft)
        {
            if (Input.GetKey(KeyCode.A))
            {
                if (isRight)
                {
                    hanten = -1;
                    isRight = false;
                    isLeft = true;
                }
                else
                {
                    hanten = 1;
                }
                scale.x *= hanten;

                if (MoveVel > -MoveVelLimit)
                {
                    MoveVel -= MoveVelLimit / AccelFrame;
                }
//                transform.Translate(MoveVel * Time.deltaTime, 0.0f, 0.0f);
                gameObject.transform.localScale = scale;
                isWallRight = false;
                CPData.Right = false;
            }
            else
            {
                if (MoveVel < 0)
                {
                    MoveVel += MoveVelLimit / AccelFrame;
                    if (MoveVel > 0)
                    {
                        MoveVel = 0;
                    }
                }
            }
        }
        if (!isWallRight)
        {
            if (Input.GetKey(KeyCode.D))
            {
                if (isLeft)
                {
                    hanten = -1;
                    isRight = true;
                    isLeft = false;
                }
                else
                {
                    hanten = 1;
                }

                scale.x *= hanten;
                if (MoveVel < MoveVelLimit)
                {
                    MoveVel += MoveVelLimit / AccelFrame;
                }
                //                transform.Translate(MoveVel * Time.deltaTime, 0.0f, 0.0f);
                gameObject.transform.localScale = scale;
                isRight = true;
                isWallLeft = false;
                CPData.Right = true;
            }
            else
            {
                if (MoveVel > 0)
                {
                    MoveVel -= MoveVelLimit / AccelFrame;
                    if (MoveVel < 0)
                    {
                        MoveVel = 0;
                    }
                }
            }
        }

        transform.Translate(MoveVel * Time.deltaTime, 0.0f, 0.0f);

        //������Ɖ����������

    }

    void Jump()
    {
        if (PreJump) // �W�����v���鏈��
        {
            jumpCount++;
            PreJump = false;
            isJump = true;
            JumpTime = 0;
            JumpInitVel = JumpPower; // �W�����v����ꍇ�ɑ������B
        }
        if (isJump) // �W�����v���i�󒆂̏����j
        {
            JumpVel = JumpInitVel - Gravity * JumpTime; // ���x������-�d�͉����x*����
            transform.Translate(0.0f, JumpVel * Time.deltaTime, 0.0f);

            JumpTime += 0.02f; // FixedUpdate�̍X�V���Ԃ�0.02�b�B
        }


    }

    private void InputLeg() // �J�X�^���E���x���ɂ��\�͂𔽉f����
    {
        if (DebugMode == true)
            return;
        switch (LegCustomCurrent) // ���݂̃J�X�^��No.
        {
            case 0:
                LegCustom0();
                break;
            case 1:
                LegCustom1();
                break;
            case 2:
                LegCustom2();
                break;
        }
        JumpHeight = JumpPower * JumpPower / 2 / Gravity; // �f�o�b�O�p�B�W�����v�̍��������p�B
        Debug.Log("�J�X�^��No." + LegCustomCurrent + "�ALv." + LegCustomLv[LegCustomCurrent] + "�A�W�����v�́F" + JumpHeight);
    }

    private void LegCustom0()
    {
        switch (LegCustomLv[0]) // �J�X�^��No.0�̃��x��
        {
            case 1:
                LegCustom0Lv1();
                break;
            case 2:
                LegCustom0Lv2();
                break;
            case 3:
                LegCustom0Lv3();
                break;
            case 4:
                LegCustom0Lv4();
                break;
        }
    }

    private void LegCustom1()
    {
        switch (LegCustomLv[1]) // �J�X�^��No.1�̃��x��
        {
            case 1:
                LegCustom1Lv1();
                break;
            case 2:
                LegCustom1Lv2();
                break;
            case 3:
                LegCustom1Lv3();
                break;
            case 4:
                LegCustom1Lv4();
                break;
        }
    }

    private void LegCustom2()
    {
        switch (LegCustomLv[2]) // �J�X�^��No.2�̃��x��
        {
            case 1:
                LegCustom2Lv1();
                break;
            case 2:
                LegCustom2Lv2();
                break;
            case 3:
                LegCustom2Lv3();
                break;
            case 4:
                LegCustom2Lv4();
                break;
        }
    }

    private void LegCustom() // �f�o�b�O�p�B�蓮���x���؂�ւ�
    {
        // ���x���A�b�v
        if (Input.GetKeyDown(KeyCode.N)) // �L�[���v����
        {
            LegCustomLv[LegCustomCurrent]++;
            if (LegCustomLv[LegCustomCurrent] > LegCustomMaxLv)
            {
                LegCustomLv[LegCustomCurrent] = LegCustomMaxLv;
            }
            else
            {
                InputLeg(); // �J�X�^���E���x���ɂ��\�͂𔽉f����
            }
        }
        // ���x���_�E��
        if (Input.GetKeyDown(KeyCode.M)) // �L�[���v����
        {
            LegCustomLv[LegCustomCurrent] --;
            if (LegCustomLv[LegCustomCurrent] < 1)
            {
                LegCustomLv[LegCustomCurrent] = 1;
            }
            else
            {
                InputLeg(); // �J�X�^���E���x���ɂ��\�͂𔽉f����
            }
        }

        // �J�X�^��No.0�ɕύX
        if (Input.GetKeyDown(KeyCode.H)) // �L�[���v����
        {
            if (LegCustomCurrent != 0)
            {
                LegCustomCurrent = 0;
                InputLeg(); // �J�X�^���E���x���ɂ��\�͂𔽉f����
            }
        }
        // �J�X�^��No.1�ɕύX
        if (Input.GetKeyDown(KeyCode.J)) // �L�[���v����
        {
            if (LegCustomCurrent != 1)
            {
                LegCustomCurrent = 1;
                InputLeg(); // �J�X�^���E���x���ɂ��\�͂𔽉f����
            }
        }
        // �J�X�^��No.2�ɕύX
        if (Input.GetKeyDown(KeyCode.K)) // �L�[���v����
        {
            if (LegCustomCurrent != 2)
            {
                LegCustomCurrent = 2;
                InputLeg(); // �J�X�^���E���x���ɂ��\�͂𔽉f����
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // ���ɓ���������J�E���g�[��
        if (other.gameObject.tag == "Floor")
        {
            jumpCount = 0;
            isJump = false;
            JumpInitVel = 0;
            JumpTime = 0;
        }

        // �ǂɓ���������true
        if (other.gameObject.tag == "RightWall")
        {
            isWallRight = true;
            MoveVel = 0;
        }
        if (other.gameObject.tag == "LeftWall")
        {
            isWallLeft = true;
            MoveVel = 0;
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        // �ǂɓ������Ă����true
        if (other.gameObject.tag == "RightWall")
        {
            isWallRight = true;
        }
        if (other.gameObject.tag == "LeftWall")
        {
            isWallLeft = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        // �����痣�ꂽ��true
        if (other.gameObject.tag == "Floor")
        {
            isJump = true;

            // �����痎���Ă��W�����v�񐔁i�n�㕪�j������i�n��W�����v+�󒆃W�����v�ŃJ�E���g�j
            if (jumpCount == 0)
            {
                jumpCount = 1;
            }
        }

    }




}