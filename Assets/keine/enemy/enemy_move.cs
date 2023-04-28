using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_move : MonoBehaviour
{

  
    private float eneMove = 0.0f;
    private float eneMove2 = 0.0f;
    private Rigidbody2D rb;

    [SerializeField]
    private float max;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float max2;

    [SerializeField]
    private float speed2;


    //ˆÚ“®‹——£
    private float move_count = 0;

    private float move_count2 = 0;

    Vector2 scale;

    // Start is called before the first frame update
    void Start()
    {
        eneMove = 2;
        eneMove2 = 1;



        rb = GetComponent<Rigidbody2D>();

        scale = transform.localScale;

      
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Move2();
    }






    void Move()
    {

      
            gameObject.transform.localScale = scale;
            transform.Translate(eneMove * Time.deltaTime* speed, 0.0f, 0.0f);

        move_count += Time.deltaTime;
        //   Debug.Log(move_count);

        if (move_count> max)
        {
            move_count=0;
            eneMove *= -1.0f;
        }


    }

    void Move2()
    {



        gameObject.transform.localScale = scale;
        transform.Translate(0.0f, eneMove2 * Time.deltaTime * speed, 0.0f);

        move_count2 += Time.deltaTime;
        //   Debug.Log(move_count);

        if (move_count2 > max2)
        {
            move_count2 = 0;
            eneMove2 *= -1.0f;
        }


    }






}