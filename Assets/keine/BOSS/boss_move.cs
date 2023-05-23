using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss_move : MonoBehaviour
{

    private float RightMove = 0.0f;
    private float LeftMove = 0.0f;
    private float jump = 0.0f;
    private float jumpTime = 0.0f;
    private bool isJump = false;
    private int jumpCount;
    private int jumpSpeed = 0;
    private Rigidbody2D rb;


    private int upForce;

    private int hanten;
    private int move_random;
    private float randomCount = 0.0f;

    private bool isRight;
    private bool isLeft;
    private bool isWallLeft;
    private bool isWallRight;
    private bool junping = false;

    [SerializeField]
    GameObject player;
    Transform length;


    Transform oldPos;

    Vector2 scale;

    // Start is called before the first frame update
    void Start()
    {

        jumpCount = 0;
        hanten = 1;
        upForce = 400;
        rb = GetComponent<Rigidbody2D>();



        scale = transform.localScale;

        isRight = true;
        isLeft = false;
        isWallLeft = false;
        isWallRight = false;


        //   oldPos.transform.position = this.transform.position;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!CPData.LastResort)
        {
            Jump();
            Move();
        }
        // oldPos.transform.position = this.transform.position;
    }

    private void Update()
    {
        //プレイヤーのジャンプが2回されてから少ししてからジャンプ
        jumpTime += Time.deltaTime;
        Debug.Log("ジャンプ" + jumpSpeed);
        if (junping)
        {
            jumpSpeed = Random.Range(8, 20);
            junping = false;
        }
        if (jumpTime > jumpSpeed)
        {
            junping = true;
            isJump = true;
            jumpSpeed = 0;
            // CPData.Player_junpCount = 0;
        }

        // Debug.Log(oldPos.transform.position.x + "aaaa" + this.transform.position.x);
    }


    void Move()
    {
        float distance = Vector3.Distance(this.transform.position, player.transform.position);
        RightMove = Random.Range(15, 25);
        LeftMove = Random.Range(15, 25);

        randomCount += Time.deltaTime;
        if (randomCount > 0.5f)
        {
            move_random = Random.Range(1, 90);
            randomCount = 0;
        }
        //bossの移動処理発動条件
        if (distance < 10)
        {
            //Debug.Log("bossが動く");
            //ボスの攻撃開始
            CPData.BOSS_start = true;

        }
        if (CPData.BOSS_start)
        {
            //移動処理
            if (move_random > 70)
            {
                if (!isWallLeft)
                {
                    //    if (distance > 1)
                    //  {
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
                    //    }
                    gameObject.transform.localScale = scale;
                    transform.Translate(-LeftMove * Time.deltaTime, 0.0f, 0.0f);
                    // Vector3 direction = this.transform.right;
                    //  this.GetComponent<Rigidbody2D>().AddForce(-direction * 1000, ForceMode2D.Force);
                    isLeft = true;
                    isWallRight = false;
                    //   CPData.Right = false;

                }
            }
            if (70 > move_random && move_random > 40)
            {
                if (!isWallRight)
                {
                    //   if (distance < -1)
                    // {

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
                    //}
                    gameObject.transform.localScale = scale;
                    transform.Translate(RightMove * Time.deltaTime, 0.0f, 0.0f);

                    isRight = true;
                    isWallLeft = false;
                    //  CPData.Right = true;

                }
            }

        }

        //if (oldPos.transform.position.x + 40 <= this.transform.position.x ) ;
        //{
        //    this.transform.position -= oldPos.transform.position;
        //}
        // if (oldPos.transform.position.x - 40 >= this.transform.position.x)
        //{
        //    this.transform.position += oldPos.transform.position;
        //}




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


        if (other.gameObject.tag == "RightWall")
        {
            isWallRight = true;
            scale.x *= hanten;
        }
        if (other.gameObject.tag == "LeftWall")
        {
            isWallLeft = true;
            scale.x *= hanten;
        }
    }





}