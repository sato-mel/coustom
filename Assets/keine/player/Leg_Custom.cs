using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leg_Custom : MonoBehaviour
{
    private bool DebugMode = false; // trueの時はSerializeを参照する。
    [SerializeField] private float LRMove; // 移動速度
    [SerializeField] private float JumpPower; // ジャンプ力。初速に代入される。
    [SerializeField] private float Gravity; // 重力
    [SerializeField] private int JumpLimit; // ジャンプ回数
    [SerializeField] private float FallVelLimit; // 落下速度制限
    [SerializeField] private float Resistance; // 滑りやすさ、抵抗
    
    private float JumpVel = 0.0f; // 上下移動速度
    private float JumpInitVel = 0.0f; // ジャンプ初速。
    private float JumpHeight; // ジャンプの高さ
    private float JumpTime; // 滞空時間

    private const int LegCustomNo = 1; // カスタムの種類
    int CurrentLegCustom = 0; // 使用中のカスタム
    private int[] LegCustomLv = new int[LegCustomNo]; // カスタムごとのレベル

    //   private float jump = 0.0f;
    private bool PreJump = false; // ジャンプする予約
    private bool isJump = true; // ジャンプしているか
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
        LRMove = 9.0f; // 移動速度
        JumpPower = 10.0f; // ジャンプ初速度
        Gravity = 20.0f; // 重力
        JumpLimit = 2; // ジャンプ回数
        FallVelLimit = 10.0f; // 落下速度制限
        Resistance = 0.0f; // 滑りやすさ、抵抗
    }
    private void LegCustom0Lv1()
    {
        LRMove = 18.0f; // 移動速度
        JumpPower = 20.0f; // ジャンプ初速度
        Gravity = 40.0f; // 重力
        JumpLimit = 3; // ジャンプ回数
        FallVelLimit = 20.0f; // 落下速度制限
        Resistance = 0.0f; // 滑りやすさ、抵抗
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

        InputLeg(); // カスタム・レベルによる能力を反映する
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Jump();
        Move();
    }

    private void Update()
    {
        LegCustom(); // デバッグ用。手動レベル切り替え
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

        //上方向と下方向を作る

    }

    void Jump()
    {
        if (PreJump) // ジャンプする処理
        {
            jumpCount++;
            PreJump = false;
            Debug.Log("ジャンプ");
            isJump = true;
            JumpTime = 0;
            JumpInitVel = JumpPower; // ジャンプする場合に代入する。
        }
        if (isJump) // ジャンプ中（空中の処理）
        {
            JumpVel = JumpInitVel - Gravity * JumpTime; // 速度＝初速-重力加速度*時間
            transform.Translate(0.0f, JumpVel * 0.02f, 0.0f);

            JumpTime += 0.02f; // FixedUpdateの更新時間は0.02秒。
        }


    }

    private void InputLeg() // カスタム・レベルによる能力を反映する
    {
        if (DebugMode == false)
        {
            // 移動速度、ジャンプ高度、ジャンプ回数、落下速度、滑りやすさ等
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
            Debug.Log("更新");
            JumpHeight = JumpPower * JumpPower / 2 / Gravity;
            Debug.Log(JumpHeight);
        }

    }

    private void LegCustom() // デバッグ用。手動レベル切り替え
    {
        //レベルアップ
        if (Input.GetKeyDown(KeyCode.N)) // キー被り要注意
        {
            LegCustomLv[CurrentLegCustom]++;
            if (LegCustomLv[CurrentLegCustom] > 1)
            {
                LegCustomLv[CurrentLegCustom] = 1;
            }
            else
            {
                Debug.Log("Nレベルアップ");
                Debug.Log(LegCustomLv[CurrentLegCustom]);
                InputLeg();
            }
        }
        //レベルダウン
        if (Input.GetKeyDown(KeyCode.M)) // キー被り要注意
        {
            LegCustomLv[CurrentLegCustom] --;
            if (LegCustomLv[CurrentLegCustom] < 0)
            {
                LegCustomLv[CurrentLegCustom] = 0;
            }
            else
            {
                Debug.Log("Mレベルダウン");
                Debug.Log(LegCustomLv[CurrentLegCustom]);
                InputLeg();
            }
        }
        ////メインウェポン
        //if (Input.GetKeyDown(KeyCode.F1))
        //{
        //    Custom_false();
        //    CPData.CustomNo1 = true;
        //    // Debug.Log("メインウェポンに変わった");
        //}
        ////サブウェポン
        //if (Input.GetKeyDown(KeyCode.F2))
        //{
        //    Custom_false();
        //    CPData.CustomNo2 = true;
        //    // Debug.Log("サブウェポンに変わった");
        //}
    }



    private void OnCollisionEnter2D(Collision2D other)
    {
        // 床に当たったらカウントゼロ
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
        // 壁に当たっている間true
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
        // 床から離れたらtrue
        if (other.gameObject.tag == "Floor")
        {
            isJump = true;
        }

    }




}