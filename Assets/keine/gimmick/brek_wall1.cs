using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brek_wall1 : MonoBehaviour
{
    [SerializeField]
    private int HP;



    //  private  spr;

   // private SpriteRenderer col = null;
    private bool Color_red = false;
    private float Color_red_count = 0.0f;

    private bool isR = false;
    private bool isG = false;
    private bool isB = true;
    private void Start()
    {
        //this.GetComponent<SpriteRenderer>().color

      //  col = GetComponent<SpriteRenderer>();
       // col.color = new Color(0, 0, 1, 1);

    }




    // Update is called once per frame
    void Update()
    {
        if (HP < 0)
        {
            Destroy(this.gameObject);
        }

        Color_cheng();


    }


    private void OnTriggerStay2D(Collider2D other)
    {

        //if (other.gameObject.tag == "Bullet2")
        //{
        //    HP -= 2;
        //}
        //if (other.gameObject.tag == "Penetration_Bullet")
        //{
        //    CPData.Penetration_speed = 200.0f;
        //}
        //else
        //{
        //    CPData.Penetration_speed = 500.0f;
        //}
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //ŠÑ’Ê’e‚ª“–‚½‚Á‚½‚ç
        if (other.gameObject.tag == "Penetration_Bullet")
        {
            HP -= 3;

            Color_red = true;
        }
    }

    void Color_cheng()
    {
        if (Color_red)
        {

          //  col.color = new Color(1, 0, 0, 1);


            //ˆêu‚ÌÔF•ÏŠ·
            Color_red_count += Time.deltaTime;
            if (Color_red_count > 0.05f)
            {

                Color_red = false;
            }

        }
        else
        {



            //if (HP > (HP / 100) * 70 && isB)
            //{
            //    col.color = new Color(0, 0, 1, 1);
            //}
            //else if(HP<70)
            //{
            //    isG = true;
            //}
            //if ((HP / 100) * 70 > HP && HP > (HP / 100) * 30 && isG)
            //{
            //    col.color = new Color(1, 1, 0, 1);
            //    isB = false;

            //}
            //else if (HP < 30)
            //{
            //    isR = true;
            //}
            //if ((HP / 100) * 30 > HP && isR)
            //{
            //    col.color = new Color(1, 0, 0, 1);
            //    isG = false;
            //}

          //  col.color = new Color(0, 0, 1, 1);


            Color_red_count = 0.0f;

        }
    }

}