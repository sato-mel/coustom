using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class player_move : MonoBehaviour
{

    private float RightMove = 0.0f;
    private float LeftMove = 0.0f;
    private float jump = 0.0f;
    private bool isJump = false;
    private int jumpCount;

    private Rigidbody2D rb;


    private int upForce;

    private int hanten;
    private bool isRight;
    private bool isLeft;
    private bool isWallLeft;
    private bool isWallRight;

    private PlayerInput playerInput;
    private CP_move_input pI;
    private Vector2 moveInput;

    Vector2 scale;

    // Start is called before the first frame update
    void Start()
    {
        RightMove = 18;
        LeftMove = 18;
        jumpCount = 0;
        hanten = 1;
        upForce = 400;
        rb = GetComponent<Rigidbody2D>();

        scale = transform.localScale;

        isRight = true;
        isLeft = false;
        isWallLeft = false;
        isWallRight = false;
    }

 
    private void Awake()
    {

        // Action�X�N���v�g�̃C���X�^���X����
        playerInput = GetComponent<PlayerInput>();
        pI = new CP_move_input();

    // Action�C�x���g�̓o�^
        pI.Player.Jump.performed += OnJump;

        pI.Enable();

    }

    private void OnDestroy()
    {
        // ���g�ŃC���X�^���X������Action�N���X��IDisposable���������Ă���̂ŁA
        // �K��Dispose����K�v������...�炵��
        pI.Dispose();

    }

    public void OnJump(InputAction.CallbackContext context)
    {
        isJump = true;
    }
    // Update is called once per frame
    void FixedUpdate()
    {

        Jump();
        Move();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) ) 
        {
            isJump = true;
           // CPData.Player_junpCount += 1;

        }


    }


    void Move()
    {

        moveInput = playerInput.currentActionMap["Move"].ReadValue<Vector2>();
        float horizontalInput = moveInput.x;
        // ���X�e�B�b�N�̐��������̓��͂��擾


        //Vector2 scale = gameObject.transform.localScale;

        //�ړ�����
        if (!isWallLeft)
        {
            if (Input.GetKey(KeyCode.A) || horizontalInput < 0)
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

                gameObject.transform.localScale = scale;
                transform.Translate(-LeftMove * Time.deltaTime, 0.0f, 0.0f);
                isWallRight = false;
                CPData.Right = false;
            }
        }
        if (!isWallRight)
        {
            if (Input.GetKey(KeyCode.D) || horizontalInput > 0)
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
                transform.Translate(RightMove * Time.deltaTime, 0.0f, 0.0f);

                isRight = true;
                isWallLeft = false;
                CPData.Right = true;
            }
        }

        //������Ɖ����������






    }

    void Jump()
    {
        //�n�C�W�����v
        if (CPData.CustomHigh)
        {
            upForce = 800;
        }


        if (!CPData.CustomHigh)
        {
            upForce = 400;
        }


        //�d��
        this.rb.AddForce(new Vector3(0, -6, 0));

        //�W�����v����
        if (jumpCount < 2)
        {
            if (isJump)
            {
                this.rb.AddForce(new Vector3(0, upForce, 0));
                jumpCount++;
                isJump = false;


            }
        }

    }




    private void OnCollisionEnter2D(Collision2D other)
    {
        //���ɓ���������J�E���g�[��
        if (other.gameObject.tag == "Floor")
        {
            jumpCount = 0;
        }

    }

    private void OnCollisionStay2D(Collision2D other)
    {
        //���ɓ���������J�E���g�[��

        if (other.gameObject.tag == "RightWall")
        {
            isWallRight = true;
        }
        if (other.gameObject.tag == "LeftWall")
        {
            isWallLeft = true;
        }
    }





}