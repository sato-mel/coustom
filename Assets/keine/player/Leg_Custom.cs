using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leg_Custom : MonoBehaviour
{
    private bool DebugMode = false; // true�̎���Serialize���Q�Ƃ���B
    [SerializeField] private float LRMove; // �ړ����x
    [SerializeField] private float JumpPower; // �W�����v�́B�����ɑ�������B
    [SerializeField] private float Gravity; // �d��
    [SerializeField] private int JumpLimit; // �W�����v��
    [SerializeField] private float FallVelLimit; // �������x����
    [SerializeField] private float Resistance; // ����₷���A��R
    
    private float JumpVel = 0.0f; // �㉺�ړ����x
    private float JumpInitVel = 0.0f; // �W�����v�����B
    private float JumpHeight; // �W�����v�̍���
    private float JumpTime; // �؋󎞊�

    private const int LegCustomNo = 1; // �J�X�^���̎��
    int CurrentLegCustom = 0; // �g�p���̃J�X�^��
    private int[] LegCustomLv = new int[LegCustomNo]; // �J�X�^�����Ƃ̃��x��

    //   private float jump = 0.0f;
    private bool PreJump = false; // �W�����v����\��
    private bool isJump = true; // �W�����v���Ă��邩
    private int jumpCount;

    private Rigidbody2D rb;

    private int hanten;
    private bool isRight;
    private bool isLeft;
    private bool isWallLeft;
    private bool isWallRight;

    Vector2 scale;

    private void LegCustom0Lv0()
    {
        LRMove = 9.0f; // �ړ����x
        JumpPower = 10.0f; // �W�����v�����x
        Gravity = 20.0f; // �d��
        JumpLimit = 2; // �W�����v��
        FallVelLimit = 10.0f; // �������x����
        Resistance = 0.0f; // ����₷���A��R
    }
    private void LegCustom0Lv1()
    {
        LRMove = 18.0f; // �ړ����x
        JumpPower = 20.0f; // �W�����v�����x
        Gravity = 40.0f; // �d��
        JumpLimit = 3; // �W�����v��
        FallVelLimit = 20.0f; // �������x����
        Resistance = 0.0f; // ����₷���A��R
    }

    // Start is called before the first frame update
    void Start()
    {
        CurrentLegCustom = 0;
        jumpCount = 0;
        hanten = 1;
        rb = GetComponent<Rigidbody2D>();

        scale = transform.localScale;

        isRight = true;
        isLeft = false;
        isWallLeft = false;
        isWallRight = false;

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
        LegCustom(); // �f�o�b�O�p�B�蓮���x���؂�ւ�
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

//                LRVelocity = -18.0f;
//                LRMove = LRVelocity;
                transform.Translate(-LRMove * Time.deltaTime, 0.0f, 0.0f);
                gameObject.transform.localScale = scale;
                isWallRight = false;
                CPData.Right = false;
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

                gameObject.transform.localScale = scale;
                transform.Translate(LRMove * Time.deltaTime, 0.0f, 0.0f);

                isRight = true;
                isWallLeft = false;
                CPData.Right = true;
            }
        }

        //������Ɖ����������

    }

    void Jump()
    {
        if (PreJump) // �W�����v���鏈��
        {
            jumpCount++;
            PreJump = false;
            Debug.Log("�W�����v");
            isJump = true;
            JumpTime = 0;
            JumpInitVel = JumpPower; // �W�����v����ꍇ�ɑ������B
        }
        if (isJump) // �W�����v���i�󒆂̏����j
        {
            JumpVel = JumpInitVel - Gravity * JumpTime; // ���x������-�d�͉����x*����
            transform.Translate(0.0f, JumpVel * 0.02f, 0.0f);

            JumpTime += 0.02f; // FixedUpdate�̍X�V���Ԃ�0.02�b�B
        }


    }

    private void InputLeg() // �J�X�^���E���x���ɂ��\�͂𔽉f����
    {
        if (DebugMode == false)
        {
            // �ړ����x�A�W�����v���x�A�W�����v�񐔁A�������x�A����₷����
            if (CurrentLegCustom == 0)
            {
                if (LegCustomLv[CurrentLegCustom] == 0)
                {
                    LegCustom0Lv0();
                }
                if (LegCustomLv[CurrentLegCustom] == 1)
                {
                    LegCustom0Lv1();
                }
            }
            Debug.Log("�X�V");
            JumpHeight = JumpPower * JumpPower / 2 / Gravity;
            Debug.Log(JumpHeight);
        }

    }

    private void LegCustom() // �f�o�b�O�p�B�蓮���x���؂�ւ�
    {
        //���x���A�b�v
        if (Input.GetKeyDown(KeyCode.N)) // �L�[���v����
        {
            LegCustomLv[CurrentLegCustom]++;
            if (LegCustomLv[CurrentLegCustom] > 1)
            {
                LegCustomLv[CurrentLegCustom] = 1;
            }
            else
            {
                Debug.Log("N���x���A�b�v");
                Debug.Log(LegCustomLv[CurrentLegCustom]);
                InputLeg();
            }
        }
        //���x���_�E��
        if (Input.GetKeyDown(KeyCode.M)) // �L�[���v����
        {
            LegCustomLv[CurrentLegCustom] --;
            if (LegCustomLv[CurrentLegCustom] < 0)
            {
                LegCustomLv[CurrentLegCustom] = 0;
            }
            else
            {
                Debug.Log("M���x���_�E��");
                Debug.Log(LegCustomLv[CurrentLegCustom]);
                InputLeg();
            }
        }
        ////���C���E�F�|��
        //if (Input.GetKeyDown(KeyCode.F1))
        //{
        //    Custom_false();
        //    CPData.CustomNo1 = true;
        //    // Debug.Log("���C���E�F�|���ɕς����");
        //}
        ////�T�u�E�F�|��
        //if (Input.GetKeyDown(KeyCode.F2))
        //{
        //    Custom_false();
        //    CPData.CustomNo2 = true;
        //    // Debug.Log("�T�u�E�F�|���ɕς����");
        //}
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
        }

    }




}