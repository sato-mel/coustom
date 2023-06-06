using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class Leg_Custom : MonoBehaviour
{
    bool DebugMode = false; // trueの時はSerializeを参照する。
    [SerializeField] private float MoveVelLimit; // 移動速度制限
    [SerializeField] private float JumpPower; // ジャンプ力。初速に代入される。
    [SerializeField] private float Gravity; // 重力
    [SerializeField] private int JumpLimit; // ジャンプ回数制限
    [SerializeField] private float FallVelLimit; // 落下速度制限
    [SerializeField] private int AccelFrame; // 最高速度に達するまでの時間（0.02s）

    private float MoveVel; // 移動速度

    private float JumpVel = 0.0f; // 上下移動速度
    private float LastJumpVel = 0.0f; // 比較用。直前の上下移動速度
    private float JumpInitVel = 0.0f; // ジャンプ初速（ただしジャンプせずに空中に出た場合は0）
    private float JumpHeight; // ジャンプの高さ（デバッグ・調整用）
    private float JumpTime; // 滞空時間

    private bool PreJump = false; // ジャンプする予約
    private bool isJump = true; // 空中にいる（ジャンプしている）か
    private int JumpCount; // ジャンプした回数（着地リセット）

    private const int LegCustomNum = 3; // カスタムの種類
    private int LegCustomCurrent = 0; // 使用中のカスタム
    private int[] LegCustomLv = new int[LegCustomNum]; // カスタムごとのレベル
    private int LegCustomMaxLv = 4; // 最大レベル

    private bool PreBody = false; // BodyCustomを使用する予約
    private bool isBody = false; // BodyCustomを使ったか
    private int BodyCount = 0; // BodyCustomを使った回数（着地リセット）
    private int BodyTime = 0; // BodyCustomを使った時間（着地リセット）

    private float BodyMoveVelLimit; // BodyCustom使用中の移動速度制限
    private float BodyFixedMoveVel; // BodyCustom使用中の強制移動速度。滑空用。
    private float BodyGravity; // BodyCustom使用中の重力
    private int BodyAccelFrame; // BodyCustom使用中の滑りやすさ
    private int BodyCountLimit; // BodyCustomの使用回数制限（着地リセット）
    private int BodyTimeLimit; // BodyCustomの使用時間制限（着地リセット）

    private bool PreCancelBody = false; // BodyCustomを解除する予約

    private const int BodyCustomNum = 3; // カスタムの種類
    private int BodyCustomCurrent = 0; // 使用中のカスタム
    private int[] BodyCustomLv = new int[BodyCustomNum]; // カスタムごとのレベル
    private int BodyCustomMaxLv = 4; // 最大レベル

    private bool DebugModeLeg = true;

    private Rigidbody2D rb;

    private int hanten;
    private bool isRight;
    private bool isLeft;
    private bool isWallLeft;
    private bool isWallRight;

    Vector2 scale;

    // 多段ジャンプ
    float[] LegCustom0_MVL = { 10.0f, 10.0f, 10.0f, 10.0f }; // 移動速度、LegCustom0のMoveVelLimitに入れる値の配列
    float[] LegCustom0_JP = { 10.0f, 10.0f, 10.0f, 10.0f }; // ジャンプ初速度、LegCustom0のJumpPowerに入れる値の配列
    float[] LegCustom0_G = { 20.0f, 20.0f, 20.0f, 20.0f }; // 重力、LegCustom0のGravityに入れる値の配列
    int[] LegCustom0_GL = { 2, 3, 3, 4 }; // ジャンプ回数、LegCustom0のJumpLimitに入れる値の配列
    float[] LegCustom0_FVL = { 20.0f, 20.0f, 20.0f, 20.0f }; // 落下速度制限、LegCustom0のFallVelLimitに入れる値の配列
    int[] LegCustom0_AF = { 25, 25, 15, 15 }; // 滑りやすさ、抵抗、LegCustom0のAccelFrameに入れる値の配列

    // 速度
    float[] LegCustom1_MVL = { 12.0f, 14.0f, 14.0f, 16.0f }; // 移動速度、LegCustom0のMoveVelLimitに入れる値の配列
    float[] LegCustom1_JP = { 12.0f, 12.0f, 14.0f, 14.0f }; // ジャンプ初速度、LegCustom0のJumpPowerに入れる値の配列
    float[] LegCustom1_G = { 28.8f, 28.8f, 39.2f, 39.2f }; // 重力、LegCustom0のGravityに入れる値の配列
    int[] LegCustom1_GL = { 1, 1, 1, 1 }; // ジャンプ回数、LegCustom0のJumpLimitに入れる値の配列
    float[] LegCustom1_FVL = { 12.0f, 12.0f, 14.0f, 14.0f }; // 落下速度制限、LegCustom0のFallVelLimitに入れる値の配列
    int[] LegCustom1_AF = { 25, 25, 15, 10 }; // 滑りやすさ、抵抗、LegCustom0のAccelFrameに入れる値の配列

    // ハイジャンプ
    float[] LegCustom2_MVL = { 10.0f, 10.0f, 10.0f, 10.0f }; // 移動速度、LegCustom0のMoveVelLimitに入れる値の配列
    float[] LegCustom2_JP = { 24.0f, 28.0f, 32.0f, 40.0f }; // ジャンプ初速度、LegCustom0のJumpPowerに入れる値の配列
    float[] LegCustom2_G = { 48.0f, 56.0f, 64.0f, 80.0f }; // 重力、LegCustom0のGravityに入れる値の配列
    int[] LegCustom2_GL = { 1, 1, 1, 1 }; // ジャンプ回数、LegCustom0のJumpLimitに入れる値の配列
    float[] LegCustom2_FVL = { 30.0f, 30.0f, 30.0f, 30.0f }; // 落下速度制限、LegCustom0のFallVelLimitに入れる値の配列
    int[] LegCustom2_AF = { 25, 25, 15, 15 }; // 滑りやすさ、抵抗、LegCustom0のAccelFrameに入れる値の配列

    // 滑空
    float[] BodyCustom0_BML = { 1.0f, 1.0f, 2.0f, 3.0f }; // 移動速度、BodyMoveVelLimitに入れる値の配列
    float[] BodyCustom0_BFM = { 15.0f, 15.0f, 16.0f, 17.0f }; // 固定移動速度、BodyFixedMoveVelに入れる値の配列
    float[] BodyCustom0_BG = { 4.0f, 3.5f, 3.5f, 3.0f }; // 重力、BodyGravityに入れる値の配列
    int[] BodyCustom0_BAF = { 10, 10, 10, 10 }; // 滑りやすさ、抵抗、BodyAccelFrameに入れる値の配列
    int[] BodyCustom0_BCL = { 1, 2, 3, 4 }; // 回数制限、BodyCountLimitに入れる配列
    int[] BodyCustom0_BTL = { 50, 75, 100, 125 }; // 時間制限(フレーム)、BodyTimeLimitに入れる配列

    // 傘
    float[] BodyCustom1_BML = { 15.0f, 16.0f, 16.0f, 17.0f }; // 移動速度、BodyMoveVelLimitに入れる値の配列
    float[] BodyCustom1_BFM = { 0.0f, 0.0f, 0.0f, 0.0f }; // 固定移動速度、BodyFixedMoveVelに入れる値の配列
    float[] BodyCustom1_BG = { 1.5f, 1.5f, 1.0f, 1.0f }; // 重力、BodyGravityに入れる値の配列
    int[] BodyCustom1_BAF = { 35, 30, 30, 25 }; // 滑りやすさ、抵抗、AccelFrameに入れる値の配列
    int[] BodyCustom1_BCL = { 2, 3, 4, 5 }; // 回数制限、BodyCountLimitに入れる配列
    int[] BodyCustom1_BTL = { 50, 100, 150, 200 }; // 時間制限(フレーム)、BodyTimeLimitに入れる配列

    // ホバリング
    float[] BodyCustom2_BML = { 5.0f, 5.0f, 5.0f, 5.0f }; // 移動速度、BodyMoveVelLimitに入れる値の配列
    float[] BodyCustom2_BFM = { 0.0f, 0.0f, 0.0f, 0.0f }; // 固定移動速度、BodyFixedMoveVelに入れる値の配列
    float[] BodyCustom2_BG = { 0.0f, 0.0f, -2.0f, -4.0f }; // 重力、BodyGravityに入れる値の配列
    int[] BodyCustom2_BAF = { 25, 25, 25, 25 }; // 滑りやすさ、抵抗、BodyAccelFrameに入れる値の配列
    int[] BodyCustom2_BCL = { 3, 4, 5, 6 }; // 回数制限、BodyCountLimitに入れる配列
    int[] BodyCustom2_BTL = { 25, 50, 75, 100 }; // 時間制限(フレーム)、BodyTimeLimitに入れる配列

    // 経験値
    int EnemyPointGet = 1; // 敵1体を倒した経験値
    int[] EnemyPointBody = { 0, 0, 0 }; // BodyCustomごとの累計経験値
    int[] EnemyPointLeg = { 0, 0, 0 }; // LegCustomごとの累計経験値
    int[] LvUpTable = { 1, 2, 3 }; // LvUpに必要な経験値

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

        InputLeg(); // Legカスタム・レベルによる能力を反映する
        InputBody(); // Bodyカスタム・レベルによる能力を反映する
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
        BodyLegCustomLvUp(); // レベルアップ用

        BodyLegCustomChange(); // 切り替え用

        LegCustom(); // デバッグ用。手動レベル・カスタム切り替え

        // ゲームパッド用
        var GamePad = Gamepad.current;
        bool aButtonDown = false; // Aボタンを押した時
        bool aButtonUp = false; // Aボタンを離した時

        if (GamePad != null)
        {
            if (GamePad.aButton.wasPressedThisFrame) // Aボタンを押した時
            {
                aButtonDown = true;
            }
            if (GamePad.aButton.wasReleasedThisFrame) // Aボタンを離した時
            {
                aButtonUp = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) || aButtonDown) // 押した時
        {
            if (JumpCount < JumpLimit)
            {
                PreJump = true;
                return;
            }
            if (BodyCount < BodyCountLimit)
            {
                if (JumpVel < 0) // 落下中
                {
                    PreBody = true;
                }
            }
        }
        if (Input.GetKeyUp(KeyCode.Space) || aButtonUp) // 離した時
        {
            if (isBody)
            {
                PreCancelBody = true;
            }
        }
    }

    void Move()
    {
        // ゲームパッド用
        var GamePad = Gamepad.current;
        bool LeftStickLeft = false; // 左（左スティック）
        bool LeftStickRight = false; // 右（左スティック）

        if (GamePad != null)
        {
            var LeftStickValue = GamePad.leftStick.ReadValue();
            float StickReaction = 0.2f; // スティックを一番傾けた時を1として、どれくらい傾けたら反応するか

            if (LeftStickValue.x < -StickReaction)
            {
                LeftStickLeft = true;
            }
            if (LeftStickValue.x > StickReaction)
            {
                LeftStickRight = true;
            }
        }
        
        //移動処理
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

                    // プレイヤー画像の反転
                    if (scale.x < 0)
                    {
                        scale.x *= -1;
                    }
                }

                gameObject.transform.localScale = scale;
                isWallRight = false;
                CPData.Right = false;
            }
            else // 左を押していない時
            {
                if (MoveVel < 0)
                {
                    MoveVel += MoveVelLimit / AccelFrame; // 減速
                    if (MoveVel > 0)
                    {
                        MoveVel = 0;
                    }
                }
            }
            if (isBody == false)
            {
                if (MoveVel < -MoveVelLimit) // 上限より速いとき
                {
                    MoveVel += MoveVelLimit / AccelFrame; // 減速
                    if (MoveVel > -MoveVelLimit) // 上限より遅くなった時
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

                    // プレイヤー画像の反転
                    if (scale.x > 0)
                    {
                        scale.x *= -1;
                    }
                }
                gameObject.transform.localScale = scale;
                isWallLeft = false;
                CPData.Right = true;
            }
            else // 右を押していない時
            {
                if (MoveVel > 0)
                {
                    MoveVel -= MoveVelLimit / AccelFrame; // 減速
                    if (MoveVel < 0)
                    {
                        MoveVel = 0;
                    }
                }
            }
            if (isBody == false)
            {
                if (MoveVel > MoveVelLimit) // 上限より速いとき
                {
                    MoveVel -= MoveVelLimit / AccelFrame; // 減速
                    if (MoveVel < MoveVelLimit) // 上限より遅くなった時
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
        //上方向と下方向を作る
    }

    void Jump()
    {
        // ゲームパッド用
        var GamePad = Gamepad.current;
        bool aButton = false; // Aボタンを押している間

        if (GamePad != null)
        {
            if (GamePad.aButton.isPressed)
            {
                aButton = true;
            }
        }

        if (PreJump) // ジャンプする処理
        {
            JumpCount++;
            PreJump = false;
            isJump = true;
            JumpTime = 0;
            JumpInitVel = JumpPower; // ジャンプする場合に代入する。
        }
        if (isJump) // ジャンプ中（空中の処理）
        {
            if (isBody)
            {
                return;
            }

            JumpVel = JumpInitVel - Gravity * JumpTime; // 速度＝初速-重力加速度*時間

            if (BodyCount < BodyCountLimit)
            {
                // ジャンプの頂点の瞬間
                if (JumpVel <= 0)
                {
                    if (LastJumpVel > 0)
                    {
                        if (Input.GetKey(KeyCode.Space) || aButton) // Aボタンを押している間
                        {
                            PreBody = true;
                            return;
                        }
                    }
                }
            }
            transform.Translate(0.0f, JumpVel * Time.deltaTime, 0.0f);
            JumpTime += 0.02f; // FixedUpdateの更新時間は0.02秒。
            LastJumpVel = JumpVel;
        }
    }

    private void BodyCustom()
    {
        if (PreBody)
        {
            PreBody = false;
            isBody = true;
            JumpCount = JumpLimit; // 着地までジャンプ禁止
            LastJumpVel = JumpVel; // ループ対策
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
                // プレイヤー画像の反転
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
                // プレイヤー画像の反転
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
            BodyTime++; // FixedUpdateの更新時間は0.02秒。
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

        // BodyCustomに経験値を追加
        EnemyPointBody[BodyCustomCurrent] += EnemyPointGet;
        // LegCustomに経験値を追加
        EnemyPointLeg[LegCustomCurrent] += EnemyPointGet;

        // LvUp判定
        bool LvUp = true; // 継続判定

        do
        {
            if (BodyCustomLv[BodyCustomCurrent] < BodyCustomMaxLv)
            {
                if (LvUpTable[BodyCustomLv[BodyCustomCurrent] - 1] <= EnemyPointBody[BodyCustomCurrent])
                {
                    BodyCustomLv[BodyCustomCurrent]++;
                    InputBody(); // カスタム・レベルによる能力を反映する
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

        // LvUp判定
        LvUp = true; // 継続判定

        do
        {
            if (LegCustomLv[LegCustomCurrent] < LegCustomMaxLv)
            {
                if (LvUpTable[LegCustomLv[LegCustomCurrent] - 1] <= EnemyPointLeg[LegCustomCurrent])
                {
                    LegCustomLv[LegCustomCurrent]++;
                    InputLeg(); // カスタム・レベルによる能力を反映する
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
        // ゲームパッド用
        var GamePad = Gamepad.current;
        bool R1ButtonDown = false; // R1ボタンを押した時
        bool L1ButtonDown = false; // L1ボタンを押した時

        if (GamePad != null)
        {
            if (GamePad.rightShoulder.wasPressedThisFrame) // R1ボタンを押した時
            {
                R1ButtonDown = true;
            }
            if (GamePad.leftShoulder.wasPressedThisFrame) // L1ボタンを押した時
            {
                L1ButtonDown = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.E) || R1ButtonDown) // 押した時
        {
            // BodyCustom切り替え
        }

        if (Input.GetKeyDown(KeyCode.Q) || L1ButtonDown) // 押した時
        {
            // LegCustom切り替え
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
        Debug.Log("LegカスタムNo." + LegCustomCurrent + "、Lv." + LegCustomLv[LegCustomCurrent] + "、ジャンプ力：" + JumpHeight);
    }

    private void InputBody() // カスタム・レベルによる能力を反映する
    {
        switch (BodyCustomCurrent) // 現在のカスタムNo.
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
        Debug.Log("BodyカスタムNo." + BodyCustomCurrent + "、Lv." + BodyCustomLv[BodyCustomCurrent]);
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

    private void LegCustom() // デバッグ用。手動レベル切り替え
    {
        // デバッグモード切替
        if (Input.GetKeyDown(KeyCode.B)) // キー被り要注意
        {
            if (DebugModeLeg)
            {
                DebugModeLeg = false;
                Debug.Log("BodyCustomデバッグモード");
                return;
            }
            else
            {
                DebugModeLeg = true;
                Debug.Log("LegCustomデバッグモード");
                return;
            }

        }

        // レベルアップ
        if (Input.GetKeyDown(KeyCode.N)) // キー被り要注意
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
                    InputLeg(); // カスタム・レベルによる能力を反映する
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
                    InputBody(); // カスタム・レベルによる能力を反映する
                }
            }
        }
        // レベルダウン
        if (Input.GetKeyDown(KeyCode.M)) // キー被り要注意
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
                    InputLeg(); // カスタム・レベルによる能力を反映する
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
                    InputBody(); // カスタム・レベルによる能力を反映する
                }
            }
        }

        // カスタムNo.0に変更
        if (Input.GetKeyDown(KeyCode.H)) // キー被り要注意
        {
            if (DebugModeLeg)
            {
                if (LegCustomCurrent != 0)
                {
                    LegCustomCurrent = 0;
                    InputLeg(); // カスタム・レベルによる能力を反映する
                }
            }
            else
            {
                if (BodyCustomCurrent != 0)
                {
                    BodyCustomCurrent = 0;
                    InputBody(); // カスタム・レベルによる能力を反映する
                }
            }
        }
        // カスタムNo.1に変更
        if (Input.GetKeyDown(KeyCode.J)) // キー被り要注意
        {
            if (DebugModeLeg)
            {
                if (LegCustomCurrent != 1)
                {
                    LegCustomCurrent = 1;
                    InputLeg(); // カスタム・レベルによる能力を反映する
                }
            }
            else
            {
                if (BodyCustomCurrent != 1)
                {
                    BodyCustomCurrent = 1;
                    InputBody(); // カスタム・レベルによる能力を反映する
                }
            }
        }
        // カスタムNo.2に変更
        if (Input.GetKeyDown(KeyCode.K)) // キー被り要注意
        {
            if (DebugModeLeg)
            {
                if (LegCustomCurrent != 2)
                {
                    LegCustomCurrent = 2;
                    InputLeg(); // カスタム・レベルによる能力を反映する
                }
            }
            else
            {
                if (BodyCustomCurrent != 2)
                {
                    BodyCustomCurrent = 2;
                    InputBody(); // カスタム・レベルによる能力を反映する
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // 床に当たったらカウントゼロ
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
            if (JumpCount == 0)
            {
                JumpInitVel = 0;
                JumpCount = 1;
            }
        }

        // 壁から離れたらfalse
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