using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leg_Custom : MonoBehaviour
{
    private bool DebugMode = false; // trueの時はSerializeを参照する。
    [SerializeField] private float MoveVelLimit; // 移動速度制限
    [SerializeField] private float JumpPower; // ジャンプ力。初速に代入される。
    [SerializeField] private float Gravity; // 重力
    [SerializeField] private int JumpLimit; // ジャンプ回数制限
    [SerializeField] private float FallVelLimit; // 落下速度制限
    [SerializeField] private int AccelFrame; // 最高速度に達するまでの時間（0.02s）

    private float MoveVel; // 移動速度

    private float JumpVel = 0.0f; // 上下移動速度
    private float JumpInitVel = 0.0f; // ジャンプ初速。（ただしジャンプせずに空中に出た場合は0）
    private float JumpHeight; // ジャンプの高さ（デバッグ・調整用）
    private float JumpTime; // 滞空時間

    private bool PreJump = false; // ジャンプする予約
    private bool isJump = true; // 空中にいる（ジャンプしている）か
    private int jumpCount; // ジャンプした回数（着地リセット）

    private const int LegCustomNum = 3; // カスタムの種類
    int LegCustomCurrent = 0; // 使用中のカスタム
    private int[] LegCustomLv = new int[LegCustomNum]; // カスタムごとのレベル
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
        MoveVelLimit = 10.0f; // 移動速度
        JumpPower = 10.0f; // ジャンプ初速度
        Gravity = 20.0f; // 重力
        JumpLimit = 2; // ジャンプ回数
        FallVelLimit = 10.0f; // 落下速度制限
        AccelFrame = 25; // 滑りやすさ、抵抗
    }
    private void LegCustom0Lv2()
    {
        MoveVelLimit = 10.0f; // 移動速度
        JumpPower = 10.0f; // ジャンプ初速度
        Gravity = 20.0f; // 重力
        JumpLimit = 3; // ジャンプ回数
        FallVelLimit = 20.0f; // 落下速度制限
        AccelFrame = 25; // 滑りやすさ、抵抗
    }
    private void LegCustom0Lv3()
    {
        MoveVelLimit = 10.0f; // 移動速度
        JumpPower = 10.0f; // ジャンプ初速度
        Gravity = 20.0f; // 重力
        JumpLimit = 3; // ジャンプ回数
        FallVelLimit = 10.0f; // 落下速度制限
        AccelFrame = 15; // 滑りやすさ、抵抗
    }
    private void LegCustom0Lv4()
    {
        MoveVelLimit = 10.0f; // 移動速度
        JumpPower = 10.0f; // ジャンプ初速度
        Gravity = 20.0f; // 重力
        JumpLimit = 4; // ジャンプ回数
        FallVelLimit = 10.0f; // 落下速度制限
        AccelFrame = 15; // 滑りやすさ、抵抗
    }

    private void LegCustom1Lv1()
    {
        MoveVelLimit = 12.0f; // 移動速度
        JumpPower = 12.0f; // ジャンプ初速度
        Gravity = 28.8f; // 重力
        JumpLimit = 1; // ジャンプ回数
        FallVelLimit = 12.0f; // 落下速度制限
        AccelFrame = 25; // 滑りやすさ、抵抗
    }
    private void LegCustom1Lv2()
    {
        MoveVelLimit = 14.0f; // 移動速度
        JumpPower = 12.0f; // ジャンプ初速度
        Gravity = 28.8f; // 重力
        JumpLimit = 1; // ジャンプ回数
        FallVelLimit = 12.0f; // 落下速度制限
        AccelFrame = 25; // 滑りやすさ、抵抗
    }
    private void LegCustom1Lv3()
    {
        MoveVelLimit = 14.0f; // 移動速度
        JumpPower = 14.0f; // ジャンプ初速度
        Gravity = 39.2f; // 重力
        JumpLimit = 1; // ジャンプ回数
        FallVelLimit = 14.0f; // 落下速度制限
        AccelFrame = 15; // 滑りやすさ、抵抗
    }
    private void LegCustom1Lv4()
    {
        MoveVelLimit = 16.0f; // 移動速度
        JumpPower = 14.0f; // ジャンプ初速度
        Gravity = 39.2f; // 重力
        JumpLimit = 1; // ジャンプ回数
        FallVelLimit = 14.0f; // 落下速度制限
        AccelFrame = 10; // 滑りやすさ、抵抗
    }

    private void LegCustom2Lv1()
    {
        MoveVelLimit = 10.0f; // 移動速度
        JumpPower = 24.0f; // ジャンプ初速度
        Gravity = 48.0f; // 重力
        JumpLimit = 1; // ジャンプ回数
        FallVelLimit = 30.0f; // 落下速度制限
        AccelFrame = 25; // 滑りやすさ、抵抗
    }
    private void LegCustom2Lv2()
    {
        MoveVelLimit = 10.0f; // 移動速度
        JumpPower = 28.0f; // ジャンプ初速度
        Gravity = 56.0f; // 重力
        JumpLimit = 1; // ジャンプ回数
        FallVelLimit = 30.0f; // 落下速度制限
        AccelFrame = 25; // 滑りやすさ、抵抗
    }
    private void LegCustom2Lv3()
    {
        MoveVelLimit = 10.0f; // 移動速度
        JumpPower = 32.0f; // ジャンプ初速度
        Gravity = 64.0f; // 重力
        JumpLimit = 1; // ジャンプ回数
        FallVelLimit = 30.0f; // 落下速度制限
        AccelFrame = 15; // 滑りやすさ、抵抗
    }
    private void LegCustom2Lv4()
    {
        MoveVelLimit = 10.0f; // 移動速度
        JumpPower = 40.0f; // ジャンプ初速度
        Gravity = 80.0f; // 重力
        JumpLimit = 1; // ジャンプ回数
        FallVelLimit = 30.0f; // 落下速度制限
        AccelFrame = 15; // 滑りやすさ、抵抗
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
        LegCustom(); // デバッグ用。手動レベル・カスタム切り替え
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
        // 押している間、最高速度まで上がり続ける
        // 押されていないとき、直前の速度から下がり続ける
        // 移動は押されていないときも必要

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

        //上方向と下方向を作る

    }

    void Jump()
    {
        if (PreJump) // ジャンプする処理
        {
            jumpCount++;
            PreJump = false;
            isJump = true;
            JumpTime = 0;
            JumpInitVel = JumpPower; // ジャンプする場合に代入する。
        }
        if (isJump) // ジャンプ中（空中の処理）
        {
            JumpVel = JumpInitVel - Gravity * JumpTime; // 速度＝初速-重力加速度*時間
            transform.Translate(0.0f, JumpVel * Time.deltaTime, 0.0f);

            JumpTime += 0.02f; // FixedUpdateの更新時間は0.02秒。
        }


    }

    private void InputLeg() // カスタム・レベルによる能力を反映する
    {
        if (DebugMode == true)
            return;
        switch (LegCustomCurrent) // 現在のカスタムNo.
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
        JumpHeight = JumpPower * JumpPower / 2 / Gravity; // デバッグ用。ジャンプの高さ明示用。
        Debug.Log("カスタムNo." + LegCustomCurrent + "、Lv." + LegCustomLv[LegCustomCurrent] + "、ジャンプ力：" + JumpHeight);
    }

    private void LegCustom0()
    {
        switch (LegCustomLv[0]) // カスタムNo.0のレベル
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
        switch (LegCustomLv[1]) // カスタムNo.1のレベル
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
        switch (LegCustomLv[2]) // カスタムNo.2のレベル
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

    private void LegCustom() // デバッグ用。手動レベル切り替え
    {
        // レベルアップ
        if (Input.GetKeyDown(KeyCode.N)) // キー被り要注意
        {
            LegCustomLv[LegCustomCurrent]++;
            if (LegCustomLv[LegCustomCurrent] > LegCustomMaxLv)
            {
                LegCustomLv[LegCustomCurrent] = LegCustomMaxLv;
            }
            else
            {
                InputLeg(); // カスタム・レベルによる能力を反映する
            }
        }
        // レベルダウン
        if (Input.GetKeyDown(KeyCode.M)) // キー被り要注意
        {
            LegCustomLv[LegCustomCurrent] --;
            if (LegCustomLv[LegCustomCurrent] < 1)
            {
                LegCustomLv[LegCustomCurrent] = 1;
            }
            else
            {
                InputLeg(); // カスタム・レベルによる能力を反映する
            }
        }

        // カスタムNo.0に変更
        if (Input.GetKeyDown(KeyCode.H)) // キー被り要注意
        {
            if (LegCustomCurrent != 0)
            {
                LegCustomCurrent = 0;
                InputLeg(); // カスタム・レベルによる能力を反映する
            }
        }
        // カスタムNo.1に変更
        if (Input.GetKeyDown(KeyCode.J)) // キー被り要注意
        {
            if (LegCustomCurrent != 1)
            {
                LegCustomCurrent = 1;
                InputLeg(); // カスタム・レベルによる能力を反映する
            }
        }
        // カスタムNo.2に変更
        if (Input.GetKeyDown(KeyCode.K)) // キー被り要注意
        {
            if (LegCustomCurrent != 2)
            {
                LegCustomCurrent = 2;
                InputLeg(); // カスタム・レベルによる能力を反映する
            }
        }
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

        // 壁に当たったらtrue
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

            // 床から落ちてもジャンプ回数（地上分）を消費（地上ジャンプ+空中ジャンプでカウント）
            if (jumpCount == 0)
            {
                jumpCount = 1;
            }
        }

    }




}