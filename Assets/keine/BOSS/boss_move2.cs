using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss_move2 : MonoBehaviour
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
            Move();
        }
        // oldPos.transform.position = this.transform.position;
    }

    private void Update()
    {

    }


    void Move()
    {
        float distance = Vector3.Distance(this.transform.position, player.transform.position);
     //   RightMove = Random.Range(15, 25);
       // LeftMove = Random.Range(15, 25);

        randomCount += Time.deltaTime;
        if (randomCount > 0.5f)
        {
            move_random = Random.Range(1, 90);
            randomCount = 0;
        }
        //bossの移動処理発動条件
        if (distance < 20)
        {
            //Debug.Log("bossが動く");
            //ボスの攻撃開始
            CPData.BOSS_start2 = true;

        }
        
    }



}