using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class Leg_Custom : MonoBehaviour
{
    bool DebugMode = false; // true�̎���Serialize���Q�Ƃ���B
    [SerializeField] private float MoveVelLimit; // �ړ����x����
    [SerializeField] private float JumpPower; // �W�����v�́B�����ɑ�������B
    [SerializeField] private float Gravity; // �d��
    [SerializeField] private int JumpLimit; // �W�����v�񐔐���
    [SerializeField] private float FallVelLimit; // �������x����
    [SerializeField] private int AccelFrame; // �ō����x�ɒB����܂ł̎��ԁi0.02s�j

    private float MoveVel; // �ړ����x

    private float JumpVel = 0.0f; // �㉺�ړ����x
    private float LastJumpVel = 0.0f; // ��r�p�B���O�̏㉺�ړ����x
    private float JumpInitVel = 0.0f; // �W�����v�����i�������W�����v�����ɋ󒆂ɏo���ꍇ��0�j
    private float JumpHeight; // �W�����v�̍����i�f�o�b�O�E�����p�j
    private float JumpTime; // �؋󎞊�

    private bool PreJump = false; // �W�����v����\��
    private bool isJump = true; // �󒆂ɂ���i�W�����v���Ă���j��
    private int JumpCount; // �W�����v�����񐔁i���n���Z�b�g�j

    private const int LegCustomNum = 3; // �J�X�^���̎��
    private int LegCustomCurrent = 0; // �g�p���̃J�X�^��
    private int[] LegCustomLv = new int[LegCustomNum]; // �J�X�^�����Ƃ̃��x��
    private int LegCustomMaxLv = 4; // �ő僌�x��

    private bool PreBody = false; // BodyCustom���g�p����\��
    private bool isBody = false; // BodyCustom���g������
    private int BodyCount = 0; // BodyCustom���g�����񐔁i���n���Z�b�g�j
    private int BodyTime = 0; // BodyCustom���g�������ԁi���n���Z�b�g�j

    private float BodyMoveVelLimit; // BodyCustom�g�p���̈ړ����x����
    private float BodyFixedMoveVel; // BodyCustom�g�p���̋����ړ����x�B����p�B
    private float BodyGravity; // BodyCustom�g�p���̏d��
    private int BodyAccelFrame; // BodyCustom�g�p���̊���₷��
    private int BodyCountLimit; // BodyCustom�̎g�p�񐔐����i���n���Z�b�g�j
    private int BodyTimeLimit; // BodyCustom�̎g�p���Ԑ����i���n���Z�b�g�j

    private bool PreCancelBody = false; // BodyCustom����������\��

    private const int BodyCustomNum = 3; // �J�X�^���̎��
    private int BodyCustomCurrent = 0; // �g�p���̃J�X�^��
    private int[] BodyCustomLv = new int[BodyCustomNum]; // �J�X�^�����Ƃ̃��x��
    private int BodyCustomMaxLv = 4; // �ő僌�x��

    private bool DebugModeLeg = true;

    private Rigidbody2D rb;

    private int hanten;
    private bool isRight;
    private bool isLeft;
    private bool isWallLeft;
    private bool isWallRight;

    Vector2 scale;

    // ���i�W�����v
    float[] LegCustom0_MVL = { 10.0f, 10.0f, 10.0f, 10.0f }; // �ړ����x�ALegCustom0��MoveVelLimit�ɓ����l�̔z��
    float[] LegCustom0_JP = { 10.0f, 10.0f, 10.0f, 10.0f }; // �W�����v�����x�ALegCustom0��JumpPower�ɓ����l�̔z��
    float[] LegCustom0_G = { 20.0f, 20.0f, 20.0f, 20.0f }; // �d�́ALegCustom0��Gravity�ɓ����l�̔z��
    int[] LegCustom0_GL = { 2, 3, 3, 4 }; // �W�����v�񐔁ALegCustom0��JumpLimit�ɓ����l�̔z��
    float[] LegCustom0_FVL = { 20.0f, 20.0f, 20.0f, 20.0f }; // �������x�����ALegCustom0��FallVelLimit�ɓ����l�̔z��
    int[] LegCustom0_AF = { 25, 25, 15, 15 }; // ����₷���A��R�ALegCustom0��AccelFrame�ɓ����l�̔z��

    // ���x
    float[] LegCustom1_MVL = { 12.0f, 14.0f, 14.0f, 16.0f }; // �ړ����x�ALegCustom0��MoveVelLimit�ɓ����l�̔z��
    float[] LegCustom1_JP = { 12.0f, 12.0f, 14.0f, 14.0f }; // �W�����v�����x�ALegCustom0��JumpPower�ɓ����l�̔z��
    float[] LegCustom1_G = { 28.8f, 28.8f, 39.2f, 39.2f }; // �d�́ALegCustom0��Gravity�ɓ����l�̔z��
    int[] LegCustom1_GL = { 1, 1, 1, 1 }; // �W�����v�񐔁ALegCustom0��JumpLimit�ɓ����l�̔z��
    float[] LegCustom1_FVL = { 12.0f, 12.0f, 14.0f, 14.0f }; // �������x�����ALegCustom0��FallVelLimit�ɓ����l�̔z��
    int[] LegCustom1_AF = { 25, 25, 15, 10 }; // ����₷���A��R�ALegCustom0��AccelFrame�ɓ����l�̔z��

    // �n�C�W�����v
    float[] LegCustom2_MVL = { 10.0f, 10.0f, 10.0f, 10.0f }; // �ړ����x�ALegCustom0��MoveVelLimit�ɓ����l�̔z��
    float[] LegCustom2_JP = { 24.0f, 28.0f, 32.0f, 40.0f }; // �W�����v�����x�ALegCustom0��JumpPower�ɓ����l�̔z��
    float[] LegCustom2_G = { 48.0f, 56.0f, 64.0f, 80.0f }; // �d�́ALegCustom0��Gravity�ɓ����l�̔z��
    int[] LegCustom2_GL = { 1, 1, 1, 1 }; // �W�����v�񐔁ALegCustom0��JumpLimit�ɓ����l�̔z��
    float[] LegCustom2_FVL = { 30.0f, 30.0f, 30.0f, 30.0f }; // �������x�����ALegCustom0��FallVelLimit�ɓ����l�̔z��
    int[] LegCustom2_AF = { 25, 25, 15, 15 }; // ����₷���A��R�ALegCustom0��AccelFrame�ɓ����l�̔z��

    // ����
    float[] BodyCustom0_BML = { 1.0f, 1.0f, 2.0f, 3.0f }; // �ړ����x�ABodyMoveVelLimit�ɓ����l�̔z��
    float[] BodyCustom0_BFM = { 15.0f, 15.0f, 16.0f, 17.0f }; // �Œ�ړ����x�ABodyFixedMoveVel�ɓ����l�̔z��
    float[] BodyCustom0_BG = { 4.0f, 3.5f, 3.5f, 3.0f }; // �d�́ABodyGravity�ɓ����l�̔z��
    int[] BodyCustom0_BAF = { 10, 10, 10, 10 }; // ����₷���A��R�ABodyAccelFrame�ɓ����l�̔z��
    int[] BodyCustom0_BCL = { 1, 2, 3, 4 }; // �񐔐����ABodyCountLimit�ɓ����z��
    int[] BodyCustom0_BTL = { 50, 75, 100, 125 }; // ���Ԑ���(�t���[��)�ABodyTimeLimit�ɓ����z��

    // �P
    float[] BodyCustom1_BML = { 15.0f, 16.0f, 16.0f, 17.0f }; // �ړ����x�ABodyMoveVelLimit�ɓ����l�̔z��
    float[] BodyCustom1_BFM = { 0.0f, 0.0f, 0.0f, 0.0f }; // �Œ�ړ����x�ABodyFixedMoveVel�ɓ����l�̔z��
    float[] BodyCustom1_BG = { 1.5f, 1.5f, 1.0f, 1.0f }; // �d�́ABodyGravity�ɓ����l�̔z��
    int[] BodyCustom1_BAF = { 35, 30, 30, 25 }; // ����₷���A��R�AAccelFrame�ɓ����l�̔z��
    int[] BodyCustom1_BCL = { 2, 3, 4, 5 }; // �񐔐����ABodyCountLimit�ɓ����z��
    int[] BodyCustom1_BTL = { 50, 100, 150, 200 }; // ���Ԑ���(�t���[��)�ABodyTimeLimit�ɓ����z��

    // �z�o�����O
    float[] BodyCustom2_BML = { 5.0f, 5.0f, 5.0f, 5.0f }; // �ړ����x�ABodyMoveVelLimit�ɓ����l�̔z��
    float[] BodyCustom2_BFM = { 0.0f, 0.0f, 0.0f, 0.0f }; // �Œ�ړ����x�ABodyFixedMoveVel�ɓ����l�̔z��
    float[] BodyCustom2_BG = { 0.0f, 0.0f, -2.0f, -4.0f }; // �d�́ABodyGravity�ɓ����l�̔z��
    int[] BodyCustom2_BAF = { 25, 25, 25, 25 }; // ����₷���A��R�ABodyAccelFrame�ɓ����l�̔z��
    int[] BodyCustom2_BCL = { 3, 4, 5, 6 }; // �񐔐����ABodyCountLimit�ɓ����z��
    int[] BodyCustom2_BTL = { 25, 50, 75, 100 }; // ���Ԑ���(�t���[��)�ABodyTimeLimit�ɓ����z��

    // �o���l
    int EnemyPointGet = 1; // �G1�̂�|�����o���l
    int[] EnemyPointBody = { 0, 0, 0 }; // BodyCustom���Ƃ̗݌v�o���l
    int[] EnemyPointLeg = { 0, 0, 0 }; // LegCustom���Ƃ̗݌v�o���l
    int[] LvUpTable = { 1, 2, 3 }; // LvUp�ɕK�v�Ȍo���l

    // Start is called before the first frame update
    void Start()
    {
        LegCustomCurrent = 0;
        JumpCount = 0;
        hanten = 1;
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;

        scale = transform.localScale;

        isRight = true;
        isLeft = false;
        isWallLeft = false;
        isWallRight = false;

        for (int i = 0; i < LegCustomNum; i++)
        {
            LegCustomLv[i] = 1;
        }
        for (int i = 0; i < BodyCustomNum; i++)
        {
            BodyCustomLv[i] = 1;
        }

        InputLeg(); // Leg�J�X�^���E���x���ɂ��\�͂𔽉f����
        InputBody(); // Body�J�X�^���E���x���ɂ��\�͂𔽉f����
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        Jump();
        BodyCustom();
    }

    private void Update()
    {
        BodyLegCustomLvUp(); // ���x���A�b�v�p

        BodyLegCustomChange(); // �؂�ւ��p

        LegCustom(); // �f�o�b�O�p�B�蓮���x���E�J�X�^���؂�ւ�

        // �Q�[���p�b�h�p
        var GamePad = Gamepad.current;
        bool aButtonDown = false; // A�{�^������������
        bool aButtonUp = false; // A�{�^���𗣂�����

        if (GamePad != null)
        {
            if (GamePad.aButton.wasPressedThisFrame) // A�{�^������������
            {
                aButtonDown = true;
            }
            if (GamePad.aButton.wasReleasedThisFrame) // A�{�^���𗣂�����
            {
                aButtonUp = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) || aButtonDown) // ��������
        {
            if (JumpCount < JumpLimit)
            {
                PreJump = true;
                return;
            }
            if (BodyCount < BodyCountLimit)
            {
                if (JumpVel < 0) // ������
                {
                    PreBody = true;
                }
            }
        }
        if (Input.GetKeyUp(KeyCode.Space) || aButtonUp) // ��������
        {
            if (isBody)
            {
                PreCancelBody = true;
            }
        }
    }

    void Move()
    {
        // �Q�[���p�b�h�p
        var GamePad = Gamepad.current;
        bool LeftStickLeft = false; // ���i���X�e�B�b�N�j
        bool LeftStickRight = false; // �E�i���X�e�B�b�N�j

        if (GamePad != null)
        {
            var LeftStickValue = GamePad.leftStick.ReadValue();
            float StickReaction = 0.2f; // �X�e�B�b�N����ԌX��������1�Ƃ��āA�ǂꂭ�炢�X�����甽�����邩

            if (LeftStickValue.x < -StickReaction)
            {
                LeftStickLeft = true;
            }
            if (LeftStickValue.x > StickReaction)
            {
                LeftStickRight = true;
            }
        }
        
        //�ړ�����
        if (!isWallLeft)
        {
            if (Input.GetKey(KeyCode.A) || LeftStickLeft)
            {
                if (isRight)
                {
                    isRight = false;
                    isLeft = true;
                }

                if (isBody)
                {
                    if (MoveVel > -BodyMoveVelLimit)
                    {
                        MoveVel -= BodyMoveVelLimit / BodyAccelFrame;
                    }
                    else
                    {
                        MoveVel = -BodyMoveVelLimit;
                    }
                }
                else
                {
                    if (MoveVel > -MoveVelLimit)
                    {
                        MoveVel -= MoveVelLimit / AccelFrame;
                    }

                    // �v���C���[�摜�̔��]
                    if (scale.x < 0)
                    {
                        scale.x *= -1;
                    }
                }

                gameObject.transform.localScale = scale;
                isWallRight = false;
                CPData.Right = false;
            }
            else // ���������Ă��Ȃ���
            {
                if (MoveVel < 0)
                {
                    MoveVel += MoveVelLimit / AccelFrame; // ����
                    if (MoveVel > 0)
                    {
                        MoveVel = 0;
                    }
                }
            }
            if (isBody == false)
            {
                if (MoveVel < -MoveVelLimit) // �����葬���Ƃ�
                {
                    MoveVel += MoveVelLimit / AccelFrame; // ����
                    if (MoveVel > -MoveVelLimit) // ������x���Ȃ�����
                    {
                        MoveVel = -MoveVelLimit;
                    }
                }
            }
        }
        if (!isWallRight)
        {
            if (Input.GetKey(KeyCode.D) || LeftStickRight)
            {
                if (isLeft)
                {
                    isRight = true;
                    isLeft = false;
                }

                if (isBody)
                {
                    if (MoveVel < BodyMoveVelLimit)
                    {
                        MoveVel += BodyMoveVelLimit / AccelFrame;
                    }
                    else
                    {
                        MoveVel = BodyMoveVelLimit;
                    }
                }
                else
                {
                    if (MoveVel < MoveVelLimit)
                    {
                        MoveVel += MoveVelLimit / AccelFrame;
                    }

                    // �v���C���[�摜�̔��]
                    if (scale.x > 0)
                    {
                        scale.x *= -1;
                    }
                }
                gameObject.transform.localScale = scale;
                isWallLeft = false;
                CPData.Right = true;
            }
            else // �E�������Ă��Ȃ���
            {
                if (MoveVel > 0)
                {
                    MoveVel -= MoveVelLimit / AccelFrame; // ����
                    if (MoveVel < 0)
                    {
                        MoveVel = 0;
                    }
                }
            }
            if (isBody == false)
            {
                if (MoveVel > MoveVelLimit) // �����葬���Ƃ�
                {
                    MoveVel -= MoveVelLimit / AccelFrame; // ����
                    if (MoveVel < MoveVelLimit) // ������x���Ȃ�����
                    {
                        MoveVel = MoveVelLimit;
                    }
                }
            }
        }

        if (isBody)
        {
            transform.Translate((MoveVel + BodyFixedMoveVel) * Time.deltaTime, 0.0f, 0.0f);
        }
        else
        {
            transform.Translate(MoveVel * Time.deltaTime, 0.0f, 0.0f);
        }
        //������Ɖ����������
    }

    void Jump()
    {
        // �Q�[���p�b�h�p
        var GamePad = Gamepad.current;
        bool aButton = false; // A�{�^���������Ă����

        if (GamePad != null)
        {
            if (GamePad.aButton.isPressed)
            {
                aButton = true;
            }
        }

        if (PreJump) // �W�����v���鏈��
        {
            JumpCount++;
            PreJump = false;
            isJump = true;
            JumpTime = 0;
            JumpInitVel = JumpPower; // �W�����v����ꍇ�ɑ������B
        }
        if (isJump) // �W�����v���i�󒆂̏����j
        {
            if (isBody)
            {
                return;
            }

            JumpVel = JumpInitVel - Gravity * JumpTime; // ���x������-�d�͉����x*����

            if (BodyCount < BodyCountLimit)
            {
                // �W�����v�̒��_�̏u��
                if (JumpVel <= 0)
                {
                    if (LastJumpVel > 0)
                    {
                        if (Input.GetKey(KeyCode.Space) || aButton) // A�{�^���������Ă����
                        {
                            PreBody = true;
                            return;
                        }
                    }
                }
            }
            transform.Translate(0.0f, JumpVel * Time.deltaTime, 0.0f);
            JumpTime += 0.02f; // FixedUpdate�̍X�V���Ԃ�0.02�b�B
            LastJumpVel = JumpVel;
        }
    }

    private void BodyCustom()
    {
        if (PreBody)
        {
            PreBody = false;
            isBody = true;
            JumpCount = JumpLimit; // ���n�܂ŃW�����v�֎~
            LastJumpVel = JumpVel; // ���[�v�΍�
            JumpTime = 0;
            JumpInitVel = 0;
            JumpVel = 0;
            BodyCount++;
            if (MoveVel > 0)
            {
                if (BodyFixedMoveVel < 0)
                {
                    BodyFixedMoveVel *= -1;
                }
                // �v���C���[�摜�̔��]
                if (scale.x > 0)
                {
                    scale.x *= -1;
                }
            }
            if (MoveVel == 0)
            {
                if (scale.x < 0)
                {
                    if (BodyFixedMoveVel < 0)
                    {
                        BodyFixedMoveVel *= -1;
                    }
                }
                else
                {
                    if (BodyFixedMoveVel > 0)
                    {
                        BodyFixedMoveVel *= -1;
                    }
                }
            }
            if (MoveVel < 0)
            {
                if (BodyFixedMoveVel > 0)
                {
                    BodyFixedMoveVel *= -1;
                }
                // �v���C���[�摜�̔��]
                if (scale.x < 0)
                {
                    scale.x *= -1;
                }
            }
        }
        if (PreCancelBody)
        {
            PreCancelBody = false;
            isBody = false;
            JumpTime = 0;
            JumpInitVel = JumpVel;
            MoveVel = MoveVel + BodyFixedMoveVel;
        }
        if (isBody)
        {
            transform.Translate(0.0f, -BodyGravity * Time.deltaTime, 0.0f);
            BodyTime++; // FixedUpdate�̍X�V���Ԃ�0.02�b�B
            if (BodyTime >= BodyTimeLimit)
            {
                PreCancelBody = true;
            }
        }
    }

    private void BodyLegCustomLvUp()
    {
        if (CPData.BodyLegCustom_LvUp)
        {
            CPData.BodyLegCustom_LvUp = false;
        }
        else
        {
            return;
        }

        // BodyCustom�Ɍo���l��ǉ�
        EnemyPointBody[BodyCustomCurrent] += EnemyPointGet;
        // LegCustom�Ɍo���l��ǉ�
        EnemyPointLeg[LegCustomCurrent] += EnemyPointGet;

        // LvUp����
        bool LvUp = true; // �p������

        do
        {
            if (BodyCustomLv[BodyCustomCurrent] < BodyCustomMaxLv)
            {
                if (LvUpTable[BodyCustomLv[BodyCustomCurrent] - 1] <= EnemyPointBody[BodyCustomCurrent])
                {
                    BodyCustomLv[BodyCustomCurrent]++;
                    InputBody(); // �J�X�^���E���x���ɂ��\�͂𔽉f����
                }
                else
                {
                    LvUp = false;
                }
            }
            else
            {
                LvUp = false;
            }
        } while (LvUp);

        // LvUp����
        LvUp = true; // �p������

        do
        {
            if (LegCustomLv[LegCustomCurrent] < LegCustomMaxLv)
            {
                if (LvUpTable[LegCustomLv[LegCustomCurrent] - 1] <= EnemyPointLeg[LegCustomCurrent])
                {
                    LegCustomLv[LegCustomCurrent]++;
                    InputLeg(); // �J�X�^���E���x���ɂ��\�͂𔽉f����
                }
                else
                {
                    LvUp = false;
                }
            }
            else
            {
                LvUp = false;
            }
        } while (LvUp);
    }

    private void BodyLegCustomChange()
    {
        // �Q�[���p�b�h�p
        var GamePad = Gamepad.current;
        bool R1ButtonDown = false; // R1�{�^������������
        bool L1ButtonDown = false; // L1�{�^������������

        if (GamePad != null)
        {
            if (GamePad.rightShoulder.wasPressedThisFrame) // R1�{�^������������
            {
                R1ButtonDown = true;
            }
            if (GamePad.leftShoulder.wasPressedThisFrame) // L1�{�^������������
            {
                L1ButtonDown = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.E) || R1ButtonDown) // ��������
        {
            // BodyCustom�؂�ւ�
        }

        if (Input.GetKeyDown(KeyCode.Q) || L1ButtonDown) // ��������
        {
            // LegCustom�؂�ւ�
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
        Debug.Log("Leg�J�X�^��No." + LegCustomCurrent + "�ALv." + LegCustomLv[LegCustomCurrent] + "�A�W�����v�́F" + JumpHeight);
    }

    private void InputBody() // �J�X�^���E���x���ɂ��\�͂𔽉f����
    {
        switch (BodyCustomCurrent) // ���݂̃J�X�^��No.
        {
            case 0:
                BodyCustom0();
                break;
            case 1:
                BodyCustom1();
                break;
            case 2:
                BodyCustom2();
                break;
        }
        Debug.Log("Body�J�X�^��No." + BodyCustomCurrent + "�ALv." + BodyCustomLv[BodyCustomCurrent]);
    }

    private void LegCustom0()
    {
        MoveVelLimit = LegCustom0_MVL[LegCustomLv[LegCustomCurrent] - 1];
        JumpPower = LegCustom0_JP[LegCustomLv[LegCustomCurrent] - 1];
        Gravity = LegCustom0_G[LegCustomLv[LegCustomCurrent] - 1];
        JumpLimit = LegCustom0_GL[LegCustomLv[LegCustomCurrent] - 1];
        FallVelLimit = LegCustom0_FVL[LegCustomLv[LegCustomCurrent] - 1];
        AccelFrame = LegCustom0_AF[LegCustomLv[LegCustomCurrent] - 1];
    }

    private void LegCustom1()
    {
        MoveVelLimit = LegCustom1_MVL[LegCustomLv[LegCustomCurrent] - 1];
        JumpPower = LegCustom1_JP[LegCustomLv[LegCustomCurrent] - 1];
        Gravity = LegCustom1_G[LegCustomLv[LegCustomCurrent] - 1];
        JumpLimit = LegCustom1_GL[LegCustomLv[LegCustomCurrent] - 1];
        FallVelLimit = LegCustom1_FVL[LegCustomLv[LegCustomCurrent] - 1];
        AccelFrame = LegCustom1_AF[LegCustomLv[LegCustomCurrent] - 1];
    }

    private void LegCustom2()
    {
        MoveVelLimit = LegCustom2_MVL[LegCustomLv[LegCustomCurrent] - 1];
        JumpPower = LegCustom2_JP[LegCustomLv[LegCustomCurrent] - 1];
        Gravity = LegCustom2_G[LegCustomLv[LegCustomCurrent] - 1];
        JumpLimit = LegCustom2_GL[LegCustomLv[LegCustomCurrent] - 1];
        FallVelLimit = LegCustom2_FVL[LegCustomLv[LegCustomCurrent] - 1];
        AccelFrame = LegCustom2_AF[LegCustomLv[LegCustomCurrent] - 1];
    }

    private void BodyCustom0()
    {
        BodyMoveVelLimit = BodyCustom0_BML[BodyCustomLv[BodyCustomCurrent] - 1];
        BodyFixedMoveVel = BodyCustom0_BFM[BodyCustomLv[BodyCustomCurrent] - 1];
        BodyGravity = BodyCustom0_BG[BodyCustomLv[BodyCustomCurrent] - 1];
        BodyAccelFrame = BodyCustom0_BAF[BodyCustomLv[BodyCustomCurrent] - 1];
        BodyCountLimit = BodyCustom0_BCL[BodyCustomLv[BodyCustomCurrent] - 1];
        BodyTimeLimit = BodyCustom0_BTL[BodyCustomLv[BodyCustomCurrent] - 1];
    }

    private void BodyCustom1()
    {
        BodyMoveVelLimit = BodyCustom1_BML[BodyCustomLv[BodyCustomCurrent] - 1];
        BodyFixedMoveVel = BodyCustom1_BFM[BodyCustomLv[BodyCustomCurrent] - 1];
        BodyGravity = BodyCustom1_BG[BodyCustomLv[BodyCustomCurrent] - 1];
        BodyAccelFrame = BodyCustom1_BAF[BodyCustomLv[BodyCustomCurrent] - 1];
        BodyCountLimit = BodyCustom1_BCL[BodyCustomLv[BodyCustomCurrent] - 1];
        BodyTimeLimit = BodyCustom1_BTL[BodyCustomLv[BodyCustomCurrent] - 1];
    }

    private void BodyCustom2()
    {
        BodyMoveVelLimit = BodyCustom2_BML[BodyCustomLv[BodyCustomCurrent] - 1];
        BodyFixedMoveVel = BodyCustom2_BFM[BodyCustomLv[BodyCustomCurrent] - 1];
        BodyGravity = BodyCustom2_BG[BodyCustomLv[BodyCustomCurrent] - 1];
        BodyAccelFrame = BodyCustom2_BAF[BodyCustomLv[BodyCustomCurrent] - 1];
        BodyCountLimit = BodyCustom2_BCL[BodyCustomLv[BodyCustomCurrent] - 1];
        BodyTimeLimit = BodyCustom2_BTL[BodyCustomLv[BodyCustomCurrent] - 1];
    }

    private void LegCustom() // �f�o�b�O�p�B�蓮���x���؂�ւ�
    {
        // �f�o�b�O���[�h�ؑ�
        if (Input.GetKeyDown(KeyCode.B)) // �L�[���v����
        {
            if (DebugModeLeg)
            {
                DebugModeLeg = false;
                Debug.Log("BodyCustom�f�o�b�O���[�h");
                return;
            }
            else
            {
                DebugModeLeg = true;
                Debug.Log("LegCustom�f�o�b�O���[�h");
                return;
            }

        }

        // ���x���A�b�v
        if (Input.GetKeyDown(KeyCode.N)) // �L�[���v����
        {
            if (DebugModeLeg)
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
            else
            {
                BodyCustomLv[BodyCustomCurrent]++;
                if (BodyCustomLv[BodyCustomCurrent] > BodyCustomMaxLv)
                {
                    BodyCustomLv[BodyCustomCurrent] = BodyCustomMaxLv;
                }
                else
                {
                    InputBody(); // �J�X�^���E���x���ɂ��\�͂𔽉f����
                }
            }
        }
        // ���x���_�E��
        if (Input.GetKeyDown(KeyCode.M)) // �L�[���v����
        {
            if (DebugModeLeg)
            {
                LegCustomLv[LegCustomCurrent]--;
                if (LegCustomLv[LegCustomCurrent] < 1)
                {
                    LegCustomLv[LegCustomCurrent] = 1;
                }
                else
                {
                    InputLeg(); // �J�X�^���E���x���ɂ��\�͂𔽉f����
                }
            }
            else
            {
                BodyCustomLv[BodyCustomCurrent]--;
                if (BodyCustomLv[BodyCustomCurrent] < 1)
                {
                    BodyCustomLv[BodyCustomCurrent] = 1;
                }
                else
                {
                    InputBody(); // �J�X�^���E���x���ɂ��\�͂𔽉f����
                }
            }
        }

        // �J�X�^��No.0�ɕύX
        if (Input.GetKeyDown(KeyCode.H)) // �L�[���v����
        {
            if (DebugModeLeg)
            {
                if (LegCustomCurrent != 0)
                {
                    LegCustomCurrent = 0;
                    InputLeg(); // �J�X�^���E���x���ɂ��\�͂𔽉f����
                }
            }
            else
            {
                if (BodyCustomCurrent != 0)
                {
                    BodyCustomCurrent = 0;
                    InputBody(); // �J�X�^���E���x���ɂ��\�͂𔽉f����
                }
            }
        }
        // �J�X�^��No.1�ɕύX
        if (Input.GetKeyDown(KeyCode.J)) // �L�[���v����
        {
            if (DebugModeLeg)
            {
                if (LegCustomCurrent != 1)
                {
                    LegCustomCurrent = 1;
                    InputLeg(); // �J�X�^���E���x���ɂ��\�͂𔽉f����
                }
            }
            else
            {
                if (BodyCustomCurrent != 1)
                {
                    BodyCustomCurrent = 1;
                    InputBody(); // �J�X�^���E���x���ɂ��\�͂𔽉f����
                }
            }
        }
        // �J�X�^��No.2�ɕύX
        if (Input.GetKeyDown(KeyCode.K)) // �L�[���v����
        {
            if (DebugModeLeg)
            {
                if (LegCustomCurrent != 2)
                {
                    LegCustomCurrent = 2;
                    InputLeg(); // �J�X�^���E���x���ɂ��\�͂𔽉f����
                }
            }
            else
            {
                if (BodyCustomCurrent != 2)
                {
                    BodyCustomCurrent = 2;
                    InputBody(); // �J�X�^���E���x���ɂ��\�͂𔽉f����
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // ���ɓ���������J�E���g�[��
        if (other.gameObject.tag == "Floor")
        {
            JumpCount = 0;
            isJump = false;
            JumpInitVel = 0;
            JumpTime = 0;

            if (isBody)
            {
                PreCancelBody = true;
                isBody = false;
            }
            BodyCount = 0;
            BodyTime = 0;
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
            if (JumpCount == 0)
            {
                JumpInitVel = 0;
                JumpCount = 1;
            }
        }

        // �ǂ��痣�ꂽ��false
        if (other.gameObject.tag == "RightWall")
        {
            isWallRight = false;
        }
        if (other.gameObject.tag == "LeftWall")
        {
            isWallLeft = false;
        }

    }




}