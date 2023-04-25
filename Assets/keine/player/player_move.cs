using System.Collections;
using System.Collections.Generic;
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

    // Update is called once per frame
    void FixedUpdate()
    {
        Jump();
        Move();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJump = true;
           // CPData.Player_junpCount += 1;

        }


    }


    void Move()
    {

        //Vector2 scale = gameObject.transform.localScale;

        //移動処理
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

                gameObject.transform.localScale = scale;
                transform.Translate(-LeftMove * Time.deltaTime, 0.0f, 0.0f);
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
                transform.Translate(RightMove * Time.deltaTime, 0.0f, 0.0f);

                isRight = true;
                isWallLeft = false;
                CPData.Right = true;
            }
        }

        //上方向と下方向を作る






    }

    void Jump()
    {
        //ハイジャンプ
        if (CPData.CustomHigh)
        {
            upForce = 800;
        }


        if (!CPData.CustomHigh)
        {
            upForce = 400;
        }


        //重力
        this.rb.AddForce(new Vector3(0, -6, 0));

        //ジャンプ処理
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
        //床に当たったらカウントゼロ
        if (other.gameObject.tag == "Floor")
        {
            jumpCount = 0;
        }

    }



    private void OnCollisionStay2D(Collision2D other)
    {
        //床に当たったらカウントゼロ

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