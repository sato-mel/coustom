using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CP_hp : MonoBehaviour
{
    [SerializeField]
    private int HP;


    private float muteki;

    private SpriteRenderer col = null;
    private bool Color_red = false;
    private float Color_red_count = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        muteki = 1.0f;

        col = GetComponent<SpriteRenderer>();
        col.color = new Color(0, 0, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(HP);
        //プレイヤーのHP
        if (HP < 0)
        {
            SceneManager.LoadScene("game_over");
            //    Destroy(this.gameObject);
            //  return;
        }

  // Debug.Log(muteki);


        //敵に当たったら一定時間無敵
        if (muteki > 0)
        {
            muteki -= Time.deltaTime;
         
        }

        Color_cheng();
        //ここに後でゲームオーバー処理入れる



    }


    private void OnTriggerStay2D(Collider2D other)
    {
        //

        if (muteki < 0)
        {
            if (other.gameObject.tag == "Enemy")
            {
                HP -= 3;
                Color_red = true;
            }
            if (other.gameObject.tag == "BOSS_Bullet")
            {
                HP -= 2;
                Color_red = true;
            }
            if (other.gameObject.tag == "BOSS_Bullet2")
            {
                HP -= 4;
                Color_red = true;
            }
            muteki = 1;
        }
       
    }


    void Color_cheng()
    {
        if (Color_red)
        {

            col.color = new Color(1, 0, 0, 1);


            //一瞬の赤色変換
            Color_red_count += Time.deltaTime;
            if (Color_red_count > 0.5f)
            {

                Color_red = false;
            }

        }
        else
        {

            col.color = new Color(1, 1, 1, 1);

            Color_red_count = 0.0f;

        }
    }






}
